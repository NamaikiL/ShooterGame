using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    // Variables Global Public.
    public int damageBullet = 10, damageRocket = 25;
    public float speedBullet = 50f, fireRateBullet = 0.5f, speedRocket = 100f, fireRateRocket = 5f;

    public Transform[] bulletSpawners, rocketSpawners;

    public GameObject bullet, rocket, shieldPlayer;

    // Variable Global Privé.
    private int _currentBulletDamage, _currentRocketDamage;
    private float _currentFireRateBullet, _currentFireRateRocket;
    private bool weapon = false;
    private IEnumerator _currentWeapon;

    private AudioSource _shoot, _missile;
    private UIManager _uimanager;
    private GameManager _gameManager;
    

    /*
    Start is called before the first frame update.
    Retourne rien.
    */
    void Start(){

        // Lie les variables aux scripts demandés.
        _uimanager = UIManager.instance;
        _gameManager = GameManager.instance;
        _shoot = GameObject.Find("Shoot").GetComponent<AudioSource>();
        _missile = GameObject.Find("MissileSound").GetComponent<AudioSource>();

        // Applique les valeurs donnés par l'utilisateur.
        _currentFireRateBullet = fireRateBullet;
        _currentBulletDamage = damageBullet;
        _currentFireRateRocket = fireRateRocket;
        _currentRocketDamage = damageRocket;

        // Commence les coroutines des tirs, selon les prefabs donnés.
        if(bullet){
            for(int i = 0; i<bulletSpawners.Length; i++){
                StartCoroutine(ShootBullet(i));
            }
        }
        
        if(rocket){
            for(int i = 0; i<rocketSpawners.Length; i++){
                StartCoroutine(ShootRocket(i));
            }
        }

    }


    /*
    Coroutine permettant aux Player de tirer des balles avec des paramètres donnés.
    Retourne rien.
    */
    IEnumerator ShootBullet(int bulletSpawnerID){

        while(true){

            if(!weapon){

                _shoot.Play();

                GameObject newBullet = Instantiate(bullet, bulletSpawners[bulletSpawnerID].position, Quaternion.Euler(transform.forward)); // Créer une balle

                // Applique les valeurs donnés dans les variables public du script.
                newBullet.GetComponent<Rigidbody>().velocity = transform.forward * speedBullet;
                newBullet.GetComponent<Bullet>().bulletDamages = _currentBulletDamage;

                Destroy(newBullet, 3f); // Détruit le GameObject de la balle après 3 secondes.

                yield return new WaitForSeconds(_currentFireRateBullet); // Tire selon le FireRate donné.

            }

            yield return null;

        }

    }


    /*
    Coroutine permettant aux Player de tirer des missiles avec des paramètres donnés.
    Retourne rien.
    */
    IEnumerator ShootRocket(int rocketSpawnerID){

        while(true){

            if(weapon){

                _missile.Play();

                GameObject newRocket = Instantiate(rocket, rocketSpawners[rocketSpawnerID].position, Quaternion.Euler(transform.forward)); // Créer une missile.

                // Applique les valeurs donnés dans les variables public du script.
                newRocket.GetComponent<Rigidbody>().velocity = transform.forward * speedRocket;
                newRocket.GetComponent<Bullet>().bulletDamages = _currentRocketDamage;
                newRocket.GetComponent<Bullet>().bulletType = Bullet.BulletType.EnemyHit;

                Destroy(newRocket, 3f); // Détruit le GameObject de le missile après 3 secondes.

                yield return new WaitForSeconds(_currentFireRateRocket); // Tire selon le FireRate donné.

            }

            yield return null;
            
        }

    }


    /*
    Fonction pour changer le mode de tir du joueur.
    Retourne rien.
    */
    public void ChangeWeapon(){

        if(!weapon) weapon = true;
        else weapon = false;

    }


    // Power-Ups


    /*
    Coroutine qui augmente les dégats à l'aide d'une valeur donné pendant un certains temps.
    Retourne rien.
    */
    public IEnumerator DamagePlus(){

        ResetPowerValues(); // Reset les valeurs pour ne pas stack.

        _currentBulletDamage *= 2;
        _currentRocketDamage *= 2;
        for(int i = 30; i >= 0; i--){

            _uimanager.UpdatePowerUpTimeAndImg(i, "DamagePlus");
            yield return new WaitForSeconds(1f);

        }
        _currentBulletDamage = damageBullet;
        _currentRocketDamage = damageRocket;

    }


    /*
    Couroutine pour doubler la cadence de tir pendant un certains temps.
    Retourne rien.
    */
    public IEnumerator FireRatePlus(){

        ResetPowerValues(); // Reset les valeurs pour ne pas stack.
        
        _currentFireRateBullet /= 2;
        _currentFireRateRocket /= 2;
        for(int i = 30; i >= 0; i--){

            _uimanager.UpdatePowerUpTimeAndImg(i, "FireRatePlus");
            yield return new WaitForSeconds(1f);

        }
        _currentFireRateBullet = fireRateBullet;
        _currentFireRateRocket = fireRateRocket;

    }


    /*
    Coroutine pour activer le shield du player pendant un certain temps.
    Retourne rien.
    */
    public IEnumerator ShieldUp(){

        ResetPowerValues(); // Reset les valeurs pour ne pas stack.

        shieldPlayer.SetActive(true);
        for(int i = 10; i >= 0; i--){

            _uimanager.UpdatePowerUpTimeAndImg(i, "ShieldUp");
            yield return new WaitForSeconds(1f);

        }
        shieldPlayer.SetActive(false);

    }


    /*
    Fonction qui détruit tout les enemy(hors boss) sur le terrain.
    Retourne rien.
    */
    public void Nuke(){

        GameObject[] activeEnemies = GameObject.FindGameObjectsWithTag("Enemy"); // Trouve tout les objet qui ont un tag Enemy et créer un tableau(pour ne pas les confondre avec les boss).

        // Appelle la fonction Death à chaque enemy.
        foreach(GameObject enemy in activeEnemies){
            StartCoroutine(enemy.GetComponent<HealthController>().Death());
            _gameManager.addEnemyDeathToCounterAndCheckIfPlayerPass();
        }

    }


    /*
    Fonction pour reset les valeurs des power up.
    Retourne rien.
    */
    public void ResetPowerValues(){

        _currentFireRateBullet = fireRateBullet;
        _currentFireRateRocket = fireRateRocket;
        _currentBulletDamage = damageBullet;
        _currentRocketDamage = damageRocket;
        shieldPlayer.SetActive(false);

    }

}
