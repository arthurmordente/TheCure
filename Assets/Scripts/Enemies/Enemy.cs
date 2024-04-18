using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public int Health { get; set; }
    public float Speed { get; private set; }
    
    public bool ready;

    public GameObject player;
    public GameObject Manager;
    public Antibody player_Script;
    public Logic logic;
    public Vector3 spawnPosition;
    public Animator animator;
    public AudioManager audioplayer;
    public Transform destino;

    public bool isDefeated;


    public Enemy(int health, float speed)
    {
        Health = health;
        Speed = speed;
    }
    protected virtual void Start()
    {
        ready = false;
        isDefeated = false;
        spawnPosition = new Vector3(0.0f,0.0f,80.0f);
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        player_Script = player.GetComponent<Antibody>();
        Manager = GameObject.FindWithTag("Manager");
        logic = Manager.GetComponent<Logic>(); 
        audioplayer = Manager.GetComponent<AudioManager>();
        Collider enemyCollider = GetComponent<Collider>();
        if (enemyCollider != null)
        {
            enemyCollider.enabled = false;
        }    
    }

    protected virtual void Update()
    {
    if(Health <= 0){
        DestroyEnemy();
        }
    }
    


    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BulletFired")){
            Destroy(other.gameObject);
            Health -= player_Script.tiroAtual.Dano;
            audioplayer.Enemy_hit.Play();
            // Debug.Log(Health);
        }
    }
    protected virtual void DestroyEnemy()
    {
        logic.KillCount++;
        audioplayer.Enemy_death.Play();
        if(gameObject.transform.position.z>-35){
            logic.TryToDrop(gameObject.transform);
        }
        isDefeated = true;
        Destroy(gameObject);
    }

    public void DefinirDestino(Transform novoDestino)
    {        
        destino = novoDestino;
    }
    public void Move(Action onMoveComplete)
    {
        StartCoroutine(SlideToWaypoint());
        StartCoroutine(OnMoveComplete(onMoveComplete));
    }
    IEnumerator SlideToWaypoint()
    {
        float journeyLength = Vector3.Distance(spawnPosition, destino.position);
        float startTime = Time.time;

        while (transform.position != destino.position)
        {
            float distanceCovered = (Time.time - startTime) * Speed;
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(spawnPosition, destino.position, fractionOfJourney);
            yield return null;
        }
    }
    private IEnumerator OnMoveComplete(Action onMoveComplete)
{
    while (Vector3.Distance(transform.position, destino.position) > 0.01f)
    {
        yield return null;
    }

    if (onMoveComplete != null)
    {
        onMoveComplete();
    }
}





}
