using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Pipe : MonoBehaviour {
    [SerializeField]
    GameManager manager;
    [SerializeField]
    bool ScoringTrigger = false;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            audioSource.Play();
            if (ScoringTrigger)
            {
                manager.Score();
            }
            else
            {
                manager.End();
            }
        }
    }
}
