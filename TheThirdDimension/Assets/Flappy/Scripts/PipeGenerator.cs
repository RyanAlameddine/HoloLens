using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGenerator : MonoBehaviour {
    List<GameObject> pipes = new List<GameObject>();

    public void Start()
    {
        for (int x = 0; x < transform.childCount; x++)
        {
            Transform child = transform.GetChild(x);
            pipes.Add(child.gameObject);
        }
    }

    public void Setup () {
        for(int x = 0; x < transform.childCount; x++)
        {
            Transform child = transform.GetChild(x);
            child.localPosition = new Vector3(5 + x*1.25f, Random.Range(-.5f, .5f), child.localPosition.z);
        }
	}
	
	void Update () {
        foreach (GameObject obj in pipes)
        {
            obj.transform.localPosition = obj.transform.localPosition - new Vector3(1 * Time.deltaTime, 0, 0);
            if(obj.transform.localPosition.x <= -5)
            {
                obj.transform.localPosition = new Vector3(5, Random.Range(-.5f, .5f), 0);
            }
        }
    }
}
