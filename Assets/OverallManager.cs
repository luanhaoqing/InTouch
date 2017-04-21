using UnityEngine;
using System.Collections;

public class OverallManager : MonoBehaviour {
    public bool AutoStart;
    public Animator helperAnim;

    public GameObject task0ObjectWrapper;
    public GameObject task1ObjectWrapper;
    public GameObject task2ObjectWrapper;
    public GameObject task3ObjectWrapper;
    public GameObject task4ObjectWrapper;
    public GameObject task5ObjectWrapper;
    public GameObject task6ObjectWrapper;

    private bool skipSceneTrigger = false;
    private float skipCounter;

    // Use this for initialization
    void Start () {
        if (AutoStart) { 
	        StartCoroutine(StartEverything());
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (skipSceneTrigger)
        {
            skipCounter += Time.deltaTime;
            if (skipCounter > 2)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
            }
        }
	
	}
    
    public void destroyStuff(GameObject GO)
    {
        GO.SetActive(false);
    }

    public void startTask0()
    {
        task0ObjectWrapper.SetActive(true);
    }

    public void startTask1()
    {
        task1ObjectWrapper.SetActive(true);
    }

    public void startTask2()
    {
        task2ObjectWrapper.SetActive(true);
    }

    public void startTask3()
    {
        task3ObjectWrapper.SetActive(true);
    }

    public void startTask4()
    {
        task4ObjectWrapper.SetActive(true);
    }

    public void startTask5()
    {
        task5ObjectWrapper.SetActive(true);
    }

    public void startTask6()
    {
        Debug.Log("TASK #6 START");
        task6ObjectWrapper.SetActive(true);
    }

    public void skipScene()
    {
        skipSceneTrigger = true;
    }

    IEnumerator StartEverything()
    {
        yield return new WaitForSeconds(1f);
        helperAnim.SetBool("fly",true);
        yield return new WaitForSeconds(1.5f);
        startTask0();
    }

}
