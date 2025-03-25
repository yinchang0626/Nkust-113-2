# C# 基礎練習題目：政府開放資料分析

## 目標
本練習旨在讓學生熟悉 C# 語言的基本操作，並透過處理台灣政府開放資料（OpenData）來練習檔案讀取與數據分析。

## 任務內容
1. **收集資料**  
   - 前往台灣政府開放資料平台（[https://data.gov.tw](https://data.gov.tw)）或其他開放數據來源，選擇一份有興趣的資料集。  
   - 檔案格式不限，可為 **JSON、CSV 或 XML**。

2. **C# 程式開發**
   - **主控台應用程式**：使用 C# 撰寫一個主控台程式，讀取所選資料集。
   - **格式解析**：根據檔案類型選擇適當的 **套件或內建函式** 進行解析：
     - JSON：`System.Text.Json` 或 `Newtonsoft.Json`
     - CSV：`CsvHelper`
     - XML：`System.Xml.Linq`
   - **基礎分析**：至少實作以下功能：
     - 顯示資料總筆數
     - 顯示部分關鍵欄位的內容（如名稱、時間等）

3. **進階加分項**
   - 使用 **物件導向概念**（如 `class`、`interface` 或設計模式）來設計程式架構。
   - 利用 **LINQ** 進行資料篩選、排序或計算統計資訊（如平均值、最大/最小值等）。

## 交付方式
1. **提交程式碼至 GitHub**：
   - 建立一個新的 GitHub Repository
   - 將完整的 C# 程式碼上傳至該 Repository
   - 在 README.md 文件中說明：
     - 選擇的資料集來源
     - 主要的程式功能
     - 使用的 C# 套件
     - 如何執行程式

2. **提交 PR（Pull Request）**
   - 個人作業：直接在自己的 Repo 開發並提交 PR（可當作版本控制練習）。
   - 團體作業：請組長 **fork** 主 Repo，並邀請組員參與開發，合併程式後再提交 PR。

## 評分標準
- **基本要求（70%）**
  - 成功讀取並解析檔案
  - 能夠顯示資料的總筆數
  - 顯示部分重要欄位資訊
- **進階加分（30%）**
  - 運用物件導向技術（如 `class` 設計）
  - 使用 LINQ 進行進一步的資料分析
  - 程式碼結構清晰、具可讀性

## 參考資源
- 台灣政府開放資料平台：[https://data.gov.tw](https://data.gov.tw)
- JSON 處理：[System.Text.Json](https://learn.microsoft.com/en-us/dotnet/api/system.text.json) / [Newtonsoft.Json](https://www.newtonsoft.com/json)
- CSV 處理：[CsvHelper](https://joshclose.github.io/CsvHelper/)
- XML 處理：[System.Xml.Linq](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq)
- LINQ：[LINQ 教學](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/)

