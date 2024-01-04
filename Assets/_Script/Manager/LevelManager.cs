using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    // Variables Global Public.
    public GameObject start, end, ui;

    /*
    Start is called before the first frame update.
    Retourne rien.
    */
    void Start(){

        LevelManagement(0, ui, false, start, true); // Affiche la fenêtre de Start.

    }


    /*
    Fonction pour commencer le niveau.
    Retourne rien.
    */
    public void StartLevel(){

        LevelManagement(1, ui, true, start, false);

    }


    /*
    Fonction pour finir le niveau.
    Retourne rien.
    */
    public void FinishLevel(){

        LevelManagement(0, ui, false, end, true);

    }


    /*
    Fonction alléger les autres fonctions.
    Retourne rien.
    */
    void LevelManagement(int timeScale, GameObject display1, bool display1State, GameObject display2, bool display2State){

        Time.timeScale = timeScale;
        display1.SetActive(display1State);
        display2.SetActive(display2State);

    }
}
