using UnityEngine;
using System.Collections.Generic;

public class ElementRandom : MonoBehaviour
{
    // Inspector で初期値を入れておきたい場合用の配列
    [SerializeField] private Bag[] elements;
    public Bag BagFrom;

    // 実際の実行時は List で管理
    private List<Bag> elementList = new List<Bag>();

    private void Awake()
    {
        // Inspector から入れておいた分を List にコピー
        if (elements != null && elements.Length > 0)
        {
            elementList.AddRange(elements);
        }

        // 同期（Inspector の配列はあくまでデバッグ確認用）
        elements = elementList.ToArray();
    }

    public void BagShuffle()
    {
        if (elementList != null && elementList.Count > 1)
        {
            Shuffle(elementList);
            elements = elementList.ToArray();
        }
    }

    // List 版のシャッフル
    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }


    public void ClearAllElementItems()
    {
        foreach (var bag in elementList)
        {
            if (bag == null) continue;
            if (bag.itemList == null) continue;

            bag.itemList.Clear();
        }

        Debug.Log("elements 内の全ての Bag.itemList をクリアしました。");
    }


    // 他スクリプトから要素追加
    public void AddElement(Bag bag)
    {
        if (bag == null) return;
        if (!elementList.Contains(bag))
        {
            elementList.Add(bag);
            elements = elementList.ToArray();   // Inspector で中身を確認したい場合
        }
    }
    public void CopyItemsFromBagFromToElements()
    {
        if (BagFrom == null || BagFrom.itemList == null)
        {
            Debug.LogWarning("BagFrom か BagFrom.itemList が設定されていません。");
            return;
        }

        if (elements == null || elements.Length == 0)
        {
            Debug.LogWarning("elements に Bag が入っていません。");
            return;
        }
       
        // BagFrom と elements の少ない方に合わせる
        int count = Mathf.Min(BagFrom.itemList.Count, elements.Length);
        for (int i = 0; i < count; i++)
        {
            Bag fromBag = BagFrom;
            Item fromItem = fromBag.itemList[i];
            Bag eleBag = elements[i];

            if (eleBag == null)
                continue;

            if (fromItem == null)
                continue;

            if (eleBag.itemList == null)
                eleBag.itemList = new List<Item>();
            // その Bag にはこの1個だけ入れる
            eleBag.itemList.Clear();
            eleBag.itemList.Add(fromItem);
        }

        Debug.Log("BagFrom.itemList[i] を elements[i].itemList に 1 個ずつコピーしました。");
    }
}