using UnityEngine;

public class Enemy_BuffedSphere : Enemy
{
    public Enemy_BuffedSphere() : base(1, 40.0f) { }

    public GameObject bulletPrefab;
    public float shootingInterval = 1.0f; // Intervalo entre os tiros
    public Transform[] shootingPoints; // Array dos pontos de tiro

    private int currentShootingPointIndex = -1; // Inicializa com -1 para indicar que ainda não começou

    protected override void Start()
    {
        base.Start();
        if (shootingPoints.Length != 8)
        {
            Debug.LogError("Deve haver exatamente 8 pontos de tiro configurados.");
            return;
        }

        InvokeRepeating("ShootFromRandomPoint", 0f, shootingInterval);

        Collider enemyCollider = GetComponent<Collider>();
        if (enemyCollider != null)
        {
            enemyCollider.enabled = true;
        } 
    }

    protected override void Update()
    {
        base.Update();
    }

    private void ShootFromRandomPoint()
    {
        // Se é o primeiro tiro, escolha um ponto aleatório
        if (currentShootingPointIndex == -1)
        {
            currentShootingPointIndex = Random.Range(0, shootingPoints.Length);
        }
        else
        {
            // Após o primeiro tiro, move para o próximo ponto
            currentShootingPointIndex = (currentShootingPointIndex + 1) % shootingPoints.Length;
        }

        Transform shootingPoint = shootingPoints[currentShootingPointIndex];
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        
        currentShootingPointIndex = (currentShootingPointIndex + 4) % shootingPoints.Length;
        shootingPoint = shootingPoints[currentShootingPointIndex];
        GameObject bullet2 = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
       
        // Adicione aqui a lógica para dar velocidade ao projétil, se necessário

    }

    protected override void DestroyEnemy(){
        audioplayer.Enemy_death.Play();
        isDefeated = true;
        Destroy(gameObject); 
    }
}
