using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour, INode
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

	private MeshRenderer _nodeRenderer;


	private void Awake()
    {
		SceneManager.Instance.sceneNodes.Add(this);
		laneWidth = new Width();
		_nodeRenderer = GetComponent<MeshRenderer>();
	}

    private void OnMouseEnter()
    {
		GetComponent<MeshRenderer>().material.color = Color.red;
    }

	private void OnMouseExit()
	{
		GetComponent<MeshRenderer>().material.color = Color.white;
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
		_nodeRenderer.enabled = false;
	}

	public void OnEditMode()
	{
		_nodeRenderer.enabled = true;
	}
}
