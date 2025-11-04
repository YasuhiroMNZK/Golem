using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MangaGet : MonoBehaviour
{
    public Item mangaItem;
    public Bag playerBag;

    public void GetManga()
    {
        if (!playerBag.itemList.Contains(mangaItem))
        {
            playerBag.itemList.Add(mangaItem);
         }
    }
}
