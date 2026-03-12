using UnityEngine;
using UnityEngine.Events;

public class BatteryCheck : TriggerBase
{
    [SerializeField] GameObject targetObject; // MoveBarコンポーネントを持つオブジェクトを指定
    [SerializeField] private UnityEvent falseAction; // エネルギーが枯渇していない場合のアクション
    
    private MoveBar moveBar; // MoveBarコンポーネントの参照

    void Start()
    {
        // 指定したオブジェクトからMoveBarコンポーネントを取得
        if (targetObject != null)
        {
            moveBar = targetObject.GetComponent<MoveBar>();
        }
    }

    // Update is called once per frame
    void Action()
    {
        if (moveBar != null && moveBar.IsEnergyDepleted == true) // MoveBarのエネルギーが枯渇していた場合
        {
            action.Invoke();
        }
        else
        {
            falseAction.Invoke();
        }
    }
}
