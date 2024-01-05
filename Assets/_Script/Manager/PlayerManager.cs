using _Script.CharacterBehavior;
using UnityEngine;

namespace _Script.Manager
{
    public class PlayerManager : MonoBehaviour
    {

        #region Variables

        [Header("Ship Parameters")]
        [SerializeField] private GameObject[] playerShips;

        // Managers Variables.
        private UIManager _uiManager;
        
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

            SpawnShip();
        }

        #endregion

        #region Ship

        /**
         * <summary>
         * Function to spawn the player.
         * </summary>
         */
        public void SpawnShip(){
            // Change the ship of the player.
            if(GameObject.Find("Player"))
                Destroy(GameObject.Find("Player"));
            
            GameObject player = Instantiate(
                playerShips[GameManager.LvlShip], 
                transform.position, 
                Quaternion.identity
                );
            player.name = "Player";

            // Weaponry
            if(GameManager.LvlShip == 2){
                _uiManager.ActivateWeapons();
            }

            // Update les points de vies.
            _uiManager.UpdateHealth(GameObject.Find("Player").GetComponent<HealthController>().MaxHealth);
        }

        #endregion

    }
}
