using UnityEngine;
using UnityEngine.UI;

public class Cancel : MonoBehaviour
{
    [SerializeField] private ImageTransfer imageTransfer; // ImageTransferスクリプトの参照
    [SerializeField] private Image[] targetImages; // 下段のImage配列
    [SerializeField] private Bag decisionBag; // 決定用のBag
    
    // targetImageとDecisionBagをリセットするメソッド
    public void ResetDecision()
    {
        // targetImagesを非表示にしてspriteをクリア
        for (int i = 0; i < targetImages.Length; i++)
        {
            targetImages[i].gameObject.SetActive(false);
            targetImages[i].sprite = null;
        }
        
        // DecisionBagの中身を全削除
        if (decisionBag != null && decisionBag.itemList != null)
        {
            decisionBag.itemList.Clear();
        }
        
        // ImageTransferの状態もリセット
        if (imageTransfer != null)
        {
            imageTransfer.ResetImageTransfer();
        }
        
        Debug.Log("決定内容をリセットしました");
    }
}
