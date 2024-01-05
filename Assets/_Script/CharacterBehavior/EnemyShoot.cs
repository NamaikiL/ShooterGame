using System.Collections;
using _Script.Stuff;
using UnityEngine;

namespace _Script.CharacterBehavior
{
    public class EnemyShoot : MonoBehaviour
    {
        #region Variables.

        [Header("Enemy bullets parameters")]
        [SerializeField] private int damageBullet = 10;
        [SerializeField] private float speedBullet = 50f;
        [SerializeField] private float fireRateBullet = 0.5f;
        [SerializeField] private Transform[] bulletSpawners;
        [SerializeField] private GameObject bullet;
        
        [Header("Enemy Rocket Parameters")] 
        [SerializeField] private int damageRocket = 25;
        [SerializeField] private float speedRocket = 100f;
        [SerializeField] private float fireRateRocket = 5f;
        [SerializeField] private Transform[] rocketSpawners;
        [SerializeField] private GameObject rocket;

        [Header("Audios")] 
        [SerializeField] private AudioSource shoot;
        [SerializeField] private AudioSource missile;

        #endregion
    
        #region Built-In Methods.

        /**
         * <summary>
         * Start is called before the first frame update.
         * </summary>
         */
        void Start()
        {
            if(bullet)
            {
                for(int i = 0; i<bulletSpawners.Length; i++)
                {
                    StartCoroutine(ShootBullet(i));
                }
            } // If there's a bullet, shoot bullet at the defined points.

            if(rocket)
            {
                for(int i = 0; i<rocketSpawners.Length; i++)
                {
                    StartCoroutine(ShootRocket(i));
                }
            } // If there's a rocket, shoot rocket at the defined points.
        }

        #endregion

        #region Shooting

        /**
         * <summary>
         * Coroutine that make the enemy shoot bullet.
         * </summary>
         * <param name="bulletSpawnerID">The id of the spawner.</param>
         */
        private IEnumerator ShootBullet(int bulletSpawnerID)
        {
            while(true)
            {
                shoot.Play();

                Vector3 directionForward = transform.forward;
                GameObject newBullet = Instantiate(
                    bullet, 
                    bulletSpawners[bulletSpawnerID].position, 
                    Quaternion.LookRotation(-directionForward)
                    );
                
                // Give the bullet its values.
                newBullet.GetComponent<Rigidbody>().velocity = -directionForward * -speedBullet;
                newBullet.GetComponent<Bullet>().BulletDamages = damageBullet;

                Destroy(newBullet, 3f); // Destroy the bullet after some time if did not collide.

                yield return new WaitForSeconds(fireRateBullet); // Fire-rate. 
            }
        }


        /**
         * <summary>
         * Coroutine that make the enemy shoot the rocket.
         * </summary>
         * <param name="rocketSpawnerID">The id of the spawner.</param>
         */
        private IEnumerator ShootRocket(int rocketSpawnerID)
        {
            while (true)
            {
                missile.Play();

                Vector3 directionForward = transform.forward;
                GameObject newRocket = Instantiate(
                    rocket, 
                    rocketSpawners[rocketSpawnerID].position, 
                    Quaternion.LookRotation(-directionForward)
                    );

                // Give the Rocket its values.
                newRocket.GetComponent<Rigidbody>().velocity = -directionForward * -speedRocket;
                newRocket.GetComponent<Bullet>().BulletDamages = damageRocket;
                newRocket.GetComponent<Bullet>().ActualBulletType = BulletType.PlayerHit;

                Destroy(newRocket, 3f); // Destroy the rocket after some time if did not collide.

                yield return new WaitForSeconds(fireRateRocket); // Fire-rate.
            }
        }
        
        #endregion
        
    }
}
