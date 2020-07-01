using System;
using System.Collections.Generic;
using Common;
using Complete;
using PowerUp;
using PowerUpBehaviours;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tank
{
    [RequireComponent(typeof(TankShooting))]
    public class TankPowerUpManager : MonoBehaviour, IEventManagerHandling
    {
        #region Data Variables for TankPowerUpManager

        public Dictionary<PowerUp.PowerUp, PowerUpBehaviour> ownedPowerUps =
            new Dictionary<PowerUp.PowerUp, PowerUpBehaviour>();
        
        private TankShooting _tankShooting;

        #endregion Data Variables for TankPowerUpManager

        #region Unity Events

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

        #endregion Unity Events

        #region Method Definitions

        // This method is callback for PowerUpInUseUpdated event
        public void OnPowerUpInUseUpdated(object data)
        {
            if (!isActiveAndEnabled)
            {
                return;
            }
            
            // Get updated list
            var updatedPowerupList = data as List<PowerUp.PowerUp>;

            // Check list status
            if (updatedPowerupList == null || updatedPowerupList.Count < 1)
            {
                return;
            }
            
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
        
        /// <summary>
        /// This method adds power up behaviour component to tank due to powerup type
        /// then return added component
        /// </summary>
        /// <param name="type">PowerUpType parameter</param>
        /// <returns>Added PowerUpBehaaviour component</returns>
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

        #endregion Method Definitions

        public void AddHandlers()
        {
            throw new NotImplementedException();
        }

        public void RemoveHandlers()
        {
            throw new NotImplementedException();
        }
    }
}