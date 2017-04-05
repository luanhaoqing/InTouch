using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UpdateHP : MonoBehaviour {
    public GameObject tile;
    public GameObject[] flame;
    private int numberofFlame=4;
	// Use this for initialization
	void Start () {
        for(int i=0;i<4;i++)
        {
            flame[i].SetActive(false);
        }

    }
	
	// Update is called once per frame
	void Update () {
        // this.GetComponent<Text>().text= tile.GetComponent<TileHealthyManager>().health.ToString();
      if(tile.GetComponent<TileHealthyManager>().health!= numberofFlame && tile.GetComponent<TileHealthyManager>().health<4)
        {
            flame[numberofFlame - 1].GetComponent<Animator>().SetTrigger("breakdown");
        //    flame[numberofFlame - 1].GetComponent<Animator>().SetBool("break",true);
            numberofFlame--;


        }
    }
}
