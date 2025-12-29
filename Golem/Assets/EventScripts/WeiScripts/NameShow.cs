using UnityEngine;
using UnityEngine.UI;

public class NameShow : MonoBehaviour
{
    [SerializeField] private Item targetItem;
    [SerializeField] private Text nameText;

    // もとのテンプレート文字列を保持しておく
    private string templateText;

    void Awake()
    {
        if (nameText != null)
        {
            // 起動時の Text 内容をテンプレートとして保存
            templateText = nameText.text;
        }
    }

    void Start()
    {
        UpdateNameText();
    }

    public void UpdateNameText()
    {
        if (nameText == null)
        {
            Debug.LogWarning("NameShow: nameText が設定されていません。");
            return;
        }

        if (string.IsNullOrEmpty(templateText))
        {
            // テンプレート未設定なら現在の text を使う
            templateText = nameText.text;
        }

        if (targetItem == null)
        {
            // Item がないときはプレースホルダーを空文字に
            nameText.text = templateText.Replace("{name}", "");
            Debug.LogWarning("NameShow: targetItem が設定されていません。");
            return;
        }

        // {name} を mangaName で置き換える
        nameText.text = templateText.Replace("{name}", targetItem.mangaName);
    }

    public void SetItem(Item item)
    {
        targetItem = item;
        UpdateNameText();
    }
}
