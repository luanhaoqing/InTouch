using UnityEngine;
using System.Collections;

public class SPAudioCenter
{

    public static AudioClip hello;
    public static AudioClip skipThisPart;
    public static AudioClip moveToIsland;
    public static AudioClip moveToIslandReminder;
    public static AudioClip positiveFeedback1;
    public static AudioClip positiveFeedback2;
    public static AudioClip lookAtItems;
    public static AudioClip getItemsReminder;
    public static AudioClip reviveItem;

    public static AudioClip sendItemFriend;
    public static AudioClip sendItemReminder;
    public static AudioClip goodJobToSend;
    public static AudioClip handMenu;
    public static AudioClip learnTileHealth;
    public static AudioClip goodJobMovingToSafeTile;
    public static AudioClip allTilesLostHealth;
    public static AudioClip clockInstruction;
    public static AudioClip gameGoal;
    public static AudioClip getKey;
    public static AudioClip goToDoor;
    public static AudioClip enterGame;

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

    public static void PlaymoveToIslandReminder()
    {
        audioCenter.Stop();
        audioCenter.PlayOneShot(moveToIslandReminder, 0.5f);
    }

    public static void PlaypositiveFeedback1()
    {
        audioCenter.Stop();
        audioCenter.PlayOneShot(positiveFeedback1, 0.5f);
    }

    public static void PlaypositiveFeedback2()
    {
        audioCenter.Stop();
        audioCenter.PlayOneShot(positiveFeedback2, 0.5f);
    }

    public static void PlaylookAtItems()
    {
        audioCenter.Stop();
        audioCenter.PlayOneShot(lookAtItems, 0.5f);
    }

    public static void PlaygetItemsReminder()
    {
        audioCenter.PlayOneShot(getItemsReminder, 0.5f);
    }

    public static void PlayreviveItem()
    {
        audioCenter.PlayOneShot(reviveItem, 0.5f);
    }

    public static void PlaySendItemFriend()
    {
        audioCenter.PlayOneShot(sendItemFriend, 0.5f);
    }

    public static void PlaySendItemReminder()
    {
        audioCenter.PlayOneShot(sendItemReminder, 0.5f);
    }

    public static void PlayGoodJobToSend()
    {
        audioCenter.PlayOneShot(goodJobToSend, 0.5f);
    }

    public static void PlayHandMenu()
    {
        audioCenter.PlayOneShot(handMenu, 0.5f);
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
