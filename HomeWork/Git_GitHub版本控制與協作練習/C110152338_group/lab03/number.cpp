#include <bits/stdc++.h>
using namespace std;

int getBitValue(int n) {
    if (n < 1 || n > 9) return 0;
    return 1 << (n - 1);  // 左移 (n-1) 位，相當於 2^(n-1)
}

int main() {
    ifstream infile("input.txt");  // 開啟檔案
    if (!infile) {
        cerr << "Can not open" << endl;
        return 1;  // 返回錯誤碼
    }

    vector<vector<int>> blank(15, vector<int>(2, 0));
    vector<int> L_to_R(8, 0);
    vector<int> T_to_B(8, 0);
    vector<int> CUBE(8, 0);
    int blank_cnt = 0;
    int grid[9][9];  // 9x9 陣列存放讀取的數據

    for (int i = 0; i < 9; i++) {
        for (int j = 0; j < 9; j++) {
            infile >> grid[i][j];  // 逐個讀取數字
            int temp = grid[i][j];
            if (grid[i][j] == 0) {
                blank[blank_cnt][0] = i;
                blank[blank_cnt][1] = j;
                blank_cnt++;  // 記錄空白數量
            }
            L_to_R[i] |= getBitValue(temp);
            T_to_B[j] |= getBitValue(temp);
            CUBE[(i / 3) * 3 + j / 3] |= getBitValue(temp);
        }
    }

    int by = blank[0][0];
    int bx = blank[0][1];
    int temp1 = L_to_R[by];
    int temp2 = T_to_B[bx];
    int temp3 = CUBE[(by / 3) * 3 + (bx / 3)];
    int ans = temp1 | temp2 | temp3;  // 修正位運算符
    int compare_data = getBitValue(5) ^ ans;

        for (int x = 0; x < 9; x++) {
        cout << L_to_R[x] * 2 << "\n";
    }
    cout <<"----------------\n";
    for (int x = 0; x < 9; x++) {
        cout << T_to_B[x] * 2 << "\n";
    }
        cout <<"----------------\n";
    for (int x = 0; x < 9; x++) {
        cout << CUBE[x] * 2 << "\n";
    }


    // 只輸出二進制格式
//    cout << bitset<9>(ans) << endl;
//    if (compare_data > ans) {
//        cout << "Yes";
//    }
    infile.close();  // 關閉檔案

    return 0;
}
