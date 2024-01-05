using System.Collections;
using _Script.Manager;
using UnityEngine;

namespace _Script.CharacterBehavior
{
    public class HealthController : MonoBehaviour
    {

        #region Variables

        [Header("Health Parameters")] 
        [SerializeField] private int health;
        [SerializeField] private int nbHealMin;
        [SerializeField] private int nbHealMax;
        [SerializeField] private float hurtCooldown = 1f;
        [SerializeField] private GameObject heal;
    
        [Header("Coin Parameters")]
        [SerializeField] private int nbCoinMin;
        [SerializeField] private int nbCoinMax;
        [SerializeField] private GameObject coin;
    
        [Header("Loot Parameters")]
        [SerializeField] private int probBonusMax = 2;
        [SerializeField] private GameObject[] drops;

        private AudioSource _explosion;
        
        // Health Variables.
        private int _currentHealth;
        private float _currentCooldown;
        
        // Managers Variables.
        private GameManager _gameManager;
        private UIManager _uiManager;
        private MenuManager _menuManager;

        #endregion

        #region Properties

        public int MaxHealth => health;
        public int CurrentHealth => _currentHealth;

        #endregion
        
        #region Built-In Methods

        /**
         * <summary>
         * Start is called before the first frame update.
         * </summary>
         */
        void Start()
        {
            _gameManager = GameManager.Instance;
            _uiManager = UIManager.Instance;
            _menuManager = MenuManager.Instance;
            _explosion = GameObject.Find("Explosion").GetComponent<AudioSource>();
            
            _currentHealth = health;
        }


        /**
         * <summary>
         * Update is called once per frame.
         * </summary>
         */
        void Update()
        {
            // Invincibility Frame.
            if(_currentCooldown > 0f)
                _currentCooldown = Mathf.Clamp(_currentCooldown - Time.deltaTime, 0f, hurtCooldown);
        }

        #endregion

        #region Custom Methods

        /**
         * <summary>
         * Function that removes HP to player/enemy.
         * </summary>
         * <param name="damage">The number of HP to remove.</param>
         */
        public void Hurt(int damage)
        {
            if(_currentCooldown > 0f) return;    // Invincibility Frame Check.
            
            _currentCooldown = hurtCooldown;
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, health);

            // UI Integration for player.
            if(gameObject.layer == LayerMask.NameToLayer("Player"))
                _uiManager.UpdateHealth(_currentHealth);

            // If Enemy/Player doesn't have any HP, call death.
            if(_currentHealth <= 0f)
                StartCoroutine(Death());
        }
        
        
        /**
         * <summary>
         * Function to heal the player.
         * </summary>
         * <param name="healRegen">The number of HP to add.</param>
         */
        public void Regenerate(int healRegen)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + healRegen, 0, health);

            // UI Integration.
            _uiManager.UpdateHealth(_currentHealth);
        }

        
        /**
         * <summary>
         * Coroutine for the death of an entity.
         * </summary>
         */
        public IEnumerator Death()
        {
            _explosion.Play();

            // If Entity has Coin.
            if(coin)
            {
                for(int i = 0; i < Random.Range(nbCoinMin, nbCoinMax); i++)
                {
                    Instantiate(
                        coin, 
                        transform.position + new Vector3(Random.Range(-4,4), 0, Random.Range(-4,4)), 
                        Quaternion.identity
                        );
                }
            }

            // If Entity has Heal.
            if(heal)
            {
                for(int i = 0; i < Random.Range(nbHealMin, nbHealMax); i++)
                {
                    Instantiate(
                        heal, 
                        transform.position + new Vector3(Random.Range(-10,10), 0, Random.Range(-10,10)), 
                        Quaternion.identity
                        );
                }
            }

            // If Entity has drop.
            if(drops.Length > 0)
            {
                if(Random.Range(0,probBonusMax) == 0)
                {
                    Instantiate(
                        drops[Random.Range(0, drops.Length)], 
                        transform.position, 
                        Quaternion.identity
                        );
                }
            }

            // Player Behavior.
            if(gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                yield return new WaitForSeconds(1f);
                _menuManager.SceneGameOver(gameObject.scene.name);
            }
            // Enemy Behavior.
            else if (gameObject.layer == LayerMask.NameToLayer("Enemy") && gameObject.CompareTag("Enemy"))
                _gameManager.AddEnemyDeathToCounterAndCheckIfPlayerPass();
            // Boss Behavior.
            else if(gameObject.layer == LayerMask.NameToLayer("Enemy") && gameObject.CompareTag("Boss"))
                _menuManager.SceneEnd();

            Destroy(gameObject);
            yield return null;
        }
        
        #endregion
        
    }
}
