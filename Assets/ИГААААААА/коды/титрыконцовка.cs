using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class титрыконцовка: MonoBehaviour
{
    public int coneq;

    void Start()
    {
        StartCoroutine(waitForLevel2());
    }

    IEnumerator waitForLevel2()
    {
        yield return new WaitForSeconds(coneq);
        SceneManager.LoadScene(4);
    }
}
