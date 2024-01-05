using UnityEngine;

namespace _Script.Manager
{
    public class LevelManager : MonoBehaviour
    {

        #region Variables

        [Header("UI Parameters")] 
        [SerializeField] private GameObject startMenuUi;
        [SerializeField] private GameObject endMenuUi;
        [SerializeField] private GameObject mainUi;

        #endregion

        #region Built-In Methods

        /**
         * <summary>
         * Start is called before the first frame update.
         * </summary>
         */
        void Start()
        {
            LevelManagement(0, mainUi, false, startMenuUi, true);
        }

        #endregion

        #region Level Management

        /**
         * <summary>
         * Function to start the level.
         * </summary>
         */
        public void StartLevel()
        {
            LevelManagement(
                1f, 
                mainUi, 
                true, 
                startMenuUi, 
                false
                );
        }


        /**
         * <summary>
         * Function to finish the level.
         * </summary>
         */
        public void FinishLevel()
        {
            LevelManagement(
                0f, 
                mainUi, 
                false, 
                endMenuUi, 
                true
                );
        }


        /**
         * <summary>
         * Function to control the UI.
         * </summary>
         * <param name="timeScale">The time scale wanted.</param>
         * <param name="display1">The first UI.</param>
         * <param name="display1State">The first UI state.</param>
         * <param name="display2">The second UI.</param>
         * <param name="display2State">The second UI state.</param>
         */
        void LevelManagement(float timeScale, GameObject display1, bool display1State, GameObject display2, bool display2State)
        {
            Time.timeScale = timeScale;
            display1.SetActive(display1State);
            display2.SetActive(display2State);
        }

        #endregion
        
    }    
}
