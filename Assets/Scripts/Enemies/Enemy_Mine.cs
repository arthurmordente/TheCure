using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mine : Enemy
{
    public Enemy_Mine() : base(10, 7.5f)
    {
    }
    public GameObject EnemyShot;
    public Transform shotSpawn;

    public float rajadaVelocidade = 18.0f;

    private Collider mineCollider; // Referência para o colisor do Enemy_Mine

    protected override void Start()
    {
        base.Start();
        transform.rotation = Quaternion.Euler(0, 90.0f, 0);
        ready = true;

        // Obtém o colisor do Enemy_Mine
        mineCollider = GetComponent<Collider>();

        // Desativa o colisor no início
        mineCollider.enabled = false;

        // Inicia uma rotina para ativar o colisor após um segundo
        StartCoroutine(ActivateColliderAfterDelay(1.0f));
    }

    protected override void Update()
    {
        if (ready)
        {
            if (player_Script.isAlive)
            {
                transform.Translate(Vector3.right * Speed * Time.deltaTime);
            }
            base.Update();
        }
    }

    protected override void DestroyEnemy()
    {
        for (int i = 0; i < 8; i++)
        {
            float angle = i * 45.0f;
            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad));
            GameObject novoTiro = Instantiate(EnemyShot, shotSpawn.transform.position, Quaternion.identity);
            Enemy_Bullet scriptDoTiro = novoTiro.GetComponent<Enemy_Bullet>();
            if (scriptDoTiro != null)
            {
                // Desativa o componente de script.
                scriptDoTiro.enabled = false;
            }
            Rigidbody tiroRigidbody = novoTiro.GetComponent<Rigidbody>();
            if (tiroRigidbody == null)
            {
                tiroRigidbody = novoTiro.AddComponent<Rigidbody>();
            }
            tiroRigidbody.velocity = direction * rajadaVelocidade;
        }
        base.DestroyEnemy();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Wall"))
        {
            DestroyEnemy();
        }
    }

    private IEnumerator ActivateColliderAfterDelay(float delay)
    {
        // Aguarda o tempo especificado
        yield return new WaitForSeconds(delay);

        // Ativa o colisor após o atraso
        mineCollider.enabled = true;
    }
}
