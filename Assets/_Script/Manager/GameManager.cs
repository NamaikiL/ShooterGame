using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Variables Global Public.
    public int nbDeadEnemiesNeeded;
    public static int lvlShip = 0;

    public static GameManager instance;

    public AudioSource coin;
    public GameObject waves, waveManager;

    // Variables Global Privé.
    private int nbEnemyDead = 0;
    private static int _coins = 0;

    private UIManager _uimanager;


    /*
    Awake is called before any Start functions.
    Retourne rien.
    */
    void Awake(){
        
        // Si une instance est déjà créer alors détruit le gameObject.
        if(instance != null){
            Destroy(gameObject);
        }

        instance = this; // Créer une instance du GameManager.

    }


    /* 
    Start is called before the first frame update.
    Retourne rien.
    */
    void Start(){

        _uimanager = UIManager.instance; // Appelle l'instance du UIManager.

        if(gameObject.scene.name == "Level1")
            ResetCoin();

        _uimanager.UpdateCoinsInfo(_coins);

    }


    /*
    Fonction qui ajoute une pièce au compteur.
    Retourne rien.
    */
    public void AddCoin(){

        coin.Play();
        _coins++; // Incrémente la valeur de _coins.
        _uimanager.UpdateCoinsInfo(_coins);

    }


    /*
    Fonction qui enlève un nombre de pièce donné en paramètres.
    Retourne rien.
    */
    public void RemoveCoin(int coinToRemove){

        _coins -= coinToRemove;
        _uimanager.UpdateCoinsInfo(_coins);

    }


    /*
    Fonction qui retourne le nombre de coins que le player a.
    Retourne INTEGER.
    */
    public int GetCoin(){

        return _coins;

    }


    /*
    Fonction qui reset le nombre de coins.
    Retourne rien.
    */
    public void ResetCoin(){

        _coins = 0;
        _uimanager.UpdateCoinsInfo(_coins);

    }


    /*
    Fonction qui s'occupe des condition de victoire liés aux nombres d'enemy tués.
    Retourne rien.
    */
    public void addEnemyDeathToCounterAndCheckIfPlayerPass(){

        // Utilisé pour le dernier niveau afin de faire apparaître le boss.
        if(nbEnemyDead++ >= nbDeadEnemiesNeeded && gameObject.scene.name == "LevelFinal"){

            waves.SetActive(false);
            waveManager.SetActive(false);
            GameObject.Find("Boss").GetComponent<Animator>().SetBool("Boss", true);

        }else if(nbEnemyDead++ >= nbDeadEnemiesNeeded && gameObject.scene.name != "LevelFinal"){

            GameObject.Find("LevelManager").GetComponent<LevelManager>().FinishLevel();

        }else{

            nbEnemyDead += 1; // Appellé 3 fois pour aucune raison(?).
            nbEnemyDead -= 2;

        }

    }
    
}
