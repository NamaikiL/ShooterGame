using System.Collections;
using UnityEngine;

namespace _Script.Stuff
{
    public class StageGenerator : MonoBehaviour
    {

        #region Variables

        [Header("Stage Parameters")]
        [SerializeField] private Transform nextStageSpawn;
        [SerializeField] private GameObject[] stages;
        
        // Stage Variables.
        private readonly float _speed = 100f;
        private bool _isTriggered;
        
        #endregion

        #region Built-In Methods

        /**
         * <summary>
         * Start is called before the first frame update.
         * </summary>
         */
        void Start()
        {
            StartCoroutine(MovingStages());
        }
        
        
        /**
         * <summary>
         * When a GameObject collides with another GameObject, Unity calls OnTriggerEnter.
         * </summary>
         * <param name="other">The other Collider involved in this collision.</param>
         */
        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Player") && !_isTriggered)
            {
                Instantiate(
                    stages[Random.Range(0, stages.Length)], 
                    nextStageSpawn.position, 
                    Quaternion.identity
                    );
                _isTriggered = true;
            }
        }

        #endregion

        #region Custom Methods

        /**
         * <summary>
         * Coroutine that make the stages translate.
         * </summary>
         */
        private IEnumerator MovingStages()
        {
            while(true)
            {
                transform.Translate(Vector3.back * (_speed * Time.deltaTime), Space.World);

                yield return new WaitForEndOfFrame();
            }
        }

        #endregion
        
    }
}
