using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    float Speed = .08f;

    bool Left, Right;
    public int TypeOfBullet;
    public GameObject [] PrefabBullet;



    bool CanShoot;
    public GameObject fireAnima;

    public bool canPlay;
    public GameObject GameController;
    void Start()
    {
        TypeOfBullet = 0;
        CanShoot = true;
        canPlay = true;
    }
    public void ResetGame()
    {
        TypeOfBullet = 0;
        canPlay = true;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
        
    }
    void Movement(){
        //GET KEYS DOWN, limit the user to press only one direction, Left o Right
        if (Input.GetKeyDown(KeyCode.A) && !Left && !Right && canPlay|| Input.GetKeyDown(KeyCode.LeftArrow) && !Left && !Right && canPlay)
        {
            fireAnima.SetActive(true);
            Left = true;
        }
        if (Input.GetKeyDown(KeyCode.D) && !Left && !Right && canPlay || Input.GetKeyDown(KeyCode.RightArrow) && !Left && !Right && canPlay)
        {
            fireAnima.SetActive(true);
            Right = true;
        }
        //GetKeyUp and set off the movement
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            fireAnima.SetActive(false);
            Left = false;
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            fireAnima.SetActive(false);
            Right = false;
        }
        //Limit the movement
        if (Left && gameObject.transform.position.x > (float)-2.57)
        {
            this.gameObject.transform.position = new Vector3(gameObject.transform.position.x - Speed, gameObject.transform.position.y);

        }
        if (Right && gameObject.transform.position.x < (float)14.10)
        {
            this.gameObject.transform.position = new Vector3(gameObject.transform.position.x + Speed, gameObject.transform.position.y);
        }
    }
    void Shoot(){
        //Shoot the bullet
        if (Input.GetKeyDown(KeyCode.W) && CanShoot && canPlay || Input.GetKeyDown(KeyCode.UpArrow) && CanShoot && canPlay )
        {
            GameObject Bullet = Instantiate(PrefabBullet[TypeOfBullet]);//select the type of bullet
            Bullet.transform.position = this.gameObject.transform.position;
            Bullet.GetComponent<BulletCode>().Instance = this.gameObject;
            Bullet.GetComponent<BulletCode>().Damage = TypeOfBullet + 1;
            gameObject.GetComponent<AudioSource>().Play();
            StartCoroutine(LimitBullet());
        }

    }
    IEnumerator LimitBullet(){
        CanShoot = false;
        yield return new WaitForSeconds((float)0.34);
        CanShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag.Contains("PowerUp"))
        {
            string Getter = col.gameObject.tag.ToString();//get the stirng
            string IntBulletType = Getter.Substring(Getter.Length -1);
            int.TryParse(IntBulletType, out TypeOfBullet); //get the type of with bullet 0,1 or 2 of the tag
            Destroy(col.gameObject);//destroy the powerUp
        }
        else if(col.gameObject.tag == "AlienBull"){
            fireAnima.SetActive(false);
            GameController.GetComponent<GameController>().OnGameOver();
        }
    }
}
