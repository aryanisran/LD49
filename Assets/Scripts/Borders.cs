using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    public GameController gm;

    public void Start()
    {
        gm = FindObjectOfType<GameController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rocket")
        {
            gm.GameOver();
        }
    }
}
