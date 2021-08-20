using System.Collections;
using System.Collections.Generic;
using System.IO;        // allows for fstream
using System.Runtime.Serialization;		// allows for surrogateselector for serialization
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// it should be noted that the Formatters.Binary from MS is not a safe way to transmit data and shouldn't
// be used. input data can be maniuplated by attackers and used to inject malicious code.
// review this link: https://docs.microsoft.com/en-us/dotnet/standard/serialization/binaryformatter-security-guide
// HOWEVER, because this is a singleplayer game, and Security is not an issue -- we can use it (something I'm familiar with)
// other object data storing methods would include MS or Unity built-in json saving -- but I don't know how to do this.


// THIS SAVE SYSTEM -- MUST -- BE REWRITTEN IF EVER MADE INTO PRODUCTION. READ NOTE ABOVE.
public class SaveManager
{
	public static bool Save(string saveName, object saveData)
	{
		// If we don't have a "Saves" dir, make one.
		if (!Directory.Exists(Application.persistentDataPath + "/Saves"))
			Directory.CreateDirectory(Application.persistentDataPath + "/Saves");

		BinaryFormatter formatter = GetBinaryFormatter();

		string filepath = Application.persistentDataPath + "/Saves/" + saveName + ".ruins";
		FileStream file = File.Create(filepath);    // create our save
		formatter.Serialize(file, saveData);
		file.Close();
		return true;
	}

	public static object Load(string pathName)
	{
		// didn't find a file that exists. return nothing
		if (!File.Exists(pathName))
		{
			Debug.Log("ERROR: FILE DOESN'T EXIST.");
			return null;
		}
		BinaryFormatter formatter = GetBinaryFormatter();
		FileStream file = File.Open(pathName, FileMode.Open);

		try
		{
			object saveData = formatter.Deserialize(file);
			file.Close();
			return saveData;
		}
		catch
		{
			Debug.Log("ERROR: FILE WAS UNABLE TO LOAD.");
			file.Close();
			return null;
		}
	}


	public static BinaryFormatter GetBinaryFormatter()
	{
		BinaryFormatter formatter = new BinaryFormatter();
		// We can use a selector so we can substitute our Vector3s
		SurrogateSelector selector = new SurrogateSelector();

		Vector3Helper v3helper = new Vector3Helper();

		selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), v3helper);
		formatter.SurrogateSelector = selector;
		return formatter;
	}

}
