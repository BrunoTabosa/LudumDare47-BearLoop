using System.Collections;
using UnityEngine;
using BearLoopGame.Utils;
using UnityEngine.Events;

public class GameController : Singleton<GameController>
{
    [Header("GameController Attributes")]
    public Transform RespawnPoint;
    public Player playerPrefab;
    public Player currentPlayer;
    [SerializeField]
    private float _respawnTime = 3.5f;

    public delegate void OnPlayerSpawnsEvent(Transform player);
    public OnPlayerSpawnsEvent OnPlayerSpawns;

    public delegate void OnPlayerDiesEvent(Transform player);
    public OnPlayerDiesEvent OnPlayerDies;

    private void Awake()
    {
        InitSingleton();
    }

    private void Start()
    {
        if (currentPlayer != null)
        {
            Destroy(currentPlayer.gameObject);
        }
        SpawnPlayer();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Respawn());
        }
    }
    public IEnumerator Respawn()
    {
        if (currentPlayer)
        {
            currentPlayer.Die();
            OnPlayerDies?.Invoke(currentPlayer.transform);
        }

        yield return new WaitForSeconds(_respawnTime);
        SpawnPlayer();

    }
    protected override void InitSingleton()
    {
        base.InitSingleton();
    }
    private void SpawnPlayer()
    {
        currentPlayer = Instantiate(playerPrefab, RespawnPoint.position, Quaternion.identity);
        OnPlayerSpawns?.Invoke(currentPlayer.transform);
    }
}
