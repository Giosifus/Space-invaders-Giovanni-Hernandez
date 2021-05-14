using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnGenerator : MonoBehaviour
{
    int GetDificulty;
    public GameObject[] PrefabEnemies;

    public int width;
    public int height;
    public GameObject tilePrefab;
    public GameObject[,] AllTiles;

    public int enemyLines;

    public GameObject GameController;
    int movementCounter;
    // Start is called before the first frame update
    void Start()
    {
        GetDificulty = PlayerPrefs.GetInt("Difficulty");

        SetUp();
    }
    public void SetUp()
    {
        AllTiles = new GameObject[width, height];
        if(GetDificulty == 1){
            CreateBoard();//level 1
        }
        else{
            CreateWallBrick();//level 2
        }
    }
    void CreateBoard(){
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (!AllTiles[i, j])
                { //instantiates background tile on every situation 
                    Vector2 tempPosition = new Vector2(i, j);
                    GameObject backgroundTile = Instantiate(tilePrefab,tempPosition, Quaternion.identity) as GameObject;
                    backgroundTile.transform.parent = this.transform;
                    backgroundTile.name = $"({i},{j})";

                    //
                    if(j >= width- enemyLines + 1 && i >= 1 && i < width - 1)
                    { //instantiate the aliens on the top 
                        GameObject Enemy = Instantiate(PrefabEnemies[Random.Range(0, 3)], new Vector3(0, 0, 0), Quaternion.identity);
                        Enemy.transform.SetParent(backgroundTile.transform, false);
                        AllTiles[i, j] = Enemy; //asignando enemigos a un tile existente
                        
                    }
                    else
                    {
                        GameObject EmptySpace = Instantiate(tilePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                        EmptySpace.transform.SetParent(backgroundTile.transform, false);
                        AllTiles[i, j] = EmptySpace;
                    }
                }
            }
        }
        GameController.GetComponent<GameController>().OnStartGame();//asign all the aliens to array
        StartCoroutine(StartMovement());
    }

    public void CreateWallBrick(){
        for (int i = 0; i < width; i++)//ancho
        {
            for (int j = 0; j < height ; j++)//largo
            {

                if (!AllTiles[i, j])
                { //instantiates background tile on every situation
                    float x = i;
                    float y = j;

                    if (y % 2 == 0){//si es par entonces
                        x -= .5f;
                    }
                    Vector2 tempPosition = new Vector2(x, y);
                    GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                    backgroundTile.transform.parent = this.transform;
                    backgroundTile.name = $"({i},{j})";

                    //
                    if (j >= width - enemyLines + 1 && i >= 1 && i < width - 1)
                    { //instantiate the aliens on the top 
                        GameObject Enemy = Instantiate(PrefabEnemies[Random.Range(0, 3)], new Vector3(0, 0, 0), Quaternion.identity);
                        Enemy.transform.SetParent(backgroundTile.transform, false);
                        AllTiles[i, j] = Enemy; //asignando enemigos a un tile existente

                    }
                    else
                    {
                        GameObject EmptySpace = Instantiate(tilePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                        EmptySpace.transform.SetParent(backgroundTile.transform, false);
                        AllTiles[i, j] = EmptySpace;
                    }

                }
            }
        }
        GameController.GetComponent<GameController>().OnStartGame();//asign all the aliens to array
        StartCoroutine(StartMovement());
    }

    public void OnGameOver()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (AllTiles[i, j] != null)
                {
                    Destroy(AllTiles[i, j]);//esta funcion limpia
                }
            }
        }
        StopAllCoroutines();//stop movement
    }
    
    IEnumerator StartMovement()
    {
        
        yield return new WaitForSecondsRealtime(6);
        bool oneMove = true;
        if(movementCounter == 0 && oneMove){
            moveLeft();
            oneMove = false;
        }
        if(movementCounter == 1 && oneMove || movementCounter == 3 && oneMove)
        {
            moveDown();
            oneMove = false;
        }
        else if(movementCounter == 2 && oneMove)
        {
            moveRight();
            oneMove = false;
        }
        movementCounter++;
        if (movementCounter > 3)
        {
            movementCounter = 0;
        }
        StopCoroutine(StartMovement());
        StartCoroutine(StartMovement());
    }
    void moveDown()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (j >= 1)
                {
                    if (AllTiles[i, j] != null)
                    {
                        AllTiles[i, j].transform.position = new Vector2(AllTiles[i, j].transform.position.x, AllTiles[i, j].transform.position.y - 1); //marca error por que no hay uno asignado
                    }
                }
            }
        }
    }
    void moveLeft(){
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (j >= 1)
                {
                    if (AllTiles[i, j] != null)
                    {
                        AllTiles[i, j].transform.position = new Vector2(AllTiles[i, j].transform.position.x-1, AllTiles[i, j].transform.position.y); //marca error por que no hay uno asignado
                    }
                }
            }
        }
    }
    void moveRight()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (j >= 1)
                {
                    if (AllTiles[i, j] != null)
                    {
                        AllTiles[i, j].transform.position = new Vector2(AllTiles[i, j].transform.position.x + 1, AllTiles[i, j].transform.position.y); //marca error por que no hay uno asignado
                    }
                }
            }
        }
    }
}
