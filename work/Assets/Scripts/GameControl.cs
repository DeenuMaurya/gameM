using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    private static GameObject whoWinsTextShadow, player1MoveText, player2MoveText, player3MoveText;
    public List<Player> players = new List<Player>();
 
    public static int diceSideThrown = 0;
    public static int turn = 0;

    public static bool gameOver = false;
    public static bool moveAllowed = false;
    public static bool wasAKill = false;

    public Transform playerOne;
    public Transform playerTwo;
    public Transform playerThree;
    public Transform playerFour;


    public Dice dice;

    public static GameControl instance;

    public Transform topicParent;
    public GameObject topicPrefeb;
   public int playerCount;


    public List<GameObject> playersP = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        whoWinsTextShadow = GameObject.Find("WhoWinsText");
        playerCount= PlayerPrefs.GetInt("PlayerCount");

        if (playerCount == 2)
        {
            playersP[2].SetActive(false);
            playersP[3].SetActive(false);

            Player player = new Player(GameObject.Find("Player1"));
             Player player2 = new Player(GameObject.Find("Player2")); 
 
            players.Add(player);
             players.Add(player2);

            playerTwo.gameObject.SetActive(true);

            playerOne.gameObject.SetActive(true);
            playerThree.gameObject.SetActive(false);
            playerFour.gameObject.SetActive(false);
        }
        else if(playerCount == 3)
        {
             playersP[3].SetActive(false);
            Player player = new Player(GameObject.Find("Player1"));
            Player player2 = new Player(GameObject.Find("Player2"));
            Player player3 = new Player(GameObject.Find("Player3"));
            players.Add(player);
             players.Add(player2);
             players.Add(player3);

            playerTwo.gameObject.SetActive(true);

            playerOne.gameObject.SetActive(true);
            playerThree.gameObject.SetActive(true);
            playerFour.gameObject.SetActive(false);
        }
        else if (playerCount == 4)
        {
            Player player = new Player(GameObject.Find("Player1"));
            Player player2 = new Player(GameObject.Find("Player2"));
             Player player3 = new Player(GameObject.Find("Player3"));
             Player player4 = new Player(GameObject.Find("Player4"));
            players.Add(player);
             players.Add(player2);
             players.Add(player3);
             players.Add(player4);

            playerTwo.gameObject.SetActive(true);

            playerOne.gameObject.SetActive(true);
            playerThree.gameObject.SetActive(true);
            playerFour.gameObject.SetActive(true);
        }


        whoWinsTextShadow.gameObject.SetActive(false);

        playerOne.GetChild(0).gameObject.SetActive(true);

    }


    public  void UpdateUserProfile()
    {
        //Debug.Log("moveee" + turn);
        if (turn == 0)
        {
            playerTwo.GetChild(0).gameObject.SetActive(true);
            playerOne.GetChild(0).gameObject.SetActive(false);
            playerThree.GetChild(0).gameObject.SetActive(false);
            playerFour.GetChild(0).gameObject.SetActive(false);
        }
        else if (turn == 1)
        {
            playerThree.GetChild(0).gameObject.SetActive(true);
            playerOne.GetChild(0).gameObject.SetActive(false);
            playerTwo.GetChild(0).gameObject.SetActive(false);
            playerFour.GetChild(0).gameObject.SetActive(false);
        }
        else if (turn == 2)
        {
            playerFour.GetChild(0).gameObject.SetActive(true);
            playerOne.GetChild(0).gameObject.SetActive(false);
            playerThree.GetChild(0).gameObject.SetActive(false);
            playerTwo.GetChild(0).gameObject.SetActive(false);
        }
        else if (turn == 3)
        {
            playerOne.GetChild(0).gameObject.SetActive(true);
            playerThree.GetChild(0).gameObject.SetActive(false);
            playerTwo.GetChild(0).gameObject.SetActive(false);
            playerFour.GetChild(0).gameObject.SetActive(false);
        }

    }








    // Update is called once per frame

    void Update(){
        checkWinner();
        if(moveAllowed){
 
            players[turn].movePlayer(diceSideThrown);
            moveAllowed = false;
            dice.coroutineAllowed = true;
        }
        checkOverlap(players[turn]);
    }

    private void checkWinner(){
        for(int i = 0; i< playerCount; i++){
            if(players[i].isWinner()){
                gameOver = true;
                whoWinsTextShadow.GetComponent<Text>().text = "Player"+(i+1)+" Wins!";
                whoWinsTextShadow.gameObject.SetActive(true);
            }
        }
    }

    private void checkOverlap(Player mainPlayer){
        foreach (Player player in players)
        {
            if( player != mainPlayer && mainPlayer.getPosition() != 0 && player.getPosition() != 0)
            {
                if(Vector3.Distance(player.getVectorPosition(), mainPlayer.getVectorPosition()) == 0 && mainPlayer.getPosition() == player.getPosition())
                {
                    player.die();
                    wasAKill = true;
                }
            }
        }
    }
}
