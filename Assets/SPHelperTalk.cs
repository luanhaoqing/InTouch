using UnityEngine;
using System.Collections;

public class SPHelperTalk : MonoBehaviour {
    
    private AudioSource audioSource;
    SPHelperAnimation HelperAnim;

    public AudioClip PositiveFeedback;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        HelperAnim = GetComponent<SPHelperAnimation>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Speak(AudioClip audio)
    {
        HelperAnim.SetHelperTalkActive(true, audio.length);
        audioSource.clip = audio;
        audioSource.Play();
        
    }

    public void SaySuccess()
    {
        audioSource.clip = PositiveFeedback;
        audioSource.Play();
    }

}
