using BearLoopGame.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using Cinemachine;
using UnityEngine.Playables;

public class CameraManager : Singleton<CameraManager>
{
    public enum CameraAnimationType
    {
        PlayerSpawn,
        PlayerDies,

    }
    [System.Serializable]
    public struct CameraAnimationStructure
    {
        [SerializeField]
        private string _name;

        [SerializeField]
        private TimelineAsset _timeLineAsset;

        [SerializeField]
        private float _transitionTime;

        [SerializeField]
        private CameraAnimationType _blendType;

        public float TransitionTime { get => _transitionTime; }
        public CameraAnimationType BlendType { get => _blendType; }
        public TimelineAsset TimeLineAsset { get => _timeLineAsset; }
    }

    [SerializeField]
    [Header("Cameras Info")]
    private CameraAnimationStructure[] _cameraAnimationStructures;
    private Dictionary<CameraAnimationType, CameraAnimationStructure> _cameraAnimationStructureDic = new Dictionary<CameraAnimationType, CameraAnimationStructure>();

    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;
    private Coroutine _currentlyRunningCoroutine = null;
    private CameraAnimationStructure _currentAnimationStructure;    
    [SerializeField]
    private PlayableDirector _directorControlPlayable;

    protected override void InitSingleton()
    {
        base.InitSingleton();

    }
    private void Initialize()
    {
        // Parsing info from array to dic and cleaning the memory
        foreach (var cameraBlend in _cameraAnimationStructures)
        {
            if (_cameraAnimationStructureDic.ContainsKey(cameraBlend.BlendType))
            {
                Debug.LogError("Could be only one CameraBlendType of each type");
            }

            _cameraAnimationStructureDic[cameraBlend.BlendType] = cameraBlend;
        }
        _cameraAnimationStructures = null;

        GameController.Instance.OnPlayerDies += delegate { PlayCameraAnimation(CameraAnimationType.PlayerDies); };
        GameController.Instance.OnPlayerSpawns += SetUpPlayerFollow;

    }
    private void Awake()
    {
        InitSingleton();
        Initialize();
    }

    private void PlayCameraAnimation(CameraAnimationType cameraBlendType)
    {
        if (_currentlyRunningCoroutine != null)
        {
            _directorControlPlayable.Stop();
            _currentlyRunningCoroutine = null;
        }

        _currentlyRunningCoroutine = StartCoroutine(PlayRoutine(_cameraAnimationStructureDic[cameraBlendType]));

    }
    private IEnumerator PlayRoutine(CameraAnimationStructure blendTypeStructure)
    {
        _currentAnimationStructure = blendTypeStructure;

        _directorControlPlayable.Play(blendTypeStructure.TimeLineAsset);

        yield return new WaitForSeconds((float)blendTypeStructure.TimeLineAsset.duration);

        _currentlyRunningCoroutine = null;

    }
    private void SetUpPlayerFollow(Transform playerTransform)
    {
        _virtualCamera.Follow = playerTransform;
        _virtualCamera.LookAt = playerTransform;
        PlayCameraAnimation(CameraAnimationType.PlayerSpawn);
    }

}

