using UnityEngine;
using System.Collections;

public class AudioContainer : MonoBehaviour {
    public AudioClip cantDoThat;
    public AudioClip eventTrigger;
    public AudioClip getItem;
    public AudioClip loseItem;
    public AudioClip roundOver;
    public AudioClip turnOver;
    public AudioClip selectionConfirm;
    public AudioClip selectionAlt;
    public AudioClip[] helperPromptList;
    public AudioClip helperPoke;

    // Use this for initialization
    void Start () {
        AudioCenter.audioCenter = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        AudioCenter.cantDoThat = cantDoThat;
        AudioCenter.eventTrigger = eventTrigger;
        AudioCenter.getItem = getItem;
        AudioCenter.loseItem = loseItem;
        AudioCenter.roundOver = roundOver;
        AudioCenter.turnOver = turnOver;
        AudioCenter.selectionConfirm = selectionConfirm;
        AudioCenter.selectionAlt = selectionAlt;
        AudioCenter.helperPromptList = helperPromptList;
        AudioCenter.helperPoke = helperPoke;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
