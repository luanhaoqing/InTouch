using UnityEngine;
using System.Collections;

public class RandomPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.transform.position = new Vector3(Random.Range(0,10),0, Random.Range(0, 10));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
