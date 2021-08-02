using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
	[SerializeField]
    DungeonGenerator dungeonGenerator = null;



	public string playerName;
	//public GameObject inputField;


	public void SaveName()
	{
        //playerName = inputField.GetComponent<Text>().text;
	}

    private void Start()
    {
		dungeonGenerator.GenerateRooms();
    }

}
