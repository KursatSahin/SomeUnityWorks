using Complete;
using PowerUp;

namespace PowerUps
{
    public class DoubleSpeedBulletsBehaviour : PowerUpBehaviour
    {
        private TankShooting _tankShooting;

        private void Start()
        {
            _tankShooting = tank.GetComponent<TankShooting>();
        }

        public override void Activate()
        {
            _tankShooting.bulletSpeed = 30f;
        }

        public override void Deactivate()
        {
            _tankShooting.bulletSpeed = 15;
        }
    }
}
