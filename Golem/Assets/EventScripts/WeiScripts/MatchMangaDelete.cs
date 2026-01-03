using UnityEngine;
using System.Collections.Generic;

public class MatchMangaDelete : MonoBehaviour
{
    public Bag RemoveBag;
    public Bag targetBag;

    public void Action()
    {
        // Bag や itemList が無い場合は何もしない
        if (RemoveBag == null || targetBag == null) return;
        if (RemoveBag.itemList == null || targetBag.itemList == null) return;

        // targetBag 側の mangaName をセットに集める
        var targetNames = new HashSet<string>();
        foreach (Item item in targetBag.itemList)
        {
            if (item == null || string.IsNullOrEmpty(item.mangaName)) continue;
            targetNames.Add(item.mangaName);
        }

        // RemoveBag を後ろから走査し、mangaName が targetBag と共通のものを削除
        for (int i = RemoveBag.itemList.Count - 1; i >= 0; i--)
        {
            Item item = RemoveBag.itemList[i];
            if (item == null) continue;

            if (!string.IsNullOrEmpty(item.mangaName) && targetNames.Contains(item.mangaName))
            {
                RemoveBag.itemList.RemoveAt(i);
            }
        }
    }
}