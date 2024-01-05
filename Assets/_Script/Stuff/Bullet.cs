using _Script.CharacterBehavior;
using UnityEngine;

namespace _Script.Stuff
{
    public enum BulletType{
        EnemyHit,
        PlayerHit
    }
    
    public class Bullet : MonoBehaviour
    {

        #region Variables

        [Header("Bullet Parameters")] 
        [SerializeField] private int bulletDamages;
        [SerializeField] private BulletType _bulletType;

        #endregion

        #region Properties

        public int BulletDamages
        {
            set => bulletDamages = value;
        }

        public BulletType ActualBulletType
        {
            set => _bulletType = value;
        }

        #endregion

        #region Built-In Methods
        
        /**
         * <summary>
         * When a GameObject collides with another GameObject, Unity calls OnTriggerEnter.
         * </summary>
         * <param name="other">The other Collider involved in this collision.</param>
         */
        void OnTriggerEnter(Collider other)
        {
            // Player Behavior.
            if(other.gameObject.layer == LayerMask.NameToLayer("Player") 
               && _bulletType == BulletType.PlayerHit)
            {
                other.gameObject.GetComponent<HealthController>().Hurt(bulletDamages);
                Destroy(gameObject);
            }

            // Enemy Behavior.
            if(other.gameObject.layer == LayerMask.NameToLayer("Enemy") 
               && _bulletType == BulletType.EnemyHit)
            {
                other.gameObject.GetComponent<HealthController>().Hurt(bulletDamages);
                Destroy(gameObject);
            }

            // Boss Behavior(Shield mode).
            if(other.gameObject.layer == LayerMask.NameToLayer("Shield") 
               && other.gameObject.CompareTag("Boss") 
               && _bulletType == BulletType.EnemyHit)
            {
                other.gameObject.GetComponent<HealthController>().Hurt(bulletDamages);
                Destroy(gameObject);
            }
        
            // Player Behavior(Shield mode).
            if(other.gameObject.layer == LayerMask.NameToLayer("Shield") 
               && other.gameObject.CompareTag("Player") 
               && _bulletType == BulletType.PlayerHit)
            {
                Destroy(gameObject);
            }
        }

        #endregion

    }
}
