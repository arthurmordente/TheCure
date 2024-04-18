using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Laser : Enemy
{

    public Enemy_Laser() : base(75,25.0f){
    }

    public GameObject telegraphPrefab; // O GameObject que simboliza o telegraph do ataque
    public GameObject laserPrefab; // O GameObject que simboliza o laser ativo
    public Transform laserSpawnPoint; // Ponto de onde o laser será gerado.
    
    public float telegraphDuration = 2.0f; // Duração do telegraph
    public float laserDuration = 2.0f; // Duração do laser ativo
    public float delayBetweenTelegraphAndLaser = 1.0f; // Atraso entre o fim do telegraph e o início do laser
    public float attackCooldown = 5.0f; // Cooldown total do ataque
    
    private GameObject telegraphInstance;
    private GameObject laserInstance;
    private Vector3 lastPlayerPosition;
    
    protected override void Start()
    {
        base.Start();
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        while (!isDefeated)
        {

            yield return new WaitForSeconds(attackCooldown);
            if (isDefeated) break;
                         
            if(player_Script.isAlive){
                telegraphInstance = Instantiate(telegraphPrefab, laserSpawnPoint.position, Quaternion.identity);
                StartCoroutine(FollowPlayer(telegraphDuration));
            }          
            yield return new WaitForSeconds(telegraphDuration);
            if (isDefeated) break;
            
            Destroy(telegraphInstance);
            yield return new WaitForSeconds(delayBetweenTelegraphAndLaser);
            if (isDefeated) break;

            if(player_Script.isAlive){
                laserInstance = Instantiate(laserPrefab, lastPlayerPosition, Quaternion.identity);
                PointLaserAt(lastPlayerPosition);
            }
            
            yield return new WaitForSeconds(laserDuration);
            if (isDefeated) break;


            Destroy(laserInstance);
        }
    }

    private IEnumerator FollowPlayer(float duration)
    {
        float startTime = Time.time;
        while ((Time.time - startTime < duration))
        {
            lastPlayerPosition = player.transform.position; 
            telegraphInstance.transform.position = laserSpawnPoint.position;
            telegraphInstance.transform.LookAt(lastPlayerPosition);              
            yield return null;
        }
    }

    private void PointLaserAt(Vector3 targetPosition)
    {
        laserInstance.transform.position = laserSpawnPoint.position;
        laserInstance.transform.LookAt(targetPosition);
    }

    protected override void DestroyEnemy()
    {
        base.DestroyEnemy();
        if (telegraphInstance != null)
        {
            Destroy(telegraphInstance);
        }
        
        if (laserInstance != null)
        {
            Destroy(laserInstance);
        }
    }
}