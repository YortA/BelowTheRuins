using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;	// inputs

public class UI_PauseMenu : MonoBehaviour
{
	// get our keyboard inputs
	//private Keyboard kb = InputSystem.GetDevice<Keyboard>();

	private PlayerInputsActions playerInputActions;
	private InputAction uiPause;

	public GameObject pauseMenu;
	public bool isPaused;


	private void Awake()
	{
		playerInputActions = new PlayerInputsActions();
	}

	private void Start()
	{
		// syntax for unity events (_) pass no parameters
		uiPause.performed += _ => RunPause();
	}
	
	private void RunPause()
	{
		if (isPaused)
			Resume();
		else
			Pause();
	}

	private void OnEnable()
	{
		// map our inputs to the UI menu
		uiPause = playerInputActions.UI.PauseMenu;
		uiPause.Enable();
	}

	private void OnDisable()
	{
		uiPause.Disable(); // ends our event
	}

	// Time scale based off real-world time, 0 = 0secs/minute
	public void Pause()
	{
		pauseMenu.SetActive(true);
		Time.timeScale = 0f;		// freezes game by setting Unity's time clock to 0s per min
		isPaused = true;
	}

	// Time scale 1 = 60secs/minute
	public void Resume()
	{
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
		isPaused = false;
	}

	public void ExitGame()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("MainScene");
	}

	public void SaveGame()
	{
		SaveManager.Save("playergame", SaveData.currentSave);
		Debug.Log("MENU: SaveGame");
	}

	public void LoadGame()
	{
		SaveData.currentSave = (SaveData)SaveManager.Load(Application.persistentDataPath + "/Saves/playergame.ruins");
		PlayerData player = SaveData.currentSave.player;
		//Debug.Log("LOADING PLAYER LOCATION: " + player.position);
		PlayerAttributes playerAttributes = new PlayerAttributes();
		playerAttributes.transform.position = player.position;

		Debug.Log("MENU: LoadGame");
	}
}
