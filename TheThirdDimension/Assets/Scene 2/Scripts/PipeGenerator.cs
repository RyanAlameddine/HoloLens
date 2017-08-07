using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGenerator : MonoBehaviour {
    List<GameObject> pipes = new List<GameObject>();
    

	void Start () {
        for(int x = 0; x < transform.childCount; x++)
        {
            Transform child = transform.GetChild(x);
            pipes.Add(child.gameObject);
            child.localPosition = new Vector3(5 + x*1.25f, child.localPosition.y, child.localPosition.z);
        }
	}
	
	void Update () {
        foreach (GameObject obj in pipes)
        {
            obj.transform.position = obj.transform.position - new Vector3(1 * Time.deltaTime, 0, 0);
            if(obj.transform.position.x <= -5)
            {
                obj.transform.localPosition = new Vector3(5, Random.Range(-.5f, .5f), 0);
            }
        }
    }
}
