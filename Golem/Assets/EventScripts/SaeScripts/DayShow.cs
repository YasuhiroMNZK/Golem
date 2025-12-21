using UnityEngine;
using UnityEngine.UI;

public class DayShow : MonoBehaviour
{
    [SerializeField] private Bag bag;
    [SerializeField] private Text dayText; // 日数を表示するテキスト
    
    void Start()
    {
        UpdateDayText();
    }
    
    public void UpdateDayText()
    {
        if (bag == null || bag.itemList == null || dayText == null) return;
        
        int itemCount = 0;
        foreach (Item item in bag.itemList)
        {
            if (item != null)
            {
                itemCount++;
            }
        }
        
        int dayNumber = itemCount + 1;
        dayText.text = $"{dayNumber}日目";
    }
}
