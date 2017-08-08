using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class TapRecorder : MonoBehaviour {

    GestureRecognizer recognizer;

	void Start () {
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += TapEvent;
        recognizer.StartCapturingGestures();
	}

    private void TapEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        RaycastHit hitInfo;
        Vector3 headPosition = Camera.main.transform.position;
        Vector3 gazeDirection = Camera.main.transform.forward;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            GameObject obj = hitInfo.collider.gameObject;
            ISelectable selectable = obj.GetComponent<ISelectable>();
            if (selectable != null)
            {
                selectable.Select();
            }
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hitInfo;
        Vector3 headPosition = Camera.main.transform.position;
        Vector3 gazeDirection = Camera.main.transform.forward;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            GameObject obj = hitInfo.collider.gameObject;
            ISelectable selectable = obj.GetComponent<ISelectable>();
            if (selectable != null)
            {
                selectable.Hover();
            }
        }
    }

    private void OnDestroy()
    {
        recognizer.StopCapturingGestures();
    }
}
