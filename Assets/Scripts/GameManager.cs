using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
	[SerializeField]
    private DungeonGenerator dungeonGenerator = null;

	public void SaveName()
	{
        //playerName = inputField.GetComponent<Text>().text;
	}

    private void Start()
    {
		dungeonGenerator.GenerateRooms();
    }

}
