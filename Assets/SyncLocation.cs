using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncLocation : NetworkBehaviour {

    public GameObject LeftHand, RightHand, Body, Player;
    public GameObject _LeftHand, _RightHand, _Body;
    private Vector3 PositionDelta;
	// Use this for initialization
	void Start () {
        LeftHand = GameObject.FindGameObjectWithTag("LH");
        RightHand = GameObject.FindGameObjectWithTag("RH");
        Body = GameObject.FindGameObjectWithTag("BODY");
        Player = GameObject.FindGameObjectWithTag("Oculus");
        PositionDelta = Player.transform.position - this.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        } else
        {
            _LeftHand.transform.position = LeftHand.transform.position- PositionDelta;
            _LeftHand.transform.eulerAngles = LeftHand.transform.eulerAngles;
            _RightHand.transform.position = RightHand.transform.position - PositionDelta;
            _RightHand.transform.eulerAngles = RightHand.transform.eulerAngles;
            _Body.transform.position = Body.transform.position - PositionDelta;
            _Body.transform.eulerAngles = Body.transform.eulerAngles;

        }
    }
}
