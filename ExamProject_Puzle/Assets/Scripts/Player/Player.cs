using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private KeyCode _keyOne;
    [SerializeField] private KeyCode _keyTwo;
    [SerializeField] private Vector3 _moveDirection;

    private void FixedUpdate()
    {
        if (Input.GetKey(_keyOne))
        {
            GetComponent<Rigidbody>().velocity += _moveDirection;
        }
        else if (Input.GetKey(_keyTwo))
        {
            GetComponent<Rigidbody>().velocity -= _moveDirection;
        }
        else if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("Player") && other.CompareTag("Finish"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(this.CompareTag("Cube") && other.CompareTag("Cube"))
        {
            foreach (Activator button in FindObjectsOfType<Activator>())
            {
                button.canPush = true;
            }
        }
    }
}
