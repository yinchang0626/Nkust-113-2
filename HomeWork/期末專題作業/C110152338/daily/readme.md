# 專案紀錄

這個專案是一個線上課程平台，包含前端和後端。

### 1. 使用者端 (Angular)

*   **瀏覽課程列表：** 首頁或課程頁面應展示所有可用的課程，包含課程名稱、簡介等基本資訊。 **(已完成)**
    *   課程列表已從 `nkust/course.txt` 讀取並顯示在 `Frontend/src/app/courses/courses.html`。
    *   課程資料顯示的欄位包含：科目名稱 (課程名稱), 課程描述 (英文簡述), 授課老師, 教室, 上課時間, 教室, 課程價格, 開課日期 (統一設定為 9/1)。
*   **查看課程詳情：** 使用者點擊任一課程後，應能看到該課程的詳細資訊，如課程完整描述、講師、價格、開課日期等。 **(已完成)**
    *   點擊課程列表中的課程名稱，可以查看該課程的詳細資訊。
    *   課程詳細資訊顯示在 `Frontend/src/app/courses/course-detail.html`。
    *   `Frontend/src/app/courses/course-detail.ts` 負責從 `CourseService` 取得課程資料。
*   **(模擬) 註冊課程：** 使用者可以對感興趣的課程進行註冊。此功能無需串接真實金流，僅需在資料庫中記錄使用者的註冊意願即可。 **(已完成)**
    *   課程詳細資訊頁面提供一個 "模擬註冊" 按鈕。
    *   點擊該按鈕會顯示一個 "模擬註冊成功！" 的訊息。
*   **(可選) 使用者註冊與登入：** 若時間充裕，可實作使用者帳號註冊及登入功能。若時間不足，此部分可簡化或省略，專注於課程的公開展示與模擬註冊。 **(待完成)**

### 2. 管理員端 (ASP.NET MVC Core)

*   **管理員登入：** 系統應提供管理員登入介面，驗證通過後方可進入管理後台。 **(待完成)**
*   **課程管理 (CRUD)：**
    *   **新增 (Create)：** 管理員可以新增課程，輸入課程名稱、描述、講師、價格等資訊。 **(待完成)**
    *   **讀取 (Read)：** 管理員可以查看所有課程列表及個別課程的詳細資訊。 **(待完成)**
    *   **更新 (Update)：** 管理員可以修改現有課程的資訊。 **(待完成)**
    *   **刪除 (Delete)：** 管理員可以刪除不再需要的課程。 **(待完成)**
*   **(可選) 查看使用者註冊列表：** 若已實作使用者註冊功能，管理員應能查看哪些使用者註冊了哪些課程。 **(待完成)**

-   前端使用 Angular 框架，位於 `Frontend/` 目錄下。
    -   `Frontend/src/app/` 目錄包含 Angular 應用程式的元件、服務和模組。
    -   `Frontend/src/app/courses/` 目錄包含課程相關的元件，例如 `courses.component.ts` (課程列表) 和 `course-detail.component.ts` (課程詳細資訊)。
    -   `Frontend/src/app/course.service.ts` 提供課程資料的服務。
    -   `Frontend/src/app/app.routes.ts` 定義了應用程式的路由。
-   後端使用 C# .NET，位於 `Backend/` 目錄下。
    -   `Backend/Controllers/` 目錄包含 API 控制器，例如 `CoursesController.cs` 和 `UsersController.cs`。
    -   `Backend/Models/` 目錄包含資料模型，例如 `Course.cs` 和 `User.cs`。
    -   `Backend/Models/ApplicationDbContext.cs` 定義了資料庫上下文。
    -   `Backend/Program.cs` 是後端應用程式的入口點。
-   資料庫使用 SQL Server，相關設定位於 `Backend/appsettings.json` 和 `nkust/database.sql`。