using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class GetMangaShow : MonoBehaviour
{
    public Bag getMangaBag;
    public GameObject TitleShow;
    public GameObject InfoShow;
    public GameObject CoverShow;
    public int bagItemIndex = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowAllMangaInfo();
    }

    public void ShowAllMangaInfo()
    {
        if (IsItemExistsAtIndex(bagItemIndex))
        {
            ShowTitle();
            ShowInfo();
            ShowImage();

            //親オブジェクトを表示
            this.gameObject.SetActive(true);
        }
        else
        {
            //親オブジェクトを非表示
            this.gameObject.SetActive(false);
        }
    }

    private bool IsItemExistsAtIndex(int index)
    {
        if (getMangaBag == null || getMangaBag.itemList == null) return false;
        if (index < 0 || index >= getMangaBag.itemList.Count) return false;
        return getMangaBag.itemList[index] != null;
    }

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

    public void ShowMangaTitle(int index, Text targetText)
    {
        if (getMangaBag == null || getMangaBag.itemList == null) return;
        if (index < 0 || index >= getMangaBag.itemList.Count) return;

        Item item = getMangaBag.itemList[index];
        if (item != null)
        {
            targetText.text = item.mangaName;
        }
    }

    public void ShowMangaInfo(int index, Text targetText)
    {
        if (getMangaBag == null || getMangaBag.itemList == null) return;
        if (index < 0 || index >= getMangaBag.itemList.Count) return;

        Item item = getMangaBag.itemList[index];
        if (item != null)
        {
            targetText.text = item.mangaInfo;
        }
    }
    
    public void ShowMangaImage(int index, Image targetImage)
    {
        if (getMangaBag == null || getMangaBag.itemList == null) return;
        if (index < 0 || index >= getMangaBag.itemList.Count) return;

        Item item = getMangaBag.itemList[index];
        if (item != null)
        {
            targetImage.sprite = item.mangaCover;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
