using System.Collections;
using Common;
using Complete;
using PowerUp;
using UnityEngine;

namespace PowerUpBehaviours
{
    public class SideFireBehaviour : PowerUpBehaviour
    {
        private TankShooting _tankShooting;
        private Coroutine _sideFireCoroutine = null;

        /// <summary>
        /// This method is initializer for PowerUpBehaviour instead of Start, Awake, OnEnable methods
        /// which is using for preventing coroutine problems when necessary
        /// </summary>
        public override void Init()
        {
            // Get TankShooting component
            _tankShooting = tank.GetComponent<TankShooting>();
        }
        
        /// <summary>
        /// This method user for activate PowerUp
        /// </summary>
        public override void Activate()
        {
            if (_sideFireCoroutine != null)
            {
                StopCoroutine(_sideFireCoroutine);
                _sideFireCoroutine = null;
            }
            
            _sideFireCoroutine = StartCoroutine(InfiniteSideFire());
        }

        /// <summary>
        /// This method user for deactivate PowerUp
        /// </summary>
        public override void Deactivate()
        {
            if (_sideFireCoroutine != null)
            {
                StopCoroutine(_sideFireCoroutine);
            }
        }
        
        /// <summary>
        ///  This method calls SideFire method in infinite loop
        /// </summary>
        public IEnumerator InfiniteSideFire()
        {
            while (true)
            {
                SideFire();
                
                if (_tankShooting.doubleFireIsActivated)
                {
                    yield return new WaitForSeconds(.1f);
                    SideFire();
                }
                
                yield return new WaitForSeconds(_tankShooting.firingRate);
            }
        }
        
        /// <summary>
        /// This method fires side bullets
        /// </summary>
        private void SideFire ()
        {
            #region Fire Left Bullet
            
            // Get left bullet from ObjectPooler
            var leftShell = ObjectPooler.SharedInstance.GetPooledObject(PoolingObjectTags.ShellPrefabTag);
            
            // Set left bullets transform datas (position and rotation)
            leftShell.transform.localPosition = _tankShooting.m_LeftFireTransform.position;
            leftShell.transform.rotation = _tankShooting.m_LeftFireTransform.rotation;

            // Set left bullet enable
            leftShell.gameObject.SetActive(true);

            // Add velocity to left bullet according to the bullet speed value of the tank
            if (leftShell.TryGetComponent(out Rigidbody leftShellRigidbody))
            {
                leftShellRigidbody.velocity = _tankShooting.bulletSpeed * _tankShooting.m_LeftFireTransform.forward;
            }
            else
            {
                var rb =  leftShell.AddComponent<Rigidbody>();
                rb.velocity = _tankShooting.bulletSpeed * _tankShooting.m_LeftFireTransform.forward;
            }
            #endregion Fire Left Bullet

            #region Fire Right Bullet

            // Get right bullet from ObjectPooler
            var rightShell = ObjectPooler.SharedInstance.GetPooledObject(PoolingObjectTags.ShellPrefabTag);
            
            // Set right bullets transform datas (position and rotation)
            rightShell.transform.localPosition = _tankShooting.m_RightFireTransform.position;
            rightShell.transform.rotation = _tankShooting.m_RightFireTransform.rotation;

            // Set right bullet enable
            rightShell.gameObject.SetActive(true);

            // Add velocity to right bullet according to the bullet speed value of the tank
            if (rightShell.TryGetComponent(out Rigidbody rightShellRigidbody))
            {
                rightShellRigidbody.velocity = _tankShooting.bulletSpeed * _tankShooting.m_RightFireTransform.forward;
            }
            else
            {
                var rb =  rightShell.AddComponent<Rigidbody>();
                rb.velocity = _tankShooting.bulletSpeed * _tankShooting.m_RightFireTransform.forward;
            }
            #endregion Fire Right Bullet
        }
    }
}