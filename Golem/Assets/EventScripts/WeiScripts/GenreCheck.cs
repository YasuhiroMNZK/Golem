using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class GenreCheck : TriggerBase
{
    public Bag Genre;
    public Bag SelectedManga;

    public void CheckGenreMatch()
    {
        if (Genre == null || Genre.itemList == null || SelectedManga == null || SelectedManga.itemList == null) return;

        int matchCount = 0;
        foreach (Item genreItem in Genre.itemList)
        {
            foreach (Item selectedItem in SelectedManga.itemList)
            {
                if (selectedItem != null && genreItem != null && selectedItem.mangaName== genreItem.mangaName)
                {
                    matchCount++;
                }
            }
        }
        if (matchCount >= 1)
        {
            action.Invoke();
        }
    }

    void Start()
    {
        CheckGenreMatch();
    }
}
