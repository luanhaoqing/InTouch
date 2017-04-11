using UnityEngine;
using System.Collections;

public class AudioCenter {

    public static AudioClip cantDoThat;
    public static AudioClip eventTrigger;
    public static AudioClip getItem;
    public static AudioClip loseItem;
    public static AudioClip roundOver;
    public static AudioClip turnOver;
    public static AudioClip selectionConfirm;
    public static AudioClip selectionAlt;

    public static AudioClip[] helperPromptList;

    public static AudioSource audioCenter;



    public static void PlayCantDoThat()
    {
        audioCenter.PlayOneShot(cantDoThat);
    }

    public static void PlayEventTrigger()
    {
        audioCenter.PlayOneShot(eventTrigger);
    }

    public static void PlayGetItem()
    {
        audioCenter.PlayOneShot(getItem);
    }

    public static void PlayLoseItem()
    {
        audioCenter.PlayOneShot(loseItem);
    }

    public static void PlayRoundOver()
    {
        audioCenter.PlayOneShot(roundOver, 0.5f);
    }

    public static void PlayTurnOver()
    {
        audioCenter.PlayOneShot(turnOver, 0.5f);
    }

    public static void PlaySelectionConfirm()
    {
        audioCenter.PlayOneShot(selectionConfirm, 0.3f);
    }

    public static void PlaySelectionAlt()
    {
        audioCenter.PlayOneShot(selectionAlt);
    }



    public static void PlayHelperPrompt()
    {
        int length = helperPromptList.Length;
        int randNumber = Random.Range(1, length);
        audioCenter.PlayOneShot(helperPromptList[randNumber]);
    }
}
