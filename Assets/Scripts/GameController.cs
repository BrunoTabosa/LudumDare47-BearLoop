﻿using System.Collections;
using UnityEngine;
using BearLoopGame.Utils;
using UnityEngine.Events;
using System;
using JetBrains.Annotations;

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

    private bool PlayerIsAlive = false;

    private float currentLifeSpan;
    public float lifeSpan = 60f;


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
        currentPlayer.Die();
        Respawn();
    }
}

