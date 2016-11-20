using UnityEngine;
using System.Collections;

public class PersistentValues : MonoBehaviour {

	// We use theese attributes to save volume level between scenes:
	public static float musicVolume = 1;
	public static float musicLastVolume = 1;
	public static float effectsVolume = 1;
	public static float effectsLastVolume = 1;
	
	// Level of difficulty:
	public static int difficulty; // Easy = 1, Medium = 2, Hard = 3

	void Awake(){
		DontDestroyOnLoad(this);
	}

}
