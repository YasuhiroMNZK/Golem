using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Bag", menuName = "Item/Bag")]
public class Bag : ScriptableObject
{
    [SerializeField] private string saveKey;
    public List<Item> itemList = new List<Item>();

    // セーブ時の識別子（未設定ならアセット名）
    public string GetSaveKey()
    {
        return string.IsNullOrEmpty(saveKey) ? name : saveKey;
    }

    public void Init()
    {
       Debug.Log($"Bag {name} を初期化しました");
    }
}
