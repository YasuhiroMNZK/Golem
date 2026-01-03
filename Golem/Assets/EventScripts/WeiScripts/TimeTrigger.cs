using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;

public class TimeTrigger : TriggerBase
{
    [Tooltip("有効化されてから何秒後にイベントを実行するか")]
    [SerializeField] private float duration = 1f;

    [Tooltip("duration 秒経過時に呼ばれるイベント")]

    private float elapsed = 0f;
    private bool isRunning = false;
    private bool hasFired = false;

    private void OnEnable()
    {
        elapsed = 0f;
        isRunning = true;
        hasFired = false;
    }

    private void OnDisable()
    {
        isRunning = false;
    }

    private void Update()
    {
        if (!isRunning || hasFired)
            return;

        elapsed += Time.deltaTime;

        if (elapsed >= duration)
        {
            hasFired = true;
            isRunning = false;


            action.Invoke();
        }
    }
}
