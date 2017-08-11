using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VR.WSA.Input;
using UnityEngine.Windows.Speech;

public class GameManager : MonoBehaviour
{
    GestureRecognizer recognizer;
    KeywordRecognizer voiceRecognizer;

    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    [SerializeField]
    PlayerController player;
    [SerializeField]
    GameObject Screen;
    [SerializeField]
    PipeGenerator generator;
    [SerializeField]
    Text scoringText;
    bool started = false;
    int score = 0;

    private void Start()
    {
        recognizer = new GestureRecognizer();

        Reset();
        recognizer.TappedEvent += TapEvent;
        recognizer.StartCapturingGestures();

        keywords.Add("kill", () =>
        {
            Reset();
        });
        keywords.Add("teleport", keywords["kill"]);
        keywords.Add("reset", () =>
        {
            SceneManager.LoadScene(2);
        });
        keywords.Add("menu", () =>
        {
            SceneManager.LoadScene(0);
        });
        voiceRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        voiceRecognizer.OnPhraseRecognized += PhraseRecognized;
        voiceRecognizer.Start();
    }

    private void Reset()
    {
        started = false;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.gameObject.SetActive(false);
        player.transform.localPosition = new Vector3(0, 0, 0);
        generator.enabled = false;
        generator.Setup();
        score = 0;
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
            scoringText.text = score + "";
            return;
        }
        player.Tap();
    }

    public void End()
    {
        if (PlayerPrefs.GetInt("Flappy") < score)
        {
            PlayerPrefs.SetInt("Flappy", score);
            PlayerPrefs.Save();
        }
        Reset();
    }

    public void Score()
    {
        score++;
        scoringText.text = score + "";
    }

    private void PhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction?.Invoke();
        }
    }

    private void OnDestroy()
    {
        voiceRecognizer?.Stop();
        recognizer?.StopCapturingGestures();
    }
}
