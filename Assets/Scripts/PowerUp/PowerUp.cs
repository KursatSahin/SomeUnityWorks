using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

namespace PowerUp
{
    /// <summary>
    /// PowerUp is base class for power up feature 
    /// </summary>
    [Serializable]
    public class PowerUp
    {
        #region Data Variables for PowerUp
        
        public PowerUpType type;
        public string Name => Regex.Replace(type.ToString(), "(?<=[a-z])([A-Z])", " $1", RegexOptions.Compiled);
        public int duration;
        [HideInInspector] public bool isActivated = false;
        
        // UnityEvents for activate and deactivate actions
        [HideInInspector] public UnityEvent activateAction;
        [HideInInspector] public UnityEvent deactivateAction;
        
        #endregion Data Variables for PowerUp

        #region Method Definitions for PowerUp

        /// <summary>
        /// This is an override method for Equals()
        /// </summary>
        /// <param name="obj">object type parameter </param>
        /// <returns>return boolean value</returns>
        public override bool Equals(object obj)
        {
            if (obj is PowerUp)
            {
                if (type == ((PowerUp) obj).type)
                {
                    return true;
                }
                return false;
            }
            else
            {
                Debug.LogWarning($"{nameof(Equals)} from {nameof(PowerUp)} is called. Object type is mismatched!");
                return false;
            }
        }

        /// <summary>
        /// This method triggers all listeners who want to be triggered to run when power up is activated.
        /// </summary>
        public void ActivatePowerUp()
        {
            activateAction.Invoke();
            isActivated = true;
        }

        /// <summary>
        /// This method triggers all listeners who want to be triggered to run when power up is deactivated.
        /// </summary>
        public void DeactivatePowerUp()
        {
            deactivateAction.Invoke();
            isActivated = false;
        }

        #endregion Method Definitions for PowerUp
    }

    /// <summary>
    /// This enum represents each type of power ups. Each one of new power ups needs to be add by enum.
    /// </summary>
    [Serializable]
    public enum PowerUpType
    {
        SideFire,
        DoubleBullets,
        DoubleFireRateBullets,
        DoubleSpeedBullets,
        CloneTank
    }
}