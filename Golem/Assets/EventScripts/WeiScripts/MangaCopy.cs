using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MangaCopy : MonoBehaviour
{
    public Bag bagFrom;
    public Bag bagTo;

    public void CopyManga()
    {
        if (bagTo != null&&bagFrom != null&&bagFrom.itemList != null)
        {
            foreach (Item item in bagFrom.itemList)
            {
                if (!bagTo.itemList.Contains(item))
                {
                    bagTo.itemList.Add(item);
                    if (bagTo.itemList != null)
                    {
                        Debug.Log("コピー済み");
                    }
                }
            }
        }
    }
}
