using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MangaCheckinUpdate : TriggerBase
{
    public Item Manga;
    public Bag SelectedBag;

    public void MangaCheck()
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
    }
    void Update()
    {
        MangaCheck();
    }
}
