using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MangaMove : MonoBehaviour
{
    public Bag bagFrom;
    public Bag bagTo;

    public void MoveManga()
    {
            foreach(Item item in bagFrom.itemList)
            {
                if(!bagTo.itemList.Contains(item))
                {
                    bagTo.itemList.Add(item);
                }
            }
            bagFrom.itemList.Clear();
    }
}
