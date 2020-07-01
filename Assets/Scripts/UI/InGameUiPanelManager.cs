using System;
using Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class InGameUiPanelManager : MonoBehaviour, IEventManagerHandling
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private GameObject hudCanvas;

        #region Unity Events

        private void Awake()
        {
            restartButton.onClick.AddListener(RestartGame);
            AddHandlers();
        }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveAllListeners();
            RemoveHandlers();
        }

        #endregion Unity Events

        #region Method Definitions
        
        /// <summary>
        /// This method is callback for RestartButton onclick listener.
        /// Reload game scene for simplify clearing datas
        /// </summary>
        private void RestartGame()
        {
            restartButton.gameObject.SetActive(false);
            EventManager.GetInstance().Notify(Events.RestartGame);
            SceneManager.LoadSceneAsync(0);
        }

        /// <summary>
        /// This method is callback for StartGame event
        /// </summary>
        /// <param name="_">Not necessary to use this parameter</param>
        private void ShowCanvas(object _)
        {
            hudCanvas.GetComponent<CanvasGroup>().DOFade(1,1).OnStart(() =>
            {
                hudCanvas.SetActive(true);
            });
        }
        
        /// <summary>
        /// This is a callback method for [Events.NewCustomEvent] event
        /// </summary>
        /// <param name="_">Not necessary to use this parameter</param>
        private void HideCanvas(object _)
        {
            hudCanvas.GetComponent<CanvasGroup>().DOFade(0,1).OnComplete(() =>
            {
                hudCanvas.SetActive(false);
            });
        }

        /// <summary>
        /// This method adds all the necessary handlers to the EventManager at the same time if possible.
        /// </summary>
        public void AddHandlers()
        {
            EventManager.GetInstance().AddHandler(Events.StartGame, ShowCanvas);
        }

        /// <summary>
        /// This method removes all handlers added by itself from EventManager at the same time if possible. 
        /// </summary>
        public void RemoveHandlers()
        {
            EventManager.GetInstance().RemoveHandler(Events.StartGame, ShowCanvas);
        }
        
        #endregion Method Definitions
    }
}

/*powerUpPanel.GetComponent<CanvasGroup>().DOFade(1, 1).OnStart(() =>
{
                
});*/