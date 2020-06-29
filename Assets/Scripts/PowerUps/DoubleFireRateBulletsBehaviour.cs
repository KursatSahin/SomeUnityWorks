using Complete;

namespace PowerUps
{
    public class DoubleFireRateBulletsBehaviour : PowerUpBehaviour
    {
        private TankShooting _tankShooting;

        private void Start()
        {
            _tankShooting = tank.GetComponent<TankShooting>();
        }

        public override void Activate()
        {
            _tankShooting.firingRate = 1f;
        }

        public override void Deactivate()
        {
            _tankShooting.firingRate = 2f;
        }
    }
}