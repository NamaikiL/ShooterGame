using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    // Variables Global Public.
    public AudioSource button;

    // Variables Global Privé.
    private static string _lastLevel;


    /*
    Fonction qui permet de charger les levels.
    Retourne rien.
    */
    public void SceneLevel(){

        button.Play();

        _lastLevel = gameObject.scene.name; // Stock l'ancienne scène dans une variable.

        // Charge le niveau demandé.
        if(_lastLevel == "Level1"){
            SceneManager.LoadScene("Level2", LoadSceneMode.Single);
        }else if(_lastLevel == "Level2"){
            SceneManager.LoadScene("LevelFinal", LoadSceneMode.Single);
        }else{
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
            GameManager.lvlShip = 0; // Reset le ship si jamais le niveau chargé est le premier.
        }
        
    }


    /*
    Fonction qui charge la scène du GameOver.
    Retourne rien.
    */
    public void SceneGameOver(string lastLevel){

        button.Play();
        _lastLevel = lastLevel;
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);

    }


    /*
    Fonction qui charge la scène du Menu.
    Retourne rien.
    */
    public void SceneMenu(){

        button.Play();
        _lastLevel = "";
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);

    }


    /*
    Fonction qui charge le dernier niveau.
    Retourne rien.
    */
    public void SceneRestart(){

        button.Play();
        SceneManager.LoadScene(_lastLevel, LoadSceneMode.Single);

    }


    /*
    Fonction qui charge la scène de la fin du jeu.
    Retourne rien.
    */
    public void SceneEnd(){

        button.Play();
        SceneManager.LoadScene("End", LoadSceneMode.Single);

    }


    /*
    Fonction qui quitte le jeu.
    Retourne rien.
    */
    public void Quit(){

        button.Play();
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;

    }

}
