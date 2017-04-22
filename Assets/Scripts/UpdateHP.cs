using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UpdateHP : MonoBehaviour {
    public GameObject tile;
    public GameObject[] flame;
    private int numberofFlame=4;
    private float length = 1.0f;

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
            if (numberofFlame > 0)
            {
                flame[numberofFlame - 1].GetComponent<Animator>().SetTrigger("breakdown");
                numberofFlame--;
                StartCoroutine(WaitAndDelete());
            }
          
        }
    }

    public void Heal()
    {
        tile.GetComponent<TileHealthyManager>().health++;
        if(numberofFlame<4)
            flame[numberofFlame].SetActive(true);
        numberofFlame++;
        Debug.Log("Healing the tile!!");
    }
    IEnumerator WaitAndDelete()
    {
        yield return new WaitForSeconds(length);

        flame[numberofFlame].SetActive(false);
        //yield return null ;
    }
}

