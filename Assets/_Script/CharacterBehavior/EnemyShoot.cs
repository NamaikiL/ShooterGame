using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    // Variables Global Public.
    public int damageBullet = 10, damageRocket = 25;
    public float speedBullet = 50f, fireRateBullet = 0.5f, speedRocket = 100f, fireRateRocket = 5f;

    public Transform[] bulletSpawners, rocketSpawners;

    public GameObject bullet, rocket;

    // Variables Global Privé.
    private AudioSource _shoot, _missile; // Obligé de le passer en privé, le son n'étant pas un prefabs il ne peut pas être passé en paramètre d'un prefabs.


    /*
    Start is called before the first frame update.
    Retourne rien.
    */
    void Start(){

        // Donne les scripts aux variables.
        _shoot = GameObject.Find("Shoot").GetComponent<AudioSource>();
        _missile = GameObject.Find("MissileSound").GetComponent<AudioSource>();

        // Tire des balles à partir des spawners donnés.
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
    Coroutine permettant aux Enemy de tirer des balles avec des paramètres donnés.
    Retourne rien.
    */
    IEnumerator ShootBullet(int bulletSpawnerID){

        while(true){

            _shoot.GetComponent<AudioSource>().Play(); // Fait le son tir quand une balle se fait tirer.

            GameObject newBullet = Instantiate(bullet, bulletSpawners[bulletSpawnerID].position, Quaternion.Euler(transform.forward)); // Instancie une balle.
                
            // Applique les valeurs donnés dans les variables public du script.
            newBullet.GetComponent<Rigidbody>().velocity = -transform.forward * -speedBullet;
            newBullet.GetComponent<Bullet>().bulletDamages = damageBullet;

            Destroy(newBullet, 3f); // Détruit le GameObject de la balle après 3 secondes.

            yield return new WaitForSeconds(fireRateBullet); // Tire selon le FireRate donné.     

        }

    }


    /*
    Coroutine permettant aux Enemy de tirer des missiles avec des paramètres donnés.
    Retourne rien.
    */
    IEnumerator ShootRocket(int rocketSpawnerID){

        while(true){

            _missile.Play();

            GameObject newRocket = Instantiate(rocket, rocketSpawners[rocketSpawnerID].position, Quaternion.Euler(transform.forward)); // Instancie un missile.

            // Applique les valeurs donnés dans les variables public du script.
            newRocket.GetComponent<Rigidbody>().velocity = -transform.forward * -speedRocket;
            newRocket.GetComponent<Bullet>().bulletDamages = damageRocket;
            newRocket.GetComponent<Bullet>().bulletType = Bullet.BulletType.PlayerHit;

            Destroy(newRocket, 3f); // Détruit le GameObject de le missile après 3 secondes.

            yield return new WaitForSeconds(fireRateRocket); // Tire selon le FireRate donné.

        }

    }

}
