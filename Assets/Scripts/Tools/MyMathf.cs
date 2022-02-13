using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMathf
{
    /// <summary>
    /// 計算滑鼠射線到任一點的距離
    /// </summary>
    /// <param name="ray"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static float DistanceRay2Point(Ray ray, Vector3 p)
    {
        float a = Mathf.Deg2Rad * Vector3.Angle(ray.direction, p - ray.origin);
        float d = Vector3.Distance(p, ray.origin);
        return Mathf.Sin(a) * d;
    }
}
