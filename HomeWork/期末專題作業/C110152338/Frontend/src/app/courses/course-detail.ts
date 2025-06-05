import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CourseService } from '../course.service';
import { CommonModule } from '@angular/common';
import { Course } from '../course';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-course-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './course-detail.html',
  styleUrl: './course-detail.css'
})
export class CourseDetail implements OnInit {
  course: Course | undefined;

  constructor(private route: ActivatedRoute, private courseService: CourseService) {}

  ngOnInit(): void {
    const courseName = this.route.snapshot.paramMap.get('id');
    this.courseService.getCourses().subscribe(
      (data: Course[]) => {
        this.course = data.find(course => course.courseName === courseName);
      },
      (error) => console.error('Error fetching course:', error)
    );
  }

  registerCourse(): void {
    alert('模擬註冊成功！');
  }
}
