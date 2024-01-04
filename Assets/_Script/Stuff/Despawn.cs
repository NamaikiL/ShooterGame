using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{

    /*
    Start is called before the first frame update.
    Retourne rien.
    */
    void Start(){

        StartCoroutine(DespawnItem());
    }


    /*
    Coroutine faisant despawn l'item après 30s.
    Retourne rien.
    */
    IEnumerator DespawnItem(){

        for(int i = 0; i<30; i++){
            // Commence l'animation de despawn après 20s.
            if(i==20){
                GetComponent<Animator>().SetBool("Despawn", true);
            }
            yield return new WaitForSeconds(1f);
        }
        
        Destroy(gameObject);

    }
    
}
