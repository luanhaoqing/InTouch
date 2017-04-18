using UnityEngine;
using System.Collections;

public class SPAudioContainer : MonoBehaviour {

    public AudioClip hello;
    public AudioClip skipThisPart;
    public AudioClip moveToIsland;
    public AudioClip moveToIslandReminder;
    public AudioClip lookAtItems;
    public AudioClip getItemsReminder;

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
        SPAudioCenter.positiveFeedback1 = positiveFeedback1;
        SPAudioCenter.positiveFeedback2 = positiveFeedback2;

        SPAudioCenter.lookAtItems = lookAtItems;
        SPAudioCenter.getItemsReminder = getItemsReminder;



        SPAudioCenter.moveMe = moveMe;
        SPAudioCenter.moveButton = moveButton;
        SPAudioCenter.youAreNatural = youAreNatural;

    }

    // Update is called once per frame
    void Update () {
	
	}
}
