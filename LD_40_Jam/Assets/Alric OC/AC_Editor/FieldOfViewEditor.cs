using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (AC_FieldOfView))]
public class FieldOfViewEditor : Editor 
{
    void OnSceneGUI()
    {
        AC_FieldOfView fow = (AC_FieldOfView)target;
        Handles.color = Color.black;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

        Handles.color = Color.red;
        foreach (Transform VisibleTarget in fow.VisibleTargets)
        {
            Handles.DrawLine(fow.transform.position, VisibleTarget.position);
        }
    }
}