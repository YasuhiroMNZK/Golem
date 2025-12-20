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
    public GameObject LogShow;
    public GameObject explainShow;
    public GameObject NPCTextShow;

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
            Title(bagItemIndex, targetText);
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
            Info(bagItemIndex, targetText);
            return;
        }
    }

    public void ShowCover()
    {
        if (getMangaBag == null || getMangaBag.itemList == null || getMangaBag.itemList.Count == 0) return;
        if (CoverShow == null) return;
        var targetImage = CoverShow.GetComponent<Image>();
        if (targetImage != null)
        {
            Cover(bagItemIndex, targetImage);
            return;
        }
    }

    public void ShowLog()
    {
        if (getMangaBag == null || getMangaBag.itemList == null || getMangaBag.itemList.Count == 0) return;
        if (LogShow == null) return;
        var targetText = LogShow.GetComponent<Text>();
        if (targetText != null)
        {
            Log(bagItemIndex, targetText);
            return;
        }
    }

    public void ShowExplain()
    {
        if (getMangaBag == null || getMangaBag.itemList == null || getMangaBag.itemList.Count == 0) return;
        if (explainShow == null) return;
        var targetText = explainShow.GetComponent<Text>();
        if (targetText != null)
        {
            Explain(bagItemIndex, targetText);
            return;
        }
    }

    public void ShowNPCText()
    {
        if (getMangaBag == null || getMangaBag.itemList == null || getMangaBag.itemList.Count == 0) return;
        if (NPCTextShow == null) return;
        var targetText = NPCTextShow.GetComponent<Text>();
        if (targetText != null)
        {
            NPCText(bagItemIndex, targetText);
            return;
        }
    }

    // Text用
    public void Title(int index, Text targetText)
    {
        if (getMangaBag == null || getMangaBag.itemList == null) return;
        if (index < 0 || index >= getMangaBag.itemList.Count) return;

        Item item = getMangaBag.itemList[index];
        if (item != null && targetText != null)
        {
            targetText.text = $"『{item.mangaName}』をゲットした！！"; // mangaTitleはItemクラスのstringプロパティと仮定
        }
    }
    public void Info(int index, Text targetText)
    {
        if (getMangaBag == null || getMangaBag.itemList == null) return;
        if (index < 0 || index >= getMangaBag.itemList.Count) return;

        Item item = getMangaBag.itemList[index];
        if (item != null && targetText != null)
        {
            targetText.text = $"「{item.mangaInfo}」が記されている！！"; // mangaTitleはItemクラスのstringプロパティと仮定
        }
    }

    public void Cover(int index, Image targetImage)
    {
        if (getMangaBag == null || getMangaBag.itemList == null) return;
        if (index < 0 || index >= getMangaBag.itemList.Count) return;

        Item item = getMangaBag.itemList[index];
        if (item != null && targetImage != null)
        {
            targetImage.sprite = item.mangaCover;
        }
    }

    public void Log(int index, Text targetText)
    {
        if (getMangaBag == null || getMangaBag.itemList == null) return;
        if (index < 0 || index >= getMangaBag.itemList.Count) return;

        Item item = getMangaBag.itemList[index];
        if (item != null && targetText != null)
        {
            targetText.text = $"{item.mangaLog}";
        }
    }

    public void Explain(int index, Text targetText)
    {
        if (getMangaBag == null || getMangaBag.itemList == null) return;
        if (index < 0 || index >= getMangaBag.itemList.Count) return;

        Item item = getMangaBag.itemList[index];
        if (item != null && targetText != null)
        {
            targetText.text = $"{item.mangaExplain}";
        }
    }

    public void NPCText(int index, Text targetText)
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