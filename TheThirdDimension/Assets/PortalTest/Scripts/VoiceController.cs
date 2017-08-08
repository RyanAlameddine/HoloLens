using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class VoiceController : MonoBehaviour {

    KeywordRecognizer recognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

	void Start () {
        keywords.Add("select", () =>
        {
            if (GestureController.cubeSelected)
            {
                GestureController.cubeSelected.GetComponent<CubeCommand>().OnSelect();
            }
        });
        keywords.Add("drop", keywords["select"]);
        keywords.Add("find cube", () =>
        {
            GameObject.FindGameObjectWithTag("Cube").GetComponent<CubeCommand>().OnSelect();
        });
        keywords.Add("reset", () =>
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        });
        keywords.Add("teleport", () =>
        {
            GameObject.FindGameObjectWithTag("Cube").transform.position = Camera.main.transform.position;
        });
        keywords.Add("menu", () =>
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        });
        recognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        recognizer.OnPhraseRecognized += PhraseRecognized;
        recognizer.Start();
	}

    private void PhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if(keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction?.Invoke();
        }
    }

    private void OnDestroy()
    {
        recognizer?.Stop();
        //recognizer.OnPhraseRecognized -= PhraseRecognized;
    }
}
