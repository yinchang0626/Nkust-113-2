import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  username: string = '';
  password: string = '';
  error: string = '';

  constructor(private router: Router) {}

  login() {
    // 簡易驗證（可串接 API）
    if (this.username === 'kerong' && this.password === 'abc123') {
      localStorage.setItem('username', this.username);
      this.router.navigate(['/courses']).then(() => {
       window.location.reload();
     });
    } else {
      this.error = '帳號或密碼錯誤';
    }
  }
}
