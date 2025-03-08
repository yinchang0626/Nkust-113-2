#include <bits/stdc++.h>
using namespace std;

// 將整數轉換為 2 位數的十六進位字串
string intToHex(int n) {
    stringstream ss;
    ss << hex << setw(2) << setfill('0') << n; // 轉為 2 位數的 16 進位
    return ss.str();
}

int main() {
    // 定義兩個向量 x 和 y，存放四個座標點的 x 和 y 值
    vector<int> x(4, 0);
    vector<int> y(4, 0);

    // 初始化四個座標點
    x[0] = 2;   y[0] = 12;
    x[1] = 9;   y[1] = 12;
    x[2] = 0;   y[2] = 0;
    x[3] = 16;  y[3] = 0;

    // 執行線性插值，遍歷 y[0] 到 y[2] 的區間，並計算對應的插值
    for (int i = 2; i <= 2; i++) {
        // 計算分子，y[2] - y[0] 用來控制插值的範圍
        int numerator = y[2] - y[0];

        // 計算 x 軸的變化量
        int dL = x[2] - x[0];  // 左邊的 x 軸變化量
        int dR = x[3] - x[1];  // 右邊的 x 軸變化量

        // 計算插值過程中的中間值
        int tempL = dL * (i - y[2]);     // 左邊的插值進行計算
        int tempR = dR * (i - y[2]);     // 右邊的插值進行計算

        // 計算每個插值的位移
        int offsetL = tempL / numerator;
        int offsetR = tempR / numerator;

        // 檢查是否有餘數，若無餘數則標記為 1
        int noRemainderL = ((tempL % numerator == 0) ? 1 : 0);
        int noRemainderR = ((tempR % numerator == 0) ? 1 : 0);

        // 根據條件修正左邊的插值偏移量，避免出現錯誤的插值結果
        if (!(numerator >= 0 && dL >= 0 || numerator < 0 && dL < 0 || noRemainderL)) {
            offsetL -= 1;  // 若無法整除，則將結果向下修正
        }

        // 根據條件修正右邊的插值偏移量，避免出現錯誤的插值結果
        if (!(numerator >= 0 && dR >= 0 || numerator < 0 && dR < 0 || noRemainderR)) {
            offsetR -= 1;  // 若無法整除，則將結果向下修正
        }

        // 輸出插值結果，將座標點格式化為十六進位
        cout << "(" << intToHex((offsetL + x[2])) << "," << intToHex((offsetR + x[3])) << ")\n";
        cout << "\n";
    }

    return 0;
}
