using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MangaAdd : MonoBehaviour
{
    public Item mangaItem;
    public Bag playerBag;

    public void AddManga()
    {
            playerBag.itemList.Add(mangaItem);
    }
}
