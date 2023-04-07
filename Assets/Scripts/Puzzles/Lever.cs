using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

namespace Puzzles
{
    public class Lever :MonoBehaviour
    {
        public List<GameObject> toDespawn;

        private AudioManager _audioManager;

        public GameObject leverHinge;

        private void Start()
        {
            _audioManager = GetComponent<AudioManager>();
        }

        public void ActivateLever()
        {
            StartCoroutine(PullLever());
        }

        private IEnumerator PullLever()
        {
            Vector3 origRot = leverHinge.transform.rotation.eulerAngles;
            Vector3 targetRot = leverHinge.transform.rotation.eulerAngles + (Vector3.up * 45);
            float elapsedTime = 0.0f;

            //_audioManager.Play("DoorOpen");
            while (elapsedTime < 0.2f)
            {
                leverHinge.transform.rotation = Quaternion.Euler(Vector3.Lerp(origRot, targetRot, elapsedTime / 0.2f));
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            _audioManager.Play("DebrisDisappear");
            foreach (GameObject g in toDespawn)
            {
                g.SetActive(false);
            }
        }
    }
}