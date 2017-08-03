using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GestureController : MonoBehaviour {

    public GameObject selectedObject { get; private set; }

    public static GameObject cubeSelected;

    GestureRecognizer recognizer;

    public GameObject portal1;
    public GameObject portal2;
    bool portal1Selected = true;

	void Start () {
        recognizer = new GestureRecognizer();

        recognizer.TappedEvent += TapEvent;
        recognizer.StartCapturingGestures();
	}

    private void TapEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        if (selectedObject == null) { return; }
        if(cubeSelected != null)
        {
            cubeSelected.GetComponent<CubeCommand>().selected = false;
            cubeSelected = null;
            return;
        }

        if (selectedObject.tag == "Cube")
        {
            selectedObject.SendMessage("OnSelect");
            cubeSelected = selectedObject;
        }
        else
        {
            RaycastHit hitInfo;
            Vector3 headPosition = Camera.main.transform.position;
            Vector3 gazeDirection = Camera.main.transform.forward;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
            {
                transform.position = hitInfo.point;
                transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                if (portal1Selected)
                {
                    portal1.transform.position = hitInfo.point;
                    portal1.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hitInfo.normal);
                    portal1.transform.position += hitInfo.normal * .15f;
                }
                else
                {
                    portal2.transform.position = hitInfo.point;
                    portal2.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hitInfo.normal);
                }
                portal1Selected = !portal1Selected;
            }
        }
    }

    private void LateUpdate()
    {
        Vector3 headPosition = Camera.main.transform.position;
        Vector3 gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            selectedObject = hitInfo.collider.gameObject;
        }
    }

    private void OnDestroy()
    {
        if(recognizer != null)
        {
            recognizer.StopCapturingGestures();
            recognizer.TappedEvent -= TapEvent;
        }
    }
}
