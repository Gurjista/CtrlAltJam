using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endzone : MonoBehaviour
{
    [Header("Events")]
    public GameEvent onEndzoneEnter;

    [SerializeField] private int _nextSceneIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onEndzoneEnter.Raise(this, _nextSceneIndex);
        }
    }
}