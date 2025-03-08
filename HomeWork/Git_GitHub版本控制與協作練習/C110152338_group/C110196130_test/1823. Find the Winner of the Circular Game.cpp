class Solution {
public:
    int findTheWinner(int n, int k) {
        int pos  = 0;
        for (int x = 2; x < n + 1; ++x) {
            pos = (pos + k) % x;
        }
        return pos + 1;
    }
};