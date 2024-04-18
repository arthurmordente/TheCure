using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Head2 : MonoBehaviour
{
    public GameObject boss;
    public Boss_2 scriptBoss;
    public Renderer blinkingPartRenderer; // Referência ao renderer da parte que piscará
    public Color normalColor = Color.white;
    public Color blinkColor = Color.red;
    public float blinkDuration = 0.2f;

    private Material blinkingMaterial;
    private bool isBlinking = false;
    // Start is called before the first frame update
    void Start()
    {
        scriptBoss = boss.GetComponent<Boss_2>();
        if (blinkingPartRenderer != null)
        {
            blinkingMaterial = blinkingPartRenderer.material;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BulletFired")){
            Destroy(other.gameObject);
            scriptBoss.Health -= scriptBoss.player_Script.tiroAtual.Dano;
            scriptBoss.audioplayer.Boss_damaged.Play();
            if (blinkingMaterial != null && !isBlinking)
            {
                isBlinking = true;
                StartCoroutine(BlinkEffect());
            }
        }
    }

    private IEnumerator BlinkEffect()
    {
        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration)
        {
            // Alterna entre as cores de piscar e normal
            blinkingMaterial.color = isBlinking ? blinkColor : normalColor;
            isBlinking = !isBlinking;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Restaura a cor normal
        blinkingMaterial.color = normalColor;
        isBlinking = false;
    }
}
