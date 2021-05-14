using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int Score;
    public Text ScoreUI;

    public List<GameObject>CheckIfUserWons;
    GameObject[] ArrayAll;

    public GameObject PanelGameOver;
    public GameObject PanelWinner;

    public GameObject Player;
    public SpawnGenerator AlienController;
    bool gameStarted;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void AddToScore(int Add, GameObject RemoveList)
    {
        Score += Add;
        ScoreUI.text = "SCORE: " + Score.ToString("0000");
        CheckIfUserWons.Remove(RemoveList);
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void OnStartGame(){
        //reset the array of enemies
        CheckIfUserWons.Clear();
        ArrayAll = GameObject.FindGameObjectsWithTag("Enemies");//add all 
        for(int i=0; i < ArrayAll.Length; i++){
            CheckIfUserWons.Add(ArrayAll[i]);
        }
        gameStarted = true;

    }
    private void Update()
    {
       if (gameStarted && CheckIfUserWons.Count == 0){
            PanelWinner.SetActive(true);
            Player.GetComponent<SpriteRenderer>().enabled = false;
            Player.GetComponent<GameManager>().canPlay = false;
            gameStarted = false;
        }
    }


    public void ResetGame(){
        //reset player position and
        Player.GetComponent<SpriteRenderer>().enabled = true;
        Player.transform.position = new Vector3((float)5.66, (float).2);
        PanelGameOver.SetActive(false);
        PanelWinner.SetActive(false);
        //reset the score
        Score = 0;
        ScoreUI.text = "SCORE: " + Score.ToString("0000");
        //
        AlienController.SetUp();
        //enable movement of the player and reset the bullet to the basic = 0
        Player.GetComponent<GameManager>().ResetGame();
    }
    public void OnGameOver(){
        PanelGameOver.SetActive(true);
        Player.GetComponent<SpriteRenderer>().enabled = false;
        AlienController.OnGameOver();//destroys the aliens
        Player.GetComponent<GameManager>().canPlay = false;
        gameStarted = false;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemies")
        {
            OnGameOver();
        }
    }
    public void ExitScene(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Selector");
    }
}
