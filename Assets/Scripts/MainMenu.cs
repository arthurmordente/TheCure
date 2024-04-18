using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public AudioManager audio_man;

    void Start(){
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            audio_man.Load();
        }
        else
        {
            audio_man.Load();
        }
    }

    public void PlayGame(){
        SceneManager.LoadScene("Act_I");
    }

    public void QuitGame(){
        Application.Quit();
        Debug.Log("Jogo fechando.");
    }



}
