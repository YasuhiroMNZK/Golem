using UnityEngine;

public class MangaHide : MonoBehaviour
{
    [SerializeField] private Bag targetBag;
    [SerializeField] private Item targetItem;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Action();
    }

    // Update is called once per frame
    void Action()
    {
        if(targetBag == null || targetItem == null)
        {
            return;
        }

        //BagにItemが存在するか確認
        if(targetBag.itemList.Contains(targetItem))
        {
            //存在したらオブジェクトを非表示にする
            gameObject.SetActive(false);
        }
    }
}
