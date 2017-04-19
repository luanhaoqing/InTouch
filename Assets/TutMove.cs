using UnityEngine;
using System.Collections;

public class TutMove : MonoBehaviour {
    public bool BeginMove;
    public GameObject PlayerModel;
    public GameObject detectBall;
    public GameObject PlayerOnBoard;
    private Vector3 target;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if ( !BeginMove)
        {

            if ((Input.GetKeyDown(KeyCode.W)
                || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp)
                || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickUp))
                )
            {

                PlayerModel.transform.LookAt(PlayerOnBoard.transform.position + new Vector3(0.2f, 0, 0));
                detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(0.2f, 0, 0);
            }
            
            if ((Input.GetKeyDown(KeyCode.S)
                || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown)
                || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickDown))
               )
            {
                //    Debug.Log("w");
                PlayerModel.transform.LookAt(PlayerOnBoard.transform.position + new Vector3(-0.2f, 0, 0));
                detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(-0.2f, 0, 0);
            }
            
            if ((Input.GetKeyDown(KeyCode.A)
                || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft)
                || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickLeft))
               )
            {
                PlayerModel.transform.LookAt(PlayerOnBoard.transform.position + new Vector3(0, 0, 0.2f));
                detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(0, 0, 0.2f);
            }
           
            if ((Input.GetKeyDown(KeyCode.D)
                || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight)
                || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickRight))
                )
            {
                //   Debug.Log("w");
                PlayerModel.transform.LookAt(PlayerOnBoard.transform.position + new Vector3(0, 0, -0.2f));
                detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(0, 0, -0.2f);
            }
          
            if (Input.GetKeyDown(KeyCode.G)
                || OVRInput.GetDown(OVRInput.Button.One)
                || OVRInput.GetDown(OVRInput.Button.Three)
                || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick)
                || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick))
            {
                if (detectBall.GetComponent<TutDetectBall>().IfCouldMove())
                {
                    //  Debug.Log("w");
                    BeginMove = true;
                    target = detectBall.transform.position;
      
                }
            }
           
        }
        if (BeginMove)
        {
            float step = 0.1f * Time.deltaTime;
            PlayerOnBoard.transform.position = Vector3.MoveTowards(PlayerOnBoard.transform.position, target, step);
            if (PlayerOnBoard.transform.position == target)
                BeginMove = false;
        }
    }
}
