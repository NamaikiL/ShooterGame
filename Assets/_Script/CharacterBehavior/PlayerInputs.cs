using UnityEngine;

namespace _Script.CharacterBehavior
{
    public class PlayerInputs : MonoBehaviour
    {

        #region Variables

        // Inputs Variables.
        private float _horizontal, _vertical;
        private bool _changeWeapons;
        
        // Instance Variable.
        private static PlayerInputs _instance;

        #endregion

        #region Properties

        public float Vertical => _vertical;
        public float Horizontal => _horizontal;
        public bool ChangeWeapons => _changeWeapons;

        public static PlayerInputs Instance => _instance;

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


        /**
         * <summary>
         * Update is called once per frame.
         * </summary>
         */
        void Update()
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");
            _changeWeapons = Input.GetButtonDown("ChangeWeapons");
        }

        #endregion
        
    }
}
