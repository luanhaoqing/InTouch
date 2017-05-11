using UnityEngine;
using System.Collections;

public class Task6Manager : MonoBehaviour
{

    public GameObject Walker;
    public GameObject Talker;
    SPHelperAnimation talkerAnim;
    public SPControllerOfPlayerOntheBoard Controller;
    public BillboardManager Billboard;
    public OverallManager overallManager;
    public Transform walkerInitialPosition;
    public Transform talkerInitialPosition;

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
        Walker.transform.position = walkerInitialPosition.position;
        Walker.transform.rotation = walkerInitialPosition.rotation;
        Talker.transform.position = talkerInitialPosition.position;
        Talker.transform.rotation = talkerInitialPosition.rotation;
        
        talkerAnim = Talker.GetComponent<SPHelperAnimation>();

        Controller.canControl = false;
        Controller.rightHandHoverUI.GetComponentInChildren<UnityEngine.UI.Text>().text = "Hold On...";
        Controller.controlMode = 1;
        Controller.ChangeToTask3();


    }

    // Update is called once per frame
    void Update () {

        Billboard.GetComponent<BillboardManager>().SetDebugInfo("Task 6 - State: " + substate);

        switch (substate)
        {
            case 1:
                Billboard.HighLight(2);
                Talker.GetComponent<SPHelperTalk>().Speak(Task6VOs[0]);
                substate = 2;
                break;
            case 2:
                if (talkerAnim.FinishedTalking())
                {
                    Talker.GetComponent<SPHelperTalk>().Speak(Task6VOs[1]);
                    RuneOnTile.SetActive(true);
                    TwoRunesInInventory.SetActive(true);
                    substate = 3;
                }

                break;
            case 3:
                if (talkerAnim.FinishedTalking())
                {
                    // Helper.GetComponent<SPHelperTalk>().Speak(Task6VOs[2]); Seems repeted;
                    Controller.canControl = true;
                    Controller.controlMode = 1;
                    Controller.rightHandHoverUI.GetComponentInChildren<UnityEngine.UI.Text>().text = "Move Mode";
                    substate = 4;
                }
                break;
            case 4:
                if (RuneOnTile.GetComponent<moveDetector>().IfTouched())
                {
                    Talker.GetComponent<SPHelperTalk>().Speak(Task6VOs[3]);
                    //Walker.transform.LookAt(Walker.transform.position + new Vector3(-0.2f, 0, 0));

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
                    Talker.GetComponent<SPHelperTalk>().Speak(Task6VOs[4]);
                    Door.GetComponent<Animator>().SetTrigger("DoorOpen");
                    KeyOnInventory.SetActive(false);
                    //Walker.transform.LookAt(Walker.transform.position + new Vector3(-0.2f, 0, 0));
                    iTween.MoveTo(Talker, iTween.Hash("position", NextPlace, "easetype", iTween.EaseType.easeInOutSine));
                    talkerAnim.Success();
                    Billboard.Check(2);

                    substate = 7;
                }
                break;
            case 7:
                if (talkerAnim.FinishedTalking())
                {
                    StartCoroutine(FinishTutorial());
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
