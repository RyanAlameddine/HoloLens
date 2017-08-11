using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour {
    Rigidbody rigidBody;
    AudioSource audioSource;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        rigidBody.useGravity = false;
    }

    public void Tap()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.AddRelativeForce(new Vector3(0, 1.6f, 0), ForceMode.VelocityChange);
        audioSource.Play();
    }

    public void FixedUpdate()
    {
        rigidBody.AddRelativeForce(Physics.gravity);
    }
}
