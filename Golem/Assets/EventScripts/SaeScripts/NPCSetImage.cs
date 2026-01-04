using UnityEngine;
using UnityEngine.UI;

public class NPCSetImage : MonoBehaviour
{
    [SerializeField] private Bag getMangaBag;
    [SerializeField] private GameObject character;

    public void Action()
    {
        Item item = getMangaBag.itemList[0];
        if (character == null || item?.NPCvisualChange == null) return;

        // UI Imageコンポーネントを取得
        Image characterImage = character.GetComponent<Image>();
        if (characterImage != null)
        {
            characterImage.sprite = item.NPCvisualChange;
            return;
        }
    }
}
