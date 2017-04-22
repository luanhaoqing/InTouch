using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

    public GameObject startPrompt;
    public OverallManager overallManager;

    GameObject Panel;
    float counter;
    float speed = 0.75f;
    private bool controllerClickMapping;

    bool BlinkingStart = true;
    public bool ThumbClicked = false;

    // Use this for initialization
    void Start () {
        Panel = startPrompt.transform.GetChild(0).gameObject;

        

    }

    // Update is called once per frame
    void Update () {
        controllerClickMapping = ((Input.GetKeyDown(KeyCode.Space)
        || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick)
        || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick)));
        if (BlinkingStart) { 
            counter += Time.deltaTime;
            if ( counter> speed)
            {
                startPrompt.SetActive(false);
            }
            if (counter > speed * 2)
            {
                startPrompt.SetActive(true);
                counter = 0;
            }
        }

      

        if (!ThumbClicked)
        {
            if (controllerClickMapping)
            {
                ThumbClicked = true;
                BlinkingStart = false;
            }
        }

        if (ThumbClicked)
        {
            ThumbClicked = false;
            Debug.Log("Clicked");
            StartCoroutine(AfterClickAnim());
        }

    }

    IEnumerator AfterClickAnim()
    {
        iTween.FadeTo(Panel, 0, 1f);
        int times = 0;
        foreach  (int i in System.Linq.Enumerable.Range(0, 10))
        {
            startPrompt.SetActive(!startPrompt.activeInHierarchy);
            yield return new WaitForSeconds(0.1f);
            times ++;
        }
        overallManager.afterSplash();
        this.gameObject.SetActive(false);
    }
}
