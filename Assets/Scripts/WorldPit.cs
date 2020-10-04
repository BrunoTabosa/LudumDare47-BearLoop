using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPit : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        Player player = collider.GetComponent<Player>();
        if (player != null)
        {
            GameController.Instance.OnPlayerDeath();
        }      
        
    }
}
