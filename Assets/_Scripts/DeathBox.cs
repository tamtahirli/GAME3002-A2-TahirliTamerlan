using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(SoundManager.instance.LoseSound, 0.5f, 1.0f);
            BallScript.instance.Reset();
        }
    }
}
