# ASP.NET Core MVC 選課系統
## 結果
![png](https://github.com/kerong2002/Nkust-113-2/blob/master/HomeWork/%E6%9C%9F%E6%9C%AB%E5%B0%88%E9%A1%8C%E4%BD%9C%E6%A5%AD/C110152310/png/%E7%99%BB%E5%85%A5%E7%95%AB%E9%9D%A2.png)
![png](https://github.com/kerong2002/Nkust-113-2/blob/master/HomeWork/%E6%9C%9F%E6%9C%AB%E5%B0%88%E9%A1%8C%E4%BD%9C%E6%A5%AD/C110152310/png/%E8%AA%B2%E7%A8%8B%E8%A1%A8.png)
## 專案目標

建立一個大學選課系統，包含學生登入、課程管理、選課功能，以及管理後台。

## 使用者角色

- **學生**：可以註冊、登入、瀏覽課程、選課、查詢已選課程、退選。
- **管理員**：可以登入後台，管理課程資料、查詢學生選課紀錄。

## 功能

- **登入/註冊系統**：
    - 學生可以註冊帳號（學號、姓名、密碼）。
    - 學生/管理員可以登入、登出。
    - 登入後會根據角色導向不同頁面。
- **課程管理（管理員）**：
    - 新增、編輯、刪除課程。
    - 課程資訊：課號、課程名稱、學分、授課教師、上課時間、教室、修別、人數上限、備註。
    - 可以查詢所有課程和學生選課紀錄。
- **選課功能（學生）**：
    - 登入後可以瀏覽所有課程。
    - 可以根據條件查詢課程。
    - 可以選擇課程加入「已選課程」。
    - 顯示已選課程清單，可以退選。
    - 選課時會檢查是否重複選課、是否超過人數上限、是否有衝堂。
    - 顯示選課成功/失敗訊息。
- **其他**：
    - 所有頁面都有簡單美觀的 UI。
    - 頁面有導覽列，顯示登入者資訊和登出按鈕。

## 資料庫設計

- **User (Id, UserName, PasswordHash, Name, Role)**
- **Course (Id, Code, Name, Credit, Teacher, Time, Room, Type, Limit, Note)**
- **Enrollment (Id, UserId, CourseId, EnrollTime)**

## API

- POST /Account/Login
- POST /Account/Register
- GET /Courses
- POST /Courses/Enroll
- POST /Courses/Drop
- GET /Admin/Courses
- POST /Admin/Courses/Create
- POST /Admin/Courses/Edit
- POST /Admin/Courses/Delete
