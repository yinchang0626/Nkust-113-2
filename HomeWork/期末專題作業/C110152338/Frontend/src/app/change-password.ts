import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-change-password',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './change-password.html',
  styleUrl: './change-password.css'
})
export class ChangePassword {
  oldPassword: string = '';
  newPassword: string = '';
  confirmPassword: string = '';
  message: string = '';
  error: string = '';

  constructor(private router: Router) {}

  changePassword() {
    if (this.newPassword !== this.confirmPassword) {
      this.error = '新密碼與確認密碼不一致';
      this.message = '';
      return;
    }
    // 模擬密碼驗證
    if (this.oldPassword === 'abc123') {
      this.message = '密碼變更成功';
      this.error = '';
    } else {
      this.error = '舊密碼錯誤';
      this.message = '';
    }
  }
}
