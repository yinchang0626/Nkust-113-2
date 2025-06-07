import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { CourseService } from '../course.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Course } from '../course';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-courses',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './courses.html',
  styleUrl: './courses.css',
  providers: [DatePipe]
})
export class Courses implements OnInit {
  courses: Course[] = [];
  username: string | null = null;
  //private apiUrl = 'http://localhost:5100/api/enrollments';

  constructor(private courseService: CourseService, private datePipe: DatePipe, private http: HttpClient) {
   }

  ngOnInit(): void {
    this.username = localStorage.getItem('username');
    // 清除 localStorage 內 courseId 為 0 的資料
    Object.keys(localStorage).forEach(key => {
      if (key.startsWith('course_0')) localStorage.removeItem(key);
    });
    this.loadCourses();
  }

  loadCourses(): void {
    this.courseService.getCourses().subscribe(
      (courses) => {
        this.courses = courses;
      }
    );
  }

  isEnrolled(course: Course): boolean {
    const selectedCourses: Course[] = JSON.parse(localStorage.getItem('selectedCourses') || '[]');
    return selectedCourses.some(c => c.courseId === course.courseId);
  }

  enrollCourse(course: Course): void {
    let selectedCourses: Course[] = JSON.parse(localStorage.getItem('selectedCourses') || '[]');
    if (!this.isEnrolled(course)) {
      selectedCourses.push(course);
      localStorage.setItem('selectedCourses', JSON.stringify(selectedCourses));
    }
  }

  unenrollCourse(course: Course): void {
    let selectedCourses: Course[] = JSON.parse(localStorage.getItem('selectedCourses') || '[]');
    selectedCourses = selectedCourses.filter(c => c.courseId !== course.courseId);
    localStorage.setItem('selectedCourses', JSON.stringify(selectedCourses));
  }

  // ...existing code for pagination, selection, etc. (好友紛頁版本)
}
