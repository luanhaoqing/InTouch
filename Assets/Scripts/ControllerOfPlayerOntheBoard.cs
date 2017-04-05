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
    private GameObject rightHandUI;
    public int currentHighlight = 0; // UI higlight
    public GameObject TurnCounter;
    public bool counterDirection;

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

    // declare five controller mapping: four for choosing menu options, one for confirm.
    private bool controllerMoveMapping;
    private bool controllerSendMapping;
    private bool controllerItemMapping;
    private bool controllerExitMapping;
    private bool controllerClickMapping;

    //
    public GameObject rightHand;
    public GameObject leftHand;

    // Use this for initialization
    void Start () {
        PlayerOnBoard.transform.position = new Vector3(0.29f,0.04f,-0.4544f);
        rightHandUI = GameObject.FindGameObjectWithTag("RightHandUI");
        rightHandUI.SetActive(false);
        TurnCounter = GameObject.Find("TrunCounter");
        if(TurnCounter.GetComponent<TurnCounter>().OwnId==1)
        {
            counterDirection = true;
        }
    }

    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
            return;

        if (TurnCounter.GetComponent<TurnCounter>().OwnId == 1)
        {
            counterDirection = true;
           
        }
        else
            counterDirection = false;
        // Set Controller Mapping, get situation at current frame.
        controllerMoveMapping = (Input.GetKeyDown(KeyCode.I)
                || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft)
                || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickLeft));
        controllerSendMapping = (Input.GetKeyDown(KeyCode.O)
                || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp)
                || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickUp));
        controllerItemMapping = (Input.GetKeyDown(KeyCode.P)
                || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight)
                || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickRight));
        controllerExitMapping = (Input.GetKeyDown(KeyCode.L)
                || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown)
                || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickDown));
        controllerClickMapping = ((Input.GetKeyDown(KeyCode.K)
                    || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick)
                    || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick)));


        // Open Menu whenever it's not on.
        if (controllerClickMapping)
        {
            menuOpen = true;
            Debug.Log("turning ON menu");
        }

        if (menuOpen)
        {
            rightHandUI.SetActive(true); // UI pops up

            // Highlighting options
            if (controllerMoveMapping) // choose move
            {
                currentHighlight = (int)Status.Move;
                MoveButton.Select();
                //Debug.Log("Move Button Highlighted");
                return;

            }

            if (controllerSendMapping) // choose send
            {
                currentHighlight = (int)Status.Send;
                SendButton.Select();
                //Debug.Log("Send Button Highlighted");
                return;

            }

            if (controllerItemMapping) // choose item
            {
                currentHighlight = (int)Status.Item;
                ItemButton.Select();
                //Debug.Log("Item Button Highlighted");
                return;
            }

            if (controllerExitMapping) // choose exit
            {
                currentHighlight = (int)Status.Exit;
                ExitButton.Select();
                //Debug.Log("Exit Button Highlighted");
                return;

            }

            // After Highlighting -- Make choices
            // 1) Set Mode to Move
            if (currentHighlight == (int)Status.Move)
            {
                if (controllerClickMapping)
                {
                    ChangeControlMode();
                    controlMode = 1;
                }
            }
            // 2) Set Mode to Send
            if (currentHighlight == (int)Status.Send)
            {
                if (controllerClickMapping)
                {
                    ChangeControlMode();
                    controlMode = 2;
                }
            }
            // 3) Set Mode to Item
            if (currentHighlight == (int)Status.Item)
            {
                if (controllerClickMapping)
                {
                    ChangeControlMode();
                    controlMode = 3;
                }
            }
            // 4) Exit and return to previous mode
            if (currentHighlight == (int)Status.Exit)
            {

                if (controllerClickMapping)
                {
                    ChangeControlMode();
                    controlMode = previousControlMode;
                }
            }



        }
        else
        {
            //Debug.Log(controlMode);
            switch (controlMode)
            {
                // Control Mode: Move
                // below is all the character movement code.
                case 1:
                    if (GameObject.FindGameObjectWithTag("Turn").GetComponent<CurrentPlayer>().MyTurn && !BeginMove)
                    {
                        //var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
                        //  var z = Input.GetAxis("Vertical") * Time.deltaTime * 0.05f;
                        // Debug.Log("TEST");
                        //  PlayerOnBoard.transform.Rotate(0, x, 0);
                        //  PlayerOnBoard.transform.Translate(0, 0, z);
                        if ((Input.GetKeyDown(KeyCode.W) 
                            || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp) 
                            || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickUp))
                            && !counterDirection)
                        {
                            //    Debug.Log("Thumbstick: " + OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp));

                            detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(0.2f, 0, 0);
                        }
                        if ((Input.GetKeyDown(KeyCode.W)
                            || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp)
                            || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickUp))
                            && counterDirection)
                        {
                            //    Debug.Log("Thumbstick: " + OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp));

                            detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(-0.2f, 0, 0);
                        }
                        if ((Input.GetKeyDown(KeyCode.S) 
                            || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown) 
                            || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickDown))
                            && !counterDirection)
                        {
                            //    Debug.Log("w");
                            detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(-0.2f, 0, 0);
                        }
                        if ((Input.GetKeyDown(KeyCode.S)
                            || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown)
                            || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickDown))
                            && counterDirection)
                        {
                            //    Debug.Log("w");
                            detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(0.2f, 0, 0);
                        }
                        if ((Input.GetKeyDown(KeyCode.A) 
                            || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft) 
                            || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickLeft))
                            && !counterDirection)
                        {
                         
                            detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(0, 0, 0.2f);
                        }
                        if ((Input.GetKeyDown(KeyCode.A)
                            || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft)
                            || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickLeft))
                            && counterDirection)
                        {
                            
                            detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(0, 0, -0.2f);
                        }
                        if ((Input.GetKeyDown(KeyCode.D) 
                            || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight) 
                            || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickRight))
                            && !counterDirection)
                        {
                            //   Debug.Log("w");
                            detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(0, 0, -0.2f);
                        }
                        if ((Input.GetKeyDown(KeyCode.D)
                            || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight)
                            || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickRight))
                            && counterDirection)
                        {
                            //   Debug.Log("w");
                            detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(0, 0, 0.2f);
                        }
                        if (Input.GetKeyDown(KeyCode.G) 
                            || OVRInput.GetDown(OVRInput.Button.One) 
                            || OVRInput.GetDown(OVRInput.Button.Three)
                            || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)
                            || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
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
                    if (!GetComponentInChildren<HandControl>().TradeModeActive)
                    {
                        GetComponentInChildren<HandControl>().ActivateTrade(true);
                        Debug.Log("Trade Mode ON");
                    }
                    
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
        rightHandUI.SetActive(false);
        menuOpen = false;
        currentHighlight = (int)Status.None;
        Debug.Log("Turning off Menu");
        GetComponentInChildren<HandControl>().ActivateTrade(false);

        Debug.Log("Trade Mode OFF");
    }
}
