using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{

    [SerializeField] private List<GameObject> enemies;

    public List<GameObject> getEnemies(){
        return this.enemies;
    }

    public void removeEnemy(GameObject enemyToDelete){
        enemies.Remove(enemyToDelete);
    }
}
