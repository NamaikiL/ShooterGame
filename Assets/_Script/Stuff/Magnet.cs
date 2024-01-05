using UnityEngine;

namespace _Script.Stuff
{
    public class Magnet : MonoBehaviour
    {

        #region Variables

        [Header("Magnet Parameters")]
        [SerializeField] private float attractionForce;
        [SerializeField] private float range = 5;
        [SerializeField] private LayerMask layerToAttract;

        private readonly Collider[] _colsToAttract = new Collider[300];
        
        #endregion

        #region Built-In Methods

        /**
         * <summary>
         * FixedUpdate is called once per fixed frame.
         * </summary>
         */
        void FixedUpdate()
        {
            Attract();
        }

        #endregion

        #region Custom Methods

        /**
         * <summary>
         * Function that attract objects by defined layer and distance.
         * </summary>
         */
        void Attract(){

            // Getting the objects in the array.
            Physics.OverlapSphereNonAlloc(transform.position, range, _colsToAttract, layerToAttract);

            // If null, return.
            if(_colsToAttract == null) return;

            // Only attract coins.
            foreach (Collider objectsToAttract in _colsToAttract)
            {
                if(objectsToAttract == null) continue;
                
                if(objectsToAttract.CompareTag("Coin"))
                {
                    Vector3 magnetPosition = transform.position;
                    Vector3 coinPosition = objectsToAttract.transform.position;
                    float distance = Vector3.Distance(coinPosition, magnetPosition);
                    distance = Mathf.Clamp(distance, 0, range);

                    Vector3 force = ((magnetPosition - coinPosition).normalized * (range - distance) / range) * attractionForce;

                    objectsToAttract.GetComponent<Rigidbody>().AddForce(force);
                }
            }
        }

        #endregion
        
    }
}
