using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterInteractable : MonoBehaviour, IInteractable
{
    public WaterState state;
    public Collider floor;

    public void CancelInteraction()
    {
        transform.Translate(0, -0.75f, 0);
        floor.enabled = false;
        state = WaterState.Low;
    }

    public void Interact()
    {
        transform.Translate(0, 0.75f, 0);
        floor.enabled = true;
        state = WaterState.High;
    }

    void OnTriggerEnter(Collider collider)
    {

        Player player = collider.GetComponent<Player>();
        if (player != null)
        {
            GameController.Instance.OnPlayerDeath();
        }

        if(collider.gameObject.tag == "Ragdoll")
        {
            if(state == WaterState.Low)
            {
                Destroy(collider.gameObject, 1f);
            }
        }
    }
}


