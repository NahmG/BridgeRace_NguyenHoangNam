using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSight : MonoBehaviour
{
    [SerializeField] Bot bot;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Bot"))
        {
            bot.SetTarget(col.GetComponent<Character>());
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Bot"))
        {
            bot.SetTarget(null);
        }
    }
}
