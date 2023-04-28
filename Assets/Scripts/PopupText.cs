using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    public GameObject FloatingTextPrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject floatingText = Instantiate(FloatingTextPrefab, other.transform.position, Quaternion.identity);
        }
    }
}
