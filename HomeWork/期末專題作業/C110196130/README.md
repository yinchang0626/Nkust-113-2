# ASP.NET MVC 選課系統

本專案為一個基於 ASP.NET MVC 的選課系統，包含課程列表、學生註冊/登入、選課功能與已選課程查詢。
## 圖片
![](https://github.com/kerong2002/Nkust-113-2/blob/master/HomeWork/%E6%9C%9F%E6%9C%AB%E5%B0%88%E9%A1%8C%E4%BD%9C%E6%A5%AD/C110196130/png/%E7%99%BB%E5%85%A5%E7%95%AB%E9%9D%A2.png)
![](https://github.com/kerong2002/Nkust-113-2/blob/master/HomeWork/%E6%9C%9F%E6%9C%AB%E5%B0%88%E9%A1%8C%E4%BD%9C%E6%A5%AD/C110196130/png/%E6%88%91%E7%9A%84%E8%AA%B2%E8%A1%A8.png)
![](https://github.com/kerong2002/Nkust-113-2/blob/master/HomeWork/%E6%9C%9F%E6%9C%AB%E5%B0%88%E9%A1%8C%E4%BD%9C%E6%A5%AD/C110196130/png/%E9%81%B8%E8%AA%B2%E6%B8%85%E5%96%AE.png)
## 執行方式

1. 安裝 .NET 9 SDK（或相容版本）。
2. 在專案根目錄執行：
   ```powershell
   dotnet run --project CourseSelectionSystem.csproj
   ```
3. 於瀏覽器開啟 http://localhost:5000 或 http://localhost:5001

## 主要功能
- 課程列表
- 學生註冊/登入
- 選課功能
- 已選課程查詢

## 專案結構
- Models：資料模型（Course, Student, Enrollment）
- Controllers：控制器（CoursesController, StudentsController, EnrollmentsController）
- Views：前端頁面

