using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
    [SerializeField]
    GameObject Linked;

    public List<GameObject> disabled = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (disabled.Contains(other.gameObject)) return;
        if (other.gameObject.tag != "Cube") return;
        other.gameObject.GetComponent<CubeCommand>().selected = false;

        Linked.GetComponent<Portal>().disabled.Add(other.gameObject);
        other.isTrigger = true;
        other.gameObject.transform.position = Linked.transform.position;

        Rigidbody rigidBody = other.gameObject.GetComponent<Rigidbody>();
        Vector3 velocity = rigidBody.velocity;
        //other.gameObject.transform.rotation = Quaternion.LookRotation(velocity, Linked.transform.up);

        Quaternion rotation = transform.rotation * Linked.transform.rotation;
        rotation = Quaternion.Inverse(rotation /** Quaternion.Euler(180, 0, 0)*/);
        Vector3 force =  /*Vector3.Reflect(*/rotation * velocity * 100/*, Vector3.up)*/;
        while(force.magnitude < 500)
        {
            force = force * 1.1f;
        }
        rigidBody.AddForce(force);
    }

    private void OnTriggerExit(Collider other)
    {
        if (disabled.Contains(other.gameObject))
        {
            disabled.Remove(other.gameObject);
            other.isTrigger = false;
        }
    }
}
