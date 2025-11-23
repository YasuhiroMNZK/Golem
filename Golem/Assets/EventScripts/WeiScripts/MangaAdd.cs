using UnityEngine;

public class MangaAdd : MonoBehaviour
{
    public Item mangaItem;
    public Bag playerBag;

    public void AddManga()
    {
        if (playerBag == null)
        {
            Debug.LogError("playerBag が設定されていません");
            return;
        }


        if (mangaItem == null)
        {
            Debug.LogError("mangaItem が設定されていません");
            return;
        }

        playerBag.itemList.Add(mangaItem);
        Debug.Log($"Manga {mangaItem.name} を playerBag に追加しました");

        // itemList の内容を確認
        Debug.Log($"現在の playerBag.itemList の内容: {string.Join(", ", playerBag.itemList)}");
    }
}