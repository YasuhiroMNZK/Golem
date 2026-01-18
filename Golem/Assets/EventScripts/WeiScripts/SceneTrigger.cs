using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneTrigger : TriggerBase
{
    // 判定対象となるシーン名（インスペクタで設定）
    [SerializeField]
    private List<string> targetSceneNames = new List<string>();

    // シーン名が targetSceneNames のいずれかと一致したときに実行


    // シーン名が一致しなかったときに実行
    [SerializeField]
    private UnityEvent NotMatchAction;



    void Update()
    {
        
        CheckSceneAndInvoke();
    }

    /// <summary>
    /// 現在のシーン名を判定して、それぞれの UnityEvent を呼ぶ
    /// </summary>
    private void CheckSceneAndInvoke()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (targetSceneNames != null && targetSceneNames.Contains(currentSceneName))
        {
            action.Invoke();;
        }
        else
        {
            NotMatchAction?.Invoke();
        }
    }
}
