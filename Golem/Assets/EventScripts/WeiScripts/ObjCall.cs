using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjCall : MonoBehaviour
{
    private Bag[] bags; // 用于存储加载的 Bag 实例
    private Item[] mangas; // 用于存储加载的 Item 实例

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        // 从 Resources/Bag 文件夹加载所有 Bag 类型的资源
        bags = Resources.LoadAll<Bag>("Bag");

        // 检查加载的结果
        if (bags.Length > 0)
        {
            Debug.Log($"Loaded {bags.Length} Bags from Resources/Bag folder:");
            foreach (var bag in bags)
            {
                Debug.Log($"Loaded Bag: {bag.name}");
            }
        }
        else
        {
            Debug.LogError("No Bags found in Resources/Bag folder.");
        }

        // 从 Resources/Manga 文件夹加载所有 Item 类型的资源
        mangas = Resources.LoadAll<Item>("Manga");

        // 检查加载的结果
        if (mangas.Length > 0)
        {
            Debug.Log($"Loaded {mangas.Length} Items from Resources/Manga folder:");
            foreach (var manga in mangas)
            {
                Debug.Log($"Loaded Item: {manga.name}");
            }
        }
        else
        {
            Debug.LogError("No Items found in Resources/Manga folder.");
        }
    }
}