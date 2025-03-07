class Solution {
public:
    vector<string> sortPeople(vector<string>& names, vector<int>& heights) {
        for (int y = 0; y < heights.size(); y++) {
            for (int x = y + 1; x < heights.size(); ++x) {
                if (heights[y] < heights[x]) {
                    swap(heights[y], heights[x]);
                    swap(names[y], names[x]);
                }
            }
        }
        return names;
    }
};