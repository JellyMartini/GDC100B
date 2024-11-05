using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Functions callable from anywhere, attached to a GameObject
public class Master : MonoBehaviour
{
    // Set the framerate to 60 (I think it gets overwritten by vsync or Gsync)
    void Awake() {
        Application.targetFrameRate = 60;
    }

    void Update() {
        // if at any point the player presses escape, quit
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    // same as above
    public void OnCancel(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            CallQuit();
        }
    }

    // Application.Quit() but callable from UI
    public void CallQuit()
    {
        Application.Quit();
    }

    // Go to the Opening cutscene when the start button pressed
    public void PressStart() {
        SceneManager.LoadScene("Opening");
    }

    // Go to the Level-1 scene when Opening cutscene ends, or the player presses try again
    public void PlayLevel()
    {
        SceneManager.LoadScene("Level-1");
    }

    // go to Endless Mode if the player pressed ENDLESS MODE
    public void PlayEndless()
    {
        SceneManager.LoadScene("Main");
    }
}
