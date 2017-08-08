using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeCommand : MonoBehaviour
{
    Transform holdSpot;

    Rigidbody rigidBody;
    [SerializeField]
    MeshRenderer cursor;
    [SerializeField]
    LineRenderer lightning1;
    [SerializeField]
    LineRenderer lightning2;

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
            cursor.enabled = false;
            lightning1.enabled = true;
            lightning2.enabled = true;
        }
        else
        {
            rigidBody.useGravity = true;
            rigidBody.drag = .5f;
            cursor.enabled = true;
            lightning1.enabled = false;
            lightning2.enabled = false;
        }
#if UNITY_EDITOR
        if (transform.position.y < -100)
        {
            selected = true;
            GestureController.cubeSelected = gameObject;
        }
#endif
    }
}
