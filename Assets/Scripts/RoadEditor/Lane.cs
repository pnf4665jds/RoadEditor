using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Lane : MonoBehaviour
{
	public class Width
	{
		public float sOffset;
		public float a = 1;
		public float b;
		public float c;
		public float d;
	}

	// 這個Lane屬於哪個Road
	public Road parentRoad;

	public Width laneWidth;

	private void Awake()
    {
		laneWidth = new Width();
	}
}
