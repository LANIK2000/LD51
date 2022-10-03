using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
	UnityEngine.Video.VideoPlayer _vid;
	void Start() {
		_vid = GetComponent<UnityEngine.Video.VideoPlayer>();
		_vid.loopPointReached += EndReached;
	}

	void EndReached(UnityEngine.Video.VideoPlayer vp) {
		SceneManager.LoadScene(1);
	}
}
