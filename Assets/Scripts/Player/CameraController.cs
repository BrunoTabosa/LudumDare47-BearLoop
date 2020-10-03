using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Vector3 FollowOffset;

    private bool IsFollowingPlayer;

    private Player target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFollowingPlayer)
        {
            transform.position = target.transform.position + FollowOffset;
        }
    }

    public void Follow(Player newTarget)
    {
        IsFollowingPlayer = true;
        target = newTarget;
    }

    public void Stop()
    {
        IsFollowingPlayer = false;
    }
}
