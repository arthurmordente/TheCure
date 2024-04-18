using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusBane : Tiros
{
    public GameObject projectile_1;
    public GameObject projectile_max;
    public bool isCurrentShot;

   
    public VirusBane() : base(10,20.0f,0.5f){
    }

    public override void Shoot_0(){
        Instantiate(projectile_1, shotSpawns[0].position , shotSpawns[0].rotation);
        Manage_Bullet();
    }
    public override void Shoot_1(){
        Shoot_0();
        for(int i=1;i<3;i+=1){
            Instantiate(projectile_1, shotSpawns[i].position , shotSpawns[i].rotation);
            Manage_Bullet();
        }
    }
    public override void Shoot_2(){
        Shoot_1();
        for(int i=3;i<5;i++){
            Instantiate(projectile_1, shotSpawns[i].position , shotSpawns[i].rotation);
            Manage_Bullet();
        }
    }
    public override void Shoot_MAX(){
        for(int i=0;i<5;i++){
            Instantiate(projectile_max, shotSpawns[i].position , shotSpawns[i].rotation);
            Manage_Bullet();
        }
    }
}
