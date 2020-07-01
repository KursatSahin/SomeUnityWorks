using Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PowerUp
{
    [RequireComponent(typeof(Toggle))]
    public class PowerUpToggle : MonoBehaviour, IEventManagerHandling
    {
        #region Private Variables
    
        private Toggle _toggle;
        private const string ToggleIsSelectedMessage = "SELECTED";
        private const string ToggleIsNotSelectedMessage = "NON SELECTED";
    
        #endregion Private Variables
    
        #region PowerUp Toggle UI Variables - Show In Inspector 
    
        [Header("Please assign following variables with related child objects..")]
        [SerializeField] private TextMeshProUGUI powerupStatusText;
        [SerializeField] private TextMeshProUGUI powerupLabelText;
        [SerializeField] private PowerUp powerUp;
    
        #endregion PowerUp Toggle UI Variables - Show In Inspector

        #region Unity Events

        private void Awake()
        {
            InitToggle();
            AddHandlers();
        }
    
        private void OnDestroy()
        {
            RemoveHandlers();
        }

        #endregion

        #region PowerUp Toggle Methods Definitions

        /// <summary>
        /// This is a initializer method for toggle
        /// </summary>
        private void InitToggle()
        {
            // Set beginning values for toggle
            _toggle = GetComponent<Toggle>();
            _toggle.interactable = true;
            _toggle.isOn = false;
            _toggle.onValueChanged.AddListener(OnToggleValueChanged);
        
            // Set texts for this toggle 
            powerupStatusText.text = ToggleIsNotSelectedMessage;
            powerupLabelText.text = powerUp.Name;
        }

        /// <summary>
        /// This method is a callback to run when the powerup limit is reached 
        /// </summary>
        /// <param name="obj"></param>
        private void OnPowerUpLimitHasReached(object obj)
        {
            if (_toggle.IsInteractable())
            {
                DisableToggle();
            }
        }

        /// <summary>
        /// This method is a listener for toggle status value
        /// </summary>
        /// <param name="status"></param>
        private void OnToggleValueChanged(bool status)
        {
            if (status)
            {
                powerupStatusText.text = ToggleIsSelectedMessage;
                DisableToggleWithoutFadeout();
                EventManager.GetInstance().Notify(Events.PowerUpIsActivated, powerUp);
            }
            else // Following block is not necessary for this demo
            { 
                powerupStatusText.text = ToggleIsNotSelectedMessage;
                EventManager.GetInstance().Notify(Events.PowerUpIsDeactivated, powerUp);
            }
        }

        /// <summary>
        /// This method disables the toggle
        /// </summary>
        private void DisableToggle()
        {
            _toggle.interactable = false;
        }

        /// <summary>
        /// This method disables the toggle without fadeout color
        /// </summary>
        private void DisableToggleWithoutFadeout()
        {
            ColorBlock colors = _toggle.colors ;
            Color disabledColor = colors.normalColor ;
            //disabledColor.a = 1 ;
            colors.disabledColor = disabledColor ;
            _toggle.colors = colors ;
        
            DisableToggle();
        }
    
        /// <summary>
        /// This method adds all the necessary handlers to the EventManager at the same time if possible.
        /// </summary>
        public void AddHandlers()
        {
            EventManager.GetInstance().AddHandler(Events.PowerUpLimitHasReached, OnPowerUpLimitHasReached);
        }

        /// <summary>
        /// This method removes all handlers added by itself from EventManager at the same time if possible. 
        /// </summary>
        public void RemoveHandlers()
        {
            EventManager.GetInstance().RemoveHandler(Events.PowerUpLimitHasReached, OnPowerUpLimitHasReached);
        }
    
        #endregion PowerUp Toggle Methods Definitions
    }
}
