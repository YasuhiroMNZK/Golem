using UnityEngine;

public class BagClear : MonoBehaviour
{
    [SerializeField] private Bag targetBag; // 消去対象のBag

    // 指定したBagの中身を全て消すメソッド
    public void ClearBag()
    {
        if (targetBag != null && targetBag.itemList != null)
        {
            targetBag.itemList.Clear();
            Debug.Log("Bagの中身を全て消去しました");
        }
        else
        {
            Debug.Log("Bagが設定されていません");
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
