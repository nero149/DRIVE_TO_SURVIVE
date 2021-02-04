using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{

public GameObject Player;
public CarSystemV2 CarControl;
public GameObject child;
public float Speed2 = 0;
[Range (0, 50)] public float smothTime = 8;


private void Awake()
{
	Player = GameObject.FindGameObjectWithTag("Player");
	child = Player.transform.Find("CameraHook").gameObject;
	CarSystemV2 CarControl = GetComponent<CarSystemV2>();
} 

private void FixedUpdate() 
{
 follow();
}
private void follow()
{
	Speed2 = Mathf.Lerp(Speed2 , CarControl.SPEED / smothTime,Time.deltaTime);
	gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,child.transform.position,Time.deltaTime * Speed2);
	gameObject.transform.LookAt(Player.gameObject.transform.position);

}
	
}

