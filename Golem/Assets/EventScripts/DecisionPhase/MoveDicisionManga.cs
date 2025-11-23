using UnityEngine;

public class MoveDicisionManga : MonoBehaviour
{
    [SerializeField] private Bag bagFrom; // 移動元のBag
    [SerializeField] private Bag bagTo; // 移動先のBag

    public void MoveItemByIndex(int itemIndex)
    {
        if (itemIndex >= 0 && itemIndex < bagFrom.itemList.Count)
        {
            Item targetItem = bagFrom.itemList[itemIndex];
            
            // nullチェック（既に移動済みの場合）
            if (targetItem == null)
            {
                Debug.Log("このアイテムは既に移動済みです");
                return;
            }
            
            if (!bagTo.itemList.Contains(targetItem))
            {
                bagTo.itemList.Add(targetItem);
            }
            
            // 移動元からは削除しない（コピーのみ）
            // bagFrom.itemList[itemIndex] = null;
        }
        else
        {
            Debug.Log("無効なアイテムインデックスです: " + itemIndex);
        }
    }
}
