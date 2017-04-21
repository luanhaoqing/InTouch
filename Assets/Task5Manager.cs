using UnityEngine;
using System.Collections;

public class Task5Manager : MonoBehaviour
{

    public GameObject Helper;
    SPHelperAnimation helperAnim;
    public SPControllerOfPlayerOntheBoard Controller;
    public BillboardManager Billboard;
    public OverallManager overallManager;
    public Transform helperInitialPosition;

    private int substate = 1;
    private bool ClockAnimDone;

    public AudioClip[] Task5VOs;

    public GameObject islandToFall;
    public GameObject[] firstBreakCrystals;
    public GameObject Clock;
    public Transform ClockSidePlace;
    public GameObject[] secondBreakCrystals;
    public Transform NextPlace;



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
        Billboard.GetComponent<BillboardManager>().SetDebugInfo("Task 5 - State: " + substate);

        switch (substate)
        {
            case 1:
                Billboard.HighLight(4);
                StartCoroutine(DestroyCrystal(firstBreakCrystals));
                StartCoroutine(IslandFall());
                substate = 2;
                break;

            case 2:
                Helper.GetComponent<SPHelperTalk>().Speak(Task5VOs[0]);
                StartCoroutine(HelperFlyToClock());

                substate = 3;
                break;

            case 3:
                if (helperAnim.FinishedTalking())
                {
                    Helper.GetComponent<SPHelperTalk>().Speak(Task5VOs[1]);
                    //StartCoroutine(DestroyCrystal(secondBreakCrystals));
                    StartCoroutine(playClockAnim());
                    substate = 4;
                }
                break;

            case 4:
                if (helperAnim.FinishedTalking() && ClockAnimDone)
                {
                    Billboard.Check(4);
                    helperAnim.Success();
                    StartCoroutine(FinishTask5());
                    substate = -1;
                }
                break;
        }


    }

    IEnumerator HelperFlyToClock()
    {
        Clock.SetActive(true);
        iTween.MoveAdd(Clock, iTween.Hash("y", 1f, "easytype", iTween.EaseType.easeInOutSine));
        AudioCenter.PlayRoundOver();

        yield return new WaitForSeconds(1f);
        Helper.transform.LookAt(Helper.transform.position + new Vector3(-0.2f, 0, 0));
        iTween.MoveTo(Helper, iTween.Hash("position", ClockSidePlace, "easetype", iTween.EaseType.easeInOutSine));
    }

    IEnumerator DestroyCrystal(GameObject[] crystalist)
    {
        foreach (GameObject crystal in crystalist)
        {
            crystal.GetComponent<Animator>().SetTrigger("breakdown");
        }
        yield return new WaitForSeconds(0.9f);
        foreach (GameObject crystal in crystalist)
        {
            crystal.SetActive(false);
        }

    }

    IEnumerator IslandFall()
    {
        islandToFall.GetComponent<Animator>().SetTrigger("Break");
        yield return new WaitForSeconds(0.95f);
        islandToFall.SetActive(false);
    }

    IEnumerator playClockAnim()
    {
        float waitTime = 2.5f;

        yield return new WaitForSeconds(6f);
        Clock.GetComponent<Clock>().DecreaseTurn();
        yield return new WaitForSeconds(waitTime);
        Clock.GetComponent<Clock>().DecreaseTurn();
        yield return new WaitForSeconds(waitTime);
        Clock.GetComponent<Clock>().DecreaseTurn();
        yield return new WaitForSeconds(waitTime);
        Clock.GetComponent<Clock>().DecreaseTurn();
        ClockAnimDone = true;
        StartCoroutine(DestroyCrystal(secondBreakCrystals));

    }

    IEnumerator FinishTask5()
    {
        yield return new WaitForSeconds(0.8f);
        Helper.transform.LookAt(Helper.transform.position + new Vector3(-0.2f, 0, 0));
        iTween.MoveTo(Helper, iTween.Hash("position", NextPlace, "easetype", iTween.EaseType.easeInOutSine));
        yield return new WaitForSeconds(0.8f);
        overallManager.startTask6();
        overallManager.destroyStuff(this.gameObject);
    }



}
