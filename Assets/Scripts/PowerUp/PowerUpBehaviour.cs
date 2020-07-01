using UnityEngine;

namespace PowerUp
{
    /// <summary>
    /// This is an abstract base class for power up behaviours.
    /// Each power up behaviour needs to extend this class and override including methods.  
    /// </summary>
    public abstract class PowerUpBehaviour : MonoBehaviour
    {
        public GameObject tank;

        /// <summary>
        /// This method is initializer for PowerUpBehaviour instead of Start, Awake, OnEnable methods
        /// which is using for preventing coroutine problems when necessary
        /// </summary>
        public virtual void Init()
        {
        }

        /// <summary>
        /// This method user for activate PowerUp
        /// </summary>
        public virtual void Activate()
        {
        }

        /// <summary>
        /// This method user for deactivate PowerUp
        /// </summary>
        public virtual void Deactivate()
        {
        }
    }
}
