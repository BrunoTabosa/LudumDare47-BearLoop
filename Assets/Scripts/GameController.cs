using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
	public Transform RespawnPoint;
	public Player playerPrefab;
    public CameraController cameraController;

	public Player currentPlayer;

    public float LifeSpan = 10.0f;

    private float currentLifeSpan = 0f;
    private bool PlayerIsAlive = false;

    [SerializeField]
    private TextMeshProUGUI timer;


    private TimeSpan timeSpan;

    private void Start()
    {
        Spawn();

        cameraController.Follow(currentPlayer);
    }

    public void Update()
    {
        HandleLifeSpan();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Respawn_Courotine());
        }
    }

    public void HandleLifeSpan()
    {
        if(PlayerIsAlive)
        {
            currentLifeSpan -= Time.deltaTime;
            timeSpan = TimeSpan.FromSeconds(currentLifeSpan);
            timer.text = timeSpan.ToString(@"mm\:ss");

            if(currentLifeSpan <= 0)
            {
                currentLifeSpan = 0;
                KillPlayer();
            }
        }
    }
    private void KillPlayer()
    {
        PlayerIsAlive = false;
        Respawn();
    }

    public void Respawn()
    {
        StartCoroutine(Respawn_Courotine());
    }

    public IEnumerator Respawn_Courotine()
    {
        currentPlayer?.Die();
        yield return new WaitForSeconds(1.5f);
        Spawn();
        cameraController.Follow(currentPlayer);
    }

    private void Spawn()
    {
        currentPlayer = Instantiate(playerPrefab, RespawnPoint.position, Quaternion.identity);
        currentLifeSpan = LifeSpan;
        PlayerIsAlive = true;
    }
}
