using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float health = 10;
    [SerializeField] private int points = 1;

    private EnemyHandler enemyHandler;
    private PlayerHandlerScript playerHandler;
    private Transform goal;
    AIDestinationSetter destinationAi;
    IAstarAI ai;

    private void Start()
    {
        ai = this.GetComponent<IAstarAI>();
        enemyHandler = FindObjectOfType<EnemyHandler>();
        playerHandler = FindObjectOfType<PlayerHandlerScript>();
        
        goal = GameObject.FindGameObjectWithTag("Goal").transform;
        destinationAi = GetComponent<AIDestinationSetter>();
        destinationAi.target = goal;
    }

    private void Update() {
        if(ai.reachedDestination){
            playerHandler.loseLives(1);
            Destroy(this.gameObject);
        }
    }

    public float getHealth(){
        return this.health;
    }

    public void takeDamage(float damage){ 
        this.health -= damage;
        if(this.health <= 0){
            playerHandler.gainPoints(this.points);
            die();
        }
    }

    private void die(){ 
        enemyHandler.removeEnemy(this.gameObject);
        Destroy(this.gameObject);
    }

    public float getDistanceFromGoal(){ 
        return ai.remainingDistance;
    }

    public void updateStats(float moveSpeed, float health, int points, Color color){
        this.moveSpeed = moveSpeed;
        this.health = health;
        this.points = points;
        this.GetComponent<AIPath>().maxSpeed = moveSpeed;
        this.GetComponent<SpriteRenderer>().color = color;
    }

}
