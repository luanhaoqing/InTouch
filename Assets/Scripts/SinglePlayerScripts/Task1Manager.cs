using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Task1Manager : MonoBehaviour
{
    public GameObject Helper;
    SPHelperAnimation helperAnim;
    public SPControllerOfPlayerOntheBoard Controller;
    public Transform CameraPosition;
    public BillboardManager Billboard;
    public OverallManager overallManager;
    public Transform helperInitialPosition;
    private int substate = 1;

    public AudioClip[] Task1VOs;

    public GameObject ThreeMoreIslands;
    public moveDetector FirstMoveDetector;
    public GameObject firstCrystal;
    public moveDetector SecondMoveDetector;
    public GameObject secondCrystal;
    public GameObject islandToFall;
    private Transform islandPosBeforeFall;
    public GameObject islandToReturn;
    public moveDetector ThirdMoveDetector;
    public GameObject thirdCrystal;
    public GameObject HealItemOnBoard;
    public GameObject Inventory;
    public Transform InventorySidePlace;
    public GameObject HealItemOnInv;


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
        Billboard.GetComponent<BillboardManager>().SetDebugInfo("Task 1 - State: " + substate);

        switch (substate)
        {
            case 1:
                Helper.GetComponent<SPHelperTalk>().Speak(Task1VOs[0]);
                Billboard.HighLight(0);
                substate = 2;
                break;
            case 2:
                if (FirstMoveDetector.IfTouched())
                {
                    // 1st island lose 1 health.
                    StartCoroutine(BreakCrystal(firstCrystal));
                    // play VO Go get item
                    Helper.GetComponent<SPHelperTalk>().Speak(Task1VOs[1]);

                    substate = 3;
                }
                break;
            case 3:
                if (SecondMoveDetector.IfTouched())
                {
                    // crystal break and island fall
                    islandPosBeforeFall = islandToFall.transform;
                    StartCoroutine(BreakCrystal(secondCrystal));
                    StartCoroutine(IslandFall());

                    // VO island HP
                    Helper.GetComponent<SPHelperTalk>().Speak(Task1VOs[2]);
                    substate = 4;

                }
                break;
            case 4:
                if (helperAnim.FinishedTalking())
                {
                    // VO move again
                    Helper.GetComponent<SPHelperTalk>().Speak(Task1VOs[3]);

                    // island with full health return
                    islandToReturn.SetActive(true);
                    iTween.MoveTo(islandToReturn, iTween.Hash("position", islandPosBeforeFall, "easetype", iTween.EaseType.easeInOutSine));

                    substate = 5;
                }
                break;
            case 5:
                if (ThirdMoveDetector.IfTouched())
                {
                    // 3st island lose 1 health.
                    StartCoroutine(BreakCrystal(thirdCrystal));

                    // VO got item, helper fly
                    Helper.GetComponent<SPHelperTalk>().Speak(Task1VOs[4]);
                    StartCoroutine(HelperFlyToInventory());

                    // item appear on inv
                    HealItemOnBoard.SetActive(false);
                    Inventory.SetActive(true);
                    HealItemOnInv.SetActive(true);

                    substate = 6;
                }
                break;
            case 6:
                if (helperAnim.FinishedTalking())
                {
                    Billboard.Check(0);
                    Helper.GetComponent<SPHelperTalk>().Speak(Task1VOs[5]);
                    substate = 7;
                }
                break;
            case 7:
                if (helperAnim.FinishedTalking())
                {
                    substate = 8;
                }
                break;
            case 8:
                overallManager.startTask2();
                overallManager.destroyStuff(this.gameObject);
                break;
        }

    }

    IEnumerator WaitAndNextState(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        substate = 8;
    }

    IEnumerator BreakCrystal(GameObject crystal)
    {
        crystal.GetComponent<Animator>().SetTrigger("breakdown");
        yield return new WaitForSeconds(0.9f);
        crystal.SetActive(false);
        yield return null;
    }

    IEnumerator BreakyManyCrystals(GameObject[] crystalist)
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
        yield return new WaitForSeconds(0.95f);
        islandToFall.GetComponent<Animator>().SetTrigger("Break");
        yield return new WaitForSeconds(0.8f);
        islandToFall.SetActive(false);
    }

    IEnumerator HelperFlyToInventory()
    {
        yield return new WaitForSeconds(1f);
        Helper.transform.LookAt(Helper.transform.position + new Vector3(-0.2f, 0, 0));
        iTween.MoveTo(Helper, iTween.Hash("position", InventorySidePlace, "easetype", iTween.EaseType.easeInOutSine));
    }

}
