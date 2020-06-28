using UnityEngine;
using UnityEngine.UI;

namespace Complete
{
    public class TankShooting : MonoBehaviour
    {
        public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
        public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
        public AudioClip m_FireClip;              // Audio that plays when each shot is fired.
        public float bulletSpeed = 15f;        // The force given to the shell if the fire button is not held.
        public float firingRate = 2f;       // How long the shell can charge for before it is fired at max force.
        private bool firingStatus = false;

        private string fireActivateButton;                // The input axis that is used for launching shells.
        private const string ShellPrefabTag = "ShellPrefabTag";
        private void Start ()
        {
            // The fire axis is based on the player number.
            fireActivateButton = "Jump";
        }


        private void Update ()
        {
            if (Input.GetButtonDown (fireActivateButton))
            {
                firingStatus = !firingStatus;
                
                if (firingStatus)
                {
                    InvokeRepeating("Fire",.5f, firingRate);    
                }
                else
                {
                    CancelInvoke("Fire");
                }
            }
        }
        
        private void Fire ()
        {
            var shell = ObjectPooler.SharedInstance.GetPooledObject(ShellPrefabTag);
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