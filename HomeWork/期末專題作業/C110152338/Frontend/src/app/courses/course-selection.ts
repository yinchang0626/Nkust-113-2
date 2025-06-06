import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-course-selection',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './course-selection.html',
  styleUrl: './courses.css'
})
export class CourseSelection {
  selectedCourses: any[] = [];

  ngOnInit() {
    const username = localStorage.getItem('username');
    if (!username) {
      this.selectedCourses = [];
      return;
    }
    this.selectedCourses = JSON.parse(localStorage.getItem('selectedCourses') || '[]');
  }

  removeFromSelection(courseId: number) {
    this.selectedCourses = this.selectedCourses.filter(c => c.courseId !== courseId);
    localStorage.setItem('selectedCourses', JSON.stringify(this.selectedCourses));
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('username');
  }
}
