using System.Collections.Generic;
using _Script.CharacterBehavior;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Script.Manager
{
    public class UIManager : MonoBehaviour
    {

        #region Variables

        [Header("Player UI Parameters")]
        [SerializeField] private Image imgPowerUp;
        [SerializeField] private List<Sprite> powerUpSprites;
        [SerializeField] private GameObject btnBullet;
        [SerializeField] private GameObject btnMissile;
        [SerializeField] private TMP_Text txtCoins;
        [SerializeField] private TMP_Text txtCoinsShop;
        [SerializeField] private TMP_Text txtPowerUpTimer;
        [SerializeField] private TMP_Text txtWaves;
        [SerializeField] private TMP_Text txtHealth;
        
        // Instance Variable.
        private static UIManager _instance;

        #endregion

        #region Properties

        public static UIManager Instance => _instance;

        #endregion

        #region Built-In Methods

        /**
         * <summary>
         * Awake is called before any Start functions.
         * </summary>
         */
        void Awake()
        {
            if(_instance) Destroy(gameObject);
            _instance = this;
        }

        #endregion

        #region Custom Methods

        /**
         * <summary>
         * Function to update the coins on the UI.
         * </summary>
         * <param name="nbCoins">The coins.</param>
         */
        public void UpdateCoinsInfo(int nbCoins)
        {
            txtCoins.text = nbCoins.ToString();
            txtCoinsShop.text = nbCoins.ToString();
        }


        /**
         * <summary>
         * Function to update the power up timer and the sprite.
         * </summary>
         * <param name="seconds">The seconds of the timer.</param>
         * <param name="powerUp">The power up name.</param>
         */
        public void UpdatePowerUpTimeAndImg(int seconds, string powerUp)
        {
            switch (powerUp)
            {
                case "DamagePlus":
                    imgPowerUp.sprite = powerUpSprites.Find(sprite => sprite.name == "DamagePlus");
                    break;
                case "FireRatePlus":
                    imgPowerUp.sprite = powerUpSprites.Find(sprite => sprite.name == "FireRatePlus");
                    break;
                case "ShieldUp":
                    imgPowerUp.sprite = powerUpSprites.Find(sprite => sprite.name == "ShieldUp");
                    break;
            }

            txtPowerUpTimer.text = seconds.ToString();
        }


        /**
         * <summary>
         * Function to update waves UI for each state.
         * </summary>
         * <param name="timeUntilNextWave">The time until the next wave.</param>
         * <param name="currentWave">The current wave.</param>
         */
        public void UpdateWaves(float timeUntilNextWave, int currentWave)
        {
            txtWaves.color = new Color32(0, 100, 0, 255);
            txtWaves.text = "Wave " + currentWave + " - Finished !" +
                            "\nTime until next wave: " + timeUntilNextWave;
        }

        
        /**
         * <summary>
         * Function to update waves UI for each state.
         * </summary>
         * <param name="currentWave">The current wave.</param>
         */
        public void UpdateWaves(int currentWave){

            txtWaves.color = new Color32(139, 0, 0, 255);
            txtWaves.text = "Wave " + currentWave + " - In Progress";

        }


        /**
         * <summary>
         * Function to update the player HP.
         * </summary>
         * <param name="health">Player health.</param>
         */
        public void UpdateHealth(int health)
        {
            txtHealth.text = health.ToString();
        }


        /**
         * <summary>
         * Function to show the weapon button.
         * </summary>
         */
        public void ActivateWeapons()
        {
            btnBullet.SetActive(true);
        }


        /**
         * <summary>
         * Function used for the weapon UI.
         * </summary>
         */
        public void UIChangeWeapons()
        {
            if(btnBullet.activeSelf){
                btnBullet.SetActive(false);
                btnMissile.SetActive(true);
                GameObject.Find("Player").GetComponent<PlayerShoot>().ChangeWeapon();
            }else if(btnMissile.activeSelf){
                btnBullet.SetActive(true);
                btnMissile.SetActive(false);
                GameObject.Find("Player").GetComponent<PlayerShoot>().ChangeWeapon();
            }
        }

        #endregion

    }
}
