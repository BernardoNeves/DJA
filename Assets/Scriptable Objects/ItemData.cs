using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemData : ScriptableObject
{

    [Header("Info")]
    public string itemName;
    public Sprite itemIcon;
    public int itemDropChance;
    public int itemStacks;
}