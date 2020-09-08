using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectyleScript : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private int pierce;
    private float timeToLive =  10;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0, speed * Time.deltaTime));
        timeToLive -= Time.deltaTime;
        if(timeToLive <= 0 || pierce <= 0){
            Destroy(this.gameObject);
        }
    }

    public void setup(float speed, float damage, int pierce){
        this.speed = speed;
        this.damage = damage;
        this.pierce = pierce;
    }

    public float dealDamage(){
        pierce -= 1;
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<EnemyScript>()){
            other.GetComponent<EnemyScript>()
                .takeDamage(dealDamage());
        }
    }
}
