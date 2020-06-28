using System;
using System.Text.RegularExpressions;
using UnityEngine;

[Serializable]
public class PowerUp
{
    public PowerUpType type;
    public string Label
    {
        get
        {
            return Regex.Replace(type.ToString(), "(?<=[a-z])([A-Z])", " $1", RegexOptions.Compiled);
        }
    }
    public int boostDuration;

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
        return base.Equals(obj);
    }
}

[Serializable]
public enum PowerUpType
{
    SideFire,
    DoubleFire,
    DoubleFiringRate,
    SpeedBullets,
    Clone
}
