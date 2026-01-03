using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class AnimatorTrigger : TriggerBase
{
    [SerializeField] private Animator targetAnimator;
    [SerializeField] private string punchingBoolName = "isPunching";

    // 前フレームの isPunching 値
    private bool _wasPunching = false;

    void Update()
    {
        if (targetAnimator == null)
        {
            return;
        }

        bool animIsPunching = targetAnimator.GetBool(punchingBoolName);

        // false → true になったフレームだけ処理
        if (!_wasPunching && animIsPunching)
        {
            // ★ここで一度だけトリガーされる
            if (action != null)
            {
                action.Invoke();
            }
        }

        _wasPunching = animIsPunching;
    }
}
