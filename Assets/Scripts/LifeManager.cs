using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    int vidas;
    int lives;
    public Logic logic;

    // As naves (objetos filhos)
    public GameObject[] lifeNaves;

    // Start is called before the first frame update
    void Start()
    {
        lives = logic.playerLives;
    }

    // Update is called once per frame
    void Update()
    {
        lives = logic.playerLives;
        if (lives != vidas)
        {
            ChangeLivesUI();
        }
    }

    public void ChangeLivesUI()
    {
        // Desativar todas as naves
        foreach (var nave in lifeNaves)
        {
            nave.SetActive(false);
        }

        // Ativar as naves de acordo com o número de vidas
        for (int i = 0; i < lives; i++)
        {
            if (i < lifeNaves.Length)
            {
                lifeNaves[i].SetActive(true);
            }
        }

        vidas = lives;
    }
}
