using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Coin : MonoBehaviour, ISelectable {
    [SerializeField]
    bool selected = false;
    public Transform holdPosition;
    Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Hover()
    {
        
    }

    public void Select()
    {
        selected = !selected;
    }

    private void Update()
    {
        if (selected)
        {
            rigidBody.useGravity = false;
            rigidBody.AddForce(new Vector3(holdPosition.position.x - transform.position.x, holdPosition.position.y - transform.position.y, holdPosition.position.z - transform.position.z), ForceMode.VelocityChange);
            rigidBody.drag = 5;
            transform.localRotation = Quaternion.LookRotation(holdPosition.transform.forward, holdPosition.transform.up);
        }
        else
        {
            rigidBody.useGravity = true;
            rigidBody.drag = .5f;
        }
    }
}
