using System;
using Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InGameMenuPanelManager : MonoBehaviour
    {
        [SerializeField] private Button restartButton;

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
        

        private void RestartGame()
        {
            restartButton.gameObject.SetActive(false);
            EventManager.GetInstance().Notify(Events.RestartGame);
        }

        private void OnStartGame(object _)
        {
            restartButton.gameObject.SetActive(true);
        }

        private void AddHandlers()
        {
            EventManager.GetInstance().AddHandler(Events.StartGame, OnStartGame);
        }

        private void RemoveHandlers()
        {
            EventManager.GetInstance().RemoveHandler(Events.StartGame, OnStartGame);
        }
    }
}