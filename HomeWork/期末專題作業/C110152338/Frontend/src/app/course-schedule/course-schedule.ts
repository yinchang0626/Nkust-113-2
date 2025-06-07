import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Course } from '../course';

@Component({
  selector: 'app-course-schedule',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './course-schedule.html',
  styleUrl: './course-schedule.css'
})
export class CourseScheduleComponent implements OnInit {
  selectedCourses: Course[] = [];
  // 時間表結構：行為節次，列為星期
  timetable: (Course | null)[][] = [];
  periods = [
    { label: '第 1 節\n0810-0900', key: '1' },
    { label: '第 2 節\n0910-1000', key: '2' },
    { label: '第 3 節\n1010-1100', key: '3' },
    { label: '第 4 節\n1110-1200', key: '4' },
    { label: '第 A 節\n1210-1300', key: 'A' },
    { label: '第 5 節\n1330-1420', key: '5' },
    { label: '第 6 節\n1430-1520', key: '6' },
    { label: '第 7 節\n1530-1620', key: '7' },
    { label: '第 8 節\n1630-1720', key: '8' }
  ];
  days = ['一', '二', '三', '四', '五'];

  ngOnInit(): void {
    this.selectedCourses = JSON.parse(localStorage.getItem('selectedCourses') || '[]');
    this.generateTimetable();
  }

  // 將課程依據 schedule 填入時間表
  generateTimetable() {
    // 9 節次 x 5 天
    this.timetable = Array.from({ length: this.periods.length }, () => Array(this.days.length).fill(null));
    for (const course of this.selectedCourses) {
      // 假設 schedule 格式如：(三)2-4
      const match = course.schedule.match(/\((.)\)([A1-8])-([A1-8])/);
      if (match) {
        const dayIdx = this.days.indexOf(match[1]);
        const start = this.periods.findIndex(p => p.key === match[2]);
        const end = this.periods.findIndex(p => p.key === match[3]);
        for (let i = start; i <= end; i++) {
          if (dayIdx >= 0 && i >= 0) {
            this.timetable[i][dayIdx] = course;
          }
        }
      }
    }
  }
}