using Complete;
using PowerUp;

namespace PowerUpBehaviours
{
    public class DoubleFireRateBulletsBehaviour : PowerUpBehaviour
    {
        private TankShooting _tankShooting;

        /// <summary>
        /// This method is initializer for PowerUpBehaviour instead of Start, Awake, OnEnable methods
        /// which is using for preventing coroutine problems when necessary
        /// </summary>
        public override void Init()
        {
            // Get TankShooting component
            _tankShooting = tank.GetComponent<TankShooting>();
        }

        /// <summary>
        /// This method user for activate PowerUp
        /// </summary>
        public override void Activate()
        {
            // Set firing rate to 1 sec
            _tankShooting.firingRate = 1f;
        }

        /// <summary>
        /// This method user for deactivate PowerUp
        /// </summary>
        public override void Deactivate()
        {
            // Set firing rate to 2 sec
            _tankShooting.firingRate = 2f;
        }
    }
}