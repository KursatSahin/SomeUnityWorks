using Complete;
using PowerUp;

namespace PowerUps
{
    public class DoubleSpeedBulletsBehaviour : PowerUpBehaviour
    {
        private TankShooting _tankShooting;

        public override void Init()
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
