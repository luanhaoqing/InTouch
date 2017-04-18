using UnityEngine;
using System.Collections;

public class SPTutorialStateManager : MonoBehaviour {
    enum TutorialState
    {   begin_idle,
        task0_begin,
        task0_ready_to_skip,
        task0_islands_show,
        task1_move,
        task2_get_item,
        task2_inventory_up,
        task2_show_item,

        skip_scene }
    int currentState = (int)TutorialState.begin_idle;

    // stuff
    public GameObject player;
    public GameObject helper;
    public GameObject twoMoreTiles;
    public GameObject healItem;
    public GameObject inventory;

    float counter = 0;

    // flags for things to happen only once
    bool playedVoice = false;
    bool exitButtonFlashed = false;
    bool moveButtonFlashed = false;
    public bool firstMoveComplete = false;
    public bool getItemComplete = false;

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
                    StartCoroutine(skipButtonFlash());
                    exitButtonFlashed = true;
                }
                // count for five seconds
                // go to next state
                counter += Time.deltaTime;
                if (counter >= 7)
                {
                    currentState = (int)TutorialState.task0_islands_show;
                    playedVoice = false;
                    counter = 0;
                }
                break;

            case (int)TutorialState.task0_islands_show:
                // play voiceover
                if (!playedVoice)
                {
                    SPAudioCenter.PlaymoveToIsland();
                    helper.GetComponent<SPHelperAnimation>().SetHelperTalkActive(true, SPAudioCenter.moveToIsland.length);
                    playedVoice = true;
                }

                // two more islands shows up
                twoMoreTiles.SetActive(true);
                // wait for voiceover to end
                // go to next state
                
                if (helper.GetComponent<SPHelperAnimation>().getHelperTalkStatus() == false)
                {
                    currentState = (int)TutorialState.task1_move;
                    playedVoice = false;
                }

                break;

            case (int)TutorialState.task1_move:
                // move button flash yellow
                if (!moveButtonFlashed)
                {
                    StartCoroutine(moveButtonFlash());
                    moveButtonFlashed = true;
                    // disable other buttons
                }

                // can move now

                counter += Time.deltaTime;
                // play prompt VO every 5 seconds
                if (counter > (5+ SPAudioCenter.moveToIslandReminder.length))
                {
                    //play reminder
                    SPAudioCenter.PlaymoveToIslandReminder();
                    helper.GetComponent<SPHelperAnimation>().SetHelperTalkActive(true, SPAudioCenter.moveToIslandReminder.length);
                    counter = 0;
                }

                // if move completed:
                // move to next stage
                if (firstMoveComplete)
                {
                    currentState = (int)TutorialState.task2_get_item;
                    // voice: you are natural!
                    SPAudioCenter.PlaypositiveFeedback1();
                    helper.GetComponent<SPHelperAnimation>().SetHelperTalkActive(true, SPAudioCenter.positiveFeedback1.length);
                    moveButtonFlashed = false;
                }
                break;

            case (int)TutorialState.task2_get_item:
                // an item shows up
                healItem.SetActive(true);

                // move button flash
                if (!moveButtonFlashed)
                {
                    StartCoroutine(moveButtonFlash());
                    moveButtonFlashed = true;
                }

                // wait for 2 seconds and play voice: look at items!
                // 
                if (!playedVoice)
                {
                    SPAudioCenter.PlaylookAtItems();
                    helper.GetComponent<SPHelperAnimation>().SetHelperTalkActive(true, SPAudioCenter.lookAtItems.length);
                    playedVoice = true;
                }

                // play prompt VO every 5 seconds
                counter += Time.deltaTime;
                if (counter > (5 + SPAudioCenter.getItemsReminder.length))
                {
                    //play reminder
                    SPAudioCenter.PlaygetItemsReminder();
                    helper.GetComponent<SPHelperAnimation>().SetHelperTalkActive(true, SPAudioCenter.getItemsReminder.length);
                    counter = 0;
                }

                // if got item
                if (getItemComplete)
                {
                    // play "well done!"
                    SPAudioCenter.PlaypositiveFeedback2();
                    helper.GetComponent<SPHelperAnimation>().SetHelperTalkActive(true, 1f);
                    playedVoice = false;
                    counter = 0;
                    // go to next state
                    currentState += 1;
                }

                break;

            case (int)TutorialState.task2_inventory_up:
                // inventory shows up
                inventory.SetActive(true);
                // helper do successful animation.
                helper.GetComponent<Animator>().SetTrigger("success");
                // helper flies to the inventory
                iTween.MoveTo(helper, iTween.Hash("position", new Vector3(0f, 0.0671f, 0.5f), "easetype", iTween.EaseType.easeInOutSine));
                currentState += 1;
                break;

            case (int)TutorialState.task2_show_item:
                Debug.Log("last state");
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


    IEnumerator skipButtonFlash()
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

    IEnumerator moveButtonFlash()
    {
        Color originalColor = Color.white;
        Color lerpedColor = Color.yellow;
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<SPControllerOfPlayerOntheBoard>().MoveButton.image.color = lerpedColor;
        yield return new WaitForSeconds(1f);
        player.GetComponent<SPControllerOfPlayerOntheBoard>().MoveButton.image.color = originalColor;
        yield return new WaitForSeconds(1f);
        player.GetComponent<SPControllerOfPlayerOntheBoard>().MoveButton.image.color = lerpedColor;
        yield return new WaitForSeconds(1f);
        player.GetComponent<SPControllerOfPlayerOntheBoard>().MoveButton.image.color = originalColor;
        yield return new WaitForSeconds(1f);
        player.GetComponent<SPControllerOfPlayerOntheBoard>().MoveButton.image.color = lerpedColor;
        yield return new WaitForSeconds(1f);
        player.GetComponent<SPControllerOfPlayerOntheBoard>().MoveButton.image.color = originalColor;
        yield return new WaitForSeconds(1f);
    }
}
