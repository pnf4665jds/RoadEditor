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
    /// ��sPath
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
	/// �]�w�O�_��ܥ|�ӱ����I
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
	/// �]�w�O�_��ܤT�Ӳ��ʶb
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
	/// �C���I��curve����A�N�N���]����e�ާ@����curve
	/// </summary>
    private void OnMouseDown()
    {
		RoadEditManager.Instance.SetCurrentBezier(this);
    }
}