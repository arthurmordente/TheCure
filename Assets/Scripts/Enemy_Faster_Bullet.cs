using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Faster_Bullet : MonoBehaviour
{
    public float speed = 15.0f; // Velocidade da bala
    public GameObject bullet;
    public Rigidbody tiroRigidbody;



    void Start(){
        tiroRigidbody = bullet.GetComponent<Rigidbody>();
    }

    void Update()
    {
       if(tiroRigidbody.velocity==Vector3.zero){ 
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        
        if(transform.position.z<(-50)||(transform.position.z>(50))){
            Destroy(bullet);
        }
    }
    void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Wall")){
                Destroy(gameObject);
            }
        }
}
