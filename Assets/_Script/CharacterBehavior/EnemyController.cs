using System.Collections;
using UnityEngine;

namespace _Script.CharacterBehavior
{
    public class EnemyController : MonoBehaviour
    {
        #region Variables

        [Header("Enemy Parameters")]
        [SerializeField] private float moveSpeed = 100f;
        [SerializeField] private Vector3 destination;

        #endregion

        #region Properties

        public Vector3 Destination
        {
            set => destination = value;
        }

        #endregion

        #region Built-In Methods

        /**
         * <summary>
         * Start is called before the first frame update.
         * </summary>
         */
        void Start()
        {
            StartCoroutine(GoToDestination());
        }

        #endregion

        #region Enemy Behavior

        /**
         * <summary>
         * Function for the enemy movements.
         * </summary>
         */
        private IEnumerator GoToDestination()
        {
            while(true)
            {
                if(GameObject.Find("Player"))
                    transform.LookAt(GameObject.Find("Player").transform.position, Vector3.up); // The enemy look at the player.

                transform.position = Vector3.MoveTowards(
                    transform.position, 
                    destination, 
                    Time.deltaTime * moveSpeed
                    );

                // If the distance between the enemy and the destination is inferior of .5f, destroy the enemy.
                if(Vector3.Distance(transform.position, destination) < 0.5f)
                {
                    Destroy(gameObject);
                }
                yield return new WaitForEndOfFrame();
            }
        }

        #endregion


    }
}
