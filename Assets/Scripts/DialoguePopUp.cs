using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePopUp : MonoBehaviour
{
    public GameObject FloatingTextPrefab;
    public float yOffset = 2f; 
    public float destroyTime = 3f;
    BoxCollider2D Bc;
    private bool isText = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (FloatingTextPrefab != null)
        {
            if (!isText)
            {
                ShowFloatingText();    
            } 
        }
    }

    void ShowFloatingText()
    {
        Vector3 position = transform.position + new Vector3(0, yOffset, 0.0f);
        Instantiate(FloatingTextPrefab, position, Quaternion.identity);
        isText = true;
    }

     void Start() 
    {
        Destroy(gameObject, destroyTime);
    }

}
