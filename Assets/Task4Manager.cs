using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Task4Manager : MonoBehaviour
{
    public GameObject Helper;
    SPHelperAnimation helperAnim;
    public SPControllerOfPlayerOntheBoard Controller;
    public BillboardManager Billboard;
    public OverallManager overallManager;
    public Transform helperInitialPosition;
    private int substate = 1;

    public AudioClip[] Task4VOs;

    public GameObject HealthPoints;
    public GameObject oneCrystalToBreak;
    public moveDetector IslandHealthMoveDetector;
    public Transform IslandHealthMoveDetectorPosition;



    // Use this for initialization
    void Start()
    {
        Helper.transform.position = helperInitialPosition.position;
        Helper.transform.rotation = helperInitialPosition.rotation;

        helperAnim = Helper.GetComponent<SPHelperAnimation>();

        Controller.canControl = false;
        Controller.rightHandHoverUI.GetComponentInChildren<UnityEngine.UI.Text>().text = "Send Mode";
        Controller.controlMode = 4;

    }

    // Update is called once per frame
    void Update()
    {
        Billboard.GetComponent<BillboardManager>().SetDebugInfo("Task 4 - State: " + substate);

        switch (substate)
        {
            case 1:
                Helper.GetComponent<SPHelperTalk>().Speak(Task4VOs[0]);
                HealthPoints.SetActive(true);
                Billboard.HighLight(3);
                substate = 2;
                break;
            case 2:
                if (helperAnim.FinishedTalking())
                {
                    Helper.GetComponent<SPHelperTalk>().Speak(Task4VOs[1]);
                    Controller.canControl = true;
                    substate = 3;
                }
                break;
            case 3:
                if (Controller.controlMode == 0)
                {
                    Helper.GetComponent<SPHelperTalk>().Speak(Task4VOs[2]);
                    substate = 4;
                }
                break;
            case 4:
                if (IslandHealthMoveDetector.IfTouched())
                {
                    Helper.GetComponent<SPHelperTalk>().Speak(Task4VOs[3]);
                    StartCoroutine(BreakCrystal());
                    // turn helper around
                    Controller.canControl = false;
                    Helper.transform.LookAt(Helper.transform.position + new Vector3(-0.2f, 0, 0));

                    helperAnim.Success();

                    substate = 5;
                }
                break;
            case 5:
                if (helperAnim.FinishedTalking())
                {
                    Billboard.Check(3);

                    StartCoroutine(FinishTask4());
                    substate = -1;
                }
                break;
        }
    }

    IEnumerator FinishTask4()
    {
        yield return new WaitForSeconds(0.8f);
        overallManager.startTask5();
        overallManager.destroyStuff(this.gameObject);
    }

    IEnumerator BreakCrystal()
    {
        oneCrystalToBreak.GetComponent<Animator>().SetTrigger("breakdown");
        oneCrystalToBreak.SetActive(false);
        yield return null;
    }
}
