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
    public GameObject rightHandMenu;
    public GameObject rightHandHoverUI;
    public int currentHighlight = 0; // UI higlight
    public GameObject TurnCounter;
    public bool counterDirection;

    public UnityEngine.UI.Button MoveButton;
    public UnityEngine.UI.Button SendButton;
    public UnityEngine.UI.Button ItemButton;
    public UnityEngine.UI.Button ExitButton;

    public bool menuOpen = false;
    private bool menuHasOpened = false;

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
    private bool controllerDownMapping;
    private bool controllerClickMapping;
    private bool controllerCancelMapping;

    //


    // Use this for initialization
    void Start () {
        PlayerOnBoard.transform.position = new Vector3(0.29f,0.04f,-0.4544f);     
        rightHandMenu.SetActive(false);
        rightHandHoverUI.SetActive(false);
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
        controllerSendMapping = (Input.GetKeyDown(KeyCode.I)
                || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft)
                || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickLeft));
        controllerMoveMapping = (Input.GetKeyDown(KeyCode.O)
                || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp)
                || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickUp));
        controllerItemMapping = (Input.GetKeyDown(KeyCode.P)
                || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight)
                || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickRight));
        controllerDownMapping = (Input.GetKeyDown(KeyCode.L)
                || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown)
                || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickDown));
        controllerClickMapping = ((Input.GetKeyDown(KeyCode.Space)
                || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick)
                || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick)));
        controllerCancelMapping = (Input.GetKeyDown(KeyCode.Backspace)
                || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)
                || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger));


        // Open Menu whenever it's your turn. Turn it off whenever it's not.
        if (GameObject.FindGameObjectWithTag("Turn").GetComponent<CurrentPlayer>().MyTurn && (!menuHasOpened))
        {
            menuHasOpened = true;
            SetMenuActive(true);
            SetHoverUIActive(true);
            Debug.Log("Opened");
        }
        else if ((!GameObject.FindGameObjectWithTag("Turn").GetComponent<CurrentPlayer>().MyTurn) && menuHasOpened)
        {
            menuHasOpened = false;
            SetMenuActive(false);
            SetHoverUIActive(false);
            Debug.Log("Closed");
        }

        // Open menu with trigger after you chose something in this turn.
        if (!menuOpen && controllerCancelMapping && menuHasOpened)
        {
            SetMenuActive(true);
        }

        if (menuOpen)
        {
            hideCursor();

            // Highlighting options
            if (controllerMoveMapping) // choose move
            {
                currentHighlight = (int)Status.Move;
                MoveButton.Select();
                AudioCenter.PlaySelectionAlt();
                //Debug.Log("Move Button Highlighted");
                return;

            }

            if (controllerSendMapping) // choose send
            {
                currentHighlight = (int)Status.Send;
                SendButton.Select();
                AudioCenter.PlaySelectionAlt();
                //Debug.Log("Send Button Highlighted");
                return;

            }

            if (controllerItemMapping) // choose item
            {
                currentHighlight = (int)Status.Item;
                ItemButton.Select();
                AudioCenter.PlaySelectionAlt();
                //Debug.Log("Item Button Highlighted");
                return;
            }

            if (controllerDownMapping) // choose exit
            {
                currentHighlight = (int)Status.Exit;
                ExitButton.Select();
                AudioCenter.PlaySelectionAlt();
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
                    AudioCenter.PlaySelectionConfirm();
                }
            }
            // 2) Set Mode to Send
            if (currentHighlight == (int)Status.Send)
            {
                if (controllerClickMapping)
                {
                    ChangeControlMode();
                    controlMode = 2;
                    // Here I need to reactivate Trade Mode. For now it's missing because trade mode cannot be syncronized across two clients.
                    AudioCenter.PlaySelectionConfirm();

                }
            }
            // 3) Set Mode to Item
            if (currentHighlight == (int)Status.Item)
            {
                if (controllerClickMapping)
                {
                    rightHandHoverUI.GetComponentInChildren<UnityEngine.UI.Text>().text = "Use Item Not Implemented Yet";
                    Debug.Log("Use Item Not Implemented Yet");
                    AudioCenter.PlayCantDoThat();

                    // Cannot change control mode right now:
                    // De-commit when it is implemented
                    /*
                    ChangeControlMode();
                    controlMode = 3;
                    AudioCenter.PlaySelectionConfirm();
                    */
                }
            }
            // 4) Exit and return to previous mode
            // obsolete because the menu is the highest rank you can go back to.
            /*
            if (controllerCancelMapping)
            {
                ChangeControlMode();
                controlMode = previousControlMode;
                AudioCenter.PlaySelectionConfirm();
            }
            */


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
                            || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick)
                            || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick))
                        {
                            if (detectBall.GetComponent<DetectionandHighLight>().IfCouldMove())
                            {
                                //  Debug.Log("w");
                                BeginMove = true;
                                target = detectBall.transform.position;
                                hideCursor();
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
                        // GetComponentInChildren<HandControl>().ActivateTrade(true);
                        GameObject.FindGameObjectWithTag("Turn").GetComponent<CurrentPlayer>().TradeOn = true;
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
        SetMenuActive(false);
        menuOpen = false;
        currentHighlight = (int)Status.None;
        Debug.Log("Turning off Menu");
        GetComponentInChildren<HandControl>().ActivateTrade(false);

        Debug.Log("Trade Mode OFF");
    }

    public void SetMenuActive(bool boolean)
    {
        rightHandMenu.SetActive(boolean);
        menuOpen = boolean;
    }

    public void SetHoverUIActive(bool boolean)
    {
        rightHandHoverUI.SetActive(boolean);
    }

    void hideCursor()
    {
        Vector3 temp = PlayerOnBoard.transform.position;
        temp.y = 10f;
        detectBall.transform.position = temp;
        detectBall.GetComponent<DetectionandHighLight>().cursor.SetActive(false);
    }
}
