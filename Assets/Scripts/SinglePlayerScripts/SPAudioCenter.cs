using UnityEngine;
using System.Collections;

public class SPAudioCenter
{

    public static AudioClip hello;
    public static AudioClip skipThisPart;
    public static AudioClip moveToIsland;
    public static AudioClip moveMe;
    public static AudioClip moveButton;
    public static AudioClip youAreNatural;
    //    public static AudioClip turnOver;
    //    public static AudioClip selectionConfirm;
    //   public static AudioClip selectionAlt;

    //    public static AudioClip[] helperPromptList;

    public static AudioSource audioCenter;



    public static void PlayHello()
    {
        audioCenter.spatialBlend = (1.0f);
        audioCenter.PlayOneShot(hello, 0.5f);
        audioCenter.spatialBlend = (0f);

    }

    public static void PlaySkipThisPart()
    {
        audioCenter.PlayOneShot(skipThisPart, 0.5f);
    }

    public static void PlaymoveToIsland()
    {
        audioCenter.PlayOneShot(moveToIsland, 0.5f);
    }


    public static void PlayMoveMe()
    {
        audioCenter.PlayOneShot(moveMe, 0.5f);
    }

    public static void PlayMoveButton()
    {
        audioCenter.PlayOneShot(moveButton, 0.5f);
    }

    public static void PlayYouAreNatural()
    {
        audioCenter.PlayOneShot(youAreNatural, 0.5f);
    }


}
