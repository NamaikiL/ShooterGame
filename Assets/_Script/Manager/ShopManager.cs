using _Script.CharacterBehavior;
using UnityEngine;

namespace _Script.Manager
{
    public class ShopManager : MonoBehaviour
    {

        #region Variables

        [Header("Power Up Parameters")] 
        [SerializeField] private int rndPowerUp;
        [SerializeField] private int pricePowerUp = 5;
        [SerializeField] private int priceHeal = 10;
        
        [Header("UI Parameters")]
        [SerializeField] private GameObject shopUi;
        [SerializeField] private GameObject mainUi;
        
        [Header("Shop Buttons")]
        [SerializeField] private GameObject[] btnShips;
        [SerializeField] private GameObject[] btnPowerUps;
        
        [Header("Audios")]
        [SerializeField] private AudioSource button;
        [SerializeField] private AudioSource buy;
        
        // Shop Variable.
        private bool _isShopOpen;

        // Managers Variables.
        private GameManager _gameManager;
        
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

            RandomPowerUp();
            ShipUpdate();
        }

        #endregion

        #region Shop Methods

         /**
          * <summary>
          * Function to open the shop.
          * </summary>
          */
        public void Shop()
        {
            button.Play();
            
            if(!_isShopOpen)
                ShopOpen(true, false, 0f, true);
            else
                ShopOpen(false, true, 1f, false);
        }


        /**
         * <summary>
         * Function to change the UI for the shop.
         * </summary>
         * <param name="shopState">The state of the Shop UI.</param>
         * <param name="mainUiState">The state of the Main UI.</param>
         * <param name="timeScale">The TimeScale.</param>
         * <param name="shopCheck">To check if the shop is active or not.</param>
         */
        private void ShopOpen(bool shopState, bool mainUiState, float timeScale, bool shopCheck)
        {
            shopUi.SetActive(shopState);
            mainUi.SetActive(mainUiState);
            Time.timeScale = timeScale;
            _isShopOpen = shopCheck;
        }


        /**
         * <summary>
         * Function to randomize the power up in the shop.
         * </summary>
         */
        private void RandomPowerUp()
        {
            rndPowerUp = Random.Range(0,btnPowerUps.Length);
            btnPowerUps[rndPowerUp].SetActive(true);
        }


        /**
         * <summary>
         * Function to buy in the shop.
         * </summary>
         */
        public void BuyBonus()
        {
            if (_gameManager.Coins < pricePowerUp) return;
            buy.Play();
            PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            PlayerShoot playerShoot = GameObject.Find("Player").GetComponent<PlayerShoot>();
            
            switch(btnPowerUps[rndPowerUp].name){
                case "BtnDamagePlus":
                    playerController.ActivatePowerUp(playerShoot.DamagePlus());
                    btnPowerUps[rndPowerUp].SetActive(false);
                    break;
                case "BtnFireRatePlus":
                    playerController.ActivatePowerUp(playerShoot.FireRatePlus());
                    btnPowerUps[rndPowerUp].SetActive(false);
                    break;
                case "BtnShieldUp":
                    playerController.ActivatePowerUp(playerShoot.ShieldUp());
                    btnPowerUps[rndPowerUp].SetActive(false);
                    break;
            }

            _gameManager.RemoveCoin(pricePowerUp);
            RandomPowerUp();
        }


        /**
         * <summary>
         * Function to buy energy in the shop.
         * </summary>
         */
        public void BuyHealth()
        {
            if (_gameManager.Coins <= priceHeal) return;
            HealthController healthController = GameObject.Find("Player").GetComponent<HealthController>();
            
            if(healthController.CurrentHealth + 10 < healthController.MaxHealth)
            {
                buy.Play();
                healthController.Regenerate(10);
                _gameManager.RemoveCoin(priceHeal);
            }
        }


        /**
         * <summary>
         * Function to update the shop when the player buy a ship.
         * </summary>
         */
        private void ShipUpdate()
        {
            switch(GameManager.LvlShip){
                case 0:
                    btnShips[0].SetActive(true);
                    break;
                case 1:
                    btnShips[1].SetActive(true);
                    break;
            }
        }


        /**
         * <summary>
         * Function to buy the ship script.
         * </summary>
         */
        public void BuyShip()
        {
            if(GameManager.LvlShip == 0 && _gameManager.Coins >= 50){
                UpgradeShip();
                btnShips[GameManager.LvlShip].SetActive(true);
                _gameManager.RemoveCoin(50);
            }else if(GameManager.LvlShip == 1 && _gameManager.Coins >= 100){
                UpgradeShip();
                _gameManager.RemoveCoin(100);
            }

        }


        /**
         * <summary>
         * Function for the ship upgrade.
         * </summary>
         */
        private void UpgradeShip()
        {
            buy.Play();
            btnShips[GameManager.LvlShip].SetActive(false);
            GameManager.LvlShip++;
            GameObject.Find("PlayerManager").GetComponent<PlayerManager>().SpawnShip();
        }

        #endregion

    }
}
