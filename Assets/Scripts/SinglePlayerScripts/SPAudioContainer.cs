using UnityEngine;
using System.Collections;

public class SPAudioContainer : MonoBehaviour {

    public AudioClip hello;
    public AudioClip skipThisPart;
    public AudioClip moveToIsland;
    public AudioClip moveToIslandReminder;
    public AudioClip lookAtItems;
    public AudioClip getItemsReminder;
    public AudioClip reviveItem;

    public AudioClip sendItemFriend;
    public AudioClip sendItemReminder;
    public AudioClip goodJobToSend;
    public AudioClip handMenu;
    public AudioClip learnTileHealth;
    public AudioClip goodJobMovingToSafeTile;
    public AudioClip allTilesLostHealth;
    public AudioClip clockInstruction;
    public AudioClip gameGoal;
    public AudioClip getKey;
    public AudioClip goToDoor;
    public AudioClip enterGame;


    public AudioClip positiveFeedback1;
    public AudioClip positiveFeedback2;

    public AudioClip moveMe;
    public AudioClip moveButton;
    public AudioClip youAreNatural;


    // Use this for initialization
    void Start () {
        SPAudioCenter.audioCenter = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        SPAudioCenter.hello = hello;
        SPAudioCenter.skipThisPart = skipThisPart;
        SPAudioCenter.moveToIsland = moveToIsland;
        SPAudioCenter.moveToIslandReminder = moveToIslandReminder;

        SPAudioCenter.lookAtItems = lookAtItems;
        SPAudioCenter.getItemsReminder = getItemsReminder;
        SPAudioCenter.reviveItem = reviveItem;

        SPAudioCenter.sendItemFriend = sendItemFriend;
        SPAudioCenter.sendItemReminder = sendItemReminder;
        SPAudioCenter.goodJobToSend = goodJobToSend;
        SPAudioCenter.handMenu = handMenu;
        SPAudioCenter.learnTileHealth = learnTileHealth;
        SPAudioCenter.goodJobMovingToSafeTile = goodJobMovingToSafeTile;
        SPAudioCenter.allTilesLostHealth = allTilesLostHealth;
        SPAudioCenter.clockInstruction = clockInstruction;
        SPAudioCenter.gameGoal = gameGoal;
        SPAudioCenter.getKey = getKey;
        SPAudioCenter.goToDoor = goToDoor;
        SPAudioCenter.enterGame = enterGame;



        SPAudioCenter.positiveFeedback1 = positiveFeedback1;
        SPAudioCenter.positiveFeedback2 = positiveFeedback2;
        SPAudioCenter.moveMe = moveMe;
        SPAudioCenter.moveButton = moveButton;
        SPAudioCenter.youAreNatural = youAreNatural;

    }

    // Update is called once per frame
    void Update () {
	
	}
}
