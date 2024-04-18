using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed { get; set; }
    private Rigidbody rb;
    public GameObject bullet;
    public GameObject Manager;
    public AudioManager audioplayer;
    public Logic logic;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = -transform.up * speed;
        Manager = GameObject.FindWithTag("Manager");
        logic = Manager.GetComponent<Logic>(); 
        audioplayer = Manager.GetComponent<AudioManager>();  
    }
    void Update()
    {
        //if(bullet!=null){
            bullet = GameObject.FindWithTag("BulletFired");
        //}
        if(bullet.transform.position.z>45){
            Destroy(bullet);
        }
    }
    void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Wall")){
                Destroy(gameObject);
            }
            if(other.CompareTag("Boss")){
                audioplayer.Damage_negated.Play();
                Destroy(gameObject);
            }
        }
}
