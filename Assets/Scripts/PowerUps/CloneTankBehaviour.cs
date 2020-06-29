using Complete;
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
            
            clonedTankMovement = tank.GetComponent<TankMovement>();
            clonedTankShooting = tank.GetComponent<TankShooting>();

            clone.transform.parent = tank.transform.parent;
            clone.transform.position = tank.transform.position + new Vector3(0, 3, 0);

            clone.SetActive(true);
        }
    }
}