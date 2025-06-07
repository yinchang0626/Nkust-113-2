import { Routes } from '@angular/router';
import { Courses } from './courses/courses';
import { CourseDetail } from './courses/course-detail';
// import { AdminPanel } from './admin-panel';
import { Login } from './login';
import { ResetPassword } from './reset-password';
import { CourseSelection } from './courses/course-selection';
import { ChangePassword } from './change-password';
import { CourseScheduleComponent } from './course-schedule/course-schedule';

export const routes: Routes = [
  { path: '', redirectTo: 'courses', pathMatch: 'full' },
  { path: 'courses', component: Courses },
  { path: 'courses/:id', component: CourseDetail },
  // { path: 'admin', component: AdminPanel },
  { path: 'login', component: Login },
  // { path: 'register', component: Register },
  { path: 'reset-password', component: ResetPassword },
  { path: 'course-selection', component: CourseSelection },
  { path: 'change-password', component: ChangePassword },
  { path: 'course-schedule', component: CourseScheduleComponent }
];
