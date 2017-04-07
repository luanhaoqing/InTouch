using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UpdateHP : MonoBehaviour {
    public GameObject tile;
    public GameObject[] flame;
    private int numberofFlame=4;
    private float length = 1.4f;

	// Use this for initialization
	void Start () {
        for(int i=0;i<4;i++)
        {
            flame[i].SetActive(false);
        }

    }


    // Update is called once per frame
    void Update()
    {
        // this.GetComponent<Text>().text= tile.GetComponent<TileHealthyManager>().health.ToString();
        if (tile.GetComponent<TileHealthyManager>().health != numberofFlame && tile.GetComponent<TileHealthyManager>().health < 4)
        {
            flame[numberofFlame - 1].GetComponent<Animator>().SetTrigger("breakdown");
            // delete after playing animation
            numberofFlame--;

            StartCoroutine(WaitAndDelete());
        }
    }

    IEnumerator WaitAndDelete()
    {
        yield return new WaitForSeconds(length);

        Destroy(flame[numberofFlame]);
        //yield return null ;
    }
}

