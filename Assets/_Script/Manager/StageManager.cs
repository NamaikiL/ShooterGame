using UnityEngine;

namespace _Script.Manager
{
    public class StageManager : MonoBehaviour
    {
        
        #region Variables

        [Header("Stage Parameters")]
        [SerializeField] private Transform nextStageSpawn;
        [SerializeField] private GameObject[] stages;
        
        #endregion

        #region Built-In Methods

        /**
         * <summary>
         * Start is called before the first frame update.
         * </summary>
         */
        void Start()
        {
            FirstStageSpawn();
        }
        
        /**
         * <summary>
         * When a GameObject collides with another GameObject, Unity calls OnTriggerEnter.
         * </summary>
         * <param name="other">The other Collider involved in this collision.</param>
         */
        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Stage"))
                Destroy(other.gameObject);
        }

        #endregion

        #region Stage Generation

        /**
         * <summary>
         * Function to spawn the first stage.
         * </summary>
         */
        void FirstStageSpawn()
        {
            Instantiate(
                stages[Random.Range(0, stages.Length)], 
                nextStageSpawn.position, 
                Quaternion.identity
                );
        }

        #endregion
        
    }
}
