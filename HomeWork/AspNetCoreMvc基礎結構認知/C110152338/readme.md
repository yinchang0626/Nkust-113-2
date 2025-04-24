# Rescue Data WebApp

此專案為 ASP.NET Core MVC 網站，提供高雄市救護資料的展示、上傳與報表功能。

![png](https://github.com/kerong2002/Nkust-113-2/blob/master/HomeWork/AspNetCoreMvc%E5%9F%BA%E7%A4%8E%E7%B5%90%E6%A7%8B%E8%AA%8D%E7%9F%A5/C110152338/PNG/2025-04-24.png)

## 使用

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Chart.js（圖表）
- Bootstrap（前端樣式）

## 結構簡述

- Controllers：RescueController 控制器
- Models：RescueData 資料模型
- Views：Index、Details、Upload、Report 四個頁面
- Services：資料讀取與處理邏輯
- wwwroot：靜態資源，如 Bootstrap、圖表套件等

## 改寫簡報

- 將原始 XML 轉為資料模型並存入資料庫。
- Razor View 中整合 Bootstrap 與 Chart.js 呈現 UI。
- 遇到的問題：Chart.js 與資料整合格式需對應、XML 結構解析需調整。

## 執行說明

1. 使用 Visual Studio 開啟專案。
2. 確保連線字串正確並執行資料庫遷移。
3. 執行網站後可透過 `/Rescue/ImportData` 匯入資料。
4. 依需求瀏覽 `/Rescue/Index`、`/Details`、`/Upload`、`/Report` 等頁面。
