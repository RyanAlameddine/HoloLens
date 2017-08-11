using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VR.WSA.Input;
using UnityEngine.Windows.Speech;

public class TapRecorder : MonoBehaviour {

    GestureRecognizer recognizer;
    KeywordRecognizer speech;

    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    void Start () {
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += TapEvent;
        recognizer.StartCapturingGestures();


        keywords.Add("menu", () =>
        {
            SceneManager.LoadScene(0);
        });
        speech = new KeywordRecognizer(keywords.Keys.ToArray());
        speech.OnPhraseRecognized += PhraseRecognized;
        speech.Start();
    }

    private void PhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction?.Invoke();
        }
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
        speech.Stop();
    }
}
