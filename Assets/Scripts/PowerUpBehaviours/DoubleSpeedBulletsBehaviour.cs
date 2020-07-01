using Complete;
using PowerUp;

namespace PowerUpBehaviours
{
    public class DoubleSpeedBulletsBehaviour : PowerUpBehaviour
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
            // Set the bullet speed to 2 times the default value
            _tankShooting.bulletSpeed *= 1.5f;
        }

        /// <summary>
        /// This method user for deactivate PowerUp
        /// </summary>
        public override void Deactivate()
        {
            // Set the bullet speed to the default value
            _tankShooting.bulletSpeed /= 1.5f;
        }
    }
}
