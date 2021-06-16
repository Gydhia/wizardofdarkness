using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MathsFunctions
{
    public static List<Vector3> GetBezierCurve(float height, float length)
    {
        Vector3[] linePoints = new Vector3[3];
        linePoints[0] = new Vector3(0f, 0f, 0f);
        linePoints[1] = new Vector3(0f, height, length / 2);
        linePoints[2] = new Vector3(0f, 0f, length);

        List<Vector3> preparedLinePoints = new List<Vector3>();

        preparedLinePoints.Add(linePoints[0]);
        for (int i = 1; i < linePoints.Length - 1; i++)
        {
            float mag1 = Vector3.Magnitude(linePoints[i] - linePoints[i - 1]);
            preparedLinePoints.Add(Vector3.Lerp(linePoints[i - 1], linePoints[i], mag1));

            preparedLinePoints.Add(linePoints[i]);

            float mag2 = Vector3.Magnitude(linePoints[i] - linePoints[i + 1]);
            preparedLinePoints.Add(Vector3.Lerp(linePoints[i], linePoints[i + 1], mag2));
        }
        preparedLinePoints.Add(linePoints[linePoints.Length - 1]);


        List<Vector3> finalPointList = new List<Vector3>();
        finalPointList.Add(preparedLinePoints.First());
        for (int i = 1; i < preparedLinePoints.Count - 2; i += 3)
        {
            Vector3 point1 = preparedLinePoints[i];
            Vector3 point2 = preparedLinePoints[i + 1];
            Vector3 point3 = preparedLinePoints[i + 2];

            List<Vector3> pointList = new List<Vector3>();
            for (float ratio = 0; ratio <= 1; ratio += 1.0f / 100f)
            {
                var tangentLine1 = Vector3.Lerp(point1, point2, ratio);
                var tangentLine2 = Vector3.Lerp(point2, point3, ratio);
                var bezierPoint = Vector3.Lerp(tangentLine1, tangentLine2, ratio);
                pointList.Add(bezierPoint);
            }
            finalPointList.AddRange(pointList);
        }
        finalPointList.Add(preparedLinePoints.Last());

        return finalPointList;
    }
}
