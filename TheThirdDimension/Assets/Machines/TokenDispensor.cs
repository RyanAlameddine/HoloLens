using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenDispensor : MonoBehaviour, ISelectable {
    [SerializeField]
    GameObject token;
    [SerializeField]
    Transform holdPosition;
    bool running;
    public void Hover()
    {
        
    }

    public void Select()
    {
        if (!running)
        {
            running = true;
            StartCoroutine(SpawnTokens());
        }
    }

    IEnumerator SpawnTokens()
    {
        for(int x = 0; x < 20; x++)
        {
            GameObject Token = Instantiate(token, transform.position, UnityEngine.Random.rotation).transform.GetChild(0).gameObject;
            Token.transform.position += Vector3.up * .1f;
            Token.GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(-.001f, .001f), 1000, UnityEngine.Random.Range(-.001f, .001f)));
            Token.GetComponent<Coin>().holdPosition = holdPosition;
            yield return new WaitForSeconds(.3f);
        }
        yield return new WaitForSeconds(5f);
        running = false;
    }
}
