using UnityEngine;
using System.Collections;

public class Enemy_DoubleShooter : Enemy
{
    public Enemy_DoubleShooter() : base(50, 25.0f) { }

    public GameObject bulletPrefab; // Prefab da bala
    public Transform bulletSpawnPointLeft; // Ponto de onde as balas serão instanciadas
    public Transform bulletSpawnPointRight; // Ponto de onde as balas serão instanciadas
    public float initialFireRate = 1.0f; // Taxa inicial de tiro (tiros por segundo)
    public float maxFireRate = 2.0f; // Taxa máxima de tiro
    public float BarrageFireRate = 6.0f; // Taxa máxima de tiro
    public float rampUpTime = 7.0f; // Tempo para atingir a taxa máxima de tiro
    public float cooldownDuration = 5.0f; // Tempo de resfriamento após alcançar a taxa máxima

    protected override void Start()
    {
        base.Start();
        StartCoroutine(AttackRoutine());
    }

    protected override void Update()
    {
        base.Update();
    }

    private IEnumerator AttackRoutine()
    {
        yield return new WaitUntil(() => ready);
        while (true)
        {          
            float timePassed = 0f;
            while (timePassed < rampUpTime)
            {
                float progress = timePassed / rampUpTime; // Progresso da interpolação
                float currentFireRate = initialFireRate + (maxFireRate - initialFireRate) * progress;
                float timeBetweenShots = 1f / currentFireRate;
                
                yield return new WaitForSeconds(timeBetweenShots);
                if(player_Script.isAlive){
                    Shoot();
                }
                timePassed += timeBetweenShots;
            }

            // Chama a barragem de tiros ao final do rampUpTime
            StartCoroutine(FireBarrage());

            // Aguarda o fim da barragem e o cooldown
            yield return new WaitForSeconds(cooldownDuration); 
        }
    }

    private IEnumerator FireBarrage()
    {
        float barrageDuration = 1.5f; // Exemplo: 2 segundos de barragem
        float barrageTime = 0f;

        while (barrageTime < barrageDuration)
        {
            float timeBetweenShots = 1f / BarrageFireRate;
            yield return new WaitForSeconds(timeBetweenShots);
            if(player_Script.isAlive){
                Shoot();
            }
            barrageTime += timeBetweenShots;
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawnPointLeft.position, bulletSpawnPointLeft.rotation);
        Instantiate(bulletPrefab, bulletSpawnPointRight.position, bulletSpawnPointRight.rotation);
    }
}
