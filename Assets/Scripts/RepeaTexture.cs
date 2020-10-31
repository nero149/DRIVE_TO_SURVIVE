using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RepeaTexture : MonoBehaviour
{
    private Vector3 _currentScale;
	public float ScrollX = 5;
	public float ScrollY = 500;
	
    private void Start()
    {

    }

    private void Update()
    {
    float OffsetX = Time.time * ScrollX;
	float OffsetY = Time.time * ScrollY;
	GetComponent<Renderer>().material.mainTextureOffset = new Vector2 (OffsetX, OffsetY);
    }

}