using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MangaCount : TriggerBase
{
    public Bag Bag;
    
    public int Count = 0;

    public void CountManga()
    {
        if (Bag != null && Bag.itemList != null && Bag.itemList.Count == Count)
        {
            action.Invoke();
        }
    }
    void Start()
    {
        CountManga();
    }
}
