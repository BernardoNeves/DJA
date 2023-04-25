using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class ItemData : ScriptableObject
{

    [Header("Info")]
    public int id;
    public new string itemName;
    public Sprite itemIcon;
    public Mesh itemMesh;
    public int dropChance;
}