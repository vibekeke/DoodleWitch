using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 1f;
    private float killZone = 15f; //Destroys object if outside of screen
    public event Action onDestroyed;

    private Camera mainCamera;
    

    void Start()
    {
        mainCamera = Camera.main;
    }
    
    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (mainCamera != null && transform.position.x < mainCamera.transform.position.x - killZone)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    void OnDestroy()
    {
        if (onDestroyed != null) {  //Avoid nullpointerexception
            onDestroyed.Invoke(); //Essentially "Emit signal"
        }
    }
}