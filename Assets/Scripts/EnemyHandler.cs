using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{

    [SerializeField] private GameObject[] enemies;

    public GameObject[] getEnemies(){
        return this.enemies;
    }
}
