using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act2Script : MonoBehaviour
{
    public GameObject EnemyShooter;
    public GameObject EnemySpawner;
    public GameObject EnemyLaser;
    public GameObject EnemyMine;
    public GameObject Boss2;
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
        if((logic.KillCount==5) & (WaveCounter==1)){
            WaveCounter = 2;
            logic.Wait(2.5f,() =>
            {                
                Wave2();
            });
        }
        if((logic.KillCount==10) & (WaveCounter==2)){
            WaveCounter = 3;
            logic.Wait(2.5f,() =>
            {
                Wave3();
            });
        }
        if((logic.KillCount==17) & (WaveCounter==3)){
            WaveCounter = 4;
            logic.Wait(2.5f,() =>
            {
                Wave4();
            });
        }
        if((logic.KillCount==26) & (WaveCounter==4)){
            WaveCounter = 5;
            logic.Wait(2.5f,() =>
            {
                Wave5();
            });
        }
        if(WaveCounter==5){
            if(logic.KillCount<42){
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
            if(logic.KillCount>40)
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
        logic.SpawnOnScreen(EnemyShooter,17,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,20,SpawnPoint);
        logic.SpawnOnScreen(EnemyLaser,8,SpawnPoint);
        logic.SpawnOnScreen(EnemyLaser,12,SpawnPoint);
        
    }
    public void Wave2(){

        Debug.Log("Voce chegou na wave " + WaveCounter);
        logic.SpawnOnScreen(EnemySpawner,0,SpawnPoint);
        logic.SpawnOnScreen(EnemySpawner,6,SpawnPoint);
        logic.SpawnOnScreen(EnemyLaser,8,SpawnPoint);
        logic.SpawnOnScreen(EnemyLaser,12,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,3,SpawnPoint);

    }
    public void Wave3(){
        Debug.Log("Voce chegou na wave " + WaveCounter);
        logic.SpawnOnScreen(EnemyLaser,7,SpawnPoint);
        logic.SpawnOnScreen(EnemyLaser,16,SpawnPoint);
        logic.SpawnOnScreen(EnemyLaser,18,SpawnPoint);
        logic.SpawnOnScreen(EnemyLaser,13,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,8,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,12,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,3,SpawnPoint);     

    }
    public void Wave4(){
        Debug.Log("Voce chegou na wave " + WaveCounter);
        logic.SpawnOnScreen(EnemyLaser,8,SpawnPoint);
        logic.SpawnOnScreen(EnemyLaser,9,SpawnPoint);
        logic.SpawnOnScreen(EnemyLaser,11,SpawnPoint);
        logic.SpawnOnScreen(EnemyLaser,12,SpawnPoint);
        logic.SpawnOnScreen(EnemySpawner,0,SpawnPoint);
        logic.SpawnOnScreen(EnemySpawner,3,SpawnPoint);
        logic.SpawnOnScreen(EnemySpawner,6,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,7,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,13,SpawnPoint);
        
        

    }
    public void Wave5(){
        WaveCounter = 5;
        Debug.Log("Voce chegou na wave " + WaveCounter);
        logic.SpawnOnScreen(EnemySpawner,0,SpawnPoint);
        logic.SpawnOnScreen(EnemyLaser,7,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,14,SpawnPoint);
        logic.SpawnOnScreen(EnemySpawner,6,SpawnPoint);
        logic.SpawnOnScreen(EnemyLaser,13,SpawnPoint);
        logic.SpawnOnScreen(EnemyShooter,20,SpawnPoint);

        logic.Wait(22.5f,() =>
        {
            logic.SpawnOnScreen(EnemyShooter,1,SpawnPoint);
            logic.SpawnOnScreen(EnemyShooter,3,SpawnPoint);
            logic.SpawnOnScreen(EnemyShooter,5,SpawnPoint);
        });
    }

    public void BossWave(){
        logic.ChangeToBossBGM();
        logic.SpawnOnScreen(Boss2,10,SpawnPoint);
    }                  
}
