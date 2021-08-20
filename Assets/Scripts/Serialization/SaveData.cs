using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    // singleton design pattern
    public static SaveData _currentSave;
	public static SaveData currentSave
	{
		get 
		{ 
			if (_currentSave == null) _currentSave = new SaveData(); 
				return _currentSave; 
		}
		set
		{
			_currentSave = currentSave;
		}
	}


	public PlayerData player;
}