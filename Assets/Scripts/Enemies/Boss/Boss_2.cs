using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss_2 : Enemy
{
    public Boss_2() : base(2000, 7.5f) { }

    private int lastAttackPattern = -1;
    private List<System.Func<IEnumerator>> attackPatternCreators;

    public GameObject bulletPrefab;

    public GameObject fasterBulletPrefab;

    public GameObject spherePrefab;
    public Vector2 spawnAreaMin; // Canto inferior esquerdo da área de spawn
    public Vector2 spawnAreaMax; // Canto superior direito da área de spawn;
    public Transform[] bulbShootingPoints;

    public Transform spawnerShootingPoint;
    public Transform laserShootingPoint;

    public Transform cannonShootingPoint1;
    public Transform cannonShootingPoint2;
    public int dropNumber;
    public int sphereNumber = 2;

    float shootingDuration = 4.0f; 
    float timeBetweenShots = 0.5f;

    public GameObject telegraphPrefab;
    public GameObject laserPrefab;
    public Vector3[] rotInicial;
    public Vector3[] rotFinal;
    public float rotationChangeDelay = 3.0f; // Tempo para alterar a rotação

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
        StartCoroutine(Pattern4()); // Inicia o padrão 4 imediatamente

    }

    protected override void Update()
    {
        base.Update();
        if(Health<=1600 && dropNumber == 0){
            logic.Drop(gameObject.transform);
            dropNumber++;
            timeBetweenShots = 0.35f;
            logic.Wait(2.5f,() =>
            {
                logic.Drop(gameObject.transform);
            });
        }
        if(Health<=1200 && dropNumber == 1){
            logic.Drop(gameObject.transform);
            dropNumber++;
            sphereNumber++;
            rotationChangeDelay = 2.2f;
            logic.Wait(2.5f,() =>
            {
                logic.Drop(gameObject.transform);
            });
        }
        if(Health<=800 && dropNumber == 2){
            logic.Drop(gameObject.transform);
            dropNumber++;
            sphereNumber++;
            logic.Wait(2.5f,() =>
            {
                logic.Drop(gameObject.transform);
            });
        }
        if(Health<=400 && dropNumber == 3){
            logic.Drop(gameObject.transform);
            dropNumber++;
            rotationChangeDelay = 1.0f;
            timeBetweenShots = 0.22f;
            logic.Wait(2.5f,() =>
            {
                logic.Drop(gameObject.transform);
            });
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

                // Exclui o quarto padrão dos padrões sorteados
                while (nextPattern == 3)
                {
                    nextPattern = GetNextAttackPattern();
                }

                yield return StartCoroutine(attackPatternCreators[nextPattern]());
            }           
            yield return new WaitForSeconds(3.5f);
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
        Debug.Log("Executando Pattern 1");
        for(int i=0;i<sphereNumber;i++){
            SpawnSuperSphere();
        }
        yield return null;
    }

    private IEnumerator Pattern2()
    {
        if (!player_Script.isAlive)
        {
            yield break; 
        }
        Debug.Log("Executando Pattern 2");
        float elapsedTime = 0f;

        while (elapsedTime < shootingDuration){
            if (player_Script != null && player_Script.isAlive){
                Vector3 playerPosition = player_Script.transform.position;

                Vector3 directionToPlayer1 = (playerPosition - cannonShootingPoint1.position).normalized;
                Vector3 directionToPlayer2 = (playerPosition - cannonShootingPoint2.position).normalized;

                cannonShootingPoint1.rotation = Quaternion.LookRotation(-directionToPlayer1);
                cannonShootingPoint2.rotation = Quaternion.LookRotation(-directionToPlayer2);

                Instantiate(fasterBulletPrefab, cannonShootingPoint1.position, cannonShootingPoint1.rotation);
                yield return new WaitForSeconds(timeBetweenShots);
                Instantiate(fasterBulletPrefab, cannonShootingPoint2.position, cannonShootingPoint2.rotation);
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
        float telegraphDuration = 2.0f; // Duração do telegraph
        int randomDirection = Random.Range(0, 2);

        GameObject telegraph = Instantiate(telegraphPrefab, laserShootingPoint.position, Quaternion.Euler(rotInicial[randomDirection]));
        yield return new WaitForSeconds(telegraphDuration);
        Destroy(telegraph);
        GameObject laser = Instantiate(laserPrefab, laserShootingPoint.position, Quaternion.Euler(rotInicial[randomDirection]));
        
        Quaternion initialRotation = Quaternion.Euler(rotInicial[randomDirection]);
        Quaternion finalRotation = Quaternion.Euler(rotFinal[randomDirection]);

        float elapsedTime = 0f;
        while (elapsedTime < rotationChangeDelay)
        {
            // Interpola suavemente a rotação do laser de rotInicial para rotFinal
            laser.transform.rotation = Quaternion.Slerp(initialRotation, finalRotation, elapsedTime / rotationChangeDelay);
            // Atualiza o tempo decorrido
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(laser);
    }

    private IEnumerator Pattern4()
    {
        while (true)
        {
            if (!player_Script.isAlive)
            {
                yield break; 
            }
            Debug.Log("Entrando no Pattern 4");
            float angleStep = 360f / 32;
            if(ready){
                for (int j = 0; j < 3; j++) 
                {
                    float angle = 0f;
                    for (int i = 0; i < 32; i++)
                    {
                        Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
                        GameObject bullet = Instantiate(bulletPrefab, bulbShootingPoints[j].position, rotation);
                        angle += angleStep;
                    }
                    yield return new WaitForSeconds(0.5f);
                }
            }
            yield return new WaitForSeconds(10.0f); // Intervalo de tempo para o próximo ataque do padrão 4
        }
    }

    private void SpawnSuperSphere()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            0, // Ajuste esta linha se estiver em 3D
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        GameObject sphere = Instantiate(spherePrefab, spawnerShootingPoint.position, Quaternion.identity);
        StartCoroutine(MoveSphereToPosition(sphere, spawnPosition));
    }
    private IEnumerator MoveSphereToPosition(GameObject sphere, Vector3 targetPosition)
    {
        // Verifica se o objeto sphere ainda não foi destruído
        while (sphere != null)
        {
            Enemy_BuffedSphere sphereScript = sphere.GetComponent<Enemy_BuffedSphere>();
            if (sphereScript == null)
            {
                Debug.LogError("Script 'Enemy_Sphere' não encontrado no objeto.");
                yield break;
            }

            float speed = sphereScript.Speed;

            while (sphere != null && sphere.activeSelf && sphere.transform.position != targetPosition)
            {
                sphere.transform.position = Vector3.MoveTowards(sphere.transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }

            // Verifica novamente se o objeto sphere não foi destruído após o loop
            if (sphere != null)
            {
                // O objeto sphere chegou ao destino ou ainda está ativo, continue normalmente
                yield return null;
            }
            else
            {
                // O objeto sphere foi destruído antes de chegar ao destino, encerra o loop
                yield break;
            }
        }
    }


}
