using UnityEngine;
using System.Collections;

public class OpenParticle : MonoBehaviour {
    public GameObject loseParticle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.localScale.x > 0.01)
            loseParticle.SetActive(true);
	}
}
