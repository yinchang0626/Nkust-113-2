export interface Course {
  courseId: number;
  courseName: string;
  description: string;
  instructor: string;
  price: number;
  startDate: string; // 修正型別為 string
  classroom: string; // 新增 classroom 屬性
  schedule: string; // 新增 schedule 屬性
}
