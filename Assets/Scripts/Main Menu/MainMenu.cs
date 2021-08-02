using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
	{
		SceneManager.LoadScene("GameScene");
	}

	public void LoadGame()
	{
		//SceneManager.LoadScene("LoadMenuScene");
	}

	public void ExitGame()
	{
		Application.Quit();     // exit our game and close our application
		Debug.Log("ExitGame() function called.");
	}
}
