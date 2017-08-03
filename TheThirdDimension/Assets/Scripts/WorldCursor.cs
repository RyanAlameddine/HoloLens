using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class WorldCursor : MonoBehaviour {
    MeshRenderer mr;
    RaycastHit hitInfo;
    public RaycastHit HitInfo
    {
        get { return hitInfo; }
    }

    void Start () {
        mr = GetComponent<MeshRenderer>();	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 headPosition = Camera.main.transform.position;
        Vector3 gazeDirection = Camera.main.transform.forward;
        if(Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            mr.enabled = true;
            transform.position = hitInfo.point;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
        else
        {
            mr.enabled = false;
        }
	}
}
