using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestOpen : MonoBehaviour, InteractableInterface
{
    public void Interact()
    {
        GetComponent<ItemDrop>().InstantiateDrop(transform.position);
        Destroy(gameObject);
    }
}