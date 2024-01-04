using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{

    // Variables Global Public.
    public Transform nextStageSpawn;
    
    public GameObject[] stages;


    /*
    Start is called before the first frame update.
    Retourne rien.
    */
    void Start(){
        
        FirstStageSpawn(); // Créer un premier terrain aléatoire.

    }


    /*
    Fonction pour faire spawn le premier Stage.
    Retourne rien.
    */
    void FirstStageSpawn(){

        Instantiate(stages[Random.Range(0, stages.Length)], nextStageSpawn.position, Quaternion.identity); // Instancie un Stage random à la position donné.

    }


    /*
    Fonction pour trigger quand un stage doit être détruit.
    Retourne rien.
    */
    void OnTriggerEnter(Collider other){

        if(other.gameObject.layer == LayerMask.NameToLayer("Stage")){
            Destroy(other.gameObject);
        }

    }
}
