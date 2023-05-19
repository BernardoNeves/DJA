using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chestOpen : MonoBehaviour, InteractableInterface
{
    //public void Interact()
    //{
    //    GetComponent<ItemDrop>().InstantiateDrop(transform.position);
    //    Destroy(gameObject);
    //}
    public List<ItemData> Drops = new List<ItemData>();
    //public Transform Content = GameManager.instance.ChestContent;
    public GameObject ChestItemPrefab;


    List<ItemData> GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<ItemData> DroppedItems = new List<ItemData>();

        List<ItemData> possibleItems = new List<ItemData>();
        foreach (ItemData item in Drops)
        {
            if (randomNumber <= item.itemDropChance)
            {
                possibleItems.Add(item);
            }
        }
        while (possibleItems.Count > 0 && DroppedItems.Count < 3)
        {
            ItemData droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            possibleItems.Remove(droppedItem);
            DroppedItems.Add(droppedItem);
        }
        return DroppedItems;
    }

    public void Interact()
    {
        List<ItemData> CurrentChestItems = GetDroppedItem();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        GameManager.instance.Player.GetComponent<StarterAssets.StarterAssetsInputs>().cursorInputForLook = false;
        Time.timeScale = 0f;
        GameManager.instance.ChestUI.SetActive(true);
        ListItems(CurrentChestItems);

        Destroy(gameObject);
    }

    public void ListItems(List<ItemData> Items)
    {
        foreach (Transform item in GameManager.instance.ChestContent)
        {
            Destroy(item.gameObject);
        }

        foreach (ItemData item in Items)
        {
            GameObject obj = Instantiate(ChestItemPrefab, GameManager.instance.ChestContent);
            obj.GetComponent<ItemPick>().itemData = item;

            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemStacks = obj.transform.Find("ItemStacks").GetComponent<Text>();
            var itemDescription = obj.transform.Find("ItemDescription").GetComponent<Text>();


            itemName.text = item.itemName;
            itemIcon.sprite = item.itemIcon;
            itemStacks.text = "Current Stacks: " + item.itemStacks.ToString();
            itemDescription.text = item.itemDescription;
        }
    }
}