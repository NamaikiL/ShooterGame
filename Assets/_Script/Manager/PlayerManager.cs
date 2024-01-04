using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    // Variables Global Public.
    public GameObject[] playerShips;

    // Variables Global Privé.
    private GameManager _gameManager;
    private UIManager _uimanager;


    /* 
    Start is called before the first frame update.
    Retourne rien.
    */
    void Start(){

        // Donne les scripts aux variables à l'aide des instances.
        _gameManager = GameManager.instance;
        _uimanager = UIManager.instance;

        SpawnShip(); // Spawn à chaque fois le Player pour qu'il puisse jouer.

    }


    /*
    Fonction pour faire spawner le Player.
    Retourne rien.
    */
    public void SpawnShip(){

        // Si un joueur est déjà sur la scène, Destroy.
        if(GameObject.Find("Player"))
            Destroy(GameObject.Find("Player"));

        // Instantie un joueur avec le niveau demandé et le nom "Player".
        GameObject player = Instantiate(playerShips[GameManager.lvlShip], transform.position, Quaternion.identity);
        player.name = "Player";

        // Si le Ship est au dernier niveau, alors active les missiles.
        if(GameManager.lvlShip == 2){
            _uimanager.ActivateWeapons();
        }

        // Update les points de vies.
        _uimanager.UpdateHealth(GameObject.Find("Player").GetComponent<HealthController>().GetMaxHealth());

    }

}
