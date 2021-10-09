using System.Collections;
using System;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class PickUp : MonoBehaviour
{
    public Int32 value;

    void Awake()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.size = new Vector3(2, 2, 3);
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider>().tag = "PickUp";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual IEnumerator ApplyEffect()
    {
        return null;
    }
}
