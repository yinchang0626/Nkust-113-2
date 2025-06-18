
-- 建立資料表
CREATE TABLE Datas (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FirstName TEXT NOT NULL,
    LastName TEXT NOT NULL,
    Email TEXT NOT NULL,
    Gender TEXT NOT NULL,
    CompanyName TEXT NOT NULL
);

-- 插入預設資料
INSERT INTO Datas (FirstName, LastName, Email, Gender, CompanyName)
VALUES 
('YAN', 'YAN', 'yanyan@gmail.com', 'Female', 'TSMC'),
('YAN', 'LING', 'yanling@gmail.com', 'Female', 'ASE');
