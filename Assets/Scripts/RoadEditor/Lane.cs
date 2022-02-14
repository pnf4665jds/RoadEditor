using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[ExecuteInEditMode]
public class Lane : INode
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

	public Lane()
    {
		SceneManager.Instance.sceneNodes.Add(this);
		laneWidth = new Width();
	}

	public void OnNodeInit()
	{
		if (EditManager.Instance.isPreviewMode)
			OnPreviewMode();
		else
			OnEditMode();
	}

	public void OnPreviewMode()
	{

	}

	public void OnEditMode()
	{

	}
}
