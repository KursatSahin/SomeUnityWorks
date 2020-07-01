using Complete;
using PowerUp;

namespace PowerUpBehaviours
{
    public class DoubleBulletsBehaviour : PowerUpBehaviour
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
            // Set double fire status is true
            _tankShooting.doubleFireIsActivated = true;
        }

        /// <summary>
        /// This method user for deactivate PowerUp
        /// </summary>
        public override void Deactivate()
        {
            // Set double fire status is false
            _tankShooting.doubleFireIsActivated = false;
        }
    }
}