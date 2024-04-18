using UnityEngine;
using System.Collections;

public class Enemy_Spawner : Enemy
{
    public GameObject spherePrefab; // Prefab do inimigo Sphere
    public float spawnInterval = 6.0f; // Intervalo de tempo para spawnar uma nova Sphere
    public Vector2 spawnAreaMin; // Canto inferior esquerdo da área de spawn
    public Vector2 spawnAreaMax; // Canto superior direito da área de spawn

    public Enemy_Spawner() : base(60, 15.0f) { }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(SpawnRoutine());
    }

    protected override void Update()
    {
        base.Update();
    }

    private IEnumerator SpawnRoutine()
    {
        // Aguarda até que 'ready' seja verdadeiro
        yield return new WaitUntil(() => ready);

        while (true)
        {
            if (player_Script.isAlive)
            {
                SpawnSphere();
                yield return new WaitForSeconds(spawnInterval);
            }
            else
            {
                // O jogador está morto, espere um pouco antes de verificar novamente
                yield return new WaitForSeconds(1.0f); // Espere 1 segundo antes de verificar novamente
            }
        }
    }

    private void SpawnSphere()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            0, // Ajuste esta linha se estiver em 3D
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        GameObject sphere = Instantiate(spherePrefab, transform.position, Quaternion.identity);
        StartCoroutine(MoveSphereToPosition(sphere, spawnPosition));
    }

    private IEnumerator MoveSphereToPosition(GameObject sphere, Vector3 targetPosition)
    {
        // Verifica se o objeto sphere ainda não foi destruído
        while (sphere != null)
        {
            Enemy_Sphere sphereScript = sphere.GetComponent<Enemy_Sphere>();
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
