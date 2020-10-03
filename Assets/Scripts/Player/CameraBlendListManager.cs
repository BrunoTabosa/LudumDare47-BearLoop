using UnityEngine;
using Cinemachine;
using BearLoopGame.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BearLoopGame.Camera.CameraBlend
{
    public class CameraBlendListManager : Singleton<CameraBlendListManager>
    {

        public enum CameraBlendType
        {
            PlayerSpawn,
            PlayerDies,

        }

        [System.Serializable]
        public struct CameraBlendTypeStructure
        {
            [SerializeField]
            private string _name;

            [SerializeField]
            private CinemachineBlendListCamera _cinemachineBlendListCamera;

            [SerializeField]
            private float _transitionTime;

            [SerializeField]
            private CameraBlendType _blendType;

            public CinemachineBlendListCamera CinemachineBlendListCamera { get => _cinemachineBlendListCamera; }
            public float TransitionTime { get => _transitionTime; set => _transitionTime = value; }
            public CameraBlendType BlendType { get => _blendType; }
        }

        [SerializeField]
        [Header("Cameras Info")]
        private CameraBlendTypeStructure[] _cameraBlends;
        private Dictionary<CameraBlendType, CameraBlendTypeStructure> _cameraBlendsDict = new Dictionary<CameraBlendType, CameraBlendTypeStructure>();


        private Transform _playerTransform;
        private Coroutine _currentlyRunningCoroutine = null;
        private CameraBlendTypeStructure _currentCameraBlend;

        protected override void InitSingleton()
        {
            base.InitSingleton();

        }
        private void InitializeCameraBlend()
        {
            // Parsing info from array to dic and cleaning the memory
            foreach (var cameraBlend in _cameraBlends)
            {
                cameraBlend.CinemachineBlendListCamera.enabled = false;
                if (_cameraBlendsDict.ContainsKey(cameraBlend.BlendType))
                {
                    Debug.LogError("Could be only one CameraBlendType of each type");
                }

                _cameraBlendsDict[cameraBlend.BlendType] = cameraBlend;
            }
            _cameraBlends = null;

            GameController.Instance.OnPlayerDies += delegate { DoTranstion(CameraBlendType.PlayerDies); };
            GameController.Instance.OnPlayerSpawns += SetUpPlayerFollow;

        }
        private void Awake()
        {
            InitSingleton();
            InitializeCameraBlend();
        }

        private void DoTranstion(CameraBlendType cameraBlendType)
        {
            if (_currentlyRunningCoroutine != null)
            {
                EnableCameraBlend(_currentCameraBlend, false);
                _currentlyRunningCoroutine = null;
            }

            _currentlyRunningCoroutine = StartCoroutine(TransitionRoutine(_cameraBlendsDict[cameraBlendType]));

        }
        private IEnumerator TransitionRoutine(CameraBlendTypeStructure blendTypeStructure)
        {
            if (_currentCameraBlend.CinemachineBlendListCamera != null)
            {
                EnableCameraBlend(_currentCameraBlend, false);
            }

            _currentCameraBlend = blendTypeStructure;

            EnableCameraBlend(blendTypeStructure, true);

            yield return new WaitForSeconds(blendTypeStructure.TransitionTime);

            _currentlyRunningCoroutine = null;

        }
        private void EnableCameraBlend(CameraBlendTypeStructure blendTypeStructure, bool state)
        {            
            blendTypeStructure.CinemachineBlendListCamera.enabled = state;
        }
        private void SetUpPlayerFollow(Transform playerTransform)
        {
            _playerTransform = playerTransform;
            foreach (var cameraBlend in _cameraBlendsDict.Values.ToArray())
            {
                cameraBlend.CinemachineBlendListCamera.Follow = _playerTransform;
            }
            DoTranstion(CameraBlendType.PlayerSpawn);
        }

    }
}

