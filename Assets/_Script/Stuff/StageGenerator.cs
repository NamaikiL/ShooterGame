using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{

    // Variables Global Public.
    public Transform nextStageSpawn;

    public GameObject[] stages;

    // Variables Global Privé.
    private float _speed = 100f;
    private bool _isTriggered = false; // Utilisé pour vérifier si le trigger a été utilisé une fois afin de ne pas génèrer des plateformes à l'infini.


    /*
    Start is called before the first frame update/
    Retourne rien.
    */
    void Start(){

        StartCoroutine(movingStages()); // Appelle la coroutine de déplacement du terrain.
        
    }

    
    /*
    Coroutine qui fait le défilement des Stages.
    Retourne rien.
    */
    IEnumerator movingStages(){

        while(true){

            transform.Translate(Vector3.back * _speed * Time.deltaTime, Space.World);

            yield return new WaitForEndOfFrame();

        }

    }


    /*
    Fonction qui génère des plateformes quand un joueur passe un triggerpoint.
    Retourne rien.
    */
    void OnTriggerEnter(Collider other){

        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && !_isTriggered){
            Instantiate(stages[Random.Range(0, stages.Length)], nextStageSpawn.position, Quaternion.identity);
            _isTriggered = true;
        }

    }
}
