using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;


public class NewTestScript
{
    public AudioMixer audioMixer;
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        SceneManager.LoadScene("Menu");

        yield return new WaitForSeconds(3);
        

    }
    [UnityTest]

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }
    [UnityTest]
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    [Test]
    public void Sound()
    {
        AudioListener.pause = !AudioListener.pause;
    }

}
