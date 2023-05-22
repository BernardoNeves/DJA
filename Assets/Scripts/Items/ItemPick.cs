using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemPick : MonoBehaviour
{
    private Item Item;
    public ItemData itemData;

    private void Start()
    {
        Item = AssignItem();
    }

    public void Pick()
    {
        Destroy(gameObject);
        bool newItem = true;
        foreach (ItemStack i in GameManager.instance._player.GetComponent<PlayerHealth>().itemList)
        {
            if (i.Name == itemData.itemName)
            {
                i._stacks += 1;
                itemData.itemStacks = i._stacks;
                newItem = false;
                break;
            }
        }
        if (newItem)
        {
            GameManager.instance.Player.GetComponent<PlayerHealth>().itemList.Add(new ItemStack(Item, 1, itemData));
            itemData.itemStacks = 1;
            InventoryManager.Instance.Add(itemData);
        }
        GameManager.instance.Player.GetComponent<PlayerHealth>().CallItemOnPickup();
        InventoryManager.Instance.ListItems();


        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.instance.Player.GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = true;
        GameManager.instance.ChestUI.SetActive(false);

        foreach (Transform item in GameManager.instance.ChestContent)
        {
            Destroy(item.gameObject);
        }
    }

    public Item AssignItem()
    {
        switch (itemData.itemName)
        {
            case "HealingItem":
                return new HealingItem();
            case "MaxHealthItem":
                return new MaxHealthItem();
            case "ShieldRate":
                return new ShieldRate();
            case "MaxShieldItem":
                return new MaxShieldItem();
            case "SpeedItem":
                return new SpeedItem();
            case "JumpItem":
                return new JumpItem();
            case "GravityItem":
                return new GravityItem();
            case "DamageItem":
                return new DamageItem();
            case "LifeStealItem":
                return new LifeStealItem();
            default:
                return null;
        }
    }

}
