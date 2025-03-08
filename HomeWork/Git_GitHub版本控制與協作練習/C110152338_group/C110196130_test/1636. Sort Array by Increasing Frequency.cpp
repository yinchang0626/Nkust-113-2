class Solution {
public:
    vector<int> frequencySort(vector<int>& nums) {
        map<int, int> mp;
        for (auto num : nums) {
            mp[num]++;
        }

        vector<pair<int, int>> freq;
        for (auto x : mp) {
            freq.push_back(x);
        }
        // for (auto x : mp) {
        //     cout << x.first << "," << x.second << "\n";
        // }
        sort(freq.begin(), freq.end(), [](const pair<int, int>& a, const pair<int, int>& b) {
            if (a.second == b.second) {
                return a.first > b.first;
            }
            return a.second < b.second;
        });

        vector<int> ans;
        for (auto y : freq) {
            for (int x = 0 ; x < y.second; ++x) {
                ans.push_back(y.first);
            }
        }
        return ans;
    }
};