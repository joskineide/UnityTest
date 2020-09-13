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
    private void Start(){
        Destroy(this.gameObject, timeToLive);
    }

    void Update()
    {
        transform.Translate(new Vector2(0, speed * Time.deltaTime));
        if(pierce <= 0){
            Destroy(this.gameObject);
        }
    }

    public void setup(float speed, float damage, int pierce){
        this.speed = speed;
        this.damage = damage;
        this.pierce = pierce;
    }

    public float dealDamage(){
        if(pierce <= 0){
            Destroy(this.gameObject);
            return 0;
        }
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
