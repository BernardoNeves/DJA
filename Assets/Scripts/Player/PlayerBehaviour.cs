using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    [SerializeField] private Transform PlayerCamera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            Interact();
        }
    }

    public void Interact() {

        var ray = new Ray(PlayerCamera.position, PlayerCamera.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 10))
        {
            InteractableInterface interactableInterface = hitInfo.transform.GetComponent<InteractableInterface>();
            interactableInterface?.Interact();

        } 
    }
}
