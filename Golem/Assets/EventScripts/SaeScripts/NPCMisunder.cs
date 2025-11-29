using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class NPCMisunder : MonoBehaviour
{
    [SerializeField] private Bag getMangaBag;
    [SerializeField] private GameObject Misunder;
    int bagItemIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowMisunder();
    }

    void ShowMisunder()
    {
        if (getMangaBag == null || getMangaBag.itemList == null || getMangaBag.itemList.Count == 0) return;
        if (Misunder == null) return;
        var misunder = Misunder.GetComponent<SpriteRenderer>();
        if (misunder != null)
        {
            MangaMisunder(bagItemIndex, misunder);
            return;
        }
    }

        public void MangaMisunder(int index, SpriteRenderer targetImage)
    {
        if (getMangaBag == null || getMangaBag.itemList == null) return;
        if (index < 0 || index >= getMangaBag.itemList.Count) return;

        Item item = getMangaBag.itemList[index];
        if (item != null && targetImage != null)
        {
            targetImage.sprite = item.misunder;
        }
    }
}
