using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{

    // Variables Global Public.
    public float horizontal, vertical;
    public bool changeWeapons;


    /*
    Update is called once per frame.
    Retourne rien.
    */
    void Update(){

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        changeWeapons = Input.GetButtonDown("ChangeWeapons");
        
    }
}
