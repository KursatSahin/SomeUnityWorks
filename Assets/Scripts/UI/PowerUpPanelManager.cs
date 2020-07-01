using System;
using System.Collections.Generic;
using Common;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class PowerUpPanelManager : MonoBehaviour, IEventManagerHandling
    {
        #region Private Variables

        private List<PowerUp.PowerUp> _powerUpsInUse;

        #endregion Private Variables

        #region PowerUp Panel UI Variables

        [SerializeField] private GameObject powerUpPanel;

        #endregion PowerUp Panel UI Variables

        #region Unity Events

        private void Awake()
        {
            _powerUpsInUse = new List<PowerUp.PowerUp>();
            AddHandlers();
        }

        private void OnDestroy()
        {
            RemoveHandlers();
        }

        #endregion Unity Events

        #region Method Definitions
        
        /// <summary>
        /// This method is not necessary for this demo
        /// </summary>
        /// <param name="data"></param>
        [Obsolete]
        private void OnPowerUpIsDeactivated(object data)
        {
            var powerUp = (PowerUp.PowerUp) data;
            _powerUpsInUse.Remove(powerUp);
        }
    
        /// <summary>
        /// This is a callback method for PowerUpIsActivated event 
        /// </summary>
        /// <param name="data">This is an generic parameter to pass</param>
        private void OnPowerUpIsActivated(object data)
        {
            var powerUp = (PowerUp.PowerUp) data;
            _powerUpsInUse.Add(powerUp);
            //powerUp.ActivatePowerUp();

            EventManager.GetInstance().Notify(Events.PowerUpInUseUpdated, _powerUpsInUse);
        
            if (_powerUpsInUse.Count > 2)
            {
                EventManager.GetInstance().Notify(Events.PowerUpLimitHasReached);
            }
        }

        /// <summary>
        /// This is a callback method for StartGame event
        /// </summary>
        /// <param name="_">Not necessary to use this parameter</param>
        private void ShowPanel(object _)
        {
            var powerUpPanelRect = powerUpPanel.GetComponent<RectTransform>();
            var targetPos = powerUpPanel.transform.localPosition;
            var beginPos = powerUpPanel.transform.localPosition -
                           new Vector3(0, powerUpPanelRect.sizeDelta.y, 0);

            powerUpPanelRect.DOAnchorPos(beginPos, 0f).OnStart(() =>
            {
                powerUpPanel.SetActive(true);
            }).OnComplete(() =>
            {
                powerUpPanelRect.DOAnchorPos(targetPos, 1);
            });
        }

        /// <summary>
        /// This is a callback method for [Events.NewCustomEvent] event
        /// </summary>
        /// <param name="_">Not necessary to use this parameter</param>
        private void HidePanel(object _)
        {
            var powerUpPanelRect = powerUpPanel.GetComponent<RectTransform>();
            var targetPos = powerUpPanel.transform.localPosition -
                            new Vector3(0, powerUpPanelRect.sizeDelta.y, 0);

            powerUpPanelRect.DOAnchorPos(targetPos, 1f).OnComplete(() =>
            {
                powerUpPanel.SetActive(true);
            });
        }

        /// <summary>
        /// This method adds all the necessary handlers to the EventManager at the same time if possible.
        /// </summary>
        public void AddHandlers()
        {
            EventManager.GetInstance().AddHandler(Events.PowerUpIsActivated, OnPowerUpIsActivated);
            // TODO : EventManager.GetInstance().AddHandler(Events.PowerUpIsDeactivated, OnPowerUpIsDeactivated);
            EventManager.GetInstance().AddHandler(Events.StartGame,ShowPanel);
        }

        /// <summary>
        /// This method removes all handlers added by itself from EventManager at the same time if possible. 
        /// </summary>
        public void RemoveHandlers()
        {
            EventManager.GetInstance().RemoveHandler(Events.PowerUpIsActivated, OnPowerUpIsActivated);
            // TODO : EventManager.GetInstance().RemoveHandler(Events.PowerUpIsDeactivated, OnPowerUpIsDeactivated);
            EventManager.GetInstance().RemoveHandler(Events.StartGame,ShowPanel);
        }
        
        #endregion Method Definitions
    }
}
