using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiros : MonoBehaviour
{
    public int Dano { get;  set; }
    public float Speed { get;  set; }

    public float fireRate { get;  set; }
    public Transform[] shotSpawns;
    protected GameObject bullet;

    protected GameObject current_burret;
    protected Bullet bullet_movement;
    [SerializeField] public Material material;


    public Tiros(int dano, float speed, float fire_rate)
    {
        Dano = dano;
        Speed = speed;
        fireRate = fire_rate;
    }

    public Material Material
    {
        get { return material; }

    }

    public virtual void Shoot_0()
    {
        Debug.Log("ERRO! SEM ARMA SELECIONADA");
    }
    public virtual void Shoot_1()
    {
        Debug.Log("ERRO! SEM ARMA SELECIONADA");
    }
    public virtual void Shoot_2()
    {
        Debug.Log("ERRO! SEM ARMA SELECIONADA");
    }
    public virtual void Shoot_MAX()
    {
        Debug.Log("ERRO! SEM ARMA SELECIONADA");
    }
    public void Manage_Bullet(){
        bullet = GameObject.FindWithTag("Bullet");
        bullet_movement = bullet.GetComponent<Bullet>();
        bullet_movement.speed = Speed;
        bullet.tag = "BulletFired";
    }
}