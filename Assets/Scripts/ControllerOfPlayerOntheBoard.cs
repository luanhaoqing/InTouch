using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ControllerOfPlayerOntheBoard : NetworkBehaviour {
    public GameObject PlayerOnBoard;
    public GameObject detectBall;
    private Vector3 target;
    private bool BeginMove;

    private int controlMode = 1; // 1 = move, 2 = send, 3 = use item, 4 = in menu
    private int previousControlMode = 1;
    private GameObject handUI;
    public int currentHighlight = 0; // UI higlight

    public UnityEngine.UI.Button MoveButton;
    public UnityEngine.UI.Button SendButton;
    public UnityEngine.UI.Button ItemButton;
    public UnityEngine.UI.Button ExitButton;

    public bool menuOpen = false;

    enum Status
    {
        None,
        Move,
        Send,
        Item,
        Exit
    }


    // Use this for initialization
    void Start () {
        PlayerOnBoard.transform.position = new Vector3(0.29f,0.04f,-0.4544f);
        handUI = GameObject.FindGameObjectWithTag("RightHandUI");
        handUI.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.K) && !menuOpen)
        {
            menuOpen = true;
        }

        if (menuOpen)
        {
            handUI.SetActive(true); // UI pops up

            // Highlighting options
            if (Input.GetKeyDown(KeyCode.I)) // choose move
            {
                currentHighlight = (int)Status.Move;
                MoveButton.Select();
                Debug.Log("Move Button Highlighted");
                return;

            }

            if (Input.GetKeyDown(KeyCode.O)) // choose send
            {
                currentHighlight = (int)Status.Send;
                SendButton.Select();
                Debug.Log("Send Button Highlighted");
                return;

            }

            if (Input.GetKeyDown(KeyCode.P)) // choose item
            {
                currentHighlight = (int)Status.Item;
                ItemButton.Select();
                Debug.Log("Item Button Highlighted");
                return;
            }

            if (Input.GetKeyDown(KeyCode.L)) // choose exit
            {
                currentHighlight = (int)Status.Exit;
                ExitButton.Select();
                Debug.Log("Exit Button Highlighted");
                return;

            }

            // After Highlighting -- Make choices

            // exit
            if (currentHighlight == (int)Status.Exit)
            {
                if (Input.GetKeyDown(KeyCode.K))
                {
                    controlMode = previousControlMode;
                    handUI.SetActive(false);
                    menuOpen = false;
                    currentHighlight = (int)Status.None;
                    Debug.Log("Turning off Menu");
                }
            }


        }
        else
        {
            Debug.Log(controlMode);
            switch (controlMode)
            {
                // Control Mode: Move
                case 1:
                    if (GameObject.FindGameObjectWithTag("Turn").GetComponent<CurrentPlayer>().MyTurn && !BeginMove)
                    {
                        //var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
                        //  var z = Input.GetAxis("Vertical") * Time.deltaTime * 0.05f;
                        // Debug.Log("TEST");
                        //  PlayerOnBoard.transform.Rotate(0, x, 0);
                        //  PlayerOnBoard.transform.Translate(0, 0, z);
                        if (Input.GetKeyDown(KeyCode.W) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp) || OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp))
                        {
                            //    Debug.Log("Thumbstick: " + OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp));

                            detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(0.2f, 0, 0);
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
                        if (Input.GetKeyDown(KeyCode.T))
                        {

                        }
                    }
                    if (BeginMove)
                    {
                        float step = 0.1f * Time.deltaTime;
                        PlayerOnBoard.transform.position = Vector3.MoveTowards(PlayerOnBoard.transform.position, target, step);
                        if (PlayerOnBoard.transform.position == target)
                            BeginMove = false;
                    }
                    break;

                // Control mode: Send
                case 2:
                    // enable touch-send control
                    GetComponentInChildren<HandControl>().ActivateTrade(true);
                    // do something to tell players they are in item mode

                    break;

                // Control mode: Use Item
                case 3:
                    break;
            }
        }




/*

            // Control mode: Menu
            case 4:
                ChangeControlMode();
                // Turn on the hand menu! (right hand at the moment)
                handUI.SetActive(true);

                // make choices with highlighting - Keyboard: I,O,P,L , confirm with K

                


                // Exit the hand menu: return to last control mode, deactivate hand UI.

    }
    */
}
    // To turn off anything that needs to be turned off whenever enters menu.
    void ChangeControlMode()
    {
        GetComponentInChildren<HandControl>().ActivateTrade(false);
    }
}
