using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienScript : MonoBehaviour
{
    public int LifeEnemy;
    int pointValue;
    public GameObject PrefabExplosion;
    public GameObject PrefabShoot;
    public GameObject []Bullets;

    GameObject GameController;
    
    // Start is called before the first frame update
    void Start()
    {
        pointValue = LifeEnemy;
        GameController = GameObject.FindGameObjectWithTag("Controller");
        if(pointValue == 3){
            StartCoroutine(AlienShoot());
        }
    }
    //only the aliens with 3 hp shoots
    IEnumerator AlienShoot(){
        yield return new WaitForSecondsRealtime(5);//every five seconds check if can shoot
        int randomShoot = Random.Range(0, 11); //only 9 and 10 shoots
        if(randomShoot >= 9){
            GameObject AlienShoot = Instantiate(PrefabShoot);//create vfx
            AlienShoot.transform.position = this.gameObject.transform.position;
        }
        StopCoroutine(AlienShoot());
        StartCoroutine(AlienShoot());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Bullet")
        {
            LifeEnemy -= col.gameObject.GetComponent<BulletCode>().Damage;//add damage with the type of bullet
            if(LifeEnemy <= 0){
                GameController.GetComponent<GameController>().AddToScore(pointValue, this.gameObject);                //add score
                GameObject VFX = Instantiate(PrefabExplosion);//create vfx
                VFX.transform.position = this.gameObject.transform.position;//asign position to the vfx
                Destroy(VFX, 1.4f);//destroy the vfx
                //Generate random number
                int Drop = Random.Range(0, 9);
                if(Drop >= 7){//if the number randomized be 7 or 8, the alien drops a bullet
                    int Rand = Random.Range(0, 3);//type of bullet 0,1,2
                    //col.gameObject.GetComponent<BulletCode>().Instance.GetComponent<GameManager>().TypeOfBullet = Rand;

                    GameObject NewBullet = Instantiate(Bullets[Rand]);//new bullet
                    NewBullet.transform.position = this.transform.position; //copy position of the alien
                }
                StopAllCoroutines();
                Destroy(this.gameObject);//destroy the alien
            }
            Destroy(col.gameObject);
        }

    }


}
