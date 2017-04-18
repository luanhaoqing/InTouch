using UnityEngine;
using System.Collections;

public class SPTutorialStateManager : MonoBehaviour {
    enum TutorialState
    {   begin_idle,
        task0_begin,
        task0_ready_to_skip,
        task0_islands_show,
        task1_move,

        skip_scene }
    int currentState = (int)TutorialState.begin_idle;

    // stuff
    public GameObject player;
    public GameObject helper;
    public GameObject twoMoreTiles;

    float counter = 0;

    // flags for things to happen only once
    bool playedVoice = false;
    bool exitButtonFlashed = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Current Tutorial State: " + currentState );
        switch (currentState)
        {
            case (int)TutorialState.begin_idle:
                // wait for 3 seconds
                if (counter <= 3)
                {
                    counter += Time.deltaTime;
                    return;
                }
                currentState = (int)TutorialState.task0_begin;
                counter = 0;
                break;

            case (int)TutorialState.task0_begin:
                // play helper animation and first voice over
                helper.GetComponent<Animator>().SetBool("fly", true);
                if (!playedVoice)
                {
                    SPAudioCenter.PlayHello();
                    helper.GetComponent<SPHelperAnimation>().SetHelperTalkActive(true, SPAudioCenter.hello.length);
                    playedVoice = true;
                }
                // go to next state
                //StartCoroutine(WaitAndChangeState(SPAudioCenter.hello.length, (int)TutorialState.task0_ready_to_skip));
                if (helper.GetComponent<SPHelperAnimation>().getHelperTalkStatus() == false) {
                    currentState = (int)TutorialState.task0_ready_to_skip;
                    playedVoice = false;
                }

                break;

            case (int)TutorialState.task0_ready_to_skip:
                // turn on control
                player.GetComponent<SPControllerOfPlayerOntheBoard>().SetControlActive(true);

                // play voiceover
                if (!playedVoice)
                {
                    SPAudioCenter.PlaySkipThisPart();
                    helper.GetComponent<SPHelperAnimation>().SetHelperTalkActive(true, SPAudioCenter.skipThisPart.length);
                    playedVoice = true;
                }

                // exit button flash
                if (!exitButtonFlashed)
                {
                    //Fading();
                    StartCoroutine(ButtonFlash());
                    exitButtonFlashed = true;
                }
                // count for five seconds
                // go to next state
                counter += Time.deltaTime;
                if (counter >= 7)
                {
                    currentState = (int)TutorialState.task0_islands_show;
                    playedVoice = false;
                }
                break;

            case (int)TutorialState.task0_islands_show:
                // play voiceover
                if (!playedVoice)
                {
                    SPAudioCenter.PlaymoveToIsland();
                    helper.GetComponent<SPHelperAnimation>().SetHelperTalkActive(true, SPAudioCenter.moveToIsland.length);
                    playedVoice = true;
                    Debug.Log("Length:" + SPAudioCenter.moveToIsland.length);
                }

                // two more islands shows up
                twoMoreTiles.SetActive(true);
                Debug.Log(helper.GetComponent<SPHelperAnimation>().getHelperTalkStatus());
                // wait for voiceover to end
                // go to next state
                /*
                if (helper.GetComponent<SPHelperAnimation>().getHelperTalkStatus() == false)
                {
                    currentState = (int)TutorialState.task1_move;
                    playedVoice = false;
                }*/

                break;

            case (int)TutorialState.task1_move:
                break;

            case (int)TutorialState.skip_scene:
                // fade black

                // change scene
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
                break;
        }

	}

    public void skipScene()
    {
        currentState = (int)TutorialState.skip_scene;
    }


    IEnumerator ButtonFlash()
    {
        Color originalColor = Color.white;
        Color lerpedColor = Color.yellow;

        yield return new WaitForSeconds(0.5f);
        player.GetComponent<SPControllerOfPlayerOntheBoard>().SkipButton.image.color = lerpedColor;
        yield return new WaitForSeconds(1f);

        player.GetComponent<SPControllerOfPlayerOntheBoard>().SkipButton.image.color = originalColor;
        yield return new WaitForSeconds(1f);

        player.GetComponent<SPControllerOfPlayerOntheBoard>().SkipButton.image.color = lerpedColor;
        yield return new WaitForSeconds(1f);

        player.GetComponent<SPControllerOfPlayerOntheBoard>().SkipButton.image.color = originalColor;
        yield return new WaitForSeconds(1f);

        player.GetComponent<SPControllerOfPlayerOntheBoard>().SkipButton.image.color = lerpedColor;
        yield return new WaitForSeconds(1f);

        player.GetComponent<SPControllerOfPlayerOntheBoard>().SkipButton.image.color = originalColor;
        yield return new WaitForSeconds(1f);

    }

}
