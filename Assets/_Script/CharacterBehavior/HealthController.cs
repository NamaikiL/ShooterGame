using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{

    // Variable Globale Public.
    public int health, nbCoinMax, nbCoinMin, nbHealMax, nbHealMin, probBonusMax = 2;
    public float hurtCooldown = 1f;

    public GameObject coin, heal;
    public GameObject[] drops;

    // Variable Globale Privé.
    private int _currentHealth;
    private float _currentCooldown = 0;

    private GameManager _gameManager;
    private UIManager _uiManager;

    private AudioSource explosion; // Obligé de le passer en privé, le son n'étant pas un prefabs il ne peut pas être passé en paramètre d'un prefabs.
    private GameObject _newCoin, _newHeal;


    /*
    Start is called before the first frame update.
    Retourne rien.
    */
    void Start(){

        _currentHealth = health; // Donne la vie choisit à la variable de vie.

        // Applique les scripts aux variables.
        _gameManager = GameManager.instance;
        _uiManager = UIManager.instance;
        explosion = GameObject.Find("Explosion").GetComponent<AudioSource>(); // Donne l'audio source à la variable privé.

    }


    /*
    Update is called once per frame.
    Retourne rien.
    */
    void Update(){

        // Vérifie si le cooldown est terminé.
        if(_currentCooldown > 0){
            _currentCooldown -= Time.deltaTime;
        }

    }


    /*
    Fonction qui enlève des points de vies au Player/Enemy.
    Retourne rien.
    */
    public void Hurt(int damage){

        if(_currentCooldown > 0) return; // Vérifie si le Player/Enemy a son cooldown.

        // Met un cooldown et enlève de la vie au Player/Enemy s'il n'a pas de cooldown actif.
        _currentCooldown = hurtCooldown;
        _currentHealth -= damage;

        if(gameObject.layer == LayerMask.NameToLayer("Player")) // Si le gameObject est un Player, update l'UI de la vie.
            _uiManager.UpdateHealth(_currentHealth);

        // Tue le Player/Enemy si jamais il n'a plus de vie.
        if(_currentHealth < 0)
            StartCoroutine(Death());

    }


    /*
    Fonction qui regénère de la vie au Player/Enemy.
    Retourne rien.
    */
    public void Regenerate(int heal){

        // Régénère le Player/Enemy si son nombre de HP est plus petit que son nombre de HP max.
        if(_currentHealth+heal < health){
            _currentHealth += heal;
        }

        // Donne la valeur max des HP si l'addition des HP du Player/Enemy et du heal est supérieur aux HP max.
        if(_currentHealth+heal > health){
            _currentHealth = health;
        }

        _uiManager.UpdateHealth(_currentHealth);
        
    }


    /*
    Fonction pour récupérer la variable de la vie maximum d'un personnage.
    Retourne INTEGER.
    */
    public int GetMaxHealth(){

        return health;

    }


    /*
    Fonction pour récupérer la variable de la vie actuel d'un personnage.
    Retourne INTEGER.
    */
    public int GetCurrentHealth(){

        return _currentHealth;

    }


    /*
    Coroutine pour la mort du Player/Enemy.
    Retourne rien.
    */
    public IEnumerator Death(){

        explosion.Play(); // Joue le son d'explosion à la mort d'un ennemi/player.

        // Si le prefabs Coin est donné, génère des pièces à la mort d'un personnage.
        if(coin){

            for(int i = 0; i < Random.Range(nbCoinMin, nbCoinMax); i++){
                _newCoin = GameObject.Instantiate(coin, transform.position + new Vector3(Random.Range(-4,4), 0, Random.Range(-4,4)), Quaternion.identity); // Instancie une pièce.
            }

        }

        // Si le prefabs Energy est donné, génère de l'energy à la mort d'un personnage.
        if(heal){

            for(int i = 0; i < Random.Range(nbHealMin, nbHealMax); i++){
                _newHeal = GameObject.Instantiate(heal, transform.position + new Vector3(Random.Range(-10,10), 0, Random.Range(-10,10)), Quaternion.identity);
            }

        }

        // Si la liste des drops contient un GameObject, alors fait spawn un GameObject au choix.
        if(drops.Length > 0){

            if(Random.Range(0,probBonusMax) == 0){

                Instantiate(drops[Random.Range(0, drops.Length)], transform.position, Quaternion.identity);

            }
            
        }

        // Si le gameObject est un player, alors déplace le joueur sur la scène du GameOver.
        if(gameObject.layer == LayerMask.NameToLayer("Player")){

            yield return new WaitForSeconds(1f);
            GameObject.Find("MenuManager").GetComponent<MenuManager>().SceneGameOver(gameObject.scene.name);

        // Si c'est un enemy, alors appelle la fonction addEnemyDeathToCounterAndCheckIfPlayerPass() de GameManager.
        }else if(gameObject.layer == LayerMask.NameToLayer("Enemy") && gameObject.tag == "Enemy"){ 
            
            _gameManager.addEnemyDeathToCounterAndCheckIfPlayerPass();

        // Si c'est un boss, envoie le joueur sur l'écran de fin.
        }else if(gameObject.layer == LayerMask.NameToLayer("Enemy") && gameObject.tag == "Boss"){

            GameObject.Find("MenuManager").GetComponent<MenuManager>().SceneEnd();

        }

        Destroy(gameObject); // Détruit le GameObject Player/Enemy.
        
        yield return null;

    }
}
