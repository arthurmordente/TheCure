using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Logic : MonoBehaviour
{

    private float baseDropChance = 0.125f;
    //private float baseDropChance = 1;
    public float dropChance;
    public GameObject[] itemsToDrop;

    public Transform[] OnScreen_Waypoints;
    public Transform[] OffScreen_Waypoints;
    public int KillCount;

    public int playerLives;
    public int playerPower;
    public Text lives;
    public GameObject gameOverScreen;
    public GameObject gameWinScreen;
    public bool GameIsOn;

    public GameObject pauseMenu;
    private bool isPaused = false;

    public Antibody player;
    public AudioManager audioplayer;


    void Start()
    {
        playerLives = 3;
        KillCount = 0;
        dropChance = baseDropChance;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void TryToDrop(Transform DropLocation)
    {
        if (Random.Range(0f, 1f) <= dropChance)
        {
            Drop(DropLocation);
            dropChance = baseDropChance;
        }
        else
        {
            dropChance *= 1.1f;
        }
    }

    public void Drop(Transform DropLocation){
        GameObject item = ChooseItemToDrop();
        Instantiate(item, DropLocation.position, Quaternion.identity);
    }



    private GameObject ChooseItemToDrop()
    {
        float totalWeight = 2 + 1 + 1;

        float choice = Random.Range(0, totalWeight);

        if (choice < 2) // 
        {
            return itemsToDrop[0];
        }
        else if (choice < 3)
        {
            return itemsToDrop[1];
        }
        else 
        {
            return itemsToDrop[2];
        }
    }

    [ContextMenu("Take Damage")]
    public void ManageHealth(int dmg){
        if(dmg<0){
            // Debug.Log("Funcao ManageHealth chamada com valor " + dmg);
            audioplayer.Damage_taken.Play();
            player.lives = player.lives + dmg;
            playerLives += dmg;
            lives.text = "Lives: " + playerLives.ToString();
        }
        if(dmg>0){
            // Debug.Log("Funcao ManageHealth chamada com valor " + dmg);
            audioplayer.Damage_healed.Play();
            player.lives = player.lives + dmg;
            playerLives += dmg;
            lives.text = "Lives: " + playerLives.ToString();
        }
    }

    public void restartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        audioplayer.BGM.Pause();
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        audioplayer.BGM.UnPause();
    }

    public void GameOver(){
        gameOverScreen.SetActive(true);
    }

    public void GameWon(){
        gameWinScreen.SetActive(true);
        player.isAlive = false;
    }

    public void BackMainMenu(){
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f; 
    }

    public void ContinueToActII(){
        SceneManager.LoadScene("Act_II");
    }

    public bool CheckEnemiesAlive()
    {
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Enemy");
        if (inimigos.Length == 0)
        {
            return true;
        }else{
            return false;
        }
    }

    public void ChangeToBossBGM(){
        audioplayer.BGM.Pause();
        audioplayer.Boss_BGM.Play();
    }

    public void SpawnOnScreen(GameObject inimigo, int posicao, GameObject spawnpoint){
        GameObject novoInimigo = Instantiate(inimigo, spawnpoint.transform.position, Quaternion.identity);       
        Enemy enemy = novoInimigo.GetComponent<Enemy>();
        enemy.transform.position = spawnpoint.transform.position;
        enemy.DefinirDestino(OnScreen_Waypoints[posicao]);       
        enemy.Move(() => {
            enemy.ready = true;
            Collider enemyCollider = enemy.GetComponent<Collider>();
            if (enemyCollider != null)
            {
                enemyCollider.enabled = true;
            }
        });
    }

    public void SpawnOffScreen(GameObject inimigo, int posicao){
        GameObject novoInimigo = Instantiate(inimigo, OffScreen_Waypoints[posicao].transform.position, Quaternion.identity);
        Enemy enemy = novoInimigo.GetComponent<Enemy>();
        enemy.DefinirDestino(OffScreen_Waypoints[posicao]);  
        Collider enemyCollider = enemy.GetComponent<Collider>();   
        enemy.Move(() => {
            enemy.ready = true;
        });  
    }

    public void DestroyAllEnemiesOnScreen()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        foreach (GameObject boss in bosses)
        {
            Destroy(boss);
        }
    }

    public void Wait(float seconds, System.Action callback)
    {
        StartCoroutine(DoWait(seconds, callback));
    }

    private IEnumerator DoWait(float seconds, System.Action callback)
    {
        yield return new WaitForSeconds(seconds);

        // Após a espera, execute o callback.
        if (callback != null)
        {
            callback();
        }
    }




    

}
    



