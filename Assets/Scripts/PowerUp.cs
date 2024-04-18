using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float speed = 8.0f; // Velocidade da bala


    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if(transform.position.z<(-50)){
            Destroy(gameObject);
        }
    }
}
