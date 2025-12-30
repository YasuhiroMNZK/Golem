using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class TextCheck : TriggerBase
{
    // 監視対象の Text。UI.Text か TMP_Text のどちらかを設定してください。
    [Header("Target Text")]
    public Text uiText;              // UGUI Text
    public TMP_Text tmpText;         // TextMeshPro Text

    [Header("Events")]
    public UnityEvent onTextNotEmpty;    // テキストに内容があるとき発火
    public UnityEvent onTextEmpty;       // テキストが空のとき発火

    // 前フレームの「空 or 非空」を保存
    private bool _wasEmpty = true;

    void Update()
    {
        string currentText = GetCurrentText();
        bool isEmpty = string.IsNullOrEmpty(currentText);

        // 状態が変わったときだけイベントを発火
        if (isEmpty != _wasEmpty)
        {
            if (isEmpty)
            {
                // 空になった → 空用イベント
                if (onTextEmpty != null)
                    onTextEmpty.Invoke();
                // TriggerBase に専用メソッドがあるならここで呼ぶ
                // e.g. TriggerEmpty();
            }
            else
            {
                // 内容が入った → 非空用イベント
                if (onTextNotEmpty != null)
                    onTextNotEmpty.Invoke();
                // e.g. TriggerNotEmpty();
            }

            _wasEmpty = isEmpty;
        }
    }

    private string GetCurrentText()
    {
        if (tmpText != null)
            return tmpText.text;
        if (uiText != null)
            return uiText.text;
        return string.Empty;
    }
}
