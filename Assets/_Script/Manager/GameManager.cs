using UnityEngine;

namespace _Script.Manager
{
    public class GameManager : MonoBehaviour
    {

        #region Variables

        [Header("Goals Parameters")]
        [SerializeField] private int nbDeadEnemiesNeeded;
        
        [Header("UI Wave Parameters")]
        [SerializeField] private GameObject waves;
        [SerializeField] private GameObject waveManager;
        
        [Header("Audios")]
        [SerializeField] private AudioSource coin;
        
        // Ship Parameters
        private static int _lvlShip;
        
        // Goals Parameters
        private int _nbEnemyDead;
        
        // Stats Parameters
        private static int _coins;
        
        // Managers Parameters
        private UIManager _uiManager;
        
        // Instance Parameters
        private static GameManager _instance;

        #endregion

        #region Properties

        public int Coins => _coins;
        public static int LvlShip
        {
            get => _lvlShip;
            set => _lvlShip = value;
        }

        public static GameManager Instance => _instance;

        #endregion

        #region Built-In Methods

        /**
         * <summary>
         * Awake is called before any Start functions.
         * </summary>
         */
        void Awake()
        {
            if(_instance) Destroy(this);
            _instance = this;
        }


        /**
         * <summary>
         * Start is called before the first frame update.
         * </summary>
         */
        void Start()
        {
            _uiManager = UIManager.Instance;

            if(gameObject.scene.name == "Level1")
                ResetCoin();

            _uiManager.UpdateCoinsInfo(_coins);
        }

        #endregion

        #region Custom Methods

        /**
         * <summary>
         * Function adding coin to the counter.
         * </summary>
         */
        public void AddCoin()
        {
            coin.Play();
            _coins++;
            _uiManager.UpdateCoinsInfo(_coins);     // UI Implementation.
        }


        /**
         * <summary>
         * Function to remove a defined coin number.
         * </summary>
         * <param name="coinToRemove">The quantity to remove.</param>
         */
        public void RemoveCoin(int coinToRemove)
        {
            _coins -= coinToRemove;
            _uiManager.UpdateCoinsInfo(_coins);     // UI Implementation.
        }


        /**
         * <summary>
         * Function to reset the coin counter.
         * </summary>
         */
        private void ResetCoin()
        {
            _coins = 0;
            _uiManager.UpdateCoinsInfo(_coins);
        }


        /**
         * <summary>
         * Function to handle the enemies killing count.
         * </summary>
         */
        public void AddEnemyDeathToCounterAndCheckIfPlayerPass()
        {
            _nbEnemyDead++;
            
            if(_nbEnemyDead >= nbDeadEnemiesNeeded && gameObject.scene.name == "LevelFinal")
            {
                waves.SetActive(false);
                waveManager.SetActive(false);
                GameObject.Find("Boss").GetComponent<Animator>().SetBool($"Boss", true);
            }
            
            else if(_nbEnemyDead >= nbDeadEnemiesNeeded && gameObject.scene.name != "LevelFinal")
                GameObject.Find("LevelManager").GetComponent<LevelManager>().FinishLevel();
        }

        #endregion
        
    }
}
