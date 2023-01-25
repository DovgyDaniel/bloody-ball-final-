using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class интро : MonoBehaviour
{
    public int waitime;

    void Start()
    {
        StartCoroutine(waitForLevel());
    }

    IEnumerator waitForLevel()
    {
        yield return new WaitForSeconds(waitime);
        SceneManager.LoadScene(1);
    }
}
