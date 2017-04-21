using UnityEngine;
using System.Collections;

public class OverallManager : MonoBehaviour {
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
        Debug.Log("TASK #2 START");
        task2ObjectWrapper.SetActive(true);
    }

    public void startTask3()
    {
        Debug.Log("TASK #3 START");
        task3ObjectWrapper.SetActive(true);
    }

    public void startTask4()
    {
        Debug.Log("TASK #4 START");
        task4ObjectWrapper.SetActive(true);
    }

    public void startTask5()
    {
        task5ObjectWrapper.SetActive(true);
    }

    public void startTask6()
    {
        task6ObjectWrapper.SetActive(true);
    }

    public void skipScene()
    {
        skipSceneTrigger = true;
    }

}
