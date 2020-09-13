using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandlerScript : MonoBehaviour
{
    [SerializeField] private int points;
    [SerializeField] private int lives;
    [SerializeField] private List<GameObject> turrets;
    [SerializeField] private GameObject playerBase;
    [SerializeField] private GameObject turretToSpawn;

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float posX = Mathf.Round(mousePos.x);
            float posY = Mathf.Round(mousePos.y);
            GameObject target = checkOcupied((int)posX, (int)posY);
            if(target == null){
                purchaseTurret((int)posX, (int)posY, turretToSpawn);
            } else {
                upgradeTurret(target);
            }
        }
    }

    public void loseLives(int lives){
        this.lives -= lives;
    }

    public void gainPoints(int points){
        this.points += points;
    }

    private void purchaseTurret(int posX, int posY, GameObject turret){
        TurretScript turretScript = turret.GetComponent<TurretScript>();
        if(turretScript.getCost() > points){
            Debug.Log("TURRET TOO EXPENSIVE");
            return;
        }
        GameObject spawnedTurret = Instantiate(turretToSpawn, new Vector3(posX, posY, 0), transform.rotation);
        spawnedTurret.GetComponent<TileScript>().setPosition(posX, posY);
        turrets.Add(spawnedTurret);
        points -= turretScript.getCost();
    }

    private void upgradeTurret(GameObject turret){
        TurretScript turretScript = turret.GetComponent<TurretScript>();
        if(turretScript.getUpgradeCost() > points){
            Debug.Log("UPGRADE TOO EXPENSIVE");
            return;
        }
        turretScript.addDamage(1);
        turretScript.addFireRatio(10);
        turretScript.addPierce(1);
        turretScript.addRange(1);
        turretScript.addProjectyleSpeed(1);
        points -= turretScript.getUpgradeCost();
        Debug.Log("Turret Upgraded!");
    }

    private GameObject checkOcupied(int x, int y){
        foreach(GameObject turret in turrets){
            TileScript tile = turret.GetComponent<TileScript>();
            if(tile.getPosX() == x && tile.getPosY() == y){
                return turret;
            }
        }
        return null;
    }
}
