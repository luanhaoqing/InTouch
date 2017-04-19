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
        task6_runes,
        task6_key,
        task6_door,

        before_skip_scene,
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
    public Transform clockSidePlace;
    public GameObject[] crystalsToBreakByClock;
    public GameObject fallingIsland;
    public GameObject[] crystalsToBreakByClockSecond;
    public moveDetector taskOneDetector;
    public moveDetector taskTwoDetector;
    public touchDetector taskThreeDetector;
    public moveDetector taskFourDetector;


    float counter = 0;
    float counter2 = 0;

    // flags for things to happen only once
    bool playedVoice = false;
    bool exitButtonFlashed = false;
    bool moveButtonFlashed = false;
    bool sendButtonFlashed = false;
    bool itemShown = false;
    bool clockFirstAnimPlayed = false;
    bool readyToSkipScene = false;

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

                // detect collision for task 1
                if (taskOneDetector.IfTouched() && firstMoveComplete == false)
                {
                    firstMoveComplete = true;
                }

                // if move completed:
                // move to next stage
                if (firstMoveComplete && helper.GetComponent<SPHelperAnimation>().getHelperTalkStatus() == false)
                {
                    currentState = (int)TutorialState.task2_get_item;
                    // voice: you are natural!
                    SPAudioCenter.PlaypositiveFeedback1();
                    helper.GetComponent<SPHelperAnimation>().SetHelperTalkActive(true, SPAudioCenter.positiveFeedback1.length);
                    moveButtonFlashed = false;
                    StartCoroutine(playHelperSuccessAnim());
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

                // check if got item
                if (taskTwoDetector.IfTouched() && getItemComplete==false)
                {
                    getItemComplete = true;
                    
                }


                // if got item
                if (getItemComplete)
                {
                    counter2 += Time.deltaTime;

                    if (counter2 > 1f) { 
                        StartCoroutine(playHelperSuccessAnim());
                        // play "well done!"
                        SPAudioCenter.PlaypositiveFeedback2();
                        helper.GetComponent<SPHelperAnimation>().SetHelperTalkActive(true, 1f);
                        playedVoice = false;
                        counter = 0;
                        counter2 = 0;

                        // helper face right direction
                        helper.transform.LookAt(helper.transform.position + new Vector3(0f, 0, -0.2f));

                        // go to next state
                        currentState += 1;
                    }
                }

                break;

            case (int)TutorialState.task2_inventory_up:
                // turn off control
                player.GetComponent<SPControllerOfPlayerOntheBoard>().canControl = false;

                // inventory shows up
                inventory.SetActive(true);
                healItem.SetActive(false);
                // helper do successful animation.
                StartCoroutine(playHelperSuccessAnim());

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
                    player.GetComponent<SPControllerOfPlayerOntheBoard>().SetMenuActive(true);
                    StartCoroutine(sendButtonFlash());
                    sendButtonFlashed = true;
                }

                // other inventory shows up
                
                inventoryOther.SetActive(true);

                // play voice: uh oh
                if (!playedVoice)
                {
                    playedVoice = true;
                    SPAudioCenter.PlaySendItemFriend();
                    helper.GetComponent<SPHelperAnimation>().SetHelperTalkActive(true, SPAudioCenter.sendItemFriend.length);
                    // can send item
                    player.GetComponent<SPControllerOfPlayerOntheBoard>().sendItemDisabled = false;
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

                

                // check if item sent
                if (taskThreeDetector.IfTouched() && sendItemComplete == false)
                {
                    sendItemComplete = true;
                }

                // if item sent, wait for talk to finish
                if (sendItemComplete)
                {
                    // cannot send anymore
                    player.GetComponent<SPControllerOfPlayerOntheBoard>().sendItemDisabled = true;
                    player.GetComponent<SPControllerOfPlayerOntheBoard>().SetMenuActive(true);

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
                    player.GetComponent<SPControllerOfPlayerOntheBoard>().canControl = true;
                }

                // right move: set islandHealthCompele to true.
                if (taskFourDetector.IfTouched())
                {
                    islandHealthComplete = true;
                }

                // wrong move: set islandHealthFail tp true.
                

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
                bool breakdownPlayed = false;
                counter += Time.deltaTime;
                clock.SetActive(true);
                if (!clockFirstAnimPlayed)
                {
                    clockFirstAnimPlayed = true;
                    // move the clock high
                    iTween.MoveAdd(clock, iTween.Hash("y", 1f, "easytype", iTween.EaseType.easeInOutSine));

                    // play the round over sound
                    AudioCenter.PlayRoundOver();

                    // reduce all island health
                    foreach (GameObject crystal in crystalsToBreakByClock)
                    {
                        crystal.GetComponent<Animator>().SetTrigger("breakdown");
                        breakdownPlayed = true;
                    }

                    // make first island fall
                    fallingIsland.GetComponent<Animator>().SetTrigger("Break");
                }

                if (counter >= 0.85f)
                {
                    foreach (GameObject crystal in crystalsToBreakByClock)
                    {
                        crystal.SetActive(false);
                    }
                }

                if (counter >= 1.7f)
                {
                    fallingIsland.SetActive(false);
                    breakdownPlayed = false;
                }

                if (!playedVoice)
                {
                    playedVoice = true;
                    // say lost health
                    SPAudioCenter.PlayAllTilesLostHealth();
                    helper.GetComponent<SPHelperAnimation>().SetHelperTalkActive(true, SPAudioCenter.allTilesLostHealth.length);
                }

                if (playedVoice && helper.GetComponent<SPHelperAnimation>().getHelperTalkStatus() == false)
                {
                    playedVoice = false;
                    counter = 0;
                    // next stage
                    currentState += 1;
                }

                    break;

            case (int)TutorialState.task5_fly_to_clock:
                if (!playedVoice)
                {
                    playedVoice = true;

                    // helper fly to clock
                    iTween.MoveTo(helper, iTween.Hash("position", clockSidePlace, "easytype", iTween.EaseType.easeInOutSine));

                    // play voice: instuction of clock
                    SPAudioCenter.PlayClockInstruction();
                    helper.GetComponent<SPHelperAnimation>().SetHelperTalkActive(true, SPAudioCenter.clockInstruction.length);

                    // play another round of clock animation
                    StartCoroutine(playClockAnim());

                }

                if (playedVoice && helper.GetComponent<SPHelperAnimation>().getHelperTalkStatus() == false)
                {
                    playedVoice = false;
                    //next state
                    currentState += 1;
                }

                break;

            case (int)TutorialState.task6_begin:
                // do once:
                    // helper flies back to island
                    // door and a key show up

                // next state
                break;
            case (int)TutorialState.task6_runes:
                // do once:
                    // two runes show up in item
                    // an island with rune show up

                // wait for desired input:
                    // if rune detect touch

                // if rune touched:
                    // rune goes to item
                    // move to next state
                break;

            case (int)TutorialState.task6_key:
                // wait for desired input:
                    // if key detect touch

                // if key touched:
                    // rune goes to item
                    // key goes to item
                    // move to next state

                
                break;

            case (int)TutorialState.task6_door:
                // do once:
                    // play VO

                // wait for desired input:
                    // if door detect touch

                // if door touched
                    // play final VO
                    // play door animation
                    // play helper fly through door (later)
                    // go to next state
                    

            case (int)TutorialState.before_skip_scene:
                Debug.Log(" --- Last State Before Skip ---");
               
                if (!playedVoice)
                {
                    playedVoice = true;
                    StartCoroutine(WaitBeforeSkipScene());
                }

                // do once:
                    // wait for animation to play thru
                if (readyToSkipScene)
                {
                    currentState = (int)TutorialState.skip_scene;
                }
                break;

            case (int)TutorialState.skip_scene:

                // change scene
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
                break;
        }

	}

    public void skipScene()
    {
        currentState = (int)TutorialState.skip_scene;
    }

    IEnumerator playHelperSuccessAnim()
    {
        helperSuccessAnim.SetActive(false);
        helperSuccessAnim.SetActive(true);
        AudioCenter.PlayGetItem();
        yield return null;
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
        float waitTime = 3f;

        yield return new WaitForSeconds(6f);

        clock.GetComponent<Clock>().DecreaseTurn();
        yield return new WaitForSeconds(waitTime);

        clock.GetComponent<Clock>().DecreaseTurn();
        yield return new WaitForSeconds(waitTime);

        clock.GetComponent<Clock>().DecreaseTurn();
        yield return new WaitForSeconds(waitTime);

        clock.GetComponent<Clock>().DecreaseTurn();

    }

    IEnumerator WaitBeforeSkipScene()
    {
        yield return new WaitForSeconds(5f);
        readyToSkipScene = true;
    }
}
