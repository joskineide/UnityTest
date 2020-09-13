using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{

    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private GameObject enemyToSpawn;

 private void Update() //TODO deletar depois de testes
    {

        if (Input.GetKey(KeyCode.Alpha1)) 
        {
            spawnEnemy();
        }


    }


    public List<GameObject> getEnemies(){
        return this.enemies;
    }

    private void spawnEnemy(){
        GameObject enemy = Instantiate(enemyToSpawn, transform.position, transform.rotation);
        enemies.Add(enemy);
    }

    public void removeEnemy(GameObject enemyToDelete){
        enemies.Remove(enemyToDelete);
    }
}
