using UnityEngine;

public class ClearBagMulti : MonoBehaviour
{
    [SerializeField] private Bag[] targetBags; // 消去対象のBagの配列

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearBags()
    {
        if (targetBags != null)
        {
            foreach (var bag in targetBags)
            {
                if (bag.itemList != null)
                {
                    bag.itemList.Clear();
                }
            }
        }
    }
}
