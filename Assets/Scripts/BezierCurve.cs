using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BezierCurve : MonoBehaviour
{
	public Shader shader;
	public GameObject[] ControlPoints;
	public GameObject[] AxisObjects;
	public bool IsSelected = false;

	private BezierPath path = new BezierPath();
	private int canvasIndex = 0;

	private void UpdatePath()
	{
		List<Vector3> c = new List<Vector3>();
		for (int o = 0; o < ControlPoints.Length; o++)
		{
			if (ControlPoints[o] != null)
			{
				Vector3 p = ControlPoints[o].transform.position;
				c.Add(p);
			}
		}
		path.DeletePath();
		path.CreateCurve(c);
	}

	// Use this for initialization
	void Start()
	{
		UpdatePath();
	}

	// Update is called once per frame 
	void Update()
	{
		/*UpdatePath();
		for (int i = 1; i < (path.pointCount); i++)
		{
			Vector3 startv = path.pathPoints[i - 1];
			Vector3 endv = path.pathPoints[i];
			CreateLine(startv, endv, 0.25f, Color.blue);
		}*/
	}

	void OnDrawGizmosSelected()
	{
		UpdatePath();
		for (int i = 1; i < (path.pointCount); i++)
		{
			Vector3 startv = path.pathPoints[i - 1];
			Vector3 endv = path.pathPoints[i];
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(startv, endv);
		}
	}

	/// <summary>
	/// 設定是否顯示四個控制點
	/// </summary>
	/// <param name="show"></param>
	public void SetShowControlPoints(bool show)
    {
		foreach(GameObject p in ControlPoints)
        {
			p.SetActive(show);
        }
    }

	/// <summary>
	/// 設定是否顯示三個移動軸
	/// </summary>
	/// <param name="show"></param>
	public void SetShowAxis(bool show)
    {
		foreach (GameObject axis in AxisObjects)
		{
			axis.SetActive(show);
		}
	}

    private void OnMouseDown()
    {
		RoadEditManager.Instance.SetCurrentBezier(this);
    }
}