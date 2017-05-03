using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SPControllerOfPlayerOntheBoard : MonoBehaviour {

    public GameObject PlayerOnBoard;
    public GameObject PlayerModel;
    public GameObject detectBall;
    private Vector3 target;
    private bool BeginMove;
    public int controlMode = 4; // 0 = menu, 1 = move, 2 = send, 3 = use item, 4 = nothing
    public GameObject rightHandMenu;
    public GameObject rightHandHoverUI;
    public int currentHighlight = 0; // UI higlight
    public GameObject TurnCounter;
    public bool counterDirection;
    public bool tradeon = false;
    public GameObject LaserPointer;

    private Selectable CurrentHighlightButton;

    public UnityEngine.UI.Button MoveButton;
    public UnityEngine.UI.Button SendButton;
    public UnityEngine.UI.Button ItemButton;
    public UnityEngine.UI.Button SkipButton;

    public bool menuOpen = false;
    private bool menuHasOpened = false;

    public bool canControl = false;

    public bool sendItemDisabled = true;
    public bool useItemDisable = true;
    public bool moveDisabled = false;

    public OverallManager tutorialStateManager;

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
    private bool controllerABMapping;
    private bool controllerTriggerMapping;

    //


    // Use this for initialization
    void Start()
    {
        SetMenuActive(false);
        TurnCounter = GameObject.Find("TrunCounter");
    }

    // Update is called once per frame
    void Update()
    {
        if (!canControl)
        {
            return;
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


        // Open menu with trigger after you chose something in this turn.
        /*
         * if (!menuOpen && controllerCancelMapping)
        {
            SetMenuActive(true);
        }
        */
        //Debug.Log(controlMode);
        switch (controlMode)
            {
                // Control Mode: Move
                // below is all the character movement code.
                case 0:

                    if (!menuOpen)
                {
                    SetMenuActive(true);
                    rightHandHoverUI.GetComponentInChildren<UnityEngine.UI.Text>().text = "";
                }

                // Highlighting options
                if (CurrentHighlightButton == null)
                {
                    Debug.Log("Nothing Selected");
                    MoveButton.Select();
                    CurrentHighlightButton = MoveButton;
                }

                else
                {
                    Debug.Log(CurrentHighlightButton.name);
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
                    if (controllerDownMapping) // choose exit
                        {
                            currentHighlight = (int)Status.Exit;
                            CurrentHighlightButton = SkipButton;
                            SkipButton.Select();
                            AudioCenter.PlaySelectionAlt();
                            
                        }
                    if (controllerUpMapping) // choose move
                    {
                        MoveButton.Select();
                        CurrentHighlightButton = MoveButton;
                        AudioCenter.PlaySelectionAlt(); 

                    }


                }


                // After Highlighting -- Make choices
                // 1) Set Mode to Move
                if (CurrentHighlightButton == MoveButton)
                    {
                        if (!moveDisabled && controllerTriggerMapping)
                        {
                            ChangeControlMode();
                            controlMode = 1;
                            AudioCenter.PlaySelectionConfirm();
                            rightHandHoverUI.GetComponentInChildren<UnityEngine.UI.Text>().text = "Move Mode";

                        if (moveDisabled && controllerTriggerMapping)
                        {
                        rightHandHoverUI.GetComponentInChildren<UnityEngine.UI.Text>().text = "Can't do that now";
                        AudioCenter.PlayCantDoThat();
                        }

                    }

                }
                // 2) Set Mode to Send
                if (CurrentHighlightButton == SendButton)
                    {
                        if (sendItemDisabled && controllerTriggerMapping)
                        {
                            rightHandHoverUI.GetComponentInChildren<UnityEngine.UI.Text>().text = "Can't do that now";
                            AudioCenter.PlayCantDoThat();
                        }

                        if (!sendItemDisabled && controllerTriggerMapping)
                        {
                            ChangeControlMode();
                            controlMode = 2;
                            AudioCenter.PlaySelectionConfirm();
                            rightHandHoverUI.GetComponentInChildren<UnityEngine.UI.Text>().text = "Send Mode";


                    }
                }
                    // 3) Set Mode to Item
                    if (CurrentHighlightButton == ItemButton)
                    {
                        if (controllerTriggerMapping)
                        {
                            rightHandHoverUI.GetComponentInChildren<UnityEngine.UI.Text>().text = "Can't do that now";
                            AudioCenter.PlayCantDoThat();
                        }
                    }
                    // 4) Exit and return to previous mode
                    if (CurrentHighlightButton == SkipButton)
                    {
                        if (controllerTriggerMapping)
                        {
                            AudioCenter.PlaySelectionConfirm();
                            tutorialStateManager.skipScene();
                        }
                    }
                    break;
                case 1:

                    if (!BeginMove)
                    {
                    LaserPointer.GetComponent<RaserPointer>().Show = true;


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
                            || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)
                            || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                        {
                            if (detectBall.GetComponent<TutDetectBall>().IfCouldMove())
                            {
                                //  Debug.Log("w");
                                BeginMove = true;
                                target = detectBall.transform.position;
                                hideCursor();
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
                    break;
                // Control mode: Send
                case 2:

                    break;

                // Control mode: Use Item
                case 3:
                    break;

                case 4:
                if (menuOpen)
                {
                    LaserPointer.GetComponent<RaserPointer>().Show = false;
                    SetMenuActive(false);
                }

                if (!menuOpen && controllerABMapping)
                {
                    LaserPointer.GetComponent<RaserPointer>().Show = false;
                    controlMode = 0;
                    AudioCenter.PlaySelectionConfirm();
                }

                break;

        }
      

    }
    // To turn off anything that needs to be turned off whenever enters menu.
    void ChangeControlMode()
    {
        SetMenuActive(false);
        menuOpen = false;
        currentHighlight = (int)Status.None;
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

    public void SetControlActive(bool boolean)
    {
        canControl = boolean;
    }
    void hideCursor()
    {
        Vector3 temp = PlayerOnBoard.transform.position;
        temp.y = 10f;
        detectBall.transform.position = temp;
        detectBall.GetComponent<TutDetectBall>().cursor.SetActive(false);
    }



    // ---
    // Button Flash Animation Couroutines
    // ---
    public void SkipButtonFlash()
    {
        if (!menuOpen)
        {
            return;
        }
        StartCoroutine(SkipButtonFlashAnim());
    }
    IEnumerator SkipButtonFlashAnim()
    {
        Color originalColor = Color.white;
        Color lerpedColor = Color.yellow;
        yield return new WaitForSeconds(0.5f);
        SkipButton.image.color = lerpedColor;
        yield return new WaitForSeconds(0.5f);
        SkipButton.image.color = originalColor;
        yield return new WaitForSeconds(0.5f);
        SkipButton.image.color = lerpedColor;
        yield return new WaitForSeconds(0.5f);
        SkipButton.image.color = originalColor;
        yield return new WaitForSeconds(0.5f);
        SkipButton.image.color = lerpedColor;
        yield return new WaitForSeconds(0.5f);
        SkipButton.image.color = originalColor;
        yield return new WaitForSeconds(0.5f);
    }

    public void MoveButtonFlash()
    {
        if (!menuOpen)
        {
            return;
        }

        StartCoroutine(MoveButtonFlashAnim());
    }
    IEnumerator MoveButtonFlashAnim()
    {
        Color originalColor = Color.white;
        Color lerpedColor = Color.yellow;
        yield return new WaitForSeconds(0.5f);
        MoveButton.image.color = lerpedColor;
        yield return new WaitForSeconds(0.5f);
        MoveButton.image.color = originalColor;
        yield return new WaitForSeconds(0.5f);
        MoveButton.image.color = lerpedColor;
        yield return new WaitForSeconds(0.5f);
        MoveButton.image.color = originalColor;
        yield return new WaitForSeconds(0.5f);
        MoveButton.image.color = lerpedColor;
        yield return new WaitForSeconds(0.5f);
        MoveButton.image.color = originalColor;
        yield return new WaitForSeconds(0.5f);
    }

    public void SendButtonFlash()
    {


        StartCoroutine(SendButtonFlashAnim());
    }
    IEnumerator SendButtonFlashAnim()
    {
        Color originalColor = Color.white;
        Color lerpedColor = Color.yellow;
        yield return new WaitForSeconds(0.5f);
        SendButton.image.color = lerpedColor;
        yield return new WaitForSeconds(0.5f);
        SendButton.image.color = originalColor;
        yield return new WaitForSeconds(0.5f);
        SendButton.image.color = lerpedColor;
        yield return new WaitForSeconds(0.5f);
        SendButton.image.color = originalColor;
        yield return new WaitForSeconds(0.5f);
        SendButton.image.color = lerpedColor;
        yield return new WaitForSeconds(0.5f);
        SendButton.image.color = originalColor;
        yield return new WaitForSeconds(0.5f);
    }

}
