using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // Velocidade de rotação em graus por segundo
    private float currentRotation = 0.0f;

    private void Update()
    {
        // Atualiza a rotação atual com base na velocidade de rotação
        currentRotation += rotationSpeed * Time.deltaTime;

        // Mantém a rotação dentro do intervalo de 0 a 360 graus
        currentRotation = Mathf.Repeat(currentRotation, 360.0f);

        // Aplica a rotação ao objeto apenas no eixo Y
        transform.rotation = Quaternion.Euler(0.0f, currentRotation, 0.0f);
    }
}
