using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SpreadSheetReader : MonoBehaviour
{
    [SerializeField] private Text displayText;
    [SerializeField] private Button[] areaButtons; // 領域選択用のボタン配列
    
    private string _sheetID = "1Ji8KRU3-VKK5mIyJZRZcLkVExqXL2w-inEcoVwEBOpQ";
    private string _sheetName = "TestSheet";
    
    // 各ボタンに対応する範囲設定
    private string[] cellRanges = {
        "A1:C1",   // ボタン1の範囲
        "A2:C2",   // ボタン2の範囲
        "A3:C3",  // ボタン3の範囲
    };

    void Start()
    {
        // ボタンにイベントを設定
        for (int i = 0; i < areaButtons.Length; i++)
        {
            int index = i; // クロージャ対策
            areaButtons[i].onClick.AddListener(() => LoadSpecificArea(index));
        }
        
        // 初期表示を削除（ボタンを押すまで何も表示しない）
        // LoadSpecificArea(0); // この行をコメントアウトまたは削除
        
        // displayTextを空にする
        if (displayText != null)
        {
            displayText.text = "";
        }
    }

    public void LoadSpecificArea(int areaIndex)
    {
        if (areaIndex >= 0 && areaIndex < cellRanges.Length)
        {
            StartCoroutine(LoadSpreadSheetRange(_sheetID, _sheetName, cellRanges[areaIndex]));
        }
    }

    public IEnumerator LoadSpreadSheetRange(string id, string name, string range)
    {
        Debug.Log($"読み込み開始 - 範囲: {range}");
        
        // 範囲指定のURL
        string url = $"https://docs.google.com/spreadsheets/d/{id}/gviz/tq?tqx=out:csv&sheet={name}&range={range}";
        
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
            if (displayText != null)
            {
                displayText.text = "エラー: " + request.error;
            }
        }
        else
        {
            string rawText = request.downloadHandler.text;
            // 引用符を削除
            string cleanedText = rawText.Replace("\"", "");
            Debug.Log("<<指定範囲>>\n" + cleanedText);
            if (displayText != null)
            {
                displayText.text = cleanedText;
            }
        }
        
        request.Dispose();
    }

    // 元のメソッドも残しておく（全体読み込み用）
    public IEnumerator LoadSpreadSheet(string id, string name)
    {
        Debug.Log("読み込み開始");
        
        UnityWebRequest request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/" + id + "/gviz/tq?tqx=out:csv&sheet=" + name);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
            if (displayText != null)
            {
                displayText.text = "エラー: " + request.error;
            }
        }
        else
        {
            Debug.Log("<<全文>>\n" + request.downloadHandler.text);
            if (displayText != null)
            {
                displayText.text = request.downloadHandler.text;
            }
        }

        request.Dispose();
    }
}
