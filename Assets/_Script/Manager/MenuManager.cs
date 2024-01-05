using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Script.Manager
{
    public class MenuManager : MonoBehaviour
    {
        
        #region Variables

        [Header("Audio")] 
        [SerializeField] private AudioSource button;

        // Levels Variables.
        private static string _lastLevel;
        
        // Instance Variable.
        private static MenuManager _instance;

        #endregion

        #region Properties

        public static MenuManager Instance => _instance;

        #endregion

        #region Built-In Methods

        /**
         * <summary>
         * Awake is called when an enabled script instance is being loaded.
         * </summary>
         */
        void Awake()
        {
            if(_instance) Destroy(this);
            _instance = this;
        }

        #endregion
        
        #region Scene Methods

        /**
         * <summary>
         * Function to change the levels.
         * </summary>
         */
        public void SceneLevel()
        {
            button.Play();

            _lastLevel = gameObject.scene.name; // Get the scene name.

            switch (_lastLevel)
            {
                case "Level1":
                    SceneManager.LoadScene("Level2", LoadSceneMode.Single);
                    break;
                case "Level2":
                    SceneManager.LoadScene("LevelFinal", LoadSceneMode.Single);
                    break;
                default:
                    SceneManager.LoadScene("Level1", LoadSceneMode.Single);
                    GameManager.LvlShip = 0; // Reset the ship if load the first scene.
                    break;
            }
        }


        /**
         * <summary>
         * Function to play the GameOver Scene.
         * </summary>
         * <param name="lastLevel">The last level saved.</param>
         */
        public void SceneGameOver(string lastLevel)
        {
            button.Play();
            _lastLevel = lastLevel;
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }


        /**
         * <summary>
         * Function to play the menu scene.
         * </summary>
         */
        public void SceneMenu()
        {
            button.Play();
            _lastLevel = "";
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }


        /**
         * <summary>
         * Function to restart the game to the last level played.
         * </summary>
         */
        public void SceneRestart()
        {
            button.Play();
            SceneManager.LoadScene(_lastLevel, LoadSceneMode.Single);
        }


        /**
         * <summary>
         * Function to play the end scene.
         * </summary>
         */
        public void SceneEnd()
        {
            button.Play();
            SceneManager.LoadScene("End", LoadSceneMode.Single);
        }


        /**
         * <summary>
         * Function to quit the game.
         * </summary>
         */
        public void Quit()
        {
            button.Play();
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }

        #endregion
        
    }
}
