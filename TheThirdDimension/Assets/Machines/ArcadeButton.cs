using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeButton : MonoBehaviour, ISelectable
{
    Vector3 ogPosition;
    bool hovering = false;
    [SerializeField]
    int scene;

    BoxCollider collider;

    private void Start()
    {
        ogPosition = transform.localPosition;
        collider = GetComponent<BoxCollider>();
    }

    public void Hover()
    {
        hovering = true;
    }

    private void Update()
    {
        if (hovering)
        {
            transform.localPosition = new Vector3(0, -.01f, 0);
        }
        else
        {
            transform.localPosition = ogPosition;
        }
        hovering = false;
    }

    public void Select()
    {
        SceneManager.LoadScene(scene);
    }
}
