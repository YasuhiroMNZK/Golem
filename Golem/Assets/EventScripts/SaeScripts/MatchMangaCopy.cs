using UnityEngine;
using System.Collections.Generic;

public class MatchMangaCopy : MonoBehaviour
{
    public Bag firstBag;
    public Bag secondBag;
    public Bag targetBag;

    public void Action()
    {
        if (firstBag == null || secondBag == null || targetBag == null) return;
        if (firstBag.itemList == null || secondBag.itemList == null) return;

        // 共通アイテムを検索してコピー
        foreach (Item item1 in firstBag.itemList)
        {
            if (item1 == null) continue;
            
            foreach (Item item2 in secondBag.itemList)
            {
                if (item2 == null) continue;
                
                // mangaNameで一致判定
                if (item1.mangaName == item2.mangaName)
                {
                    // 重複チェック（既にターゲットバッグにある場合はスキップ）
                    if (!IsItemAlreadyInBag(targetBag, item1))
                    {
                        targetBag.itemList.Add(item1);
                    }
                    break; // 同じアイテムが複数あっても1つだけコピー
                }
            }
        }
    }

    private bool IsItemAlreadyInBag(Bag bag, Item item)
    {
        if (bag.itemList == null) return false;
        
        foreach (Item existingItem in bag.itemList)
        {
            if (existingItem != null && existingItem.mangaName == item.mangaName)
            {
                return true;
            }
        }
        return false;
    }
}