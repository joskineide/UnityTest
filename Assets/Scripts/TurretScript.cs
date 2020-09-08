using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{

    [SerializeField] private float fireSpeed = 1;
    [SerializeField] private float damage = 1;
    [SerializeField] private float projectyleSpeed = 1;
    [SerializeField] private int pierce = 1;
    [SerializeField] private float radious = 1;
    [SerializeField] private float cooldown = 0;
    [SerializeField] enum turretTargetType {Close, Far, Healthy, Weak};
    [SerializeField] private GameObject currentEnemy;
    [SerializeField] private GameObject projectyle;
    [SerializeField] private EnemyHandler enemyHandler;

    // Start is called before the first frame update
    void Start()
    {
        enemyHandler = FindObjectOfType<EnemyHandler>();
        updateAtributes(fireSpeed, damage, projectyleSpeed, radious);
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown > 0){
            cooldown -= Time.deltaTime;
            return;
        }

        currentEnemy = searchForTarget();

        if(currentEnemy.Equals(null)){
            return;
        }

        lookAtTarget(currentEnemy.transform.position);

        Instantiate<GameObject>(projectyle, transform.position, this.transform.rotation); 

        cooldown = fireSpeed;
    }

    private GameObject searchForTarget(){
        return enemyHandler.getEnemies()[0];
    }

    private void lookAtTarget(Vector3 enemyPosition){
        Vector3 dir = enemyPosition - transform.position;
        System.Single angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void updateAtributes(float fireSpeed, float damage, float bulletSpeed, float radious){

        this.fireSpeed = fireSpeed;
        this.damage = damage;
        this.projectyleSpeed = bulletSpeed;
        this.radious = radious;

        projectyle.GetComponent<ProjectyleScript>().setup(bulletSpeed, damage, pierce);
    }
}
