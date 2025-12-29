using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameInput : MonoBehaviour
{
    public Item mangaItem;

    // このスクリプトが載っているテキストボックスに付いている Input コンポーネント
    [SerializeField] private InputField nameInputField;       // uGUI 用
    [SerializeField] private TMP_InputField nameTMPInputField; // TextMeshPro 用

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // UIボタンなどから呼ぶ：現在のテキストを mangaItem.MangaName に反映
    public void ApplyNameToMangaItem()
    {
        if (mangaItem == null)
        {
            Debug.LogWarning("NameInput: mangaItem が設定されていません。");
            return;
        }

        string text = null;

        // TextMeshPro が優先
        if (nameTMPInputField != null)
        {
            text = nameTMPInputField.text;
        }
        else if (nameInputField != null)
        {
            text = nameInputField.text;
        }
        else
        {
            // このスクリプトの GameObject から自動取得を試みる
            nameTMPInputField = GetComponent<TMP_InputField>();
            nameInputField    = GetComponent<InputField>();

            if (nameTMPInputField != null)
            {
                text = nameTMPInputField.text;
            }
            else if (nameInputField != null)
            {
                text = nameInputField.text;
            }
            else
            {
                Debug.LogWarning("NameInput: InputField / TMP_InputField が見つかりません。");
                return;
            }
        }

        // mangaItem の MangaName に代入（プロパティ名は実際の Item クラスに合わせて変更）
        mangaItem.mangaName = text;
    }
}
