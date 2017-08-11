using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arcade : MonoBehaviour {
    Transform coin;
    [SerializeField]
    Transform Left;
    [SerializeField]
    Transform Right;
    float x = -.4f;
    public string tokenKey;
    [SerializeField]
    ScoreLoader tokenLoader;
    Vector3 ogPosition;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Cube")
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            coin = other.transform;
            ogPosition = coin.position;
            coin.GetComponent<ISelectable>().Select();
            coin.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    private void Update()
    {
        if (!coin) return;
        x += Time.deltaTime * .9f;
        if (x < 0) return;
        if (x >= 1f)
        {
            Destroy(coin.gameObject);
            coin = null;
            PlayerPrefs.SetInt(tokenKey, PlayerPrefs.GetInt(tokenKey) + 1);
            tokenLoader.Load();
            x = -.4f;
            return;
        }
        if(Vector3.Distance(Left.position, coin.position) < .2)
        {
            coin.position = Vector3.Lerp(ogPosition, Left.position, x);
        }
        else
        {
            coin.position = Vector3.Lerp(ogPosition, Right.position, x);
        }
    }
}
