using System.Collections; //コルーチン（実行を一時停止し、任意のタイミングで中断した場所から処理を再開できる特殊な関数）を使用するため
using System.Collections.Generic; //リストを使用するため
using UnityEngine; //Unityの基本機能を使用するため
using UnityEngine.Networking; //Webリクエストを使用するため
using UnityEngine.UI; //UI要素を使用するため

public class SpreadSheetReader : MonoBehaviour
{
    [SerializeField] private Text displayText; // 読み込んだデータを表示するUIテキスト
    [SerializeField] private Button[] areaButtons; // 領域選択用のボタン配列
    
    private string _sheetID = "1Ji8KRU3-VKK5mIyJZRZcLkVExqXL2w-inEcoVwEBOpQ"; // スプレッドシートのID
    private string _sheetName = "TestSheet"; // スプレッドシートのシート名
    
    // 各ボタンに対応する範囲設定
    [SerializeField] private string[] cellRanges = {
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
            areaButtons[i].onClick.AddListener(() => LoadSpecificArea(index)); // ボタンが押されたときに対応する範囲を読み込む
        }
        
        // 初期表示
        // LoadSpecificArea(0);
        
        // displayTextを空にする
        if (displayText != null)
        {
            displayText.text = "";
        }
    }

    // 指定された範囲を読み込むメソッド
    public void LoadSpecificArea(int areaIndex)
    {
        if (areaIndex >= 0 && areaIndex < cellRanges.Length)
        {
            StartCoroutine(LoadSpreadSheetRange(_sheetID, _sheetName, cellRanges[areaIndex]));
        }
    }

    // 指定範囲を読み込むコルーチン
    public IEnumerator LoadSpreadSheetRange(string id, string name, string range)
    {
        Debug.Log($"読み込み開始 - 範囲: {range}"); // 読み込み開始ログ

        // 範囲指定のURL
        string url = $"https://docs.google.com/spreadsheets/d/{id}/gviz/tq?tqx=out:csv&sheet={name}&range={range}";

        UnityWebRequest request = UnityWebRequest.Get(url); // 指定範囲のデータを取得
        yield return request.SendWebRequest(); // リクエスト送信と待機

        // エラーチェック
        if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
            if (displayText != null)
            {
                displayText.text = "エラー: " + request.error;
            }
        }
        else // 成功時の処理
        {
            string rawText = request.downloadHandler.text; // 生データ取得

            // 引用符を削除
            string cleanedText = rawText.Replace("\"", "");

            Debug.Log("<<指定範囲>>\n" + cleanedText);
            if (displayText != null)
            {
                displayText.text = cleanedText;
            }
        }

        request.Dispose(); // リクエストの破棄
    }

    // 元のメソッドも残しておく（全体読み込み用）
//    public IEnumerator LoadSpreadSheet(string id, string name)
//    {
//        Debug.Log("読み込み開始");
//        
//        UnityWebRequest request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/" + id + "/gviz/tq?tqx=out:csv&sheet=" + name);
//        yield return request.SendWebRequest();
//
//        if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError)
//        {
//            Debug.Log(request.error);
//            if (displayText != null)
//            {
//                displayText.text = "エラー: " + request.error;
//            }
//        }
//        else
//        {
//            Debug.Log("<<全文>>\n" + request.downloadHandler.text);
//           if (displayText != null)
//            {
//                displayText.text = request.downloadHandler.text;
//            }
//        }
//
//        request.Dispose();
//    }
}
