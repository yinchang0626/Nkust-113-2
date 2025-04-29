選擇的資料集來源
-https://data.gov.tw/dataset/168047（提供署屬氣象站日射量資料）
主要的程式功能
-從文件中讀取 JSON 內容。
-使用 JsonConvert.DeserializeObject<CwaopendataRoot> 來解析 JSON。
-顯示總筆數。
-最多顯示 5 筆資料，並顯示站名、站點 ID、觀測時間、海拔高度、日射量等欄位。
使用的 C# 套件
-Newtonsoft.Json（13.0.3）
按run直接開始
