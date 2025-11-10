using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InfoText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text targetText; // 表示・非表示を切り替えるテキスト

    // カーソルがボタンに入った時
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetText != null)
        {
            targetText.gameObject.SetActive(true);
        }
    }

    // カーソルがボタンから出た時
    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetText != null)
        {
            targetText.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        // 初期状態では非表示
        if (targetText != null)
        {
            targetText.gameObject.SetActive(false);
        }
    }
}
