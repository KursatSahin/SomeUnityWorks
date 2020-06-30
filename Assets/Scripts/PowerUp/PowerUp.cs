using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

namespace PowerUp
{
    [Serializable]
    public class PowerUp
    {
        public PowerUpType type;
        public string Name
        {
            get
            {
                return Regex.Replace(type.ToString(), "(?<=[a-z])([A-Z])", " $1", RegexOptions.Compiled);
            }
        }
        public int duration;
        public bool isActivated = false;

        public UnityEvent activateAction;
        public UnityEvent deactivateAction;
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

        public void ActivatePowerUp()
        {
            activateAction.Invoke();
            isActivated = true;
        }

        public void DeactivatePowerUp()
        {
            deactivateAction.Invoke();
            isActivated = false;
        }
    }

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