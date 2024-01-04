using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{

    // Variables Global Public.
    public float attractionForce;
    public float range = 5;

    public LayerMask layerToAttract;

    // Variable Global Privé.
    private Collider[] colsToAttract = new Collider[300];


    /*
    FixedUpdate is called once per fixed frame.
    Retourne rien.
    */
    void FixedUpdate(){

        Attract(); // Appelle la fonction Attract à chaque frame fix.
        
    }


    /*
    Fonction permettant d'attirer un GameObject avec un layer choisi en calculant la force d'attraction par la distance.
    Retourne rien.
    */
    void Attract(){

        // Créer une sphere autour du GameObject avec une range donnée, un tableau avec un nombre maximum d'object pouvant être calculé et le layer choisi.
        Physics.OverlapSphereNonAlloc(transform.position, range, colsToAttract, layerToAttract);

        // Si le tableau n'a aucun objet en vue, alors ne renvoie rien.
        if(colsToAttract == null) return;

        // Pour toutes les pièces étant dans la zone de magnet, utilise une force d'attraction pour les amener vers le Player.
        for(int i = 0; i < colsToAttract.Length; i++){
            if(colsToAttract[i] == null) continue;

            // Si le tag du collectible est une pièce, alors attire l'objet.
            if(colsToAttract[i].tag == "Coin"){
                float distance = Vector3.Distance(colsToAttract[i].transform.position, transform.position);
                distance = Mathf.Clamp(distance, 0, range);

                Vector3 force = ((transform.position - colsToAttract[i].transform.position).normalized * (range - distance) / range) * attractionForce;

                colsToAttract[i].GetComponent<Rigidbody>().AddForce(force);
            }
            
        }

    }
}
