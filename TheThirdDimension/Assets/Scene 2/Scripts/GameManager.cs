using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GameManager : MonoBehaviour
{
    GestureRecognizer recognizer;
    [SerializeField]
    PlayerController player;
    [SerializeField]
    GameObject Screen;
    [SerializeField]
    PipeGenerator generator;
    bool started = false;

    private void Start()
    {
        recognizer = new GestureRecognizer();

        Reset();
        recognizer.TappedEvent += TapEvent;
        recognizer.StartCapturingGestures();
    }

    private void Reset()
    {
        started = false;
        player.gameObject.SetActive(false);
        generator.enabled = false;
    }

    private void TapEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        if(started == false)
        {
            started = true;
            player.gameObject.SetActive(true);
            Screen.SetActive(false);
            transform.SetParent(null);
            generator.enabled = true;
            return;
        }
        player.Tap();
    }
}
