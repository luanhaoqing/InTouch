using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Task0Manager : MonoBehaviour {
    public GameObject Talker;
    public SPControllerOfPlayerOntheBoard Controller;
    public GameObject Billboard;
    public OverallManager overallManager;

    private int substate = 1;
    bool state1Passed;
    bool state2Passed;

    public AudioClip[] VOs;

	// Use this for initialization
	void Start () {
        Billboard.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
        Billboard.GetComponent<BillboardManager>().SetDebugInfo("Task 0 - State: " + substate);
	
        switch (substate)
        {
            case 1:
                if (!state1Passed) {
                    state1Passed = true;
                    Talker.GetComponent<SPHelperTalk>().Speak(VOs[0]);
                }

                if (Talker.GetComponent<SPHelperAnimation>().FinishedTalking())
                {
                    substate = 2;
                }
                break;
                
            case 2:
                Talker.GetComponent<SPHelperTalk>().Speak(VOs[1]);
                Controller.canControl = true;
                Controller.controlMode = 0;
                Controller.SkipButtonFlash();
                substate = 3;
                
                break;

            case 3:
                if (Talker.GetComponent<SPHelperAnimation>().FinishedTalking())
                {
                    overallManager.startTask1();
                    overallManager.destroyStuff(this.gameObject);
                }

                break;
        }

	}
}
