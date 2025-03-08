/**
 * Definition for singly-linked list.
 * struct ListNode {
 *     int val;
 *     ListNode *next;
 *     ListNode() : val(0), next(nullptr) {}
 *     ListNode(int x) : val(x), next(nullptr) {}
 *     ListNode(int x, ListNode *next) : val(x), next(next) {}
 * };
 */
class Solution {
public:
    vector<int> nodesBetweenCriticalPoints(ListNode* head) {
        vector<int> ans{-1, -1};
        ListNode* pre = head;
        ListNode* cur = head->next;
        int prePos = -1;
        int curPos = -1;
        int first = -1;
        int position = 0;


        while (cur->next != nullptr) {
            if ((cur->val < pre->val && cur->val < cur->next->val) ||
                (cur->val > pre->val && cur->val > cur->next->val)) {
                prePos = curPos;
                curPos = position;
                if (first == -1) {
                    first = position;
                }
                if(prePos != -1) {
                    if(ans[0] == -1) {
                        ans[0] = curPos - prePos;
                    }
                    else{
                        ans[0] = min(ans[0], curPos - prePos);
                    }
                    ans[1] = position - first;   
                }
            }
            position++;
            pre = cur;
            cur = cur->next;
        }

        return ans;
    }
};