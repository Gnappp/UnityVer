using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Threading.Tasks;

public class Board
{
    public int[,] gameboard;
    public char turn;
    public int endPos;
    public int startPos;

    Board()
    {
        
    }

    public Board(int[,] mb,char t,int sp,int ep)
    {
        gameboard = mb;
        turn = t;
        startPos = sp;
        endPos = ep;
    }
}


public class GameBoard : MonoBehaviour {


    public static GameBoard instance = null;
    //public Board board;
    //public GameObject[] pieceimg = new GameObject[12];
    //public GameObject[] colorFiledimg = new GameObject[2];
   // public GameObject boardimg;

    public int[,] game_board;
    public char turn;
    public int endPos;
    public int startPos;

    protected DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    protected DatabaseReference mDatabaseRef;
    private void Awake()
    {
        game_board = new int[8, 8]
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
        startPos = 0;
        endPos = 0;
        turn = 'B';
        if (instance==null)
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

        /* FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
             dependencyStatus = task.Result;
             if (dependencyStatus == DependencyStatus.Available)
             {
                 InitializeFirebase();
             }
             else
             {
                 Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
             }
         });*/

        /*for(int a=0;a<8;a++)
        {
            for(int b=0;b<8;b++)
            {
                if (game_board[a, b] != 99)
                {
                    GameObject piece = Instantiate(pieceimg[game_board[a, b]]) as GameObject;  
                    piece.transform.position = new Vector3((float)b, (float)a, (float)2.0);
                }
            }
        }*/
        //       GameObject board = Instantiate(boardimg) as GameObject;
        //       board.transform.position = new Vector3((float)3.5, (float)3.5, (float)0.0);

        InitializeFirebase();

        // Get the root reference location of the database.
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unitytest-567b5.firebaseio.com/");
    }

    protected virtual void InitializeFirebase()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        // NOTE: You'll need to replace this url with your Firebase App's database
        // path in order for the database connection to work correctly in editor.
        app.SetEditorDatabaseUrl("https://unitytest-567b5.firebaseio.com");
        if (app.Options.DatabaseUrl != null)
            app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);

        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void writeNewUser(string userId)
    {

        Board board = new Board(game_board, turn, startPos, endPos);

        string json = JsonUtility.ToJson(board);

        mDatabaseRef.Child(userId).Child(userId).SetRawJsonValueAsync(json);
    }



    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            writeNewUser("unitytest-567b5");
        }
        /*else if (Input.GetKeyDown(KeyCode.W))
        {
            FirebaseDatabase.DefaultInstance.GetReference("users").GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    Debug.Log("failed");
                }
                else if (task.IsCompleted)
                {
                    Firebase.Database.DataSnapshot snapshot = task.Result;
                    Debug.Log(snapshot.Value.ToString());
                    foreach (var childSnapshot in snapshot.Children)
                    {
                        Debug.Log("users name : " +
                            childSnapshot.Child("username").Value.ToString());
                    }
                }
            });
        }*/
    }
}
