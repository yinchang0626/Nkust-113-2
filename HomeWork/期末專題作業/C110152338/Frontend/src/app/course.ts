export interface Course {
  courseId: number;
  courseName: string;
  description: string;
  instructor: string;
  price: number;
  startDate: string; // 修正型別為 string
}