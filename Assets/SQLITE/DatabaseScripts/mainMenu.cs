using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;

public class mainMenu : MonoBehaviour
{

	
    public void goToRegister()
    {
    	SceneManager.LoadScene("registerMenu 1");
    }
    
    public void goToLogin()
    {
    	SceneManager.LoadScene("loginMenu");
    }

    public void closeApplication() 
    {
        Application.Quit();
    }

    
}
