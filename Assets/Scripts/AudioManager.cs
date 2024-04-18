using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource Enemy_hit;
    public AudioSource Enemy_death;
    public AudioSource Power_up;
    public AudioSource Damage_taken;
    public AudioSource Damage_healed;
    public AudioSource Damage_negated;
    public AudioSource Boss_damaged;
    public AudioSource BGM;

    [SerializeField] Slider volumeSlider;

    public AudioSource Boss_BGM;
    public void PlayEnemy_hit(){
        Enemy_hit.Play();    
    }
    public void PlayEnemy_death(){
        Enemy_death.Play();    
    }
    public void PlayPower_up(){
        Power_up.Play();    
    }
    public void PlayDamage_taken(){
        Damage_taken.Play();    
    }
    public void PlayDamage_healed(){
        Damage_healed.Play();    
    }
    public void PlayDamage_negated(){
        Damage_negated.Play();    
    }
    public void PlayBoss_damaged(){
        Boss_damaged.Play();    
    }
    

    void Start(){
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else{
            Load();
        }
    }

    public void ChangeVolume(){
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    public void Load(){
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save(){
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}


