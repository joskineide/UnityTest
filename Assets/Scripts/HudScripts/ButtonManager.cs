﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject menu;
 
    public void DeactivateMenu() 
    {
        menu.SetActive(false);
    }
}
