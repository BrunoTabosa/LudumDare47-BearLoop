using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private bool HasCorpse = false;

    void OnTriggerEnter(Collider collider)
    {
        if (HasCorpse) return;
        Player player = collider.GetComponent<Player>();
        if(player != null)
        {
            GameController.Instance.OnPlayerDeath();
            HasCorpse = true;
        }
    }
}
