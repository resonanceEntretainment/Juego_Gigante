using UnityEngine;
using System.Collections;
 
public class fps : MonoBehaviour
{
	float deltaTime = 0.0f;
 
	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
	        Debug.Log("FPS:" + 1.0f / deltaTime);
	}
}

