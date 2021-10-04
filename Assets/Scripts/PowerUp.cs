using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum _power { bounce, boost, health };
    public _power power;

    public PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rocket"))
        {
            switch (power)
            {
                case _power.bounce:
                    AudioManager.instance.Play("powerupcollect");
                    GameController.instance.SetBounce();
                    break;
                case _power.boost:
                    AudioManager.instance.Play("turbo");
                    other.GetComponent<PlayerController>().SetBoosting();
                    break;
                case _power.health:
                    AudioManager.instance.Play("powerupcollect");
                    player = FindObjectOfType<PlayerController>();
                    player.health++;
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }
}
