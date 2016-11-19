using UnityEngine;
using System.Collections;

public class PersistentValues : MonoBehaviour {

	public static float musicVolume = 1;
	public static float musicLastVolume = 1;
	public static float effectsVolume = 1;
	public static float effectsLastVolume = 1;

	void Awake(){
		DontDestroyOnLoad(this);
	}

}
