using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreLoader : MonoBehaviour {
    Text text;
    [SerializeField]
    string message = "HighScore: ";
    [SerializeField]
    string key;

	void Start () {
        text = GetComponent<Text>();
        Load();
	}

    public void Load()
    {
        text.text = message + PlayerPrefs.GetInt(key);
    }
}
