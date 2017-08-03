using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeCommand : MonoBehaviour
{
    Transform holdSpot;

    Rigidbody rigidBody;

    public bool selected = false;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        holdSpot = GameObject.FindGameObjectWithTag("HoldSpot").transform;
    }

    public void OnSelect()
    {
        rigidBody.isKinematic = false;
        selected = !selected;
    }

    private void Update()
    {
        if (selected)
        {
            rigidBody.useGravity = false;
            rigidBody.AddForce(new Vector3(holdSpot.position.x - transform.position.x, holdSpot.position.y - transform.position.y, holdSpot.position.z - transform.position.z), ForceMode.VelocityChange);
            rigidBody.drag = 5;
        }
        else
        {
            rigidBody.useGravity = true;
            rigidBody.drag = .5f;
        }
        if (transform.position.y < -100)
        {
            selected = true;
            GestureController.cubeSelected = gameObject;
        }
    }
}
