using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{

public GameObject Player;
public GameObject child;
public float Speed = 0;


private void Awake()
{
	Player = GameObject.FindGameObjectWithTag("Player");
	child = Player.transform.Find("CameraHook").gameObject;
} 

private void FixedUpdate() 
{
 follow();
}
private void follow()
{
	gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,child.transform.position,Time.deltaTime * Speed);
	gameObject.transform.LookAt(Player.gameObject.transform.position);
	
}
	
}

