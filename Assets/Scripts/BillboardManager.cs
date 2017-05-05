using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BillboardManager : MonoBehaviour {
    public GameObject[] Right;
    public GameObject[] Left;
    public Sprite checkedPicture;
    public Sprite[] crossedLine;
    public Text DebugInfo;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Check(int lineNum)
    {
        Left[lineNum].GetComponent<Image>().sprite = checkedPicture;
    }

    public void HighLight(int lineNum)
    {
        Right[lineNum].GetComponent<Image>().sprite = crossedLine[lineNum];
    }

    /*public void HighLight(int lineNum)
    {
        Right[lineNum].GetComponent<Text>().color = Color.yellow;
    }
    */

    public void SetDebugInfo(string thing)
    {
        if (DebugInfo == null)
        {
            return;
        }
        DebugInfo.text = thing;
    }
}
