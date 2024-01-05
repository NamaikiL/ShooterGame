using System.Collections;
using System.Collections.Generic;
using _Script.CharacterBehavior;
using UnityEngine;

namespace _Script.Manager
{
    public class WaveManager : MonoBehaviour
    {

        #region Variables

        [Header("Enemies Behavior")] 
        [SerializeField] private int nbEnemySpawnMin;
        [SerializeField] private int nbEnemySpawnMax;
        [SerializeField] private float timeBetweenSpawn = 5f;
        [SerializeField] private GameObject[] enemies;
        
        [Header("Phases Behavior")]
        [SerializeField] private int nbPhasePerWaves;
        [SerializeField] private int minAddPhasePerWaves;
        [SerializeField] private int maxAddPhasePerWaves;
        [SerializeField] private float timeBetweenPhase = 5f;
        
        [Header("Waves Behavior")]
        [SerializeField] private int wavesNeededToWin;
        [SerializeField] private float timeBetweenWaves = 20f;

        [Header("Spawn Positions")] 
        [SerializeField] private List<Transform> spawnPositionsLeft;
        [SerializeField] private List<Transform> spawnPositionsRight;
        
        // Wave Variables.
        private IEnumerator _waveCoroutine;
        private int _currentPhase, _phaseInitial, _currentWave = 1;
        
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
            
            _phaseInitial = nbPhasePerWaves;
            _currentPhase = nbPhasePerWaves;
            
            Waves();
        }

        #endregion

        #region Custom Methods

        /**
         * <summary>
         * Function to generate waves.
         * </summary>
         */
        void Waves()
        {
            Vector3 left = spawnPositionsLeft[Random.Range(0, spawnPositionsLeft.Count)].position;
            Vector3 right = spawnPositionsRight[Random.Range(0, spawnPositionsRight.Count)].position;
            
            _waveCoroutine = Random.Range(0, 2) == 0 
                ? SpawnEnemyAndWavesController(left, right) 
                : SpawnEnemyAndWavesController(right, left);
        
            StartCoroutine(_waveCoroutine);
        }


        /**
         * <summary>
         * Coroutine to spawn enemies and handle the waves and the phases.
         * Waves -> Phase -> Spawn enemy.
         * </summary>
         */
        private IEnumerator SpawnEnemyAndWavesController(Vector3 origin, Vector3 destination)
        {
            // Enemies Spawn.
            for(int i = 0; i < Random.Range(nbEnemySpawnMin, nbEnemySpawnMax); i++)
            {
                GameObject spawnedEnemy = Instantiate(
                    enemies[Random.Range(0, enemies.Length)], 
                    origin, 
                    Quaternion.identity
                    );
                spawnedEnemy.GetComponent<EnemyController>().Destination = destination;
                yield return new WaitForSeconds(timeBetweenSpawn);
            }
            
            // Check if there's any enemies left.
            while(GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
                yield return null;

            // Check for the infinite wave behavior and if there's no phase left.
            if(_currentPhase == 0 && wavesNeededToWin != -1)
            {
                // Check if the waves are infinite or not.
                if(_currentWave == wavesNeededToWin)
                {
                    GameObject.Find("LevelManager").GetComponent<LevelManager>().FinishLevel();
                    yield return null;
                }
                else
                { 
                    // If there's still enemies left.
                    while(GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
                        yield return null;

                    for(float x = timeBetweenWaves; x > 0; x--)
                    {
                        _uiManager.UpdateWaves(x, _currentWave);
                        yield return new WaitForSeconds(1f);
                    }

                    _currentWave++;
                    
                    _phaseInitial += Random.Range(minAddPhasePerWaves, maxAddPhasePerWaves);
                    _currentPhase = _phaseInitial;
                
                    _uiManager.UpdateWaves(_currentWave);

                    Waves();
                }

            }
            else
            {
                yield return new WaitForSeconds(timeBetweenPhase);

                _currentPhase--;
                Waves();
            }  
        }

        #endregion

    }
}
