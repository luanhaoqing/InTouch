using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HandControl : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trade"))
        {
            if (this.GetComponentInParent<PlayerIDOnBoard>().ItemNumber != 0)
            {

                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                for (int i = 0; i < 2; i++)
                {
                    if (players[i] != this.gameObject)
                    {

                        players[i].GetComponent<PlayerIDOnBoard>().Items[players[i].GetComponent<PlayerIDOnBoard>().ItemNumber] = this.GetComponentInParent<PlayerIDOnBoard>().Items[this.GetComponentInParent<PlayerIDOnBoard>().ItemNumber - 1];
                        players[i].GetComponent<PlayerIDOnBoard>().Items[players[i].GetComponent<PlayerIDOnBoard>().ItemNumber].transform.position = this.transform.parent.transform.position;
                        players[i].GetComponent<PlayerIDOnBoard>().Items[players[i].GetComponent<PlayerIDOnBoard>().ItemNumber].transform.parent = this.transform.parent;
                        players[i].GetComponent<PlayerIDOnBoard>().ItemNumber++;

                        this.GetComponentInParent<PlayerIDOnBoard>().ItemNumber--;
                        this.GetComponentInParent<PlayerIDOnBoard>().Items[this.GetComponentInParent<PlayerIDOnBoard>().ItemNumber] = null;
                    }
                }
            }
        }
    }
}
