using System.Collections;
using UnityEngine;

namespace _Script.Stuff
{
    public class Despawn : MonoBehaviour
    {

        #region Built-In Methods

        /**
         * <summary>
         * Start is called before the first frame update.
         * </summary>
         */
        void Start()
        {
            StartCoroutine(DespawnItem());
        }

        #endregion

        #region Custom Methods

        /**
         * <summary>
         * Coroutine to despawn items after 30s.
         * </summary>
         */
        private IEnumerator DespawnItem()
        {
            for(int i = 0; i<30; i++)
            {
                if(i==20)
                    GetComponent<Animator>().SetBool($"Despawn", true);
                
                yield return new WaitForSeconds(1f);
            }
        
            Destroy(gameObject);
        }

        #endregion
    
    }
}
