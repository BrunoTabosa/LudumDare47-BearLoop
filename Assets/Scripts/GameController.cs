using System.Collections;
using UnityEngine;
using BearLoopGame.Utils;
using UnityEngine.Events;
using System;
using JetBrains.Annotations;
using TMPro;


public class GameController : Singleton<GameController>
{
    [Header("GameController Attributes")]
    public Transform RespawnPoint;
    public Player playerPrefab;
    public RagdollCharacter ragdollPrefab;
    public TextMeshPro codeText;
    private Player currentPlayer;
    [SerializeField]
    private float _respawnTime = 3.5f;

    public delegate void OnPlayerSpawnsEvent(Transform player);
    public OnPlayerSpawnsEvent OnPlayerSpawns;

    public delegate void OnPlayerDiesEvent(Transform player);
    public OnPlayerDiesEvent OnPlayerDies;

    public delegate void OnPlayerWinsEvent(Transform player);
    public OnPlayerWinsEvent OnPlayerWins;

    private bool PlayerIsAlive = false;

    private float currentLifeSpan;
    public float lifeSpan = 60f;

    private int code = 123;

    public GameObject NumpadDoor;

    public AudioSource audioSource_bgm;
    //public AudioSource audioSource_scream;

    public RandomAudioClipsController playerDeathRandomAudioClipsController;

    public AudioSource audioSource_numPadCorrect;
    public AudioSource audioSource_numPadWrong;

    private void Awake()
    {
        InitSingleton();
    }
    protected override void InitSingleton()
    {
        base.InitSingleton();
    }

    private void Start()
    {
        if (currentPlayer != null)
        {
            Destroy(currentPlayer.gameObject);
        }

        GenerateKeyCode();
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(4f);
        UIController.Instance.TutorialToggle(false);
        SpawnPlayer();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnPlayerDeath();
        }
        HandleLifeSpan();
    }

    public void Respawn()
    {
        StartCoroutine(Respawn_Coroutine());
    }
    public IEnumerator Respawn_Coroutine()
    {
        if (currentPlayer)
        {
            OnPlayerDies?.Invoke(currentPlayer.transform);
        }

        yield return new WaitForSeconds(_respawnTime);
        SpawnPlayer();

    }

    private void SpawnPlayer()
    {
        currentPlayer = Instantiate(playerPrefab, RespawnPoint.position, Quaternion.identity);
        PlayerIsAlive = true;
        currentLifeSpan = lifeSpan;
        OnPlayerSpawns?.Invoke(currentPlayer.transform);
        audioSource_bgm.Play();
    }

    public void HandleLifeSpan()
    {
        if (PlayerIsAlive)
        {
            currentLifeSpan -= Time.deltaTime;
            UIController.Instance.UpdateTimer(currentLifeSpan);

            if (currentLifeSpan <= 0)
            {
                currentLifeSpan = 0;
                OnPlayerDeath();
            }
        }
    }

    public void OnPlayerDeath()
    {
        PlayerIsAlive = false;
        currentPlayer.gameObject.SetActive(false);
        var ragdoll = Instantiate(ragdollPrefab, currentPlayer.transform.position, currentPlayer.transform.rotation);
        ragdoll.head.velocity = currentPlayer.GetComponent<CharacterController>().velocity;
        currentPlayer.Die();
        audioSource_bgm.Stop();
        //audioSource_scream.Play();
        playerDeathRandomAudioClipsController.PlayOneShot();
        Respawn();
    }
    public void EnterCode(string value)
    {
        if (value == code.ToString())
        {
            audioSource_numPadCorrect.Play();
            NumpadDoor.GetComponent<IInteractable>().Interact();
        }
        else
        {
            audioSource_numPadWrong.Play();
        }
        
    }

    private void GenerateKeyCode()
    {
        code = UnityEngine.Random.Range(100, 1000);
        if (codeText == null) return;
        codeText.text = code.ToString();
        print($"code: {code}");
    }

    public void GameEnd()
    {
        OnPlayerWins?.Invoke(currentPlayer.transform);
        PlayerIsAlive = false;
        currentPlayer.EndGameAction();
        UIController.Instance.EndGameToggle(true);

    }
}

