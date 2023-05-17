using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject dropPrefab;
    public List<ItemData> Drops = new List<ItemData>();

    ItemData GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101); 

        List<ItemData> possibleItems = new List<ItemData>();
        foreach(ItemData item in Drops)
        { 
            if(randomNumber <= item.itemDropChance)
            {
                possibleItems.Add(item);
            }
        }
        if(possibleItems.Count > 0)
        {
            ItemData droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        GameManager.instance.PlayerHealth.Damage(25);
        return null;
    }

    public void InstantiateDrop(Vector3 spawnPosition)
    {
        ItemData droppedItem = GetDroppedItem();
        if(droppedItem != null)
        {
            GameObject dropGameObject = Instantiate(dropPrefab, spawnPosition, Quaternion.identity);
            float dropForce = 500f;
            Vector3 dropDirection = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f));
            dropGameObject.GetComponent<Rigidbody>().AddForce(dropDirection * dropForce, ForceMode.Impulse);
            dropGameObject.GetComponent<ItemPickup>().itemData = droppedItem;
        }
    }
}
