using System.Collections;
using Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuManager : MonoBehaviour
    {
        #region UI Element Variables
        
        [SerializeField] private GameObject uiCanvas;
        [SerializeField] private Button startButton;
        [SerializeField] private Button quitButton;
        
        [Space(20f)] 
        [SerializeField] private GameObject gameWorld;

        #endregion

        private Coroutine _spinCoroutine;

        #region Unity Events

        private void Awake()
        {
            startButton.onClick.AddListener(StartDemo);
            quitButton.onClick.AddListener(QuitDemo);
        }

        private void OnDestroy()
        {
            startButton.onClick.RemoveAllListeners();
            quitButton.onClick.RemoveAllListeners();
        }

        private void Start()
        {
            _spinCoroutine = StartCoroutine(Spin());
        }

        #endregion

        #region Method Definitions

        private void StartDemo()
        {
            // Disable main menu canvas
            uiCanvas.SetActive(false);

            // Disable fireworks
            var fireworks = GameObject.FindGameObjectsWithTag("FireWorkTag");
            foreach (var firework in fireworks)
            {
                firework.SetActive(false);
            }
            
            // Disable spin
            if (_spinCoroutine != null)
            {
                StopCoroutine(_spinCoroutine);
                _spinCoroutine = null;
                gameWorld.transform.eulerAngles = Vector3.zero;
            }
            
            // Notify the game starts
            EventManager.GetInstance().Notify(Events.StartGame);
        }
        
        private void QuitDemo()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        IEnumerator Spin(){
            while(true){
                float timer = 0f;
                while(timer<1){
                    gameWorld.transform.Rotate(0,30*Time.deltaTime,0);
                    timer = timer + Time.deltaTime;
                    yield return null;
                }
            }
        }

        private void AddHandlers()
        {
            
        }

        private void RemoveHandlers()
        {
            
        }

        #endregion Method Definitions
    }
}