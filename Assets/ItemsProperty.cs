using UnityEngine;
using System.Collections;

public class ItemsProperty : MonoBehaviour {
    public int Player_ID;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerOnBoard"))
        {
            Player_ID = other.GetComponentInParent<PlayerIDOnBoard>().PlayerIDOB;
            this.transform.position = other.transform.parent.transform.position;
        }
    }
}
