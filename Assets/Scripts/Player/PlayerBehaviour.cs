using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerBehaviour : MonoBehaviour
{

    [SerializeField] private Transform PlayerCamera;
    public StarterAssetsInputs _input;
    public static Action shootInput;
    public static Action reloadInput;


    private void Start()
    {
        _input = transform.root.GetComponent<StarterAssetsInputs>();

    }

    void Update()
    {
        if (_input.interact)
        {
            Interact();
        }
        if (_input.shoot)
        {
            shootInput?.Invoke();
        }


        if (_input.reload)
        {
            reloadInput?.Invoke();
        }
    }
    

    public void Interact() {

        var ray = new Ray(PlayerCamera.position, PlayerCamera.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 3))
        {
            InteractableInterface interactableInterface = hitInfo.transform.GetComponent<InteractableInterface>();
            interactableInterface?.Interact();

        } 
    }
}
