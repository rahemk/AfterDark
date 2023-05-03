using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    public Animator transistion;
    public float transistionTime = 1f;
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int LevelIndex)
    {
        transistion.SetTrigger("Start");

        yield return new WaitForSeconds(transistionTime);

        SceneManager.LoadScene(LevelIndex);
    }
}
