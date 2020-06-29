using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Complete
{
    public class TankShooting : MonoBehaviour
    {
        public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
        public Transform m_LeftFireTransform;           // A child of the tank where the shells are spawned.
        public Transform m_RightFireTransform;           // A child of the tank where the shells are spawned.
        public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
        public AudioClip m_FireClip;              // Audio that plays when each shot is fired.
        public float bulletSpeed = 15f;        // The force given to the shell if the fire button is not held.
        public float firingRate = 2f;       // How long the shell can charge for before it is fired at max force.
        public bool doubleFireIsActivated = true;
        private Coroutine _fireCoroutine = null;

        private List<PowerUp> _ownedPowerUps;
        
        private string fireActivateButton = "Jump";        // The input axis that is used for launching shells.
        private void Awake ()
        {
            EventManager.GetInstance().AddHandler(Events.PowerUpInUseUpdated, OnPowerUpInUseUpdated);
        }

        private void Start()
        {
            ActivateFire();
        }

        private void OnPowerUpInUseUpdated(object data)
        {
            // Get updated list
            _ownedPowerUps = data as List<PowerUp>;
            
            // First stop fire for prevent causing sync problems
            DeactivateFire();
            
            // For same reason stop all powerups too
            foreach (var powerUp in _ownedPowerUps)
            {
                if (powerUp.type == PowerUpType.CloneTank)
                {
                    continue;
                }

                if (powerUp.isActivated)
                {
                    powerUp.DeactivatePowerUp();
                }
            }
            
            // Then reactivate fire method and powerups
            foreach (var powerUp in _ownedPowerUps)
            {
                if (powerUp.type == PowerUpType.CloneTank)
                {
                    continue;
                }

                powerUp.ActivatePowerUp();
            }
            
            ActivateFire();
        }

        private void ActivateFire()
        {
            
            if (_fireCoroutine != null)
            {
                StopCoroutine(_fireCoroutine);
                _fireCoroutine = null;
            }
            
            _fireCoroutine = StartCoroutine(InfiniteFire());
        }
        
        private void DeactivateFire()
        {
            if (_fireCoroutine != null)
            {
                StopCoroutine(_fireCoroutine);
            }
        }

        public IEnumerator InfiniteFire()
        {
            while (true)
            {
                Fire();
                
                if (doubleFireIsActivated)
                {
                    yield return new WaitForSeconds(.1f);
                    Fire();
                }
                
                yield return new WaitForSeconds(firingRate);
            }
        }

        private void Fire ()
        {
            var shell = ObjectPooler.SharedInstance.GetPooledObject(ObjectPooler.PoolingObjectTags.ShellPrefabTag);
            shell.transform.position = m_FireTransform.position;
            shell.transform.rotation = m_FireTransform.rotation;

            shell.gameObject.SetActive(true);

            if (shell.TryGetComponent(out Rigidbody shellRigidbody))
            {
                shellRigidbody.velocity = bulletSpeed * m_FireTransform.forward;
            }
            else
            {
                var rb =  shell.AddComponent<Rigidbody>();
                rb.velocity = bulletSpeed * m_FireTransform.forward;
            }
            
            // Change the clip to the firing clip and play it.
            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play ();
        }
    }
}