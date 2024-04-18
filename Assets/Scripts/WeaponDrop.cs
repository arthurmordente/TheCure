using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{
    public float speed = 6.0f; // Velocidade da bala
    public int weaponCode;

    protected Renderer rend; 
    protected Material material;

    

    void Start()
    {
        rend = GetComponent<Renderer>();
        material = rend.material;
        if (material.name.StartsWith("Red"))
        {
            weaponCode = 1;
        }
        else if (material.name.StartsWith("Green"))
        {
            weaponCode = 2;
        }
        /*else if (material.name.StartsWith("Blue"))
        {
            weaponCode = 3;
        }*/
    }
    
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if(transform.position.z<(-50)){
            Destroy(gameObject);
        }
    }
}
