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

    public AudioClip[] Task2VOs;

    public GameObject Tiles;
    public moveDetector ItemMoveDetector;
    public GameObject ItemOnBoard;
    public GameObject Inventory;
    public GameObject ItemOnInventory;
    public Transform InventorySidePlace;
    public moveDetector AtInventoryMoveDetector;



    // Use this for initialization
    void Start()
    {
        Helper.transform.position = helperInitialPosition.position;
        Helper.transform.rotation = helperInitialPosition.rotation;

        helperAnim = Helper.GetComponent<SPHelperAnimation>();

        Controller.canControl = false;
        Controller.controlMode = 4;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Task 2 - state: " + substate);

        switch (substate) { 
            case 1:
                Helper.GetComponent<SPHelperTalk>().Speak(Task2VOs[0]);
                Tiles.SetActive(true);
                ItemOnBoard.SetActive(true);
                Billboard.HighLight(1);
                substate = 2;
                break;
            case 2:
                Controller.canControl = true;
                Controller.controlMode = 1; // can move
                substate = 3;
                break;
            case 3:
                if (ItemMoveDetector.IfTouched())
                {
                    ItemOnBoard.SetActive(false);
                    Inventory.SetActive(true);
                    ItemOnInventory.SetActive(true);
                    StartCoroutine(HelperFlyToInventory());

                    helperAnim.Success();
                    Helper.GetComponent<SPHelperTalk>().SaySuccess();

                    substate = 4;
                }
                break;
            case 4:
                if (AtInventoryMoveDetector.IfTouched())
                {
                    Helper.GetComponent<SPHelperTalk>().Speak(Task2VOs[1]);
                    substate = 5;
                }
                break;
            case 5:
                if (helperAnim.FinishedTalking())
                {
                    Billboard.Check(1);
                    overallManager.startTask3();
                    overallManager.destroyStuff(this.gameObject);
                }
                break;
        }
    }

    IEnumerator HelperFlyToInventory()
    {
        yield return new WaitForSeconds(1f);
        Helper.transform.LookAt(Helper.transform.position + new Vector3(-0.2f, 0, 0));
        iTween.MoveTo(Helper, iTween.Hash("position", InventorySidePlace, "easetype", iTween.EaseType.easeInOutSine));
    }
}

