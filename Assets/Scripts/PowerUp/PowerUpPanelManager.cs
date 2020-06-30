using System;
using System.Collections.Generic;
using Common;
using DG.Tweening;
using UnityEngine;

namespace PowerUp
{
    public class PowerUpPanelManager : MonoBehaviour
    {
        #region Private Variables

        private List<PowerUp> _powerUpsInUse = new List<PowerUp>();

        #endregion Private Variables

        #region PowerUp Panel UI Variables

        [SerializeField] private GameObject powerUpPanel;

        #endregion PowerUp Panel UI Variables

        #region Unity Events

        private void Awake()
        {
            AddHandlers();
        }

        private void OnDestroy()
        {
            RemoveHandlers();
        }

        #endregion Unity Events
        

        /// <summary>
        /// This method is not necessary for this demo
        /// </summary>
        /// <param name="data"></param>
        [Obsolete]
        private void OnPowerUpIsDeactivated(object data)
        {
            var powerUp = (PowerUp) data;
            _powerUpsInUse.Remove(powerUp);
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private void OnPowerUpIsActivated(object data)
        {
            var powerUp = (PowerUp) data;
            _powerUpsInUse.Add(powerUp);
            //powerUp.ActivatePowerUp();

            EventManager.GetInstance().Notify(Events.PowerUpInUseUpdated, _powerUpsInUse);
        
            if (_powerUpsInUse.Count > 2)
            {
                EventManager.GetInstance().Notify(Events.PowerUpLimitHasReached);
            }
        }

        private void ShowPanel(object _)
        {
            powerUpPanel.GetComponent<CanvasGroup>().DOFade(1, 1);
        }

        private void HidePanel()
        {
            powerUpPanel.GetComponent<CanvasGroup>().DOFade(0, 1);
        }

        private void AddHandlers()
        {
            EventManager.GetInstance().AddHandler(Events.PowerUpIsActivated, OnPowerUpIsActivated);
            //EventManager.GetInstance().AddHandler(Events.PowerUpIsDeactivated, OnPowerUpIsDeactivated);
            EventManager.GetInstance().AddHandler(Events.StartGame,ShowPanel);
        }

        private void RemoveHandlers()
        {
            EventManager.GetInstance().RemoveHandler(Events.PowerUpIsActivated, OnPowerUpIsActivated);
            //EventManager.GetInstance().RemoveHandler(Events.PowerUpIsDeactivated, OnPowerUpIsDeactivated);
            EventManager.GetInstance().RemoveHandler(Events.StartGame,ShowPanel);
        }
    }
}
