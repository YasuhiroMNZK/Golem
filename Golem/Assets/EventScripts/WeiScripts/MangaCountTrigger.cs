using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MangaCountTrigger : TriggerBase
{
    public Bag Bag;
    
    public int Count = 0;

    public void CountMangaTrigger()
    {
        if (Bag != null && Bag.itemList != null && Bag.itemList.Count == Count)
        {
            action.Invoke();
        }
    }
    void Update()
    {
        CountMangaTrigger();
    }
}
