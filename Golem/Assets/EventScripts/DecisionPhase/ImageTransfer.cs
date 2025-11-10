using UnityEngine;
using UnityEngine.UI;

public class ImageTransfer : MonoBehaviour
{
    [SerializeField] private Image[] sourceImages; //上段のImage配列
    [SerializeField] private Image[] targetImages; //下段のImage配列

    private int currentIndex = 0; //次に転送する画像のインデックス
    private bool[] usedSourceImages; //上段の画像が使用されたかどうかのフラグ配列

    // ボタンがクリックされたときに呼ばれるメソッド
    public void OnButtonClick(int sourceIndex)
    {
        if (currentIndex < targetImages.Length && sourceIndex < sourceImages.Length)
        {
            // 既に使用された画像かどうかをチェック
            if (usedSourceImages[sourceIndex])
            {
                Debug.Log("この画像は既に使用されています");
                return; // 既に使用された画像なら処理を中断
            }
            // 画像を転送する処理
            targetImages[currentIndex].sprite = sourceImages[sourceIndex].sprite; // 上段の画像を下段にコピー
            targetImages[currentIndex].gameObject.SetActive(true); // targetImageを表示
            usedSourceImages[sourceIndex] = true; // 使用済みフラグを立てる
            currentIndex++; // 次のインデックスに進む
        }
        else
        {
            Debug.Log("すべての枠が埋まりました");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // usedSourceImages配列を初期化
        usedSourceImages = new bool[sourceImages.Length];
        
        // すべてのtargetImagesを初めは非表示にする
        for (int i = 0; i < targetImages.Length; i++)
        {
            targetImages[i].gameObject.SetActive(false);
        }
    }

    // ImageTransferの状態をリセットするメソッド
    public void ResetImageTransfer()
    {
        // currentIndexを0にリセット
        currentIndex = 0;

        // usedSourceImagesをすべてfalseにリセット
        for (int i = 0; i < usedSourceImages.Length; i++)
        {
            usedSourceImages[i] = false;
        }

        // targetImagesを非表示にする
        for (int i = 0; i < targetImages.Length; i++)
        {
            targetImages[i].gameObject.SetActive(false);
            targetImages[i].sprite = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
