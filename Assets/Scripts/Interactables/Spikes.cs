using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        Player player = collider.GetComponent<Player>();
        if(player != null)
        {
            player.Die();
        }
    }
}
