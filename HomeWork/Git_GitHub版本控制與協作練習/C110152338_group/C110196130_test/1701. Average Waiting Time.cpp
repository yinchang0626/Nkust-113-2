class Solution {
public:
    double averageWaitingTime(vector<vector<int>>& customers) {
        int time = customers[0][0];
        double ans = 0;
        for (int x = 0; x < customers.size(); ++x) {
            if (time < customers[x][0]) {
                time = customers[x][0] + customers[x][1];
            }
            else {
                time += customers[x][1];
            }
            ans += time - customers[x][0];
            // cout << ans << "," << time << endl;
        }
        ans /= (double)customers.size();
        return ans;
    }
};