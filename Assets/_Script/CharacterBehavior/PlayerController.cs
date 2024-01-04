using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    
    // Variables Global Public.
    public float moveSpeed = 5;
    public float xMax, zMax;
    public float dragOffsetZ;
    public float dragLerpMultiplier;
    public bool useClamp = true;

    public IEnumerator _currentPowerUp;

    // Variable Global Privé.
    private bool _isDragging = false;

    private Camera _cam;
    private PointerEventData _lastData;
    private Vector3 _wantedPos;

    private AudioSource _shield, _health;
    private GameManager _gameManager;
    private PlayerInputs _inputs;


    /*
    Start is called before the first frame update.
    Retourne rien.
    */
    void Start(){

        _inputs = GetComponent<PlayerInputs>(); // Appelle le script PlayerInputs de l'objet.
        _shield = GameObject.Find("Shield").GetComponent<AudioSource>();
        _health = GameObject.Find("Energy").GetComponent<AudioSource>();

        _gameManager = GameManager.instance; // Appelle l'instance du UIManager.
        _cam = Camera.main; // Appelle la main camera.

    }


    /*
    Update is called once per frame.
    Retourne rien.
    */
    void Update(){

        Move(); // Appelle la fonction Move() à chaque frame.

        if(_inputs.changeWeapons && GetComponent<PlayerShoot>().bullet && GetComponent<PlayerShoot>().rocket)
            GetComponent<PlayerShoot>().ChangeWeapon();
        
    }


    /*
    Fonction pour les interactions entre le Player et les GameObjects pouvant être trigger.
    Retourne rien.
    */
    void OnTriggerEnter(Collider other){

        // Si le trigger toucher a comme nom de layer "Collectables".
        if(other.gameObject.layer == LayerMask.NameToLayer("Collectables")){
            // Switch() qui agit selon le tag de l'objet ramassé.
            switch(other.gameObject.tag){
                case "Coin":
                    _gameManager.AddCoin(); // Ajoute un coin au compteur.
                    Destroy(other.gameObject);
                    break;
                case "Damage":
                    ActivatePowerUp(GetComponent<PlayerShoot>().DamagePlus());
                    Destroy(other.gameObject);
                    break;
                case "FireRate":
                    ActivatePowerUp(GetComponent<PlayerShoot>().FireRatePlus());
                    Destroy(other.gameObject);
                    break;
                case "Shield":
                    _shield.Play();
                    ActivatePowerUp(GetComponent<PlayerShoot>().ShieldUp());
                    Destroy(other.gameObject);
                    break;
                case "Nuke":
                    GetComponent<PlayerShoot>().Nuke();
                    Destroy(other.gameObject);
                    break;
                case "Heal":
                    _health.Play();
                    GetComponent<HealthController>().Regenerate(15);
                    Destroy(other.gameObject);
                    break;
                default:
                    Debug.Log("Erreur: Il manque un cas pour cet objet.");
                    break;
            }
            
        }

    }


    /*
    Fonction pour les déplacements du joueur.
    Retourne rien.
    */
    void Move(){
        
        _wantedPos = transform.position; // Applique la position du Player à la variable.

        // Si le joueur utilise la souris/le téléphone portable alors déplace le Player la ou il clique/appuie.
        if(_lastData != null && _isDragging){
            RaycastHit hit;
            
            if(Physics.Raycast(_cam.ScreenPointToRay(_lastData.position), out hit, 300, 1<<31)){
                _wantedPos = Vector3.Lerp(transform.position, hit.point + Vector3.forward * dragOffsetZ, Time.deltaTime * dragLerpMultiplier);
            }
        }
        // Sinon utilise les touches du clavier.
        else{
            _wantedPos = transform.position + (Vector3.right * _inputs.horizontal + Vector3.forward * _inputs.vertical).normalized * moveSpeed * Time.deltaTime;
        }

        // Si le joueur est sur le bord de l'écran.
        if(useClamp){
            _wantedPos.x = Mathf.Clamp(_wantedPos.x, _cam.transform.position.x - xMax, _cam.transform.position.x + xMax);
            _wantedPos.z = Mathf.Clamp(_wantedPos.z, _cam.transform.position.z - zMax, _cam.transform.position.z + zMax);
        }

        transform.position = _wantedPos; // Donne les positions au Player.

    }


    /*
    Fonction pour les appliquer les powerups.
    Retourne rien.
    */
    public void ActivatePowerUp(IEnumerator coroutinePowerUp){

        // Si un Powerup est déjà actif, alors on l'enlève.
        if(_currentPowerUp != null)
            StopCoroutine(_currentPowerUp);
        
        
        _currentPowerUp = coroutinePowerUp; // Applique le PowerUp voulu.
        StartCoroutine(_currentPowerUp);

    }


    // Drag/Déplacement tactile.


    // Liste de Fonction Drag() pour permettre au Player de se déplacer à l'aide du clique de la souris ou sur écran de Téléphone.
    public void OnBeginDrag(PointerEventData eventData){

        _lastData = eventData;

    }


    public void OnEndDrag(PointerEventData eventData){

        _lastData = eventData;

    }


    public void OnDrag(PointerEventData eventData){

        _lastData = eventData;

    }


    public void OnPointerDown(PointerEventData eventData){

        _lastData = eventData;
        _isDragging = true;

    }


    public void OnPointerUp(PointerEventData eventData){

        _lastData = eventData;
        _isDragging = false;

    }

}
