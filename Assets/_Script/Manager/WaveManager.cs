using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{

    // Variables Global Public.
    public int nbEnemySpawnMin, nbEnemySpawnMax, nbPhasePerWaves, minAddPhasePerWaves, maxAddPhasePerWaves, wavesNeededToWin;
    public float timeBetweenSpawn = 5f, timeBetweenPhase = 5f, timeBetweenWaves = 20f;
    public IEnumerator waveCoroutine;
    public List<Transform> spawnPositionsLeft = new List<Transform>();
    public List<Transform> spawnPositionsRight = new List<Transform>();

    public GameObject[] enemies;

    // Variables Global Privé.
    private int _currentPhase, _phaseInitial, _currentWave = 1;

    private UIManager _uimanager;


    /* 
    Start is called before the first frame update.
    Retourne rien.
    */
    void Start(){

        _phaseInitial = nbPhasePerWaves;
        _currentPhase = nbPhasePerWaves;

        _uimanager = UIManager.instance;

        Waves(); // Appelle la fonction Waves() une première fois.

    }


    /*
    Fonction pour générer des waves en donnant les paramètres.
    Retourne rien.
    */
    void Waves(){

        // Donné un spawn/une destination random dans les listes donnés pour les enemy.
        Vector3 gauche = spawnPositionsLeft[Random.Range(0, spawnPositionsLeft.Count)].position;
        Vector3 droite = spawnPositionsRight[Random.Range(0, spawnPositionsRight.Count)].position;

        // Génère un nombre random pour choisir de quelle liste vont partir les enemy.
        if(Random.Range(0, 2) == 0){

            waveCoroutine = SpawnEnemyAndWavesController(gauche, droite);

        }else{

            waveCoroutine = SpawnEnemyAndWavesController(droite, gauche);

        }
        
        StartCoroutine(waveCoroutine);

    }


    /*
    Coroutine qui s'occupe du spawn des ennemis et de la gestion des waves. Waves -> Phase -> Spawn enemy.
    Retourne rien.
    */
    IEnumerator SpawnEnemyAndWavesController(Vector3 origin, Vector3 destination){

        // Génère un nombre d'Enemy aléatoire dans une range donné par le MJ.
        for(int i = 0; i < Random.Range(nbEnemySpawnMin, nbEnemySpawnMax); i++){
                GameObject spawnedEnemy = Instantiate(enemies[Random.Range(0, enemies.Length)], origin, Quaternion.identity);
                spawnedEnemy.GetComponent<EnemyController>().destination = destination;
                yield return new WaitForSeconds(timeBetweenSpawn);
        }

        // Vérifie si tout les Enemy sont morts sur la map.
        while(GameObject.FindGameObjectsWithTag("Enemy").Length > 0){
            yield return null;
        }

        // Si le nombre de phase est égale à 0, et que le nombre de wave pour gagner est différent de -1(Utilisé pour la wave infini).
        if(_currentPhase == 0 && wavesNeededToWin != -1){

            // Si le nombre de wave est égale au nombre de waves demandés pour gagner, alors fini le niveau.
            if(_currentWave == wavesNeededToWin){
                GameObject.Find("LevelManager").GetComponent<LevelManager>().FinishLevel();
                yield return null;
            }else{ // Sinon recréer une wave après un certains nombre de secondes.
                while(GameObject.FindGameObjectsWithTag("Enemy").Length > 0){
                    yield return null;
                }

                for(float x = timeBetweenWaves; x > 0; x--){
                    _uimanager.UpdateWaves(x, _currentWave);
                    yield return new WaitForSeconds(1f);
                }

                _currentWave++;

                // Créer un nombre de phase supérieur à la précédente.
                _phaseInitial += Random.Range(minAddPhasePerWaves, maxAddPhasePerWaves);
                _currentPhase = _phaseInitial;
                
                _uimanager.UpdateWaves(_currentWave);

                Waves();
            }

        }else{
            // Sinon passe à la phase prochaine.
            yield return new WaitForSeconds(timeBetweenPhase);

            _currentPhase--;
            Waves();
        }  

    }

}
