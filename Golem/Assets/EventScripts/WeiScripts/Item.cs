using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "Manga", menuName = "Item/Manga")]
public class Item : ScriptableObject
{
    public string mangaName;
    public Sprite mangaCover;
    [TextArea]
    public string mangaInfo;

    [TextArea]
    public string mangaText;
}
