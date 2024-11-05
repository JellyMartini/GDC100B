using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Can't be included in Master since it's used by an AnimationEvent, and needs to be attached to that GameObject
// It's either that or a second Master
public class GoToMainScene : MonoBehaviour
{
    public void OpeningEnded() {
        SceneManager.LoadScene(2);
    }
}
