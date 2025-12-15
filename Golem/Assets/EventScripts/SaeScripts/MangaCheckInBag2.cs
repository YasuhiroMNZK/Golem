using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MangaCheckInBag2 : TriggerBase
{
    public Item Manga;
    public Bag SelectedBag;
    public UnityEngine.Events.UnityEvent elseAction; // アイテムが見つからない場合のアクション

    public void Action()
    {
        if (Manga == null || SelectedBag == null || SelectedBag.itemList == null) return;
        foreach (Item item in SelectedBag.itemList)
        {
            if (item != null && Manga != null && item.mangaName == Manga.mangaName)
            {
                action.Invoke();
                return;
            }
        }
        // アイテムが見つからない場合
        elseAction.Invoke();
    }
    //void Start()
    //{
    //    MangaCheck();
    //}
}
