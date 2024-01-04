using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    // Variables Global Public.
    public int rndPowerUp, pricePowerUp = 5, priceHeal = 10;

    public AudioSource button, buy;
    public GameObject shop, ui;
    public GameObject[] btnsShips;
    public GameObject[] btnsPowerUps;

    // Variables Global Privé.
    private bool _isShopOpen = false;

    private GameManager _gameManager;


    /*
    Start is called before the first frame update.
    Retourne rien.
    */
    void Start(){
        
        _gameManager = GameManager.instance;

        RandomPowerUp(); // Randomise le power up vendu lorsque le jeu est lancé.
        ShipUpdate(); // Update le ship vendu dans le shop.

    }


    /*
    Fonction pour ouvrir/fermer le shop.
    Retourne rien.
    */
    public void Shop(){

        button.Play();
        if(!_isShopOpen){
            shop.SetActive(true);
            ui.SetActive(false);
            Time.timeScale = 0;
            _isShopOpen = true;
        }
        else{
            shop.SetActive(false);
            ui.SetActive(true);
            Time.timeScale = 1;
            _isShopOpen = false;
        }

    }


    /*
    Fonction pour générer un power up random dans le shop.
    Retourne rien
    */
    public void RandomPowerUp(){

        rndPowerUp = Random.Range(0,btnsPowerUps.Length);
        btnsPowerUps[rndPowerUp].SetActive(true);

    }


    /*
    Fonction qui s'occupe de l'achat des powers up dans le shop.
    Retourne rien.
    */
    public void BuyBonus(){

        if(_gameManager.GetCoin() >= pricePowerUp){
            buy.Play();

            switch(btnsPowerUps[rndPowerUp].name){
                case "BtnDamagePlus":
                    GameObject.Find("Player").GetComponent<PlayerController>().ActivatePowerUp(GameObject.Find("Player").GetComponent<PlayerShoot>().DamagePlus());
                    btnsPowerUps[rndPowerUp].SetActive(false);
                    break;
                case "BtnFireRatePlus":
                    GameObject.Find("Player").GetComponent<PlayerController>().ActivatePowerUp(GameObject.Find("Player").GetComponent<PlayerShoot>().FireRatePlus());
                    btnsPowerUps[rndPowerUp].SetActive(false);
                    break;
                case "BtnShieldUp":
                    GameObject.Find("Player").GetComponent<PlayerController>().ActivatePowerUp(GameObject.Find("Player").GetComponent<PlayerShoot>().ShieldUp());
                    btnsPowerUps[rndPowerUp].SetActive(false);
                    break;
            }

            _gameManager.RemoveCoin(pricePowerUp); // Enlève les pièces.
            RandomPowerUp(); // ReRandomise le power up du shop.
        }

    }


    /*
    Fonction pour acheter de l'energy dans le shop.
    Retourne rien.
    */
    public void BuyHealth(){

        if(_gameManager.GetCoin() >= priceHeal){

            if(GameObject.Find("Player").GetComponent<HealthController>().GetCurrentHealth() + 10 < GameObject.Find("Player").GetComponent<HealthController>().GetMaxHealth()){
                buy.Play();
                GameObject.Find("Player").GetComponent<HealthController>().Regenerate(10);
                _gameManager.RemoveCoin(priceHeal);
            }

        }

    }


    /*
    Fonction pour actualiser le ship vendu lorsque le Player ouvre le Shop.
    Retourne rien.
    */
    public void ShipUpdate(){

        switch(GameManager.lvlShip){
            case 0:
                btnsShips[0].SetActive(true);
                break;
            case 1:
                btnsShips[1].SetActive(true);
                break;
            default:
                break;
        }

    }


    /*
    Fonction qui s'occupe de l'achat des Ships.
    Retourne rien.
    */
    public void BuyShip(){

        if(GameManager.lvlShip == 0 && _gameManager.GetCoin() >= 50){
            upgradeShip();
            btnsShips[GameManager.lvlShip].SetActive(true);
            _gameManager.RemoveCoin(50);
        }else if(GameManager.lvlShip == 1 && _gameManager.GetCoin() > 100){
            upgradeShip();
            _gameManager.RemoveCoin(100);
        }

    }


    /*
    Fonction utilisé pour Buy Ship pour les achats du vaisseau.
    Retourne rien.
    */
    void upgradeShip(){

            buy.Play();
            btnsShips[GameManager.lvlShip].SetActive(false);
            GameManager.lvlShip++;
            GameObject.Find("PlayerManager").GetComponent<PlayerManager>().SpawnShip();

    }

}
