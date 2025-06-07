import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Course } from './course';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  private apiUrl = 'http://localhost:5100/api/courses'; // Replace with your API URL

  constructor(private http: HttpClient) { }

  getCourses(): Observable<Course[]> {
    const courses = this.http.get<Course[]>(this.apiUrl);
    courses.subscribe(data => console.log('Courses:', data));
    return courses;
  }

  getCourse(id: number): Observable<Course> {
    return this.http.get<Course>(`${this.apiUrl}/${id}`);
  }
}