using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum _power { bounce, boost };
    public _power power;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rocket"))
        {
            switch (power)
            {
                case _power.bounce:
                    GameController.instance.SetBounce();
                    break;
                case _power.boost:
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }
}
