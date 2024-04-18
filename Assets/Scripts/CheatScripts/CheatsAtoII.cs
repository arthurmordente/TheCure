using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatsAtoII : MonoBehaviour
{


    public Act2Script ato2;
    public Antibody player;
    public Logic player_logic;


    private void Start()
    {

    }
    private void Update()

    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            player_logic.DestroyAllEnemiesOnScreen();
            player_logic.KillCount = 0;
            ato2.WaveCounter = 1;
            ato2.Wave1();
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            player_logic.DestroyAllEnemiesOnScreen();
            player_logic.KillCount = 5;
            ato2.WaveCounter = 2;
            ato2.Wave2();
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            player_logic.DestroyAllEnemiesOnScreen();
            player_logic.KillCount = 10;
            ato2.WaveCounter = 3;
            ato2.Wave3();
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            player_logic.DestroyAllEnemiesOnScreen();
            player_logic.KillCount = 17;
            ato2.WaveCounter = 4;
            ato2.Wave4();
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            player_logic.DestroyAllEnemiesOnScreen();
            player_logic.KillCount = 26;
            ato2.WaveCounter = 5;
            ato2.Wave5();
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            player_logic.DestroyAllEnemiesOnScreen();
            player_logic.KillCount = 65;
            ato2.WaveCounter = 6;
            ato2.BossWave();
        }
        else if (Input.GetKeyDown(KeyCode.F7))
        {
            player_logic.ManageHealth(1);
            player.StartInvulnerability(300);
        }
        else if (Input.GetKeyDown(KeyCode.F8))
        {
            player.power = 300;
        }
    }


}