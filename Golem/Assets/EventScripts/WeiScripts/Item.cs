using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "Manga", menuName = "Item/Manga")]
public class Item : ScriptableObject
{
    [SerializeField] private string saveKey;

    // セーブ時の識別子（未設定ならアセット名）
    public string GetSaveKey()
    {
        return string.IsNullOrEmpty(saveKey) ? name : saveKey;
    }

    public string mangaName;
    public Sprite mangaCover;
    [TextArea]
    public string mangaInfo;

    [TextArea]
    public string mangaLog;
    [TextArea]
    public string mangaExplain;

    public Sprite NPCvisualChange;

    public AnimationClip NPCanimation;

    [TextArea]
    public string NPCText;
}
