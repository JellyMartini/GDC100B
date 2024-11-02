using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Master : MonoBehaviour
{
    void Awake() {
        Application.targetFrameRate = 60;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    public void OnCancel(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            Application.Quit();
        }
    }

    public void PressStart() {
        SceneManager.LoadScene(1);
    }
}
