using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    private Joystick _joystick;
    [SerializeField] private Vector3 _moveDirection;
    [SerializeField] private float _joystickSensitivity = 1f;
    [SerializeField] private bool _useVertical;

    private void Start()
    {
        _joystick = FindObjectOfType<Joystick>();
    }

    private void FixedUpdate()
    {
        float axis = _useVertical ? _joystick.Vertical : _joystick.Horizontal;
        
        if (axis >= 0.1)
        {
            GetComponent<Rigidbody>().velocity -= _moveDirection * (axis * _joystickSensitivity);
        }
        else if (axis <= -0.1)
        {
            GetComponent<Rigidbody>().velocity += _moveDirection * -(axis * _joystickSensitivity);
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene()
    {
        if(SceneManager.GetActiveScene().buildIndex == 5) SceneManager.LoadScene(0);
        else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("Player") && other.CompareTag("Finish"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 5) SceneManager.LoadScene(0);
            else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(this.CompareTag("Cube") && other.CompareTag("Cube"))
        {
            foreach (Activator button in FindObjectsOfType<Activator>())
            {
                button.canPush = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(this.CompareTag("Cube") && other.CompareTag("Cube"))
        {
            foreach (Activator button in FindObjectsOfType<Activator>())
            {
                button.canPush = true;
            }
        }
    }
}
