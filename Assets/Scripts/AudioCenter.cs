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
    public static AudioClip helperPoke;
    public static AudioClip[] helperPromptList;
    public static AudioClip crystalBreak;
    public static AudioClip islandDeath;
    public static AudioClip islandLowHealth;
    public static AudioClip footstep;
    public static AudioClip useAP;

    public static AudioSource audioCenter;



    public static void PlayCantDoThat()
    {
        audioCenter.PlayOneShot(cantDoThat, 0.5f);
    }

    public static void PlayEventTrigger()
    {
        audioCenter.PlayOneShot(eventTrigger, 0.5f);
    }

    public static void PlayGetItem()
    {
        audioCenter.PlayOneShot(getItem, 0.5f);
    }

    public static void PlayLoseItem()
    {
        audioCenter.PlayOneShot(loseItem, 0.5f);
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
        audioCenter.PlayOneShot(selectionAlt, 0.5f);
    }



    public static void PlayHelperPrompt()
    {
        int length = helperPromptList.Length;
        int randNumber = Random.Range(1, length);
        audioCenter.PlayOneShot(helperPromptList[randNumber], 0.5f);
    }

    public static void PlayHelperPoke()
    {
        audioCenter.PlayOneShot(helperPoke, 0.5f);
    }

    public static void PlayCrystalBreak()
    {
        audioCenter.PlayOneShot(crystalBreak, 0.5f);
    }

    public static void PlayIslandDeath()
    {
        audioCenter.PlayOneShot(islandDeath, 0.5f);
    }

    public static void PlayIslandLowHealth()
    {
        audioCenter.PlayOneShot(islandLowHealth, 0.5f);
    }

    public static void PlayFootstep()
    {
        audioCenter.PlayOneShot(footstep, 0.5f);
    }

    public static void PlayUseAp()
    {
        audioCenter.PlayOneShot(useAP, 0.5f);
    }


}
