using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;
    public ItemData itemData;

    private void Start()
    {
        Item = AssignItem();
    }

    private void Pickup()
    {
        Destroy(gameObject);
        foreach(ItemStack i in GameManager.instance._player.GetComponent<PlayerHealth>().itemList)
        {
            if(i.Name == itemData.itemName)
            {
                i._stacks += 1;
                itemData.itemStacks = i._stacks;
                return;
            }
        }
        GameManager.instance._player.GetComponent<PlayerHealth>().itemList.Add(new ItemStack(Item, 1, itemData));
        itemData.itemStacks = 1;
        InventoryManager.Instance.Add(itemData);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Pickup();
            InventoryManager.Instance.ListItems();
        }
    }

    public Item AssignItem()
    {
        switch (itemData.name)
        {
            case "HealingItem":
                return new HealingItem();
            default:
                return null;
        }
    }

}