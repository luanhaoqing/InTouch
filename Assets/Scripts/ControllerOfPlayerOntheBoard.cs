using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ControllerOfPlayerOntheBoard : NetworkBehaviour {
    public GameObject PlayerOnBoard;
    public GameObject detectBall;
    private Vector3 target;
    private bool BeginMove;



	// Use this for initialization
	void Start () {
        PlayerOnBoard.transform.position = new Vector3(0.29f,0.04f,-0.4544f);
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;
        if (GameObject.FindGameObjectWithTag("Turn").GetComponent<CurrentPlayer>().MyTurn&&!BeginMove)
        {
            //var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
          //  var z = Input.GetAxis("Vertical") * Time.deltaTime * 0.05f;
            // Debug.Log("TEST");
          //  PlayerOnBoard.transform.Rotate(0, x, 0);
          //  PlayerOnBoard.transform.Translate(0, 0, z);
          if(Input.GetKeyDown(KeyCode.W) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp) || OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp)) 
            {
            //    Debug.Log("Thumbstick: " + OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp));
                
                detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(0.2f,0,0);
            }
            if (Input.GetKeyDown(KeyCode.S) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown) || OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown))
            {
            //    Debug.Log("w");
                detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(-0.2f, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.A) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft) || OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft))
            {
             //   Debug.Log("w");
                detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(0, 0, 0.2f);
            }
            if (Input.GetKeyDown(KeyCode.D) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight) || OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight))
            {
             //   Debug.Log("w");
                detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(0, 0, -0.2f);
            }
            if (Input.GetKeyDown(KeyCode.G) || OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Three))
            {
                if (detectBall.GetComponent<DetectionandHighLight>().IfCouldMove())
                {
                    //  Debug.Log("w");
                    BeginMove = true;
                    target = detectBall.transform.position;
                    detectBall.transform.position = PlayerOnBoard.transform.position;
                }
            }
            if(Input.GetKeyDown(KeyCode.T))
            {
                
            }
        }
        if(BeginMove)
        {
            float step = 0.1f * Time.deltaTime;
            PlayerOnBoard.transform.position = Vector3.MoveTowards(PlayerOnBoard.transform.position, target, step);
            if (PlayerOnBoard.transform.position == target)
                BeginMove = false;
        }


    }

}
