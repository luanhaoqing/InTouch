using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ControllerOfPlayerOntheBoard : NetworkBehaviour {
    public GameObject PlayerOnBoard;
    public GameObject detectBall;
    public GameObject HealingCursor;
    public GameObject PlayerModel;
    private Vector3 target;
    private bool BeginMove;
    public GameObject rabbit;
    public GameObject rabbitForItem;
    public int controlMode = 0; // 0 = menu, 1 = move, 2 = send, 3 = use item, 4 = null, 5 = ready to heal
    private int previousControlMode = 1;
    public GameObject rightHandMenu;
    public GameObject rightHandHoverUI;
    public int currentHighlight = 0; // UI higlight
    public GameObject TurnCounter;
    public bool counterDirection;
    //public bool tradeon = false;

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
    private bool controllerUpMapping;
    private bool controllerLeftMapping;
    private bool controllerRightMapping;
    private bool controllerDownMapping;
    private bool controllerClickMapping;
    private bool controllerTriggerMapping;

    
    // Use this for initialization
    void Start () {
        PlayerOnBoard.transform.position = new Vector3(0.29f+0.2f*2,0.04f,-0.4544f+0.2f*3);     
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
        controllerLeftMapping = (Input.GetKeyDown(KeyCode.A)
                || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft)
                || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickLeft));
        controllerUpMapping = (Input.GetKeyDown(KeyCode.W)
                || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp)
                || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickUp));
        controllerRightMapping = (Input.GetKeyDown(KeyCode.D)
                || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight)
                || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickRight));
        controllerDownMapping = (Input.GetKeyDown(KeyCode.S)
                || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown)
                || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickDown));
        controllerClickMapping = ((Input.GetKeyDown(KeyCode.Space)
                || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick)
                || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick)));
        controllerTriggerMapping = (Input.GetKeyDown(KeyCode.Backspace)
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
        switch (controlMode)
        {
            // Control Mode: Menu
            case 0:
                hideCursor();

                // Highlighting options
                if (controllerUpMapping) // choose move
                {
                    currentHighlight = (int)Status.Move;
                    MoveButton.Select();
                    AudioCenter.PlaySelectionAlt();
                    //Debug.Log("Move Button Highlighted");
                    return;
                }

                if (controllerLeftMapping) // choose send
                {
                    currentHighlight = (int)Status.Send;
                    SendButton.Select();
                    AudioCenter.PlaySelectionAlt();
                    //Debug.Log("Send Button Highlighted");
                    return;
                }

                if (controllerRightMapping) // choose item
                {
                    currentHighlight = (int)Status.Item;
                    ItemButton.Select();
                    AudioCenter.PlaySelectionAlt();
                    return;
                }

                if (controllerDownMapping) // choose exit
                {
                    currentHighlight = (int)Status.Exit;
                    ExitButton.Select();
                    AudioCenter.PlaySelectionAlt();
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
                        AudioCenter.PlaySelectionConfirm();

                    }
                }

                // 3) Set Mode to Item
                if (currentHighlight == (int)Status.Item)
                {
                    if (controllerClickMapping)
                    {
                        //rightHandHoverUI.GetComponentInChildren<UnityEngine.UI.Text>().text = "Use Item Not Implemented Yet";
                        //Debug.Log("Use Item Not Implemented Yet");
                        //AudioCenter.PlayCantDoThat();

                        ChangeControlMode();
                        controlMode = 3;
                        AudioCenter.PlaySelectionConfirm();

                    }
                }
                break;


            // Control Mode: Move
            // below is all the character movement code.
            case 1:
                if (GameObject.FindGameObjectWithTag("Turn").GetComponent<CurrentPlayer>().MyTurn && !BeginMove)
                {
                        
                    // Move Up
                    if ((controllerUpMapping)
                        && !counterDirection)
                    {

                        PlayerModel.transform.LookAt(PlayerOnBoard.transform.position + new Vector3(0.2f, 0, 0));
                        detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(0.2f, 0, 0);
                    }
                    if ((controllerUpMapping)
                        && counterDirection)
                    {
                        PlayerModel.transform.LookAt(PlayerOnBoard.transform.position + new Vector3(-0.2f, 0, 0));
                        detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(-0.2f, 0, 0);
                    }

                    // Move Down
                    if ((controllerDownMapping)
                        && !counterDirection)
                    {
                        PlayerModel.transform.LookAt(PlayerOnBoard.transform.position + new Vector3(-0.2f, 0, 0));
                        detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(-0.2f, 0, 0);
                    }
                    if ((controllerDownMapping)
                        && counterDirection)
                    {
                        PlayerModel.transform.LookAt(PlayerOnBoard.transform.position + new Vector3(0.2f, 0, 0));
                        detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(0.2f, 0, 0);
                    }

                    // Move Left
                    if ((controllerLeftMapping)
                        && !counterDirection)
                    {
                        PlayerModel.transform.LookAt(PlayerOnBoard.transform.position + new Vector3(0, 0, 0.2f));
                        detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(0, 0, 0.2f);
                    }
                    if ((controllerLeftMapping)
                        && counterDirection)
                    {
                        PlayerModel.transform.LookAt(PlayerOnBoard.transform.position + new Vector3(0, 0, -0.2f));
                        detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(0, 0, -0.2f);
                    }

                    // Move Right
                    if ((controllerRightMapping)
                        && !counterDirection)
                    {
                        PlayerModel.transform.LookAt(PlayerOnBoard.transform.position + new Vector3(0, 0, -0.2f));
                        detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(0, 0, -0.2f);
                    }
                    if ((controllerRightMapping)
                        && counterDirection)
                    {
                        PlayerModel.transform.LookAt(PlayerOnBoard.transform.position + new Vector3(0, 0, 0.2f));
                        detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(0, 0, 0.2f);
                    }


                    if (Input.GetKeyDown(KeyCode.G) 
                        || controllerClickMapping)
                    {
                        if (detectBall.GetComponent<DetectionandHighLight>().IfCouldMove())
                        {
                            //  Debug.Log("w");
                            BeginMove = true;
                            target = detectBall.transform.position;
                            hideCursor();
                        }
                    }

                    // Go Back to Menu
                    if (controllerTriggerMapping)
                    {
                        SetMenuActive(true);
                        controlMode = 0;
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
                    rabbit.transform.position = new Vector3(100, 100, 100);
                    Debug.Log("Trade Mode ON");
                }

                // Go Back to Menu with trigger
                if (controllerTriggerMapping)
                {
                    SetMenuActive(true);
                    controlMode = 0;
                }

                // do something to tell players they are in item mode

                break;

            // Control mode: Use Item
            case 3:
                if (!GetComponentInChildren<HandControl>().UseItemActive)
                {
                    rabbitForItem.transform.position = new Vector3(100, 100, 100);
                    GetComponentInChildren<Inventory>().ActiveUseItem();
                    Debug.Log("Use Item Mode On");
                }

                // Go Back to Menu with trigger
                if (controllerTriggerMapping)
                {
                    SetMenuActive(true);
                    controlMode = 0;
                }

                break;



            // choose which tile to use heal
            // Jeremy never touched anything in case 5
            case 5:

                //  HealingCursor.GetComponent<DetectHealing>().model.SetActive(true);
                if (GameObject.FindGameObjectWithTag("Turn").GetComponent<CurrentPlayer>().MyTurn)
                {

                    if ((Input.GetKeyDown(KeyCode.W)
                        || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp)
                        || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickUp))
                        && !counterDirection)
                    {
                        HealingCursor.transform.position += new Vector3(0.2f, 0, 0);
                    }
                    if ((Input.GetKeyDown(KeyCode.W)
                        || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp)
                        || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickUp))
                        && counterDirection)
                    {


                        HealingCursor.transform.position += new Vector3(-0.2f, 0, 0);
                    }
                    if ((Input.GetKeyDown(KeyCode.S)
                        || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown)
                        || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickDown))
                        && !counterDirection)
                    {
                        //    Debug.Log("w");
                        HealingCursor.transform.position += new Vector3(-0.2f, 0, 0);
                    }
                    if ((Input.GetKeyDown(KeyCode.S)
                        || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown)
                        || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickDown))
                        && counterDirection)
                    {
                        //    Debug.Log("w");
                        HealingCursor.transform.position += new Vector3(0.2f, 0, 0);
                    }
                    if ((Input.GetKeyDown(KeyCode.A)
                        || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft)
                        || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickLeft))
                        && !counterDirection)
                    {
                        HealingCursor.transform.position += new Vector3(0, 0, 0.2f);
                    }
                    if ((Input.GetKeyDown(KeyCode.A)
                        || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft)
                        || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickLeft))
                        && counterDirection)
                    {
                        HealingCursor.transform.position += new Vector3(0, 0, -0.2f);
                    }
                    if ((Input.GetKeyDown(KeyCode.D)
                        || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight)
                        || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickRight))
                        && !counterDirection)
                    {
                        //   Debug.Log("w");
                        HealingCursor.transform.position += new Vector3(0, 0, -0.2f);
                    }
                    if ((Input.GetKeyDown(KeyCode.D)
                        || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight)
                        || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickRight))
                        && counterDirection)
                    {
                        //   Debug.Log("w");
                        HealingCursor.transform.position += new Vector3(0, 0, 0.2f);
                    }
                    if (Input.GetKeyDown(KeyCode.G)
                        || OVRInput.GetDown(OVRInput.Button.One)
                        || OVRInput.GetDown(OVRInput.Button.Three)
                        || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick)
                        || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick))
                    {
                        if (HealingCursor.GetComponent<DetectHealing>().couldHeal)
                        {
                            HealingCursor.transform.position -= new Vector3(0, 200f, 0);
                            SetMenuActive(true);
                        }
                    }
                }
                    break;
            
    }




/*

        // Control mode: Menu
            case 4:
                ChangeControlMode();
                // Turn on the hand menu! (right hand at the moment)
                handUI.SetActive(true);

                // make choices with highlighting - Keyboard: I,O,P,L , confirm with K


    }
    */
}
    // To turn off anything that needs to be turned off whenever enters menu.
    void ChangeControlMode()
    {
        SetMenuActive(false);
        menuOpen = false;
        currentHighlight = (int)Status.None;
        // GetComponentInChildren<HandControl>().ActivateTrade(false);
        // tradeon = false;

        rabbit.transform.position = new Vector3(0, 0, 0);
        rabbitForItem.transform.position = new Vector3(0,0,0);
        GetComponentInChildren<Inventory>().DeActiveUseItem();
    }

    public void SetMenuActive(bool boolean)
    {
        rightHandMenu.SetActive(boolean);
        if (boolean == true)
        {
            controlMode = 0;
        }
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
