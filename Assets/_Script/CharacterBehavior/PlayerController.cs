using System.Collections;
using _Script.Manager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Script.CharacterBehavior
{
    public class PlayerController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {

        #region Variables

        [Header("Player Movements Parameters")] 
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float xMax;
        [SerializeField] private float zMax;
        [SerializeField] private float dragOffsetZ;
        [SerializeField] private float dragLerpMultiplier;
        [SerializeField] private bool useClamp = true;
        
        [Header("Audio")] 
        [SerializeField] private AudioSource shield;
        [SerializeField] private AudioSource health;
        
        // Power Up Variables.
        private IEnumerator _currentPowerUp;

        // Click Event Variables.
        private bool _isDragging;
        private PointerEventData _lastData;
        private Vector3 _wantedPos;

        // Components Variables.
        private Camera _cam;
        private PlayerInputs _inputs;
        private PlayerShoot _playerShoot;
        
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
            _inputs = PlayerInputs.Instance;
            _playerShoot = GetComponent<PlayerShoot>();
            _gameManager = GameManager.Instance;
            _cam = Camera.main;
        }


        /**
         * <summary>
         * Update is called once per frame.
         * </summary>
         */
        void Update()
        {
            Move();

            if(_inputs.ChangeWeapons && _playerShoot.Bullet && _playerShoot.Rocket)
                _playerShoot.ChangeWeapon();
        }


        /**
         * <summary>
         * When a GameObject collides with another GameObject, Unity calls OnTriggerEnter. 
         * </summary>
         * <param name="other">The other Collider involved in this collision.</param>
         */
        void OnTriggerEnter(Collider other)
        {
            // Collectible Behavior.
            if(other.gameObject.layer == LayerMask.NameToLayer("Collectables"))
            {
                switch(other.gameObject.tag)
                {
                    case "Coin":
                        _gameManager.AddCoin();
                        break;
                    case "Damage":
                        ActivatePowerUp(_playerShoot.DamagePlus());
                        break;
                    case "FireRate":
                        ActivatePowerUp(_playerShoot.FireRatePlus());
                        break;
                    case "Shield":
                        shield.Play();
                        ActivatePowerUp(_playerShoot.ShieldUp());
                        break;
                    case "Nuke":
                        _playerShoot.Nuke();
                        break;
                    case "Heal":
                        health.Play();
                        GetComponent<HealthController>().Regenerate(15);
                        break;
                    default:
                        Debug.LogError("Error: Missing object behavior.");
                        break;
                }
                Destroy(other.gameObject);
            }
        }

        #endregion

        #region Custom Methods

        /**
         * <summary>
         * Function for the player movements.
         * </summary>
         */
        private void Move()
        {
            _wantedPos = transform.position;
            
            // Movements with mouse inputs.
            if(_lastData != null && _isDragging)
            {
                if(Physics.Raycast(_cam.ScreenPointToRay(_lastData.position), out RaycastHit hit, 300, 1<<31))
                    _wantedPos = Vector3.Lerp(transform.position, hit.point + Vector3.forward * dragOffsetZ, Time.deltaTime * dragLerpMultiplier);
            }
            
            // Movements with key inputs.
            else
                _wantedPos = transform.position + (Vector3.right * _inputs.Horizontal + Vector3.forward * _inputs.Vertical).normalized * (moveSpeed * Time.deltaTime);

            // Border clamp.
            if(useClamp)
            {
                Vector3 cameraPosition = _cam.transform.position;
                _wantedPos.x = Mathf.Clamp(_wantedPos.x, cameraPosition.x - xMax, cameraPosition.x + xMax);
                _wantedPos.z = Mathf.Clamp(_wantedPos.z, cameraPosition.z - zMax, cameraPosition.z + zMax);
            }
            
            transform.position = _wantedPos;    // Give player the pos.
        }
        
        
        /**
         * <summary>
         * Function to give Power Ups.
         * </summary>
         * <param name="coroutinePowerUp">The coroutine for the power up.</param>
         */
        public void ActivatePowerUp(IEnumerator coroutinePowerUp)
        {
            // Check if there's a already a power-up.
            if(_currentPowerUp != null)
                StopCoroutine(_currentPowerUp);
            
            _currentPowerUp = coroutinePowerUp;
            StartCoroutine(_currentPowerUp);
        }

        #endregion

        #region Drag Events

        /**
         * <summary>
         * Called by a BaseInputModule before a drag is started.
         * </summary>
         * <param name="eventData">Current event data.</param>
         */
        public void OnBeginDrag(PointerEventData eventData)
        {
            _lastData = eventData;
        }


        /**
         * <summary>
         * Called by a BaseInputModule when a drag is ended.
         * </summary>
         * <param name="eventData">Current event data.</param>
         */
        public void OnEndDrag(PointerEventData eventData)
        {
            _lastData = eventData;
        }


        /**
         * <summary>
         * Called by the EventSystem every time the pointer is moved during dragging.
         * </summary>
         * <param name="eventData">Current event data.</param>
         */
        public void OnDrag(PointerEventData eventData)
        {
            _lastData = eventData;
        }


        /**
         * <summary>
         * Evaluate current state and transition to pressed state.
         * </summary>
         * <param name="eventData">The EventData usually sent by the EventSystem.</param>
         */
        public void OnPointerDown(PointerEventData eventData)
        {
            _lastData = eventData;
            _isDragging = true;
        }


        /**
         * <summary>
         * Evaluate eventData and transition to appropriate state.
         * </summary>
         * <param name="eventData">The EventData usually sent by the EventSystem.</param>
         */
        public void OnPointerUp(PointerEventData eventData)
        {
            _lastData = eventData;
            _isDragging = false;
        }

        #endregion

    }
}
