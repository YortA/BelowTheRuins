using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UI_PlayerName : MonoBehaviour
{
	private Keyboard kb = InputSystem.GetDevice<Keyboard>();

	public string playerName;

	private void Update()
	{
		if (kb.enterKey.wasPressedThisFrame)
		{
			SaveName();
			Hide();
		}
	}

	public void SaveName()
	{
		playerName = GetComponent<Text>().text;
		Debug.Log("NAME: " + playerName);
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

}
