using UnityEngine;
using UnityEngine.EventSystems;

public class CursorCheck : TriggerBase, IPointerEnterHandler, IPointerExitHandler
{
    public bool isEnter;

    // カーソルが入った時のアクション
    public void OnPointerEnter(PointerEventData eventData)
    {
        action.Invoke();
        isEnter = true;
    }
    // カーソルが出た時のアクション
    public void OnPointerExit(PointerEventData eventData)
    {
        isEnter = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
