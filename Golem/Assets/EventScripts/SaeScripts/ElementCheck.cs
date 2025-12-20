using UnityEngine;

public class ElementCheck : TriggerBase
{
    [SerializeField] private Bag bag;
    [SerializeField] private int ElementIndex = 2; // 必要なエレメント数
    public UnityEngine.Events.UnityEvent notFoundAction; // 条件を満たさない場合のアクション

    public void Action()
    {
        if (bag == null || bag.itemList == null) 
        {
            notFoundAction.Invoke();
            return;
        }
        
        int itemCount = 0;
        foreach (Item item in bag.itemList)
        {
            if (item != null)
            {
                itemCount++;
            }
        }

        if (itemCount == ElementIndex)
        {
            action.Invoke(); // アイテムが2個見つかった場合
            Debug.Log($"アイテムが{itemCount}個見つかりました");
        }
        else
        {
            notFoundAction.Invoke(); // アイテムが多かったり少なかったりする場合
            Debug.Log($"アイテムが{itemCount}個しかありません（必要数: {ElementIndex}）");
        }
    }
}
