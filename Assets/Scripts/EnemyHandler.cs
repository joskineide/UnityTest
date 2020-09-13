using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EnemyHandler : MonoBehaviour
{
    private enum EnemyType {Swarm, Bulky, Speedy, Boss};

    [SerializeField] private List<EnemyType> waves;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private string enemyStory = ""; //TODO Implementar resto das funções do spawn de letras
    private Queue<char> enemyStoryQueue = new Queue<char>();
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private EnemyType nextEnemyType;
    [SerializeField] private int waveSize;
    [SerializeField] private float spawnRate;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float health;
    [SerializeField] private int points;
    [SerializeField] private int waveNumber = 1;    
    [SerializeField] private Color enemyColor;
    [SerializeField] private float nextWaveTime = 10;
    private float nextWaveTimer;

    private void Start() {
        setupNextWave();
        sendWave();
        char[] storyChars = enemyStory.ToCharArray();
        for(int i = 0 ; i < enemyStory.Length ; i++){
            enemyStoryQueue.Enqueue(storyChars[i]);
        }    
    }
    
    private void Update() {
        nextWaveTimer -= Time.deltaTime;
        if(nextWaveTimer <= 0 || Input.GetKeyDown(KeyCode.Space)){
            sendWave();
        }
    }

    public void updateEnemyPath(){
        AstarPath.active.Scan(); 
    }

    public List<GameObject> getEnemies(){
        return this.enemies;
    }

    private IEnumerator spawnEnemy(float movementSpeed, float health, int points, Color enemyColor, float spawnRate, float delay){
        yield return new WaitForSeconds(delay/spawnRate);
        GameObject enemy = Instantiate(enemyToSpawn, transform.position, transform.rotation);
        enemy.GetComponent<EnemyScript>().updateStats(movementSpeed, health, points, enemyColor);
        enemies.Add(enemy);
    }

    public void removeEnemy(GameObject enemyToDelete){
        enemies.Remove(enemyToDelete);
    }

    private void sendWave(){
        for(float i = 0; i < waveSize; i++){
            StartCoroutine(spawnEnemy(movementSpeed, health, points, enemyColor, spawnRate, i));
        }
        setupNextWave();
        nextWaveTimer = nextWaveTime;
    }
    private void setupNextWave(){

        nextEnemyType = (EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);

        switch(nextEnemyType)
        {
            case EnemyType.Swarm:
            {
                waveSize = 20;
                movementSpeed = 1;
                health = 5 * Mathf.Pow(1.1f, waveNumber);
                points = waveNumber;
                spawnRate = 4;
                break;
            }
            case  EnemyType.Bulky:
            {
                waveSize = 5;
                movementSpeed = 0.5f;
                health = 15 * Mathf.Pow(1.1f, waveNumber);
                points = waveNumber * 4;
                spawnRate = 2;
                break;
            }
            case  EnemyType.Speedy:
            {
                waveSize = 10;
                movementSpeed = 3;
                health = 2 * Mathf.Pow(1.1f, waveNumber);
                points = waveNumber * 2;
                spawnRate = 1;
                break;
            }
            case EnemyType.Boss:
            {
                waveSize = 1;
                movementSpeed = 0.8f;
                health = 50 * Mathf.Pow(1.1f, waveNumber);
                points = waveNumber * 25;
                spawnRate = 1;
                break;
            }
        }

        enemyColor = Random.ColorHSV();
        waveNumber += 1;
    }
}
