using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatsAtoI : MonoBehaviour
{


    public Act1Script ato1;
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
            ato1.Wave1();
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            player_logic.DestroyAllEnemiesOnScreen();
            player_logic.KillCount = 7;
            ato1.Wave2();
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            player_logic.DestroyAllEnemiesOnScreen();
            player_logic.KillCount = 21;
            ato1.Wave3();
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            player_logic.DestroyAllEnemiesOnScreen();
            player_logic.KillCount = 28;
            ato1.Wave4();
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            player_logic.DestroyAllEnemiesOnScreen();
            player_logic.KillCount = 43;
            ato1.Wave5();
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            player_logic.DestroyAllEnemiesOnScreen();
            player_logic.KillCount = 65;
            ato1.BossWave();
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