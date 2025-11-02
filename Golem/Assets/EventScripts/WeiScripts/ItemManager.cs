using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ItemManager : MonoBehaviour
{
    static ItemManager instance;
    public Bag getMangaBag;
    public GameObject mangaShow;
    public Slot slotPrefab;
    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    public static void CreateNewItem(Item item)
    {
        Slot newItem = Instantiate(instance.slotPrefab, instance.mangaShow.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.mangaShow.transform);
        newItem.slotitem = item;
        newItem.slotImage.sprite = item.mangaCover;
        newItem.slotName.text = $"『{item.mangaName}』をゲットした！！";
        newItem.slotInfo.text = $"「{item.mangaInfo}」が記されている！！";
    }

    public static void DestroyItem()
    {
        Destroy(instance.mangaShow.transform.GetChild(0).gameObject);
    }
}
