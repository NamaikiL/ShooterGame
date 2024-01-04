using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    // Variables Global Public.
    public float moveSpeed = 100f;

    public Vector3 destination; // Valeur donné à l'aide du script WaveManager.


    /*
    Start is called before the first frame update.
    Retourne rien.
    */
    void Start(){
        
        StartCoroutine(GoToDestination());

    }


    /*
    Fonction de déplacement des enemies.
    Retourne rien.
    */
    IEnumerator GoToDestination(){
        
        while(true){
            if(GameObject.Find("Player")) // Debug.
                transform.LookAt(GameObject.Find("Player").transform.position, Vector3.up); // L'enemy regarde le Player.

            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * moveSpeed); // Changement du transform.Translate() par un Vector3.MoveTowards() dû à un bug ou les enemy n'allaient pas à la destination donné(?).

            // Si la distance caculé entre l'enemy et sa destination à un espace inférieur à 0.5 alors l'enemy est détruit.
            if(Vector3.Distance(transform.position, destination) < 0.5f){
                Destroy(gameObject);
            }
            
            yield return new WaitForEndOfFrame();
        }

    }

}
