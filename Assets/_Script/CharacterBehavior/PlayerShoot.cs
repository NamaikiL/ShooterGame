using System;
using System.Collections;
using _Script.Manager;
using _Script.Stuff;
using UnityEngine;

namespace _Script.CharacterBehavior
{
    public enum WeaponType
    {
        Bullet,
        Rocket
    }
    
    public class PlayerShoot : MonoBehaviour
    {

        #region Variables

        [Header("Bullet Parameters")] 
        [SerializeField] private int damageBullet = 10;
        [SerializeField] private float speedBullet = 50f;
        [SerializeField] private float fireRateBullet = .5f;
        [SerializeField] private Transform[] bulletSpawners;
        [SerializeField] private GameObject bullet;
        
        [Header("Rocket Parameters")]
        [SerializeField] private int damageRocket = 25;
        [SerializeField] private float speedRocket = 100f;
        [SerializeField] private float fireRateRocket = 5f;
        [SerializeField] private Transform[] rocketSpawners;
        [SerializeField] private GameObject rocket;
        
        [Header("Shield Parameters")]
        [SerializeField] private GameObject shieldPlayer;
        
        [Header("Audios")]
        [SerializeField] private AudioSource shoot;
        [SerializeField] private AudioSource missile;

        // Weapons Parameters.
        private int _currentBulletDamage, _currentRocketDamage;
        private int _currentWeaponType;
        private float _currentFireRateBullet, _currentFireRateRocket;
        private WeaponType _weapon;

        // Managers Variables.
        private UIManager _uiManager;

        #endregion

        #region Properties

        public GameObject Bullet => bullet;
        public GameObject Rocket => rocket;

        #endregion

        #region Built-In Methods


        /**
         * <summary>
         * Start is called before the first frame update.
         * </summary>
         */
        void Start()
        {
            _uiManager = UIManager.Instance;

            // Stock the bullets & rockets stats in variables.
            _currentFireRateBullet = fireRateBullet;
            _currentBulletDamage = damageBullet;
            _currentFireRateRocket = fireRateRocket;
            _currentRocketDamage = damageRocket;
            
            if(bullet)
            {
                for(int i = 0; i<bulletSpawners.Length; i++)
                {
                    StartCoroutine(ShootBullet(i));
                }
            }   // If bullet isn't null.
        
            if(rocket)
            {
                for(int i = 0; i<rocketSpawners.Length; i++)
                {
                    StartCoroutine(ShootRocket(i));
                }
            }   // If rocket isn't null.
        }

        #endregion

        #region Shooting Behavior

        /**
         * <summary>
         * Coroutine making the player shoot bullets.
         * </summary>
         * <param name="bulletSpawnerID">The spawner id.</param>
         */
        private IEnumerator ShootBullet(int bulletSpawnerID)
        {
            while(true)
            {
                // Weapon Type.
                if(_weapon == WeaponType.Bullet)
                {
                    shoot.Play();
                    Vector3 playerForward = transform.forward;
                    
                    GameObject newBullet = Instantiate(
                        bullet, 
                        bulletSpawners[bulletSpawnerID].position, 
                        Quaternion.Euler(playerForward)
                        );

                    // Give the bullet its stats
                    newBullet.GetComponent<Rigidbody>().velocity = playerForward * speedBullet;
                    newBullet.GetComponent<Bullet>().BulletDamages = _currentBulletDamage;

                    Destroy(newBullet, 3f); // Destroy the bullet after some time.

                    yield return new WaitForSeconds(_currentFireRateBullet);
                }
                yield return null;
            }
        }


        /**
         * <summary>
         * Coroutine making the player shoot rockets.
         * </summary>
         * <param name="rocketSpawnerID">The id of the spawner.</param>
         */
        private IEnumerator ShootRocket(int rocketSpawnerID)
        {
            while(true)
            {
                // Weapon type.
                if(_weapon == WeaponType.Rocket)
                {
                    missile.Play();
                    Vector3 playerForward = transform.forward;
                    
                    GameObject newRocket = Instantiate(
                        rocket, 
                        rocketSpawners[rocketSpawnerID].position, 
                        Quaternion.Euler(playerForward)
                        );

                    // Apply rocket stats.
                    newRocket.GetComponent<Rigidbody>().velocity = playerForward * speedRocket;
                    newRocket.GetComponent<Bullet>().BulletDamages = _currentRocketDamage;
                    newRocket.GetComponent<Bullet>().ActualBulletType = BulletType.EnemyHit;

                    Destroy(newRocket, 3f); // Destroy the rocket after some time.

                    yield return new WaitForSeconds(_currentFireRateRocket); // Fire-rate.
                }
                yield return null;
            }
        }


        /**
         * <summary>
         * Function to change the type of weapon.
         * </summary>
         */
        public void ChangeWeapon()
        {
            _currentWeaponType++;

            if (_currentWeaponType >= Enum.GetValues(typeof(WeaponType)).Length) _currentWeaponType = 0;

            _weapon = (WeaponType)_currentWeaponType;
        }

        #endregion

        #region Power Ups

        /**
         * <summary>
         * Coroutine that add damage to the player for defined time.
         * </summary>
         */
        public IEnumerator DamagePlus()
        {
            ResetPowerValues(); // Reset Values.

            _currentBulletDamage *= 2;
            _currentRocketDamage *= 2;
            
            for(int i = 30; i >= 0; i--)
            {
                _uiManager.UpdatePowerUpTimeAndImg(i, "DamagePlus");
                yield return new WaitForSeconds(1f);
            }
            
            _currentBulletDamage = damageBullet;
            _currentRocketDamage = damageRocket;
        }


        /**
         * <summary>
         * Coroutine that add fire-rate to the player for defined time.
         * </summary>
         */
        public IEnumerator FireRatePlus()
        {
            ResetPowerValues(); // Reset Values.
        
            _currentFireRateBullet /= 2;
            _currentFireRateRocket /= 2;
            
            for(int i = 30; i >= 0; i--)
            {
                _uiManager.UpdatePowerUpTimeAndImg(i, "FireRatePlus");
                yield return new WaitForSeconds(1f);
            }
            
            _currentFireRateBullet = fireRateBullet;
            _currentFireRateRocket = fireRateRocket;
        }


        /**
         * <summary>
         * Coroutine that add shield to the player for defined time.
         * </summary>
         */
        public IEnumerator ShieldUp()
        {
            ResetPowerValues(); // Reset Values.

            shieldPlayer.SetActive(true);
            
            for(int i = 10; i >= 0; i--)
            {
                _uiManager.UpdatePowerUpTimeAndImg(i, "ShieldUp");
                yield return new WaitForSeconds(1f);
            }
            
            shieldPlayer.SetActive(false);
        }


        /**
         * <summary>
         * Function that destroy all enemies on map.
         * </summary>
         */
        public void Nuke()
        {
            foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                StartCoroutine(enemy.GetComponent<HealthController>().Death());
            }
        }


        /**
         * <summary>
         * Function to reset all power-ups values.
         * </summary>
         */
        private void ResetPowerValues(){
            _currentFireRateBullet = fireRateBullet;
            _currentFireRateRocket = fireRateRocket;
            _currentBulletDamage = damageBullet;
            _currentRocketDamage = damageRocket;
            shieldPlayer.SetActive(false);
        }

        #endregion
        
    }
}
