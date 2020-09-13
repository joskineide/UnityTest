using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] private int posX = 1;
    [SerializeField] private int posY = 1;

    public void setPosition(int posX, int posY){
        this.posX = posX;
        this.posY = posY;
        this.gameObject.transform.position = new Vector2(posX, posY);
    }

    public int getPosX(){
        return this.posX;
    }

    public int getPosY(){
        return this.posY;
    }

}
