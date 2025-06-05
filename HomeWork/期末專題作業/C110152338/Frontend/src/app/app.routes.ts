import { Routes } from '@angular/router';
import { Courses } from './courses/courses';
import { CourseDetail } from './courses/course-detail';

export const routes: Routes = [
  { path: '', redirectTo: 'courses', pathMatch: 'full' },
  { path: 'courses', component: Courses },
  { path: 'courses/:id', component: CourseDetail }
];
