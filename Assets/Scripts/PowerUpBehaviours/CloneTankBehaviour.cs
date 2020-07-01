using System.Linq;
using Common;
using PowerUp;
using Tank;
using UnityEngine;

namespace PowerUpBehaviours
{
    public class CloneTankBehaviour : PowerUpBehaviour
    {
        private GameObject _clonedTank;
        
        /// <summary>
        /// This method user for activate PowerUp
        /// </summary>
        public override void Activate()
        {
            CloneTank();
        }

        /// <summary>
        /// This method user for deactivate PowerUp
        /// </summary>
        public override void Deactivate()
        {
            _clonedTank.SetActive(false);
        }

        /// <summary>
        /// This method duplicate our Tank with all attributes.
        /// </summary>
        private void CloneTank()
        {
            // Get instantiated tank from ObjectPooler
            _clonedTank = ObjectPooler.SharedInstance.GetPooledObject(PoolingObjectTags.CompleteTankPrefabTag);
            
            // Get necessary custom components of original tank
            var clonedTankPowerUpManager = _clonedTank.GetComponent<TankPowerUpManager>();

            // Set necessary transform datas to clonedTank from original tank  
            _clonedTank.transform.parent = tank.transform.parent;
            _clonedTank.transform.position = tank.transform.position + new Vector3(3, 0, 0);

            // clonedTank is activated after all assignments are completed
            _clonedTank.SetActive(true);

            // Get all power ups from original tank as a list  
            var originalPowerUps = tank.GetComponent<TankPowerUpManager>().ownedPowerUps.Keys.ToList();

            // Remove "CloneTank" powerup if the list contains to prevent generating infinite clones 
            var clonePowerUp = originalPowerUps.Find(item => item.type == PowerUpType.CloneTank);
            
            if (clonePowerUp != null)
            {
                originalPowerUps.Remove(clonePowerUp);
            }

            // Set powerup list to cloned tank
            clonedTankPowerUpManager.OnPowerUpInUseUpdated(originalPowerUps);
        }
    }
}