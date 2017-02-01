using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System;

public static class StaticData {

	public static float gravity = -9.81f;

	public enum AvailableGameStates {
		Menu,	//Player is consulting the menu
		Starting,	//Game is starting
		Paused,	//Game is paused
		Playing	//Game is playing
	};

	public static Storage storedData = new Storage();

	public static void SaveData() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + Path.PathSeparator + "storedGameData.dat", FileMode.OpenOrCreate);
		bf.Serialize (file, storedData);
		file.Close ();
	}

	public static void LoadData(GameObject scriptsBucket) {
		if (!System.IO.File.Exists(Application.persistentDataPath + "/storedGameData.dat")) {
			SaveData ();
		} else {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open(Application.persistentDataPath + "/storedGameData.dat", FileMode.Open);
			storedData = (Storage)bf.Deserialize (file);
			file.Close ();
		}
	}
}