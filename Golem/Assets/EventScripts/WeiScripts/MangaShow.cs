using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MangaShow : MonoBehaviour
{
    public Bag getMangaBag;
    public GameObject TitleShow;
    public GameObject InfoShow;
    public GameObject CoverShow; // 画像投影用GameObject
    public GameObject TextShow;

    // Unityインスペクターで指定するBag要素の序列
    public int bagItemIndex = 0;

    // 指定indexのBag要素のmangaTitleをmangaShowに投影
    public void ShowTitle()
    {
        if (getMangaBag == null || getMangaBag.itemList == null || getMangaBag.itemList.Count == 0) return;
        if (TitleShow == null) return;
        var targetText = TitleShow.GetComponent<Text>();
        if (targetText != null)
        {
            ShowMangaTitle(bagItemIndex, targetText);
            return;
        }
    }

    
    public void ShowInfo()
    {
        if (getMangaBag == null || getMangaBag.itemList == null || getMangaBag.itemList.Count == 0) return;
        if (InfoShow == null) return;
        var targetText = InfoShow.GetComponent<Text>();
        if (targetText != null)
        {
            ShowMangaInfo(bagItemIndex, targetText);
            return;
        }
    }

    public void ShowImage()
    {
        if (getMangaBag == null || getMangaBag.itemList == null || getMangaBag.itemList.Count == 0) return;
        if (CoverShow == null) return;
        var targetImage = CoverShow.GetComponent<Image>();
        if (targetImage != null)
        {
            ShowMangaImage(bagItemIndex, targetImage);
            return;
        }
    }

    public void ShowText()
    {
        if (getMangaBag == null || getMangaBag.itemList == null || getMangaBag.itemList.Count == 0) return;
        if (TextShow == null) return;
        var targetText = TextShow.GetComponent<Text>();
        if (targetText != null)
        {
            ShowMangaText(bagItemIndex, targetText);
            return;
        }
    }

    // Text用
    public void ShowMangaTitle(int index, Text targetText)
    {
        if (getMangaBag == null || getMangaBag.itemList == null) return;
        if (index < 0 || index >= getMangaBag.itemList.Count) return;

        Item item = getMangaBag.itemList[index];
        if (item != null && targetText != null)
        {
            targetText.text = $"『{item.mangaName}』をゲットした！！"; // mangaTitleはItemクラスのstringプロパティと仮定
        }
    }
    public void ShowMangaInfo(int index, Text targetText)
    {
        if (getMangaBag == null || getMangaBag.itemList == null) return;
        if (index < 0 || index >= getMangaBag.itemList.Count) return;

        Item item = getMangaBag.itemList[index];
        if (item != null && targetText != null)
        {
            targetText.text = $"「{item.mangaInfo}」が記されている！！"; // mangaTitleはItemクラスのstringプロパティと仮定
        }
    }

    public void ShowMangaImage(int index, Image targetImage)
    {
        if (getMangaBag == null || getMangaBag.itemList == null) return;
        if (index < 0 || index >= getMangaBag.itemList.Count) return;

        Item item = getMangaBag.itemList[index];
        if (item != null && targetImage != null)
        {
            targetImage.sprite = item.mangaCover;
        }
    }

    public void ShowMangaText(int index, Text targetText)
    {
        if (getMangaBag == null || getMangaBag.itemList == null) return;
        if (index < 0 || index >= getMangaBag.itemList.Count) return;

        Item item = getMangaBag.itemList[index];
        if (item != null && targetText != null)
        {
            targetText.text = $"{item.NPCText}";
        }
    }
    
}