using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Chaser : Enemy
{

    public Enemy_Chaser() : base(100,35.0f){
    }

    private float step;
    public bool playerFound;
    public bool charging;
    public bool returning;
    Vector3 chargeDirection;
    public float maxCharge = -50.0f;

    // Start is called before the first frame update
    protected override void Start()
    {   
        base.Start();
        step = Speed * Time.deltaTime;
        playerFound = false;
        charging = false;
        returning = false;
    }

    // Update is called once per frame
    protected override void Update()
    {   
        if(ready){    
            if(player_Script.isAlive){
                base.Update();
                if(!playerFound){
                    //Debug.Log("Procurando...");
                    if(findPlayer()){
                        playerFound = true;
                        //Debug.Log("Encontrado!");
                        spawnPosition = transform.position;
                        chargeDirection = TrackPlayer(player.transform);
                    }
                }
                if(charging){
                    transform.Translate(chargeDirection * Speed * Time.deltaTime);       
                }
                if(transform.position.z<maxCharge){
                    charging = false;
                    returning = true;
                }
                if(returning){
                    transform.position = Vector3.MoveTowards(transform.position, spawnPosition, step);
                    if(transform.position==spawnPosition){
                        returning = false;
                        playerFound = false;
                    }    
                } 
            } 
        }           
    }

    bool findPlayer(){
        int layerMask = 1 << 8;
        Transform eyesTransform = transform.Find("Eyes");
        RaycastHit hit;
        if (Physics.Raycast(eyesTransform.position, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(eyesTransform.position, transform.TransformDirection(Vector3.back) * hit.distance, Color.yellow);
            return true;
        }
        else
        {
            Debug.DrawRay(eyesTransform.position, transform.TransformDirection(Vector3.back) * 1000, Color.white);
            return false;
        }    
    }
    Vector3 TrackPlayer(Transform playerTransform){
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        directionToPlayer.Normalize();
        charging = true;
        return directionToPlayer;
    }
}
