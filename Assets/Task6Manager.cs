using UnityEngine;
using System.Collections;

public class Task6Manager : MonoBehaviour
{

    public GameObject Helper;
    SPHelperAnimation helperAnim;
    public SPControllerOfPlayerOntheBoard Controller;
    public BillboardManager Billboard;
    public OverallManager overallManager;
    public Transform helperInitialPosition;

    private int substate = 1;

    public AudioClip[] Task6VOs;
    public GameObject RuneOnTile;
    public GameObject TwoRunesInInventory;
    public GameObject ThirdRuneInInventory;
    public GameObject Door;
    public GameObject KeyOnTile;
    public GameObject KeyOnInventory;

    public GameObject Camera;

    public Transform NextPlace;



    // Use this for initialization
    void Start()
    {
        Helper.transform.position = helperInitialPosition.position;
        Helper.transform.rotation = helperInitialPosition.rotation;

        helperAnim = Helper.GetComponent<SPHelperAnimation>();

        Controller.canControl = false;
        Controller.rightHandHoverUI.GetComponentInChildren<UnityEngine.UI.Text>().text = "Hold On...";
        Controller.controlMode = 1;

    }

    // Update is called once per frame
    void Update () {
        switch (substate)
        {
            case 1:
                Billboard.HighLight(5);
                Helper.GetComponent<SPHelperTalk>().Speak(Task6VOs[0]);
                substate = 2;
                break;
            case 2:
                if (helperAnim.FinishedTalking())
                {
                    Helper.GetComponent<SPHelperTalk>().Speak(Task6VOs[1]);
                    RuneOnTile.SetActive(true);
                    TwoRunesInInventory.SetActive(true);
                    substate = 3;
                }

                break;
            case 3:
                if (helperAnim.FinishedTalking())
                {
                    // Helper.GetComponent<SPHelperTalk>().Speak(Task6VOs[2]); Seems repeted;
                    Controller.canControl = true;
                    Controller.controlMode = 1;
                    substate = 4;
                }
                break;
            case 4:
                if (RuneOnTile.GetComponent<moveDetector>().IfTouched())
                {
                    Helper.GetComponent<SPHelperTalk>().Speak(Task6VOs[3]);
                    Helper.transform.LookAt(Helper.transform.position + new Vector3(-0.2f, 0, 0));

                    AudioCenter.PlayGetItem();
                    RuneOnTile.SetActive(false);
                    ThirdRuneInInventory.SetActive(true);
                    substate = 5;

                }
                break;
            case 5:
                if (KeyOnTile.GetComponent<moveDetector>().IfTouched())
                {
                    AudioCenter.PlayGetItem();
                    ThirdRuneInInventory.SetActive(false);
                    TwoRunesInInventory.SetActive(false);
                    KeyOnTile.SetActive(false);
                    KeyOnInventory.SetActive(true);
                    substate = 6;
                }
                break;

            case 6:
                if (Door.GetComponent<moveDetector>().IfTouched())
                {
                    Helper.GetComponent<SPHelperTalk>().Speak(Task6VOs[4]);
                    Door.GetComponent<Animator>().SetTrigger("DoorOpen");
                    KeyOnInventory.SetActive(false);
                    Helper.transform.LookAt(Helper.transform.position + new Vector3(-0.2f, 0, 0));
                    iTween.MoveTo(Helper, iTween.Hash("position", NextPlace, "easetype", iTween.EaseType.easeInOutSine));
                    helperAnim.Success();
                    substate = 7;
                }
                break;
            case 7:
                if (helperAnim.FinishedTalking())
                {
                    StartCoroutine(FinishTutorial());
                    Billboard.Check(5);
                    substate = -1;
                }
                break;
        }

    }
    IEnumerator FinishTutorial()
    {

        
        //fade out here.
        yield return new WaitForSeconds(1f);
        overallManager.skipScene();
    }
}
