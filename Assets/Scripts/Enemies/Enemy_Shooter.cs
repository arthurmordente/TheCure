using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooter : Enemy
{
    public Enemy_Shooter() : base(50,20.0f){
    }
    public GameObject EnemyShot;
    public float Fire_Interval = 2.0f;
    private float Time_to_shoot;
    public Transform shotSpawn;


    protected override void Start()
    {
        base.Start();
        Time_to_shoot = 2.0f;   
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(ready){
            if(player_Script.isAlive){
                base.Update();
                Time_to_shoot -= Time.deltaTime;
                if(Time_to_shoot<=0.0f){
                    Shoot();
                    Time_to_shoot = Fire_Interval;
                }    
            } 
        }  
    }
    void Shoot(){
        Instantiate(EnemyShot, shotSpawn.position, Quaternion.identity);

    }
}
