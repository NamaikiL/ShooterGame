using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    // Variables Global Public.
    public static UIManager instance;

    public Image imgPowerUp;
    public Sprite[] powerUpSprites;

    public GameObject btnBullet, btnMissile;
    public TMP_Text txtCoins, txtCoinsShop, txtPowerUpTimer, txtWaves, txtHealth;

    // Variable Global Privé.
    private GameManager _gameManager;


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

        _gameManager = GameManager.instance; // Appelle l'instance du GameManager.

    }


    /*
    Fonction qui update l'affichage de nombre de pièce à chaque pièce ramassé.
    Retourne rien.
    */
    public void UpdateCoinsInfo(int nbCoins){

        txtCoins.text = nbCoins.ToString();
        txtCoinsShop.text = nbCoins.ToString();

    }


    /*
    Fonction qui update l'affichage du temps restant des powerups.
    Retourne rien.
    */
    public void UpdatePowerUpTimeAndImg(int seconds, string powerUp){

        if(powerUp == "DamagePlus")
            imgPowerUp.sprite = powerUpSprites[0];
        else if(powerUp == "FireRatePlus")
            imgPowerUp.sprite = powerUpSprites[1];
        else if(powerUp == "ShieldUp")
            imgPowerUp.sprite = powerUpSprites[2];


        txtPowerUpTimer.text = seconds.ToString();

    }


    /*
    Fonction qui update l'affichage des waves à la fin de chacune d'entre elles.
    1. Utilisé quand le joueur a finit toutes les manches.
    2. Utilisé pour la pause entre chaque manche.
    3. Utilisé quand c'est la prochaine manche.
    Retourne rien.
    */
    public void UpdateWaves(float timeUntilNextWave, int currentWave){

        txtWaves.color = new Color32(0, 100, 0, 255);
        txtWaves.text = "Wave " + currentWave.ToString() + " - Finished !" +
                        "\nTime until next wave: " + timeUntilNextWave.ToString();

    }

    public void UpdateWaves(int currentWave){

        txtWaves.color = new Color32(139, 0, 0, 255);
        txtWaves.text = "Wave " + currentWave.ToString() + " - In Progress";

    }


    /*
    Fonction utilisé pour update les HP du joueur.
    Retourne rien.
    */
    public void UpdateHealth(int health){

        txtHealth.text = health.ToString();

    }


    /*
    Fonction utilisé pour activé le bouton de changement d'armes.
    Retourne rien.
    */
    public void ActivateWeapons(){

        btnBullet.SetActive(true);

    }


    /*
    Fonction utilisé pour le changement d'armes dans l'UI.
    Retourne rien.
    */
    public void UIChangeWeapons(){

        if(btnBullet.activeSelf){
            btnBullet.SetActive(false);
            btnMissile.SetActive(true);
            GameObject.Find("Player").GetComponent<PlayerShoot>().ChangeWeapon();
        }else if(btnMissile.activeSelf){
            btnBullet.SetActive(true);
            btnMissile.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerShoot>().ChangeWeapon();
        }

    }

}
