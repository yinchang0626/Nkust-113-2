#include <bits/stdc++.h>
using namespace std;
int main(){
    int W = 7;
    int VGS = 7;
    int VDS = 4;
    int mode = 3 % 2;
//    cin >> W >> VGS >> VDS;
//    cin >> mode;
    int ID, gm;
    if (VGS - 1 > VDS) {
        cout << "Triode mode\n";
        ID = W * (2 * (VGS - 1) * VDS - VDS * VDS) / 3;
        gm = 2 * W * VDS / 3;
    }
    else {
        cout << "Saturation mode\n";
        ID = W * ( (VGS - 1) * (VGS - 1) ) / 3;
        gm = 2 * (W * (VGS - 1)) / 3;
    }
    cout << ID << "," << gm << endl;
    printf("------------------\n");
    if (mode == 1) {
        printf("ID = %d\n", ID);
        cout << (3 * ID + 4 * ID + 5 * ID) / 12 << "\n";
    }
    else {
        printf("gm = %d\n", gm);
        cout << 3 * gm / 3 << "\n";

    }
    return 0;
}
