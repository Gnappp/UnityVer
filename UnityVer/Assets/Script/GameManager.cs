using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public static GameManager instance = null;

    public GameObject[] pieceimg = new GameObject[12];
    public GameObject[] colorFiledimg = new GameObject[2];
    public GameObject boardimg;

    private int[,] gameboard;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else if(instance!=null)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

	// Use this for initialization
	void Start () 
    {
        gameboard = new int[8, 8]
        { 
            { 1,2,3,4,5,3,2,1},
            { 0,0,0,0,0,0,0,0},
            { 99,99,99,99,99,99,99,99},
            { 99,99,99,99,99,99,99,99},
            { 99,99,99,99,99,99,99,99},
            { 99,99,99,99,99,99,99,99},
            { 6,6,6,6,6,6,6,6},
            { 7,8,9,10,11,9,8,7}
        };

        
        for(int a=0;a<8;a++)
        {
            for(int b=0;b<8;b++)
            {
                if (gameboard[a, b] != 99)
                {
                    GameObject piece = Instantiate(pieceimg[gameboard[a, b]]) as GameObject;
                    piece.transform.position = new Vector3((float)b, (float)a, (float)2.0);
                }
            }
        }
 //       GameObject board = Instantiate(boardimg) as GameObject;
 //       board.transform.position = new Vector3((float)3.5, (float)3.5, (float)0.0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
