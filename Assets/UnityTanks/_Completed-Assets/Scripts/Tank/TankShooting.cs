using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using PowerUp;
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
        public bool doubleFireIsActivated = false;
        private Coroutine _fireCoroutine = null;

        private List<PowerUp.PowerUp> _ownedPowerUps;
        
        private void OnEnable ()
        {
            EventManager.GetInstance().AddHandler(Events.StartGame, OnStartGame);
        }

        private void OnDisable()
        {
            EventManager.GetInstance().RemoveHandler(Events.StartGame, OnStartGame);
        }

        public void ActivateFire()
        {
            if (_fireCoroutine != null)
            {
                StopCoroutine(_fireCoroutine);
                _fireCoroutine = null;
            }
            
            _fireCoroutine = StartCoroutine(InfiniteFire());
        }
        
        public void DeactivateFire()
        {
            if (_fireCoroutine != null)
            {
                StopCoroutine(_fireCoroutine);
            }
        }

        private void OnStartGame(object _)
        {
            _fireCoroutine = StartCoroutine(InfiniteFire(1));
        }

        public IEnumerator InfiniteFire(float initialDelay = 0)
        {
            yield return new WaitForSeconds(initialDelay);
            
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
            var shell = ObjectPooler.SharedInstance.GetPooledObject(PoolingObjectTags.ShellPrefabTag);
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