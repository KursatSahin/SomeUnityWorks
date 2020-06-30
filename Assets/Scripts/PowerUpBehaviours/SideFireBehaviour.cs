using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Common;
using UnityEngine;
using Complete;
using PowerUp;

namespace PowerUps
{
    public class SideFireBehaviour : PowerUpBehaviour
    {
        private TankShooting _tankShooting;
        private Coroutine _sideFireCoroutine = null;

        public override void Init()
        {
            _tankShooting = tank.GetComponent<TankShooting>();
        }

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

        public override void Activate()
        {
            if (_sideFireCoroutine != null)
            {
                StopCoroutine(_sideFireCoroutine);
                _sideFireCoroutine = null;
            }
            
            _sideFireCoroutine = StartCoroutine(InfiniteSideFire());
        }

        public override void Deactivate()
        {
            if (_sideFireCoroutine != null)
            {
                StopCoroutine(_sideFireCoroutine);
            }
        }
        
        private void SideFire ()
        {
            #region LeftShell

            var leftShell = ObjectPooler.SharedInstance.GetPooledObject(ObjectPooler.PoolingObjectTags.ShellPrefabTag);
            leftShell.transform.localPosition = _tankShooting.m_LeftFireTransform.position;
            leftShell.transform.rotation = _tankShooting.m_LeftFireTransform.rotation;

            leftShell.gameObject.SetActive(true);

            if (leftShell.TryGetComponent(out Rigidbody leftShellRigidbody))
            {
                leftShellRigidbody.velocity = _tankShooting.bulletSpeed * _tankShooting.m_LeftFireTransform.forward;
            }
            else
            {
                var rb =  leftShell.AddComponent<Rigidbody>();
                rb.velocity = _tankShooting.bulletSpeed * _tankShooting.m_LeftFireTransform.forward;
            }
            #endregion

            #region RightShell

            var rightShell = ObjectPooler.SharedInstance.GetPooledObject(ObjectPooler.PoolingObjectTags.ShellPrefabTag);
            rightShell.transform.localPosition = _tankShooting.m_RightFireTransform.position;
            rightShell.transform.rotation = _tankShooting.m_RightFireTransform.rotation;

            rightShell.gameObject.SetActive(true);

            if (rightShell.TryGetComponent(out Rigidbody rightShellRigidbody))
            {
                rightShellRigidbody.velocity = _tankShooting.bulletSpeed * _tankShooting.m_RightFireTransform.forward;
            }
            else
            {
                var rb =  rightShell.AddComponent<Rigidbody>();
                rb.velocity = _tankShooting.bulletSpeed * _tankShooting.m_RightFireTransform.forward;
            }
            #endregion
            
        }
    }
}