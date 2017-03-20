using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ControllerOfPlayerOntheBoard : NetworkBehaviour {
    public GameObject PlayerOnBoard;
    public GameObject detectBall;
    private Vector3 target;
    private bool BeginMove;
	// Use this for initialization
	void Start () {
        PlayerOnBoard.transform.position = new Vector3(0.2606f,0.02f,-0.5f);
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;
        if (GameObject.FindGameObjectWithTag("Turn").GetComponent<CurrentPlayer>().MyTurn&&!BeginMove)
        {
            //var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
          //  var z = Input.GetAxis("Vertical") * Time.deltaTime * 0.05f;
            // Debug.Log("TEST");
          //  PlayerOnBoard.transform.Rotate(0, x, 0);
          //  PlayerOnBoard.transform.Translate(0, 0, z);
          if(Input.GetKeyDown(KeyCode.W))
            {
            //    Debug.Log("w");
                detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(0.2f,0,0);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
           //     Debug.Log("w");
                detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(-0.2f, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
             //   Debug.Log("w");
                detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(0, 0, 0.2f);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
             //   Debug.Log("w");
                detectBall.transform.position = PlayerOnBoard.transform.position + new Vector3(0, 0, -0.2f);
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                //  Debug.Log("w");
                BeginMove = true;
                target = detectBall.transform.position;
                detectBall.transform.position = PlayerOnBoard.transform.position;
            }
            if(Input.GetKeyDown(KeyCode.T))
            {
                if(this.GetComponent<PlayerIDOnBoard>().ItemNumber!=0)
                {
                   
                    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                    for(int i=0;i<2;i++)
                    {
                        if(players[i]!=this.gameObject)
                        {
                            players[i].GetComponent<PlayerIDOnBoard>().Items[players[i].GetComponent<PlayerIDOnBoard>().ItemNumber] = this.GetComponent<PlayerIDOnBoard>().Items[this.GetComponent<PlayerIDOnBoard>().ItemNumber-1];
                            players[i].GetComponent<PlayerIDOnBoard>().ItemNumber++;
                            this.GetComponent<PlayerIDOnBoard>().ItemNumber--;
                            this.GetComponent<PlayerIDOnBoard>().Items[this.GetComponent<PlayerIDOnBoard>().ItemNumber] = null;
                        }
                    }
                }
            }
        }
        if(BeginMove)
        {
            float step = 0.1f * Time.deltaTime;
            PlayerOnBoard.transform.position = Vector3.MoveTowards(PlayerOnBoard.transform.position, target, step);
            if (PlayerOnBoard.transform.position == target)
                BeginMove = false;
        }


    }

}
