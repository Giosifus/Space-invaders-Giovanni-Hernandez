using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCode : MonoBehaviour
{
    public float BulletVelocity;
    public GameObject Instance;
    public int Damage;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + BulletVelocity);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "CleanSpace"){
            Destroy(this.gameObject);//si la bala sale del espacio entonces se elimina para no generar basura
        }
    }
}
