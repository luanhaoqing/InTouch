using UnityEngine;
using System.Collections;

public class EventPropoty : MonoBehaviour {
    public GameObject turnCounter;
	// Use this for initialization
	void Start () {
        turnCounter = GameObject.Find("TrunCounter");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerOnBoard"))
        {
            Debug.Log("EVENT TRIGGER:AP TO 0");
            if(turnCounter.GetComponent<CurrentPlayer>().RemainActionPoint!=3)
                turnCounter.GetComponent<CurrentPlayer>().RemainActionPoint = 0;
            this.gameObject.SetActive(false);
        }
    }
}
