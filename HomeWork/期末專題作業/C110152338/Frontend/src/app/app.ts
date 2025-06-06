import { Component, ChangeDetectorRef } from '@angular/core';
import { RouterModule, Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'Frontend';
  private _username: string | null = localStorage.getItem('username');

  constructor(private cdr: ChangeDetectorRef, private router: Router) {}

  get username(): string | null {
    return this._username;
  }

  set username(value: string | null) {
    this._username = value;
  }

  logout() {
    localStorage.removeItem('username');
    localStorage.removeItem('selectedCourses'); // Clear course selection data
    this.username = null;
    this.cdr.detectChanges();
    this.router.navigate(['/login']);
  }

  ngOnInit() {
    this.username = localStorage.getItem('username');
  }
}
