using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    // Variables Global Public.
    public int bulletDamages = 0;

    public enum BulletType{
        EnemyHit,
        PlayerHit
    }

    public BulletType bulletType;


    /*
    Start is called before the first frame update.
    Retourne rien.
    */
    void Start(){

        // Switch permettant à chaque type de balle d'éviter des collision précises.
        switch(bulletType){
            case BulletType.PlayerHit:
                //Physics.IgnoreLayerCollision(6, 3, true); // Vue en classe, mais empêche le OnTriggerEnter de s'activer. || Ne marche pas non plus avec un OnCollisionEnter.
                //Physics.IgnoreLayerCollision(6, 9, true);
                break;
            case BulletType.EnemyHit:
                //Physics.IgnoreLayerCollision(6, 7, true); // Vue en classe, mais empêche le OnTriggerEnter de s'activer. || Ne marche pas non plus avec un OnCollisionEnter.
                break;
        }

    }


    /*
    Fonction qui s'occupe des points de dégâts reçu par le Player/Enemy.
    Retourne rien.
    */
    void OnTriggerEnter(Collider other){

        // Si le player se fait toucher par une balle enemy, enlève un nombre de HP définit.
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && bulletType == BulletType.PlayerHit){
            other.gameObject.GetComponent<HealthController>().Hurt(bulletDamages);
            Destroy(gameObject);
        }

        // Si l'enemy se fait toucher par une balle player, enlève un nombre de HP définit.
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy") && bulletType == BulletType.EnemyHit){
            other.gameObject.GetComponent<HealthController>().Hurt(bulletDamages);
            Destroy(gameObject);
        }

        // Si le joueur tire sur le bouclier du Boss, enlève un nombre de HP définit.
        if(other.gameObject.layer == LayerMask.NameToLayer("Shield") && other.gameObject.tag == "Boss" && bulletType == BulletType.EnemyHit){
            other.gameObject.GetComponent<HealthController>().Hurt(bulletDamages);
            Destroy(gameObject);
        }
        
        // Si l'Enemy tire sur le bouclier, annule les dégats.
        if(other.gameObject.layer == LayerMask.NameToLayer("Shield") && other.gameObject.tag == "Player" && bulletType == BulletType.PlayerHit){
            Destroy(gameObject);
        }

    }


    // Test pour le Physics.IgnoreLayerCollision.
    /*void OnCollisionEnter(Collision other){

        // Si le player se fait toucher par une balle enemy, enlève un nombre de HP définit.
        if(bulletType == BulletType.PlayerHit){
            Debug.Log("J'ai hit un Player");
            other.gameObject.GetComponent<HealthController>().Hurt(bulletDamages);
            Destroy(gameObject);
        }

        // Si l'enemy se fait toucher par une balle player, enlève un nombre de HP définit.
        if(bulletType == BulletType.EnemyHit){
            Debug.Log("J'ai hit un Enemy");
            other.gameObject.GetComponent<HealthController>().Hurt(bulletDamages);
            Destroy(gameObject);
        }

        // Si l'Enemy tire sur le bouclier, annule les dégats.
        if(other.gameObject.layer == LayerMask.NameToLayer("Shield") && bulletType == BulletType.PlayerHit){
            Debug.Log("J'ai hit le bouclier");
            Destroy(gameObject);
        }

    }*/

}
