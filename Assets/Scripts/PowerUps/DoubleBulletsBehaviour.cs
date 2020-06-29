using Complete;

namespace PowerUps
{
    public class DoubleBulletsBehaviour : PowerUpBehaviour
    {
        private TankShooting _tankShooting;
        
        private void Start()
        {
            _tankShooting = tank.GetComponent<TankShooting>();
        }

        public override void Activate()
        {
            _tankShooting.doubleFireIsActivated = true;
        }

        public override void Deactivate()
        {
            _tankShooting.doubleFireIsActivated = false;
        }
    }
}