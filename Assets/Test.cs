using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    private int i = 0;

     [SerializeField] private int incrementor = 1;

     [SerializeField] private int breakPoint = 100;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("BEGGINING PROCEDURE");
    }

    // Update is called once per frame
    void Update()
    {
        i += incrementor;
        if(i % breakPoint == 0){
            Debug.Log("IT WORKS " + i);
        }

    }
}
