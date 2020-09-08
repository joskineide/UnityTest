using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1;

    [SerializeField] private float health = 10;

     [SerializeField] private float points = 1;

     ProjectyleScript projectyle;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)){
            transform.Translate(new Vector2(-moveSpeed,0));
        }
        else if(Input.GetKeyDown(KeyCode.D)){
            transform.Translate(new Vector2(moveSpeed,0));
        }
        else if(Input.GetKeyDown(KeyCode.W)){
            transform.Translate(new Vector2(0,moveSpeed));
        }
        else if(Input.GetKeyDown(KeyCode.S)){
            transform.Translate(new Vector2(0,-moveSpeed));
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if( other.GetComponent<ProjectyleScript>()){
            float damage = other.GetComponent<ProjectyleScript>().dealDamage();
            takeDamage(damage);
        }
    }

    private void takeDamage(float damage){ //TODO separado por questões de dots e etcs tbm vão ter que passar por aqui, não só balas vão dar dano
        this.health -= damage;
        if(this.health <= 0){
            die();
        }
    }

    private void die(){ //TODO fazer todo o esquema de pontos e etc...
        Destroy(this.gameObject);
    }
}
