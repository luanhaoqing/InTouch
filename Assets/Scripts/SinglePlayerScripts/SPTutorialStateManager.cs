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
        task3_send_item,
        task3_send_item_well_done,
        task3_action_point_light,
        task4_island_health,
        task4_fail,
        task4_success,
        task5_clock,
        task5_fly_to_clock,
        task6_begin,

        skip_scene }
    public int currentState = (int)TutorialState.begin_idle;

    // stuff
    public GameObject player;
    public GameObject helper;
    public GameObject helperSuccessAnim;
    public GameObject twoMoreTiles;
    public GameObject healItem;
    public GameObject inventory;
    public GameObject crystalOnInv;
    public GameObject inventoryOther;
    public GameObject crystalOnInvOther;
    public GameObject actionPoints;
    public GameObject healthPoints;
    public GameObject crystalToBreak;
    public GameObject clock;
    public GameObject[] crystalsToBreakByClock;
    public GameObject fallingIsland;
    public GameObject[] crystalsToBreakByClockSecond;


    float counter = 0;

    // flags for things to happen only once
    bool playedVoice = false;
    bool exitButtonFlashed = false;
    bool moveButtonFlashed = false;
    bool sendButtonFlashed = false;
    bool itemShown = false;
    bool clockFirstAnimPlayed = false;

    public bool firstMoveComplete = false;
    public bool getItemComplete = false;
    public bool sendItemComplete = false;
    public bool islandHealthComplete = false;
    bool islandHealthFail = false;

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
                helperSuccessAnim.SetActive(true);

                // helper flies to the inventory
                iTween.MoveTo(helper, iTween.Hash("position", new Vector3(0f, 0.0671f, 0.5f), "easetype", iTween.EaseType.easeInOutSine));
                currentState += 1;
                break;

            case (int)TutorialState.task2_show_item:

                // wait for 2 seconds
                counter += Time.deltaTime;
                if (counter <= 2)
                {
                    return;
                }

                if (!itemShown)
                {
                    itemShown = true;
                    // rise crystal a bit when introduced.
                    iTween.MoveAdd(crystalOnInv, iTween.Hash("y", 0.05f, "easytype", iTween.EaseType.easeInOutSine));
                    // play voice: here's the magical crystal.
                    SPAudioCenter.PlayreviveItem();
                    helper.GetComponent<SPHelperAnimation>().SetHelperTalkActive(true, SPAudioCenter.reviveItem.length);
                }

                // when finished talking
                if (itemShown && (helper.GetComponent<SPHelperAnimation>().getHelperTalkStatus() == false))
                {
                    // go to next state
                    currentState += 1;
                    counter = 0;
                }

                break;

            case (int)TutorialState.task3_send_item:
                // send button flash
                if (!sendButtonFlashed)
                {
                    StartCoroutine(sendButtonFlash());
                    sendButtonFlashed = true;
                }

                // other inventory shows up
                healItem.SetActive(false);
                inventoryOther.SetActive(true);

                // play voice: uh oh
                if (!playedVoice)
                {
                    playedVoice = true;
                    SPAudioCenter.PlaySendItemFriend();
                    helper.GetComponent<SPHelperAnimation>().SetHelperTalkActive(true, SPAudioCenter.sendItemFriend.length);
                }

                // prompt every five seconds
                counter += Time.deltaTime;
                if (counter > (5 + SPAudioCenter.sendItemReminder.length))
                {
                    //play send item friend reminder
                    SPAudioCenter.PlaySendItemReminder();
                    helper.GetComponent<SPHelperAnimation>().SetHelperTalkActive(true, SPAudioCenter.sendItemReminder.length);
                    counter = 0;
                }


                // if item sent, wait for talk to finish
                if (sendItemComplete)
                {
                    crystalOnInv.SetActive(false);
                    crystalOnInvOther.SetActive(true);
                    if (helper.GetComponent<SPHelperAnimation>().getHelperTalkStatus() == false)
                    {
                        playedVoice = false;
                        counter = 0;
                        currentState += 1;
                    }
                }


                break;

            case (int)TutorialState.task3_send_item_well_done:
                // chosen item disappears & shows up at the other inventory

                if (!playedVoice)
                {
                    // rise a bit of other item
                    iTween.MoveAdd(crystalOnInvOther, iTween.Hash("y", 0.05f, "easytype", iTween.EaseType.easeInOutSine));
                    // say "well done"
                    playedVoice = true;
                    SPAudioCenter.PlayGoodJobToSend();
                    helper.GetComponent<SPHelperAnimation>().SetHelperTalkActive(true, SPAudioCenter.goodJobToSend.length);
                }

                // finish talk and go to next state
                if (playedVoice && (helper.GetComponent<SPHelperAnimation>().getHelperTalkStatus() == false)) 
                {
                    playedVoice = false;
                    currentState += 1;
                }

                break;

            case (int)TutorialState.task3_action_point_light:

                // play voice
                if (!playedVoice)
                {
                    playedVoice = true;
                    SPAudioCenter.PlayHandMenu();
                    helper.GetComponent<SPHelperAnimation>().SetHelperTalkActive(true, SPAudioCenter.handMenu.length);
                }

                // action points start to go off one by one
                counter += Time.deltaTime;
                if (counter >= 2)
                {
                    actionPoints.transform.GetChild(0).gameObject.SetActive(false);
                }
                if (counter >= 4)
                {
                    actionPoints.transform.GetChild(1).gameObject.SetActive(false);
                }
                if (counter >= 6)
                {
                    actionPoints.transform.GetChild(2).gameObject.SetActive(false);
                }

                // after talking, go to next
                if (playedVoice && helper.GetComponent<SPHelperAnimation>().getHelperTalkStatus() == false)
                {
                    actionPoints.transform.GetChild(0).gameObject.SetActive(true);
                    actionPoints.transform.GetChild(1).gameObject.SetActive(true);
                    actionPoints.transform.GetChild(2).gameObject.SetActive(true);
                    counter = 0;
                    playedVoice = false;

                    currentState += 1;
                }
                break;

            case (int)TutorialState.task4_island_health:
                // play voice: island health
                if (!playedVoice)
                {
                    playedVoice = true;
                    SPAudioCenter.PlayLearnTileHealth();
                    helper.GetComponent<SPHelperAnimation>().SetHelperTalkActive(true, SPAudioCenter.learnTileHealth.length);
                    
                    // helper flys to center island.
                    iTween.MoveTo(helper, iTween.Hash("position", new Vector3(0.18f, 0.0671f, 0.65f), "easytype", iTween.EaseType.easeInOutSine));

                    // two islands shows different health status.
                    healthPoints.SetActive(true);

                    // allow movement now.

                    // right move: set islandHealthCompele to true.

                    // wrong move: set islandHealthFail tp true.



                }

                if (islandHealthComplete && helper.GetComponent<SPHelperAnimation>().getHelperTalkStatus()==false)
                {
                    playedVoice = false;
                    //go to successful state
                    currentState = (int)TutorialState.task4_success;
                }
                
                if (islandHealthFail)
                {
                    playedVoice = false;
                    currentState = (int)TutorialState.task4_fail;
                    // go to fail state
                }

                break;

            case (int)TutorialState.task4_fail:
                Debug.Log("Island health task - failed");
                break;

            case (int)TutorialState.task4_success:
                if (!playedVoice)
                {
                    playedVoice = true;
                    // say good choice.
                    SPAudioCenter.PlayGoodJobMovingToSafeTile();
                    helper.GetComponent<SPHelperAnimation>().SetHelperTalkActive(true, SPAudioCenter.goodJobMovingToSafeTile.length);
                }

                if (playedVoice && helper.GetComponent<SPHelperAnimation>().getHelperTalkStatus() == false)
                {
                    playedVoice = false;
                    // next state
                    currentState += 1;
                }

                break;

            case (int)TutorialState.task5_clock:
                // clock shows up.
                clock.SetActive(true);
                if (!clockFirstAnimPlayed)
                {
                    clockFirstAnimPlayed = true;
                    // move the clock high
                    iTween.MoveAdd(clock, iTween.Hash("y", 1f, "easytype", iTween.EaseType.easeInOutSine));

                    // play the round over anim
                    clock.GetComponent<Clock>().DecreaseTurn();
                    clock.GetComponent<Clock>().DecreaseTurn();
                    clock.GetComponent<Clock>().DecreaseTurn();
                    clock.GetComponent<Clock>().DecreaseTurn();

                    // reduce all island health
                    foreach (GameObject crystal in crystalsToBreakByClock)
                    {
                        crystal.GetComponent<Animator>().SetTrigger("breakdown");
                    }

                    // make first island fall
                    fallingIsland.GetComponent<Animator>().SetTrigger("Break");
                }

                if (!playedVoice)
                {
                    playedVoice = true;
                    // say lost health
                    SPAudioCenter.PlayAllTilesLostHealth();
                    helper.GetComponent<SPHelperAnimation>().SetHelperTalkActive(true, SPAudioCenter.goodJobMovingToSafeTile.length);
                }

                if (playedVoice && helper.GetComponent<SPHelperAnimation>().getHelperTalkStatus() == false)
                {
                    playedVoice = false;
                    // next stage
                    currentState += 1;
                }

                    break;

            case (int)TutorialState.task5_fly_to_clock:

                Debug.Log(" --- Last State ---");
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

    IEnumerator sendButtonFlash()
    {
        Color originalColor = Color.white;
        Color lerpedColor = Color.yellow;
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<SPControllerOfPlayerOntheBoard>().SendButton.image.color = lerpedColor;
        yield return new WaitForSeconds(1f);
        player.GetComponent<SPControllerOfPlayerOntheBoard>().SendButton.image.color = originalColor;
        yield return new WaitForSeconds(1f);
        player.GetComponent<SPControllerOfPlayerOntheBoard>().SendButton.image.color = lerpedColor;
        yield return new WaitForSeconds(1f);
        player.GetComponent<SPControllerOfPlayerOntheBoard>().SendButton.image.color = originalColor;
        yield return new WaitForSeconds(1f);
        player.GetComponent<SPControllerOfPlayerOntheBoard>().SendButton.image.color = lerpedColor;
        yield return new WaitForSeconds(1f);
        player.GetComponent<SPControllerOfPlayerOntheBoard>().SendButton.image.color = originalColor;
        yield return new WaitForSeconds(1f);
    }

    IEnumerator playClockAnim()
    {
        float waitTime = 2f;

        clock.GetComponent<Clock>().DecreaseTurn();
        yield return new WaitForSeconds(waitTime);

        clock.GetComponent<Clock>().DecreaseTurn();
        yield return new WaitForSeconds(waitTime);

        clock.GetComponent<Clock>().DecreaseTurn();
        yield return new WaitForSeconds(waitTime);

        clock.GetComponent<Clock>().DecreaseTurn();
        yield return new WaitForSeconds(waitTime);

    }
}
