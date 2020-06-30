using System;
using System.Collections.Generic;
using Common;
using Complete;
using PowerUp;
using PowerUps;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tank
{
    [RequireComponent(typeof(TankShooting))]
    public class TankPowerUpManager : MonoBehaviour
    {
        public Dictionary<PowerUp.PowerUp, PowerUpBehaviour> ownedPowerUps =
            new Dictionary<PowerUp.PowerUp, PowerUpBehaviour>();
        private TankShooting _tankShooting;

        private void Awake()
        {
            EventManager.GetInstance().AddHandler(Events.PowerUpInUseUpdated, OnPowerUpInUseUpdated);
            _tankShooting = gameObject.GetComponent<TankShooting>();
        }

        private void OnDestroy()
        {
            EventManager.GetInstance().RemoveHandler(Events.PowerUpInUseUpdated, OnPowerUpInUseUpdated);

            foreach (var dictItem in ownedPowerUps)
            {
                dictItem.Key.activateAction.RemoveListener(dictItem.Value.Activate);
                dictItem.Key.activateAction.RemoveListener(dictItem.Value.Deactivate);
            }
        }

        public void OnPowerUpInUseUpdated(object data)
        {
            if (!isActiveAndEnabled)
            {
                return;
            }
            
            // Get updated list
            var updatedPowerupList = data as List<PowerUp.PowerUp>;

            // Add new Power ups to ownedPowerUps List
            foreach (var listItem in updatedPowerupList)
            {
                if (!ownedPowerUps.ContainsKey(listItem))
                {
                    var behaviour = AddPowerUpBehaviourComponent(listItem.type);
                 
                    ownedPowerUps.Add(listItem,behaviour);
                    
                    listItem.activateAction.AddListener(behaviour.Activate);
                    listItem.deactivateAction.AddListener(behaviour.Deactivate);
                }
            }
            
            // First stop fire and power ups for prevent causing coroutines sync problems
            _tankShooting.DeactivateFire();
            
            foreach (var powerUp in ownedPowerUps)
            {
                // Clone tank is special case for this situation 
                if (powerUp.Key.type == PowerUpType.CloneTank)
                {
                    continue;
                }

                if (powerUp.Key.isActivated)
                {
                    powerUp.Key.DeactivatePowerUp();
                }
            }
            
            // Then reactivate fire method and powerups
            _tankShooting.ActivateFire();
            
            foreach (var powerUp in ownedPowerUps)
            {
                if (!powerUp.Key.isActivated)
                {
                    powerUp.Key.ActivatePowerUp();
                }
            }
        }
        
        private PowerUpBehaviour AddPowerUpBehaviourComponent(PowerUpType type)
        {
            PowerUpBehaviour tempBehaviour;
            
            switch (type)
            {
                case PowerUpType.DoubleBullets:
                    tempBehaviour = gameObject.AddComponent<DoubleBulletsBehaviour>();
                    break;
                case PowerUpType.SideFire:
                    tempBehaviour = gameObject.AddComponent<SideFireBehaviour>();
                    break;
                case PowerUpType.DoubleSpeedBullets:
                    tempBehaviour = gameObject.AddComponent<DoubleSpeedBulletsBehaviour>();
                    break;
                case PowerUpType.DoubleFireRateBullets:
                    tempBehaviour = gameObject.AddComponent<DoubleFireRateBulletsBehaviour>();
                    break;
                case PowerUpType.CloneTank:
                    tempBehaviour = gameObject.AddComponent<CloneTankBehaviour>();
                    break;
                default:
                    tempBehaviour = gameObject.AddComponent<DoubleBulletsBehaviour>();
                    break;
            }

            tempBehaviour.tank = gameObject;
            tempBehaviour.Init();

            return tempBehaviour;
        }
    }
}