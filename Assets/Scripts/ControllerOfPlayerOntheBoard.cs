using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ControllerOfPlayerOntheBoard : NetworkBehaviour {
    public GameObject PlayerOnBoard;
	// Use this for initialization
	void Start () {
        PlayerOnBoard.transform.position = new Vector3(0.2606f,0.02f,-0.5f);
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;
        if (GameObject.FindGameObjectWithTag("Turn").GetComponent<CurrentPlayer>().MyTurn)
        {
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
            var z = Input.GetAxis("Vertical") * Time.deltaTime * 0.05f;
            // Debug.Log("TEST");
            PlayerOnBoard.transform.Rotate(0, x, 0);
            PlayerOnBoard.transform.Translate(0, 0, z);
        }


    }
}
