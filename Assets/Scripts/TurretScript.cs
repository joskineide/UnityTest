using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{

    private enum TurretTargetTypeEnum {First, Last, Close, Far, Healthy, Weak};
    [SerializeField] private float fireSpeed = 1;
    [SerializeField] private float damage = 1;
    [SerializeField] private float projectyleSpeed = 1;
    [SerializeField] private int pierce = 1;
    [SerializeField] private float radious = 1;
    [SerializeField] private float cooldown = 0;
    [SerializeField] private TurretTargetTypeEnum turretTargetType =  TurretTargetTypeEnum.Close;
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

        if(currentEnemy == null){
            return;
        }

        lookAtTarget(currentEnemy.transform.position);

        fire();

    }

    private GameObject searchForTarget(){

        GameObject target = null;

        foreach(GameObject enemy in enemyHandler.getEnemies()){
            if(getDistance(enemy) < radious){
                target = checkTarget(target, enemy);
            }
        }

        return target;
    }

    private float getDistance(GameObject other){
        return Mathf.Abs(other.transform.position.x - this.transform.position.x) + 
            Mathf.Abs(other.transform.position.y - this.transform.position.y);
    }

    private GameObject checkTarget(GameObject currentTarget, GameObject newTarget){
        if(currentTarget == null){
            return newTarget;
        }

        switch(turretTargetType)
        {
            case TurretTargetTypeEnum.Close:
            {
                if(getDistance(newTarget) < getDistance(currentTarget)){
                    return newTarget;
                }
                break;
            }
            case TurretTargetTypeEnum.Far:
            {
                if(getDistance(newTarget) > getDistance(currentTarget)){
                    return newTarget;
                }
                break;
            }
            case TurretTargetTypeEnum.Healthy:
            {
                if(newTarget.GetComponent<EnemyScript>().getHealth() > 
                    currentTarget.GetComponent<EnemyScript>().getHealth()){
                    return newTarget;
                }
                break;
            }
            case TurretTargetTypeEnum.Weak:
            {
                if(newTarget.GetComponent<EnemyScript>().getHealth() <
                    currentTarget.GetComponent<EnemyScript>().getHealth()){
                    return newTarget;
                }
                break;
            }
             case TurretTargetTypeEnum.First: //TODO
            {
                return currentTarget;
                break;
            }
             case TurretTargetTypeEnum.Last: //TODO
            {
                return newTarget;
                break;
            }
            default: return currentTarget;
        }
        return currentTarget;
    }

    private void lookAtTarget(Vector3 enemyPosition){
        Vector3 dir = enemyPosition - transform.position;
        System.Single angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void updateAtributes(float fireSpeed, float damage, float projectyleSpeed, float radious){

        this.fireSpeed = fireSpeed;
        this.damage = damage;
        this.projectyleSpeed = projectyleSpeed;
        this.radious = radious;

    }

    private void fire(){

        GameObject firedProjectyle = Instantiate<GameObject>(projectyle, transform.position, this.transform.rotation); 

        firedProjectyle.GetComponent<ProjectyleScript>().setup(projectyleSpeed, damage, pierce);

        cooldown = fireSpeed;
    }
}
