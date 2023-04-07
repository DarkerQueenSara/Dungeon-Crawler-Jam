using System;
using System.Collections;
using Audio;
using UnityEngine;

namespace Puzzles
{
    public class AnimatedDoor : MonoBehaviour
    {
        public GameObject doorCamera;
        public GameObject hinge;
        private AudioManager _audioManager;
        private bool _started;

        private Transform _defaultDoorCameraTransform;
        private Transform _defaultHingeCameraTransform;

        public float cameraDistance1;
        public float cameraMoveDuration1;
        public float cameraPauseDuration;
        public float cameraDistance2;
        public float cameraMoveDuration2;

        public float hingeRotation;
        public float hingeRotDuration;
        
        private void Start()
        {
            _audioManager = GetComponent<AudioManager>();
            _defaultDoorCameraTransform = doorCamera.transform;
            _defaultHingeCameraTransform = hinge.transform;
            _started = true;
        }

        private void OnEnable()
        {
            if (!_started) Start();
            doorCamera.transform.position = _defaultDoorCameraTransform.position;
            hinge.transform.rotation = _defaultHingeCameraTransform.rotation;
            StartCoroutine(MoveCamera());
            StartCoroutine(RotateHinge());
        }

        private IEnumerator MoveCamera()
        {
            Vector3 origPos = doorCamera.transform.position;
            Vector3 targetPos = doorCamera.transform.position + (Vector3.back * cameraDistance1);
            float elapsedTime = 0.0f;
            while (elapsedTime < cameraMoveDuration1)
            {
                doorCamera.transform.position = Vector3.Lerp(origPos, targetPos, elapsedTime / cameraMoveDuration1);
                elapsedTime += Time.deltaTime;

                yield return null;
            }
            
            yield return new WaitForSeconds(cameraPauseDuration);

            elapsedTime = 0.0f;
            origPos = doorCamera.transform.position;
            targetPos = doorCamera.transform.position + (Vector3.back * cameraDistance2);
            while (elapsedTime < cameraMoveDuration2)
            {
                doorCamera.transform.position = Vector3.Lerp(origPos, targetPos, elapsedTime / cameraMoveDuration2);
                elapsedTime += Time.deltaTime;

                yield return null;
            }
            //_audioManager.Play("DoorClose");
        }
        
        private IEnumerator RotateHinge()
        {
            yield return new WaitForSeconds(cameraMoveDuration1);

            Vector3 origRot = hinge.transform.rotation.eulerAngles;
            Vector3 targetRot = hinge.transform.rotation.eulerAngles + (Vector3.up * hingeRotation);
            float elapsedTime = 0.0f;

            _audioManager.Play("door-open");
            while (elapsedTime < hingeRotDuration)
            {
                hinge.transform.rotation = Quaternion.Euler(Vector3.Lerp(origRot, targetRot, elapsedTime / hingeRotDuration));
                elapsedTime += Time.deltaTime;

                yield return null;
            }        
            _audioManager.Play("door-close");

        }
    }
}