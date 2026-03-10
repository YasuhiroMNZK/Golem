using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MangaCheckinUpdate : TriggerBase
{
    public Item Manga;
    public Bag SelectedBag;

    // 目标没找到时に実行する別アクション
    [SerializeField] private UnityEngine.Events.UnityEvent notFoundAction;

    public void MangaCheck()
    {
        if (Manga == null || SelectedBag == null || SelectedBag.itemList == null)
        {
            // 情報不足も「見つからなかった」とみなして別アクション
            notFoundAction?.Invoke();
            return;
        }

        foreach (Item item in SelectedBag.itemList)
        {
            if (item != null && Manga != null && item.mangaName == Manga.mangaName)
            {
                // 目标が見つかった場合のアクション
                action.Invoke();
                return;
            }
        }

        // ループを最後まで回っても見つからなかった場合
        notFoundAction?.Invoke();
    }

    void Update()
    {
        MangaCheck();
    }
}
