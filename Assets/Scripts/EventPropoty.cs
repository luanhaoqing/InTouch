using UnityEngine;
using System.Collections;

public class EventPropoty : MonoBehaviour {
    public GameObject turnCounter;
    public GameObject EventAnimationManager;
    public GameObject body;
	// Use this for initialization
	void Start () {
        turnCounter = GameObject.Find("TrunCounter");
        EventAnimationManager = GameObject.FindGameObjectWithTag("EventAnimationManager");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerOnBoard"))
        {
            Debug.Log("EVENT TRIGGER:AP TO 0");
            AudioCenter.PlayEventTrigger();
            if (turnCounter.GetComponent<CurrentPlayer>().RemainActionPoint!=3)
                turnCounter.GetComponent<CurrentPlayer>().RemainActionPoint = 0;
            this.gameObject.SetActive(false);

            //play the event sheet animation

            //1.get the player's camera position
            body = other.GetComponentInParent<SyncLocation>().Body.gameObject;
            Transform targetPosition = body.transform;
            //2.send the position to
            EventAnimationManager.GetComponent<EventSheetMovement>().StartFlying(targetPosition);

        }
    }
}
