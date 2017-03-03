using UnityEngine;
using System.Collections;

public class PlayerIDOnBoard : MonoBehaviour {
    public int PlayerIDOB;
	// Use this for initialization
	void Start () {
        PlayerIDOB = GameObject.Find("TrunCounter").GetComponent<TurnCounter>().OwnId;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
