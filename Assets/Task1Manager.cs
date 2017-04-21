using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Task1Manager : MonoBehaviour
{
    public GameObject Helper;
    SPHelperAnimation helperAnim;
    public SPControllerOfPlayerOntheBoard Controller;
    public BillboardManager Billboard;
    public OverallManager overallManager;
    public Transform helperInitialPosition;
    private int substate = 1;

    public AudioClip[] Task1VOs;

    public GameObject TwoMoreIslands;
    public moveDetector FirstMoveDetector;
    public GameObject EventScrollOnBoard;
    public moveDetector EventMoveDetector;
    public EventSheetMovement EventAnim;

    // Use this for initialization
    void Start()
    {
        Helper.transform.position = helperInitialPosition.position;
        Helper.transform.rotation = helperInitialPosition.rotation;

        helperAnim = Helper.GetComponent<SPHelperAnimation>();

        Controller.canControl = true;
        Controller.controlMode = 0;

    }

    // Update is called once per frame
    void Update()
    {
        switch (substate)
        {
            case 1:
                Helper.GetComponent<SPHelperTalk>().Speak(Task1VOs[0]);
                Billboard.HighLight(0);
                substate = 2;
                break;
            case 2:
                if (helperAnim.FinishedTalking())
                {
                    Controller.MoveButtonFlash();
                    substate = 3;
                }
                break;
            case 3:
                if (Controller.controlMode == 1)
                {
                    Helper.GetComponent<SPHelperTalk>().Speak(Task1VOs[1]);
                    substate = 4;
                }
                break;
            case 4:
                if (FirstMoveDetector.IfTouched())
                {
                    Helper.GetComponent<SPHelperTalk>().Speak(Task1VOs[2]);
                    helperAnim.Success();
                    substate = 5;
                }
                break;
            case 5:
                if (helperAnim.FinishedTalking())
                {
                    Helper.GetComponent<SPHelperTalk>().Speak(Task1VOs[3]);
                    EventScrollOnBoard.SetActive(true);
                    substate = 6;
                }
                break;

            case 6:
                if (EventMoveDetector.IfTouched())
                {

                    EventScrollOnBoard.SetActive(false);
                    EventAnim.StartFlying(Controller.transform);
                    AudioCenter.PlayEventTrigger();
                    Helper.GetComponent<SPHelperTalk>().Speak(Task1VOs[4]);

                    Billboard.Check(0);
                    substate = 8;
                    //StartCoroutine(WaitAndNextState(6f));
                }
                break;
            case 7:
                break;
            case 8:
                if (helperAnim.FinishedTalking())
                {
                    overallManager.startTask2();
                    overallManager.destroyStuff(this.gameObject);
                }
                break;
        }

    }
    IEnumerator WaitAndNextState(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        substate = 8;
    }
}
