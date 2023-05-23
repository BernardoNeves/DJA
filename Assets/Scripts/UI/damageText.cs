using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageText : MonoBehaviour
{
    public float destroyTime = 1.5f;
    private Camera _cam;

    void Start()
    {
        Destroy(gameObject, destroyTime);
        transform.localPosition += new Vector3(Random.Range(-1f, 1f) ,0 ,0);
        _cam = Camera.main;
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
    }
}
