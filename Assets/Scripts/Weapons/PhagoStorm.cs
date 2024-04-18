using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhagoStorm : Tiros
{
    public GameObject projectile_1;
    public GameObject projectile_max;
    public bool isCurrentShot;

    public PhagoStorm() : base(10,20.0f,0.5f){
    }

    public override void Shoot_0()
    {
        Instantiate(projectile_1, shotSpawns[0].position, shotSpawns[0].rotation);
        Manage_Bullet();

    }

    public override void Shoot_1()
    {


        Quaternion rotation1 = Quaternion.Euler(0, 0, -10);
        Instantiate(projectile_1, shotSpawns[1].position, shotSpawns[1].rotation * rotation1);
        Manage_Bullet();


        Quaternion rotationMinus1 = Quaternion.Euler(0, 0, 10);
        Instantiate(projectile_1, shotSpawns[2].position, shotSpawns[2].rotation * rotationMinus1);
        Manage_Bullet();
    }

    public override void Shoot_2()
    {
        
        Shoot_0();
        Shoot_1();
    
    }

    public void Shoot_0(GameObject bullet)
    {
        Instantiate(bullet, shotSpawns[0].position, shotSpawns[0].rotation);
        Manage_Bullet();

    }
    public void Shoot_1(GameObject bullet)
    {


        Quaternion rotation1 = Quaternion.Euler(0, 0, -10);
        Instantiate(bullet, shotSpawns[1].position, shotSpawns[1].rotation * rotation1);
        Manage_Bullet();
        Quaternion rotationMinus1 = Quaternion.Euler(0, 0, 10);
        Instantiate(bullet, shotSpawns[2].position, shotSpawns[2].rotation * rotationMinus1);
        Manage_Bullet();
    }

    public override void Shoot_MAX()
    {
        Shoot_0(projectile_max);
        Shoot_1(projectile_max);


        Quaternion rotation2 = Quaternion.Euler(0, 0, -20);
        Instantiate(projectile_max, shotSpawns[3].position, shotSpawns[3].rotation * rotation2);
        Manage_Bullet();


        Quaternion rotationMinus2 = Quaternion.Euler(0, 0, 20);
        Instantiate(projectile_max, shotSpawns[4].position, shotSpawns[4].rotation * rotationMinus2);
        Manage_Bullet();
    }
}