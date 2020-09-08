using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] private int posX = 1;
    [SerializeField] private int posY = 1;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.position = new Vector2(posX, posY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
