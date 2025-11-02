using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Bag", menuName = "Item/Bag")]
public class Bag : ScriptableObject
{
    public List<Item> itemList = new List<Item>();
}
