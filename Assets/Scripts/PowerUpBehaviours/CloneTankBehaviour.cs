using System.Linq;
using Common;
using Complete;
using PowerUp;
using Tank;
using UnityEngine;

namespace PowerUps
{
    public class CloneTankBehaviour : PowerUpBehaviour
    {
        private GameObject clone;
        
        public override void Activate()
        {
            CloneTank();
        }

        public override void Deactivate()
        {
            clone.SetActive(false);
        }

        private void CloneTank()
        {
            clone = ObjectPooler.SharedInstance.GetPooledObject(ObjectPooler.PoolingObjectTags.CompleteTankPrefabTag);
            var clonedTankMovement = clone.GetComponent<TankMovement>();
            var clonedTankShooting = clone.GetComponent<TankShooting>();
            var clonedTankPowerUpManager = clone.GetComponent<TankPowerUpManager>();
            
            clonedTankMovement = tank.GetComponent<TankMovement>();
            clonedTankShooting = tank.GetComponent<TankShooting>();

            clone.transform.parent = tank.transform.parent;
            clone.transform.position = tank.transform.position + new Vector3(0, 3, 0);

            clone.SetActive(true);

            var originalPowerUps = tank.GetComponent<TankPowerUpManager>().ownedPowerUps.Keys.ToList();

            var clonePowerUp = originalPowerUps.Find(item => item.type == PowerUpType.CloneTank);

            if (clonePowerUp != null)
            {
                originalPowerUps.Remove(clonePowerUp);
            }

            clonedTankPowerUpManager.OnPowerUpInUseUpdated(originalPowerUps);
        }
    }
}