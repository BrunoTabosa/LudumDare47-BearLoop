using System.Collections;
using UnityEngine;
public class GameController : MonoBehaviour
{
	public Transform RespawnPoint;
	public Player playerPrefab;
    public CameraController cameraController;



	public Player currentPlayer;


    private void Start()
    {
        if(currentPlayer == null)
            currentPlayer = Instantiate(playerPrefab, RespawnPoint.position, Quaternion.identity);

        cameraController.Follow(currentPlayer);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Respawn());
        }
    }
    public IEnumerator Respawn()
    {
        currentPlayer?.Die();
        yield return new WaitForSeconds(1.5f);
        currentPlayer = Instantiate(playerPrefab, RespawnPoint.position, Quaternion.identity);
        cameraController.Follow(currentPlayer);
    }
}
