﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ControllerOfPlayerOntheBoard : NetworkBehaviour {
    public GameObject PlayerOnBoard;
    public GameObject detectBall;
    public GameObject raycast_detect;
    public GameObject detectBallgroup;
    public GameObject HealingCursor;
    public GameObject PlayerModel;
    private Vector3 target;
    private bool BeginMove;
    public GameObject rabbit;
    public GameObject rabbitForItem;
    public int controlMode = -1; // 0 = menu, 1 = move, 2 = send, 3 = use item, 4 = null, 5 = ready to heal
    private int previousControlMode = 1;
    public GameObject rightHandMenu;
    public GameObject rightHandHoverUI;
    public GameObject TurnCounter;
    public bool counterDirection;
    public GameObject Turn_board;

    UnityEngine.EventSystems.EventSystem myEventSystem;
    //public bool tradeon = false;

    public UnityEngine.UI.Button MoveButton;
    public UnityEngine.UI.Button SendButton;
    public UnityEngine.UI.Button ItemButton;

    private Selectable CurrentHighlightButton;


    public bool menuOpen = false;
    private bool menuHasOpened = false;
    Text constantText;


    // declare five controller mapping: four for choosing menu options, one for confirm.
    private bool controllerUpMapping;
    private bool controllerLeftMapping;
    private bool controllerRightMapping;
    private bool controllerDownMapping;
    private bool controllerABMapping;
    private bool controllerTriggerMapping;

    
    // Use this for initialization
    void Start () {
        PlayerOnBoard.transform.position = new Vector3(0.29f+0.2f*2,0.04f,-0.4544f+0.2f*3);     
        TurnCounter = GameObject.Find("TrunCounter");
        SetMenuActive(false);
        SetHoverUIActive(false);

        if(TurnCounter.GetComponent<TurnCounter>().OwnId==1)
        {
            counterDirection = true;
        }
        constantText = rightHandHoverUI.GetComponentInChildren<Text>();
        myEventSystem = GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
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
        controllerABMapping = ((Input.GetKeyDown(KeyCode.Space)
                || OVRInput.GetDown(OVRInput.Button.One)
                || OVRInput.GetDown(OVRInput.Button.Two)));
        controllerTriggerMapping = (Input.GetKeyDown(KeyCode.Backspace)
                || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)
                || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger));


        // Open Menu whenever it's your turn. Turn it off whenever it's not.
        
        if (GameObject.FindGameObjectWithTag("Turn").GetComponent<CurrentPlayer>().MyTurn && (controlMode == -1))
        {

         //   Debug.Log("Menu Open");   
            SetMenuActive(true);
            SetHoverUIActive(true);
            constantText.text = "Your Turn";
            Turn_board.GetComponent<Animator>().SetBool("YourTurn", true);

        }
        else if ((!GameObject.FindGameObjectWithTag("Turn").GetComponent<CurrentPlayer>().MyTurn) && !BeginMove)
        {
          //  Debug.Log("Menu Close");
            SetMenuActive(false);
            SetHoverUIActive(false);
            controlMode = -1;
            Turn_board.GetComponent<Animator>().SetBool("YourTurn", false);
            raycast_detect.GetComponent<RaserPointer>().Show = false;
        }

        // Open menu with trigger after you chose something in this turn.
        switch (controlMode)
        {
       
            // Control Mode: Menu
            case 0:
                raycast_detect.GetComponent<RaserPointer>().Show = false;
                hideCursor();


                // Highlighting options
                if (CurrentHighlightButton == null)
                {
                //    Debug.Log("Nothing Selected");
                    MoveButton.Select();
                    CurrentHighlightButton = MoveButton;
                }

                else
                {
              //      Debug.Log(CurrentHighlightButton.name);
                    // press left and find left
                    if (controllerLeftMapping)
                    {
                        Selectable newButton = CurrentHighlightButton.FindSelectableOnLeft();
                        if (newButton != null)
                        {
                            newButton.Select();
                            CurrentHighlightButton = newButton;
                            AudioCenter.PlaySelectionAlt();
                        }
                        else
                        {
                            AudioCenter.PlayCantDoThat();
                        }

                    }

                    if (controllerRightMapping)
                    {
                        Selectable newButton = CurrentHighlightButton.FindSelectableOnRight();
                        if (newButton != null)
                        {
                            newButton.Select();
                            CurrentHighlightButton = newButton;
                            AudioCenter.PlaySelectionAlt();
                        }
                        else
                        {
                            AudioCenter.PlayCantDoThat();
                        }

                    }
                }
                

                    /*************************************
                    // After Highlighting -- Make choices
                    **************************************/
                    // 1) Set Mode to Move
                    if (CurrentHighlightButton == MoveButton)
                {
                    if (controllerTriggerMapping)
                    {
                        ChangeControlMode();
                        controlMode = 1;
                        AudioCenter.PlaySelectionConfirm();
                        constantText.text = "Move - Button to cancel";
                    }
                }
                // 2) Set Mode to Send
                if (CurrentHighlightButton == SendButton)
                {
                    if (controllerTriggerMapping)
                    {
                        ChangeControlMode();
                        controlMode = 2;
                        AudioCenter.PlaySelectionConfirm();
                        constantText.text = "Move - Button to cancel";

                    }
                }

                // 3) Set Mode to Item
                if (CurrentHighlightButton == ItemButton)
                {
                    if (controllerTriggerMapping)
                    {
                        //rightHandHoverUI.GetComponentInChildren<UnityEngine.UI.Text>().text = "Use Item Not Implemented Yet";
                        //Debug.Log("Use Item Not Implemented Yet");
                        //AudioCenter.PlayCantDoThat();
                        ChangeControlMode();
                        controlMode = 3;
                        AudioCenter.PlaySelectionConfirm();
                        constantText.text = "Use - Button to cancel";
                    }
                }
                break;


            // Control Mode: Move
            // below is all the character movement code.
            case 1:
                if (GameObject.FindGameObjectWithTag("Turn").GetComponent<CurrentPlayer>().MyTurn && !BeginMove)
                {
                    raycast_detect.GetComponent<RaserPointer>().Show = true;
                    detectBallgroup.transform.position = PlayerOnBoard.transform.position;
                    // Move Up
                    /////////////////////
                    /*
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
                    */
                    //////////////////////

                    if (Input.GetKeyDown(KeyCode.G) 
                        || controllerTriggerMapping)
                    {
                     //   if (detectBall.GetComponent<DetectionandHighLight>().IfCouldMove())
                     if(raycast_detect.GetComponent<RaserPointer>().tile!=null&& raycast_detect.GetComponent<RaserPointer>().tile.GetComponent<TileHealthyManager>().couldMoveTo)
                        {
                            //  Debug.Log("w");
                            BeginMove = true;
                          
                            target = raycast_detect.GetComponent<RaserPointer>().tile.transform.position;
                            target.y = PlayerOnBoard.transform.position.y;
                            PlayerModel.transform.LookAt(target);
                            hideCursor();
                        }
                    }

                    // Go Back to Menu from move
                    if (controllerABMapping)
                    {
                        SetMenuActive(true);
                        controlMode = 0;
                        constantText.text = "Your Turn";
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
                raycast_detect.GetComponent<RaserPointer>().Show = false;
                // enable touch-send control
                if (!GetComponentInChildren<HandControl>().TradeModeActive)
                {
                    rabbit.transform.position = new Vector3(100, 100, 100);
                 //   Debug.Log("Trade Mode ON");
                }

                // Go Back to Menu with trigger from Send
                if (controllerABMapping)
                {
                    SetMenuActive(true);
                    controlMode = 0;
                    CurrentHighlightButton = SendButton;
                    SendButton.Select();
                    constantText.text = "Your Turn";
                }

                // do something to tell players they are in item mode

                break;

            // Control mode: Use Item
            case 3:
                raycast_detect.GetComponent<RaserPointer>().Show = false;
                if (!GetComponentInChildren<HandControl>().UseItemActive)
                {
                    rabbitForItem.transform.position = new Vector3(100, 100, 100);
                    GetComponentInChildren<Inventory>().ActiveUseItem();
                 //   Debug.Log("Use Item Mode On");
                }

                // Go Back to Menu with trigger
                if (controllerABMapping)
                {
                    SetMenuActive(true);
                    controlMode = 0;
                    CurrentHighlightButton = ItemButton;
                    ItemButton.Select();
                    constantText.text = "Your Turn";
                }

                break;



            // choose which tile to use heal
            // Jeremy never touched anything in case 5
            case 5:
                raycast_detect.GetComponent<RaserPointer>().Show = false;
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
                        || controllerTriggerMapping)
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

}
    // To turn off anything that needs to be turned off whenever enters menu.
    void ChangeControlMode()
    {
        SetMenuActive(false);
        menuOpen = false;
        CurrentHighlightButton = null;
        // GetComponentInChildren<HandControl>().ActivateTrade(false);
        // tradeon = false;

        rabbit.transform.position = new Vector3(0, 0, 0);
        rabbitForItem.transform.position = new Vector3(0,0,0);
      //  GetComponentInChildren<Inventory>().DeActiveUseItem();
    }

    public void SetMenuActive(bool boolean)
    {
        rightHandMenu.SetActive(boolean);
        if (boolean == true)
        {
            controlMode = 0;
            CurrentHighlightButton = MoveButton;
            MoveButton.Select();
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
        detectBallgroup.transform.position = temp;
     //   detectBall.transform.position = temp;
     //   detectBall.GetComponent<DetectionandHighLight>().cursor.SetActive(false);
    }
}
