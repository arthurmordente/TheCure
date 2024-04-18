using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antibody : MonoBehaviour
{

    public GameObject Manager;
    public Logic logic;
    public float movementSpeed = 15.0f;
    public Transform spawnPoint;
    public Transform deadZone;
    public Material blue;
    public Material white;
    public Tiros tiroAtual;
    public int power;
    public int lives;
    public bool isAlive;
    private bool isDestroyed = false;
    private bool isInvulnerable = false;
    public float respawnTime = 3f;
    public float invulnerabilityTime = 3f;
    public float blinkInterval = 0.1f;
    public GameObject shield;
    public Material shieldMaterial;
    public AudioManager audioplayer;
    public Transform minBounds;
    public Transform maxBounds;


    void Start()
    {
        power = 0;
        lives = 3;
        shield.SetActive(false);
        isAlive = true;
        tiroAtual = GetComponent<VirusBane>();
        //tiroAtual = GetComponent<PhagoStorm>();
        Manager = GameObject.FindWithTag("Manager");
        logic = Manager.GetComponent<Logic>();
        audioplayer = Manager.GetComponent<AudioManager>();
        StartCoroutine(AutoShoot());
    }

    void Update()
    {
        
    }

    private IEnumerator AutoShoot()
    {
        yield return new WaitForSeconds(4f);
        while (true) // Infinite loop, be careful with these!
        {
            if(isAlive && Time.timeScale == 1){
                if(power==0){
                    tiroAtual.Shoot_0();
                }
                else if(power==1){
                    tiroAtual.Shoot_1();
                }
                else if(power==2){
                    tiroAtual.Shoot_2();
                }
                else if(power>=3){
                    tiroAtual.Shoot_MAX();               
                }
            }
            yield return new WaitForSeconds(tiroAtual.fireRate); // Wait for the specified interval
        }
    }


    void FixedUpdate()
    {
        if (isAlive)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 movementDirection = new Vector3(horizontalInput, 0.0f, verticalInput);
            Vector3 displacement = movementDirection * movementSpeed * Time.deltaTime;
            Vector3 newPosition = transform.position + displacement;

            // Limita a nova posição do jogador dentro dos limites da fase
            newPosition.x = Mathf.Clamp(newPosition.x, minBounds.position.x, maxBounds.position.x);
            newPosition.z = Mathf.Clamp(newPosition.z, minBounds.position.z, maxBounds.position.z);

            // Aplica o deslocamento à posição limitada
            transform.position = newPosition;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if(isAlive){
            if(other.CompareTag("EnemyBullet")){
                LoseLife();
                Destroy(other.gameObject);
            }
            if(other.CompareTag("Enemy")){
                LoseLife();
                logic.KillCount++;
                Destroy(other.gameObject);
            }
            if(other.CompareTag("Boss")){
                LoseLife();
            }
            if(other.CompareTag("Drop")){
                Power();   
                Destroy(other.gameObject);
            }
            if(other.CompareTag("WeaponDrop")){
                WeaponDrop weaponScript = other.GetComponent<WeaponDrop>();
                ChangeWeapon(weaponScript.weaponCode);  
                Destroy(other.gameObject);
            }
            if(other.CompareTag("Wall")){
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");   
                transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * movementSpeed * Time.deltaTime * 2);    
            }
        }    
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Wall"){
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");   
            transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * movementSpeed * Time.deltaTime * 2);    
        }
    }

    void ChangeWeapon(int code){
        if(code == 1){
            if(tiroAtual.material.name.StartsWith("Red")){
                Power();
            }else{
                tiroAtual = GetComponent<VirusBane>();
                ChangeColorUp(tiroAtual);
            }
        }
        else if(code == 2){
            if(tiroAtual.material.name.StartsWith("Green")){
                Power();
            }else{
                tiroAtual = GetComponent<PhagoStorm>();
                ChangeColorUp(tiroAtual);
            }
        }
        /*else if(code == 3){

        }*/
    }

    void LoseLife(){
        if(isInvulnerable){
            return;
        }
        if(!isDestroyed){
            isDestroyed = true;
            isAlive = false;
            BreakShip();
            logic.ManageHealth(-1);
            if(lives==0){
                logic.GameOver();
                isAlive = false;
            }else{
                Invoke(nameof(RespawnShip), respawnTime);
            }
            if(power<=2){
                tiroAtual.Dano = 10;
        
            }
            if(power>0){
                power--;
                ChangeColorDown();
            }
        }
    }
    private void BreakShip()
    {
        // TODO: Adicione aqui a lógica para "quebrar" a nave, como reproduzir animações de explosão, sons, etc.
        transform.position = deadZone.position; 
        transform.rotation = deadZone.rotation; 
    }

    private void RespawnShip()
    {
        isAlive = true;
        transform.position = spawnPoint.position; 
        transform.rotation = spawnPoint.rotation; 
        isDestroyed = false; 
        StartInvulnerability(invulnerabilityTime);
    }

    public void StartInvulnerability(float time)
    {
        shield.SetActive(true);
        StartCoroutine(BlinkShield(time));
        isInvulnerable = true;
        // TODO: Adicione efeitos visuais ou lógica para indicar que a nave está invulnerável
        Invoke(nameof(EndInvulnerability), time); 
    }

    private void EndInvulnerability()
    {
        shield.SetActive(false); 
        StopCoroutine(BlinkShield(invulnerabilityTime)); 
        isInvulnerable = false;
        // TODO: Remova efeitos visuais ou lógica de invulnerabilidade aqui
    }
    private IEnumerator BlinkShield(float time)
    {
        // Loop enquanto o escudo estiver ativo
        while (shield.activeSelf)
        {
            // Alterna a transparência do escudo
            float alpha = shieldMaterial.color.a == 0.5f ? 0f : 0.5f;
            shieldMaterial.color = new Color(shieldMaterial.color.r, shieldMaterial.color.g, shieldMaterial.color.b, alpha);
            
            // Espera pelo intervalo de tempo definido antes de mudar novamente
            yield return new WaitForSeconds(time/30);
        }
    }

    void Power(){
        if(power<=2){
            power++;
            audioplayer.Power_up.Play(); 
            ChangeColorUp(tiroAtual);
            if(power==3){
                tiroAtual.Dano = 15;
            }
        }
        else if(power>=3 && lives<3){
            logic.ManageHealth(1);
        }
    }

    void ChangeColorUp(Tiros tiro_atual){
        // Debug.Log("Coloring");
        GameObject partToColor; 
        Renderer rend;
        if(power>=1){
            partToColor = GameObject.Find("L_part");
            rend = partToColor.GetComponent<Renderer>();
            rend.material = tiro_atual.Material;
        }
        if(power>=2){
            partToColor = GameObject.Find("R_part");
            rend = partToColor.GetComponent<Renderer>();
            rend.material = tiro_atual.Material;
        }
        if(power>=3){
            partToColor = GameObject.Find("B_part1");
            rend = partToColor.GetComponent<Renderer>();
            rend.material = tiro_atual.Material;
            partToColor = GameObject.Find("B_part2");
            rend = partToColor.GetComponent<Renderer>();
            rend.material = tiro_atual.Material;
        }
    }
    void ChangeColorDown(){
        // Debug.Log("Coloring");
        GameObject partToColor; 
        Renderer rend;
        if(power==0){
            partToColor = GameObject.Find("L_part");
            rend = partToColor.GetComponent<Renderer>();
            rend.material = white;
        }
        if(power==1){
            partToColor = GameObject.Find("R_part");
            rend = partToColor.GetComponent<Renderer>();
            rend.material = white;
        }
        if(power==2){
            partToColor = GameObject.Find("B_part1");
            rend = partToColor.GetComponent<Renderer>();
            rend.material = white;
            partToColor = GameObject.Find("B_part2");
            rend = partToColor.GetComponent<Renderer>();
            rend.material = white;
        }
    }


}
