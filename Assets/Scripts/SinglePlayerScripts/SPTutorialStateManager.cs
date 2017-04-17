using UnityEngine;
using System.Collections;

public class SPTutorialStateManager : MonoBehaviour {
    enum TutorialState {task0_begin, task0_ready_to_skip, skip_scene }
    int currentState = (int)TutorialState.task0_begin;

    // stuff
    public GameObject helper;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        switch (currentState)
        {
            case (int)TutorialState.task0_begin:
                // play first voice over
                helper.GetComponent<Animator>().SetBool("fly", true);
                // go to next state
                currentState = (int)TutorialState.task0_ready_to_skip;
                break;

            case (int)TutorialState.task0_ready_to_skip:
                // play voiceover

                // exit button flash

                // count for five seconds
                    // go to next state

                break;


            case (int)TutorialState.skip_scene:
                // fade black

                // change scene
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
                break;





        }

	}


}
