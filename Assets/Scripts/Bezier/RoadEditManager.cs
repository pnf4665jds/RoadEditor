using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadEditManager : MonoSingleton<RoadEditManager>
{
    public BezierCurve CurrentBezier { get; private set; }

    public void SetCurrentBezier(BezierCurve curve)
    {
        if (CurrentBezier)
        {
            CurrentBezier.SetShowControlPoints(false);
            CurrentBezier.SetShowAxis(false);
        }

        CurrentBezier = curve;
        CurrentBezier.SetShowControlPoints(true);
        CurrentBezier.SetShowAxis(true);
    }
}