using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act1Script : MonoBehaviour
{
    public GameObject EnemyShooter;
    public GameObject EnemyChaser;
    public GameObject EnemyMine;
    public GameObject Boss1;
    public Logic logic;
    public GameObject SpawnPoint;
    public Transform[] OnScreen_Waypoints;
    public Transform[] OffScreen_Waypoints;
    
    private float frenzy_spawnTime = 0.0f;
    private float frenzy_interval = 5.5f;
    private bool spawnAtEvenPositions = false;


    public int WaveCounter;



    // Start is called before the first frame update
    void Start()
    {
        WaveCounter = 1;
        Invoke("Wave1", 2.0f);     
    }

    // Update is called once per frame
    void Update()
    {
        if((logic.KillCount==7) & (WaveCounter==1)){
            WaveCounter = 2;
            logic.Wait(2.5f,() =>
            {                
                Wave2();
            });
        }
        if((logic.KillCount==21) & (WaveCounter==2)){
            WaveCounter = 3;
            logic.Wait(2.5f,() =>
            {
                Wave3();
            });
        }
        if((logic.KillCount==28) & (WaveCounter==3)){
            WaveCounter = 4;
            logic.Wait(2.5f,() =>
            {
                Wave4();
            });
        }
        if((logic.KillCount==43) & (WaveCounter==4)){
            WaveCounter = 5;
            logic.Wait(2.5f,() =>
            {
                Wave5();
            });
        }
        if(WaveCounter==5){
            if(logic.KillCount<65){
                if(spawnAtEvenPositions){
                    if (Time.time >= frenzy_spawnTime){                   
                        logic.SpawnOffScreen(EnemyMine, 0);
                        logic.SpawnOffScreen(EnemyMine, 2);
                        logic.SpawnOffScreen(EnemyMine, 4);
                        logic.SpawnOffScreen(EnemyMine, 6);
                        frenzy_spawnTime = Time.time + frenzy_interval;
                        spawnAtEvenPositions = !spawnAtEvenPositions;
                    } 
                }else
                {
                    if (Time.time >= frenzy_spawnTime){                   
                        logic.SpawnOffScreen(EnemyMine, 1);
                        logic.SpawnOffScreen(EnemyMine, 3);
                        logic.SpawnOffScreen(EnemyMine, 5);
                        frenzy_spawnTime = Time.time + frenzy_interval;
                        spawnAtEvenPositions = !spawnAtEvenPositions;
                    }    
                }

            }
            if(logic.KillCount>59)
            {
                if(logic.CheckEnemiesAlive() && WaveCounter == 5){
                    BossWave();
                    WaveCounter = 6;
                }   
            }
        }
    }



    public void Wave1(){
        Debug.Log("Voce chegou na wave " + WaveCounter);
        logic.SpawnOnScreen(EnemyShooter,14,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,8,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,2,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,3,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,4,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,12,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,20,SpawnPoint);        

        
    }
    public void Wave2(){

        Debug.Log("Voce chegou na wave " + WaveCounter);
        logic.SpawnOnScreen(EnemyShooter,0,SpawnPoint);
        logic.SpawnOffScreen(EnemyMine,0);
        logic.SpawnOnScreen(EnemyShooter,8,SpawnPoint);
        logic.SpawnOffScreen(EnemyMine,1);
        logic.SpawnOnScreen(EnemyShooter,16,SpawnPoint);
        logic.SpawnOffScreen(EnemyMine,2);
        logic.SpawnOnScreen(EnemyShooter,24,SpawnPoint);
        logic.SpawnOffScreen(EnemyMine,3);
        logic.SpawnOnScreen(EnemyShooter,18,SpawnPoint);
        logic.SpawnOffScreen(EnemyMine,4);
        logic.SpawnOnScreen(EnemyShooter,12,SpawnPoint);
        logic.SpawnOffScreen(EnemyMine,5);
        logic.SpawnOnScreen(EnemyShooter,6,SpawnPoint);
        logic.SpawnOffScreen(EnemyMine,6);
    }
    public void Wave3(){
        Debug.Log("Voce chegou na wave " + WaveCounter);
        logic.SpawnOnScreen(EnemyChaser,7,SpawnPoint);
        logic.SpawnOnScreen(EnemyChaser,16,SpawnPoint);
        logic.SpawnOnScreen(EnemyChaser,18,SpawnPoint);
        logic.SpawnOnScreen(EnemyChaser,13,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,8,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,12,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,3,SpawnPoint);     

    }
    public void Wave4(){
        Debug.Log("Voce chegou na wave " + WaveCounter);
        logic.SpawnOnScreen(EnemyChaser,7,SpawnPoint);
        logic.SpawnOnScreen(EnemyChaser,9,SpawnPoint);
        logic.SpawnOnScreen(EnemyChaser,11,SpawnPoint);
        logic.SpawnOnScreen(EnemyChaser,13,SpawnPoint);
        logic.SpawnOnScreen(EnemyMine,0,SpawnPoint);
        logic.SpawnOnScreen(EnemyMine,2,SpawnPoint);
        logic.SpawnOnScreen(EnemyMine,4,SpawnPoint);
        logic.SpawnOnScreen(EnemyMine,6,SpawnPoint);
        logic.SpawnOffScreen(EnemyMine,0);
        logic.SpawnOffScreen(EnemyMine,2);
        logic.SpawnOffScreen(EnemyMine,4);
        logic.SpawnOffScreen(EnemyMine,6);
        logic.Wait(4.5f,() =>
        {
            logic.SpawnOffScreen(EnemyMine,1);
            logic.SpawnOffScreen(EnemyMine,3);
            logic.SpawnOffScreen(EnemyMine,5);
        });

    }
    public void Wave5(){
        WaveCounter = 5;
        Debug.Log("Voce chegou na wave " + WaveCounter);
        logic.SpawnOnScreen(EnemyShooter,0,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,1,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,5,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,6,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,9,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,10,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,11,SpawnPoint);
        logic.SpawnOnScreen(EnemyChaser,15,SpawnPoint);
        logic.SpawnOnScreen(EnemyChaser,17,SpawnPoint);
        logic.SpawnOnScreen(EnemyChaser,19,SpawnPoint);
        logic.SpawnOnScreen(EnemyChaser,28,SpawnPoint);
        logic.SpawnOnScreen(EnemyChaser,34,SpawnPoint);
        logic.SpawnOnScreen(EnemyMine,30,SpawnPoint);
        logic.SpawnOnScreen(EnemyMine,31,SpawnPoint);    
        logic.SpawnOnScreen(EnemyMine,32,SpawnPoint);
    }  

    public void BossWave(){
        logic.ChangeToBossBGM();
        logic.SpawnOnScreen(Boss1,10,SpawnPoint);
    }               
}
