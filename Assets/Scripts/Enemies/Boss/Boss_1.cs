using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss_1 : Enemy
{
    public Boss_1() : base(2000, 5.0f) { }

    private int lastAttackPattern = -1;
    private List<System.Func<IEnumerator>> attackPatternCreators;

    public GameObject bulletPrefab;

    public GameObject fasterBulletPrefab;
    public Transform bulbShootingPoint;
    public Transform cannonShootingPoint;

    public Transform arm;
    public int dropNumber;

    protected override void Start()
    {
        base.Start();
        dropNumber = 0;
        attackPatternCreators = new List<System.Func<IEnumerator>>
        {
            Pattern1, 
            Pattern2, 
            Pattern3  
        };
        StartCoroutine(AttackRoutine());
    }

    protected override void Update()
    {
        base.Update();
        if(Health<=1600 && dropNumber == 0){
            logic.Drop(gameObject.transform);
            dropNumber++;
        }
        if(Health<=1200 && dropNumber == 1){
            logic.Drop(gameObject.transform);
            dropNumber++;
        }
        if(Health<=800 && dropNumber == 2){
            logic.Drop(gameObject.transform);
            dropNumber++;
        }
        if(Health<=400 && dropNumber == 3){
            logic.Drop(gameObject.transform);
            dropNumber++;
        }
    }

    protected override void DestroyEnemy()
    {
        logic.GameWon();
        Destroy(gameObject);
    }

    private IEnumerator AttackRoutine()
    {
        yield return new WaitUntil(() => ready);
        while (true)
        {
            if(player_Script.isAlive){
                int nextPattern = GetNextAttackPattern();
                yield return StartCoroutine(attackPatternCreators[nextPattern]());
            }           
            yield return new WaitForSeconds(3.0f);
        }
    }

    private int GetNextAttackPattern()
    {
        int nextPattern;
        do
        {
            nextPattern = Random.Range(0, attackPatternCreators.Count);
        }
        while (nextPattern == lastAttackPattern);

        lastAttackPattern = nextPattern;
        return nextPattern;
    }

    private IEnumerator Pattern1()
    {
        if (!player_Script.isAlive)
        {
            yield break; 
        }
        Debug.Log("Entrando no Pattern 1");
        float angleStep = 360f / 32;
        for (int j = 0; j < 5; j++) 
        {
            float angle = 0f;
            for (int i = 0; i < 32; i++)
            {
                Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
                GameObject bullet = Instantiate(bulletPrefab, bulbShootingPoint.position, rotation);
                angle += angleStep + j*2;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator Pattern2()
    {
        if (!player_Script.isAlive)
        {
            yield break; 
        }
        Debug.Log("Executando Pattern 2");
        float shootingDuration = 4.0f; 
        float timeBetweenShots = 0.2f; 
        float elapsedTime = 0f;

        while (elapsedTime < shootingDuration)
        {
            if (player_Script != null && player_Script.isAlive)
            {
                Vector3 playerPosition = player_Script.transform.position;
                
                
                Vector3 directionToPlayer = (playerPosition - cannonShootingPoint.position).normalized;

                
                cannonShootingPoint.rotation = Quaternion.LookRotation(-directionToPlayer);

                Instantiate(fasterBulletPrefab, cannonShootingPoint.position, cannonShootingPoint.rotation);
            }

            elapsedTime += timeBetweenShots;
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }



    private IEnumerator Pattern3()
    {
        if (!player_Script.isAlive)
        {
            yield break; 
        }
        Debug.Log("Executando Pattern 3");

        float chaseDuration = 3.5f; // 
        float returnDuration = 1.0f;
        float chaseSpeed = 20.0f;

        Vector3 originalPosition = arm.position;
        Quaternion originalRotation = arm.rotation;

        // Fase de perseguição
        float elapsedTime = 0f;
        while (elapsedTime < chaseDuration)
        {
            if (player_Script != null && player_Script.isAlive)
            {
                Vector3 playerPosition = player_Script.transform.position;
                arm.position = Vector3.MoveTowards(arm.position, playerPosition, Time.deltaTime * chaseSpeed);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Fase de retorno
        elapsedTime = 0f;
        while (elapsedTime < returnDuration)
        {
            arm.position = Vector3.Lerp(arm.position, originalPosition, elapsedTime / returnDuration);
            arm.rotation = Quaternion.Lerp(arm.rotation, originalRotation, elapsedTime / returnDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // O braço retornou à posição original
    }

}
