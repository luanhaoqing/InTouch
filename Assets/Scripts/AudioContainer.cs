using UnityEngine;
using System.Collections;

public class AudioContainer : MonoBehaviour {
    public AudioClip cantDoThat;
    public AudioClip eventTrigger;
    public AudioClip loseItem;
    public AudioClip turnOver;
    public AudioClip selectionConfirm;
    public AudioClip[] helperPromptList;
    public AudioClip helperPoke;

    public AudioClip crystalBreak;
    public AudioClip getItem;
    public AudioClip islandDeath;
    public AudioClip islandLowHealth;
    public AudioClip footstep;
    public AudioClip roundOver;
    public AudioClip selectionAlt;
    public AudioClip useAP;


    // Use this for initialization
    void Start () {
        AudioCenter.audioCenter = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        AudioCenter.cantDoThat = cantDoThat;
        AudioCenter.eventTrigger = eventTrigger;
        AudioCenter.loseItem = loseItem;
        AudioCenter.turnOver = turnOver;
        AudioCenter.selectionConfirm = selectionConfirm;
        AudioCenter.helperPromptList = helperPromptList;
        AudioCenter.helperPoke = helperPoke;

        AudioCenter.crystalBreak = crystalBreak;
        AudioCenter.getItem = getItem;
        AudioCenter.islandDeath = islandDeath;
        AudioCenter.islandLowHealth = islandLowHealth;
        AudioCenter.footstep = footstep;
        AudioCenter.roundOver = roundOver;
        AudioCenter.selectionAlt = selectionAlt;
        AudioCenter.useAP = useAP;


    }

    // Update is called once per frame
    void Update () {
	
	}
}
