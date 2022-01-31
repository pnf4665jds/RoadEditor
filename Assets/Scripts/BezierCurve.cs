using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BezierCurve : MonoBehaviour
{
	public Shader shader;
	public LineRenderer LineRenderer;
	public GameObject[] ControlPoints;
	public GameObject[] AxisObjects;
	public bool IsSelected = false;

	private BezierPath path;

    private void Start()
    {
		path = new BezierPath(LineRenderer);
	}

    /// <summary>
    /// 更新Path
    /// </summary>
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
		path.CreateCurve(c);
	}

	void Update()
	{
		UpdatePath();
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

	/// <summary>
	/// 每當點擊curve方塊，就將它設為當前操作中的curve
	/// </summary>
    private void OnMouseDown()
    {
		RoadEditManager.Instance.SetCurrentBezier(this);
    }
}