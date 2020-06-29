using UnityEngine;

namespace PowerUp
{
    public abstract class PowerUpBehaviour : MonoBehaviour
    {
        public GameObject tank;

        public virtual void Activate()
        {
        }

        public virtual void Deactivate()
        {
        }
    }
}
