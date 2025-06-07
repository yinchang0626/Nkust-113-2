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
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.courseService.getCourses().subscribe(
        (data: Course[]) => {
          this.course = data.find(course => String(course.courseId) === String(id)); // 兩邊都轉字串
        },
        (error) => console.error('Error fetching course:', error)
      );
    } else {
      console.error('Course ID is null');
    }
  }

  registerCourse(): void {
    alert('模擬註冊成功！');
  }
}
