import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './reset-password.html',
  styleUrl: './reset-password.css'
})
export class ResetPassword {
  username: string = '';
  key: string = '';
  newPassword: string = '';
  message: string = '';
  error: string = '';

  constructor(private router: Router) {}

  resetPassword() {
    // 模擬重設金鑰驗證
    if (this.key === 'kerong2002') {
      this.message = '密碼重設成功，請重新登入。';
      this.error = '';
    } else {
      this.error = '重設金鑰錯誤';
      this.message = '';
    }
  }
}
