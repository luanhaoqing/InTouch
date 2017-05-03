using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Task2Manager : MonoBehaviour
{
    public GameObject Helper;
    SPHelperAnimation helperAnim;
    public SPControllerOfPlayerOntheBoard Controller;
    public BillboardManager Billboard;
    public OverallManager overallManager;
    public Transform helperInitialPosition;
    private int substate = 1;

    public AudioClip[] Task3VOs;

    public GameObject ItemOnInventory;
    public GameObject OtherInventory;
    public GameObject ItemOnOtherInventory;
    public GameObject ActionPoints;
    public Transform islandPosition;
    

    // Use this for initialization
    void Start()
    {
        Helper.transform.position = helperInitialPosition.position;
        Helper.transform.rotation = helperInitialPosition.rotation;

        helperAnim = Helper.GetComponent<SPHelperAnimation>();

        Controller.canControl = false;
        Controller.rightHandHoverUI.GetComponentInChildren<UnityEngine.UI.Text>().text = "Move Mode";
        Controller.controlMode = 4;

    }

    // Update is called once per frame
    void Update()
    {
        Billboard.GetComponent<BillboardManager>().SetDebugInfo("Task 2 - State: " + substate);

        switch (substate)
        {
            case 1:
                Helper.GetComponent<SPHelperTalk>().Speak(Task3VOs[0]);
                OtherInventory.SetActive(true);
                Billboard.HighLight(1);
                substate = 2;
                break;
            case 2:
                if (helperAnim.FinishedTalking())
                {
                    Controller.canControl = true;
                    Controller.moveDisabled = true;
                    substate = 3;
                    Controller.rightHandHoverUI.GetComponentInChildren<UnityEngine.UI.Text>().text = "Move - Button to cancel";
                }
                break;
            case 3:
                if (Controller.controlMode == 0)
                {
                    Helper.GetComponent<SPHelperTalk>().Speak(Task3VOs[1]);

                    //disable move here, in case the player choose move again.
                    Controller.sendItemDisabled = false;
                    Controller.SendButtonFlash();
                    substate = 4;
                }
                break;
            case 4:
                if (Controller.controlMode == 2)
                {
                    Helper.GetComponent<SPHelperTalk>().Speak(Task3VOs[2]);
                    Controller.moveDisabled = false;
                    Controller.sendItemDisabled = true;
                    substate = 5;
                }
                break;
            case 5:
                if (ItemOnInventory.GetComponent<touchDetector>().IfTouched())
                {
                    Helper.GetComponent<SPHelperTalk>().Speak(Task3VOs[3]);
                    ItemOnInventory.SetActive(false);
                    ItemOnOtherInventory.SetActive(true);
                    StartCoroutine(ActionPointsAnim());
                    substate = 7;
                }
                break;
            case 7:
                break;

            case 8:
                if (helperAnim.FinishedTalking())
                {
                    Billboard.Check(1);
                    StartCoroutine(HelperFlyToIsland());
                }
                break;
        }
    }

    IEnumerator HelperFlyToIsland()
    {
        yield return new WaitForSeconds(1f);
        Helper.transform.LookAt(Helper.transform.position + new Vector3(-0.2f, 0, 0));
        iTween.MoveTo(Helper, iTween.Hash("position", islandPosition, "easetype", iTween.EaseType.easeInOutSine));
        yield return new WaitForSeconds(0.5f);
        overallManager.startTask4();
        overallManager.destroyStuff(this.gameObject);
    }

    IEnumerator ActionPointsAnim()
    {
        yield return new WaitForSeconds(2f);
        ActionPoints.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        ActionPoints.transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        ActionPoints.transform.GetChild(2).gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        ActionPoints.transform.GetChild(0).gameObject.SetActive(true);
        ActionPoints.transform.GetChild(1).gameObject.SetActive(true);
        ActionPoints.transform.GetChild(2).gameObject.SetActive(true);

        substate = 8;

    }
}

