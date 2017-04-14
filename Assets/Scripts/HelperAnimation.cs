using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HelperAnimation : NetworkBehaviour {
    public GameObject turnCount;
    public GameObject helper;
    public GameObject countsShow;
    float helperTalkSpeed = 0.05f;
    public Texture[] helperTalkTextureArray;

    bool prompted;
    bool first_time = true;

    bool helperTalkActive = false;
    float helperTalkCounter = 0;
    Material helperBodyMaterial;
    int helperTalkIndex = 0;
    float helperTalkDuration = 0;
    float thisTalkDuration = 0;


    // Use this for initialization
    void Start () {
        if (!isLocalPlayer)
            return;
        turnCount = GameObject.Find("TrunCounter");
        helperBodyMaterial = helper.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
    }

    // Update is called once per frame
    void Update () {

        if (!isLocalPlayer)
            return;
        {
            if(turnCount.GetComponent<TurnCounter>().OwnId==turnCount.GetComponent<CurrentPlayer>().CurrentPlayerID)
            {
                int remainAction = turnCount.GetComponent<CurrentPlayer>().RemainActionPoint;
                countsShow.GetComponent<Text>().text = "You have " + remainAction + " action left";
                helper.GetComponent<Animator>().SetBool("fly",true);

                if (!prompted)
                {
                    HelperPrompt();
                }
            }
            else
            {
                helper.GetComponent<Animator>().SetBool("fly", false);
                prompted = false;
            }
        }

        // The helper talking animation
        // check if switch is on
        if (helperTalkActive)
        {
            helperTalkCounter += Time.deltaTime; // the usual time counter
            thisTalkDuration += Time.deltaTime; // to set total talking animation time
            
            if (helperTalkCounter > helperTalkSpeed)
            {
                helperTalkCounter = 0;
                if (helperTalkIndex < helperTalkTextureArray.Length)
                {
                    helperBodyMaterial.SetTexture("_MainTex", helperTalkTextureArray[helperTalkIndex]); // change the mouth texture to next in line
                    helperTalkIndex++;
                }
                else
                {
                    helperTalkIndex = 0;
                }
            }
            if (thisTalkDuration >= helperTalkDuration) // when cycle is over, turn it off.
            {
                SetHelperTalkActive(false, 0);
            }
        }
    }

    void HelperPrompt()
    {
        if (!first_time) {
            AudioCenter.PlayHelperPrompt();
            prompted = true;
        }

        else
        {
            first_time = false;
            prompted = true;
        }


    }

    public void SetHelperTalkActive(bool boolean, float seconds)
    {
        helperTalkActive = boolean;
        helperTalkDuration = seconds;
        if (boolean == false) // when turning off, make it go back to closed mouth.
        {
            helperBodyMaterial.SetTexture("_MainTex", helperTalkTextureArray[9]);
        }
    }
}
