using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class AnimatorTrigger : TriggerBase
{
    [SerializeField] private Animator targetAnimator;
    [SerializeField] private string punchingBoolName = "isPunching";
    [SerializeField] private float requiredPunchingDuration = 1.0f;

    // isPunching が true だった累計時間
    private float _punchingTime = 0f;

    // すでに発火済みかどうか（累計時間に対して1回だけ発火）
    private bool _triggeredForCurrentPunch = false;

    private bool _wasPunching = false;

    void Update()
    {
        if (targetAnimator == null)
        {
            return;
        }

        bool animIsPunching = targetAnimator.GetBool(punchingBoolName);

        // isPunching が true のフレームだけ累計時間を加算（連続ではなく合計）
        if (animIsPunching)
        {
            _punchingTime += Time.deltaTime;

            if (!_triggeredForCurrentPunch && _punchingTime >= requiredPunchingDuration)
            {
                if (action != null)
                {
                    action.Invoke();
                }
                _triggeredForCurrentPunch = true;
            }
        }

        // ※ false になっても _punchingTime はリセットしない
        //   何度も発火させたい場合は、条件に応じてここで
        //   _punchingTime = 0f; と _triggeredForCurrentPunch = false; を行う

        _wasPunching = animIsPunching;
    }
}
