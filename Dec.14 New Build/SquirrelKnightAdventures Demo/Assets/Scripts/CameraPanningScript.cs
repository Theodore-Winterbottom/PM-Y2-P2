using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

#region Main Script
public class CameraPanningScript : MonoBehaviour
{
    [SerializeField] Transform target_object;

    [SerializeField] float distance_from_camera_cutoff;

    [SerializeField] float distance_from_camera;

    [SerializeField] float camera_speed_multiplier;

    [SerializeField] float distance_from_center_cutoff;

    private bool InRange;
    private void FixedUpdate()
    {
        calculateDistance();
    }
    private void calculateDistance ()
    {
        if (target_object != null)
        {
            distance_from_camera = target_object.position.x - GetComponent<Transform>().position.x;

            if (Mathf.Abs(distance_from_camera) <= distance_from_center_cutoff)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }

            if (Mathf.Abs(distance_from_camera) > distance_from_camera_cutoff)
            {
                InRange = true;
            }
            else
            {
                InRange = false;
            }

            if (InRange)
            {
                panCamera();
            }
        }
        
    }
    private void panCamera()
    {
        Vector3 move_direction = new Vector3(distance_from_camera, transform.position.y, transform.position.z);

        GetComponent<Rigidbody>().velocity = (move_direction * camera_speed_multiplier);
    }
}
#endregion

#region Editor Region
[CustomEditor(typeof(CameraPanningScript))]
public class CameraEditor : Editor
{
    SerializedProperty target_object;
    SerializedProperty distance_from_camera_cutoff;
    SerializedProperty distance_from_camera;
    SerializedProperty camera_speed_multiplier;
    SerializedProperty distance_from_center_cutoff;
    void OnEnable()
    {
        target_object = serializedObject.FindProperty("target_object");
        distance_from_camera_cutoff = serializedObject.FindProperty("distance_from_camera_cutoff");
        distance_from_camera = serializedObject.FindProperty("distance_from_camera");
        camera_speed_multiplier = serializedObject.FindProperty("camera_speed_multiplier");
        distance_from_center_cutoff = serializedObject.FindProperty("distance_from_center_cutoff");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.indentLevel = 10;

        EditorGUILayout.Space(5f);

        EditorGUILayout.LabelField("Camera Panning Script", EditorStyles.boldLabel);
        
        EditorGUI.indentLevel = 0;

        EditorGUILayout.Space(5f);
        EditorGUILayout.LabelField("Target Object", EditorStyles.boldLabel);
        EditorGUILayout.Space(7f);

        EditorGUILayout.ObjectField(target_object, GUIContent.none);
        EditorGUILayout.Space(15f);
        EditorGUILayout.LabelField("Distance From Camera Cutoff"
            + "                                   Current Distance: " +
            (Mathf.Abs(Mathf.Round(distance_from_camera.floatValue * 100) / 100)).ToString(), EditorStyles.boldLabel);

        EditorGUILayout.Space(7f);
        EditorGUILayout.Slider(distance_from_camera_cutoff, 0, 20, GUIContent.none);

        EditorGUILayout.Space(15f);
        EditorGUILayout.LabelField("Camera Speed Multiplier", EditorStyles.boldLabel);
        EditorGUILayout.Space(7f);
        EditorGUILayout.Slider(camera_speed_multiplier, 0, 2, GUIContent.none);

        EditorGUILayout.Space(15f);
        EditorGUILayout.LabelField("Distance From Center Cutoff", EditorStyles.boldLabel);
        EditorGUILayout.Space(7f);
        EditorGUILayout.Slider(distance_from_center_cutoff, 0, 2, GUIContent.none);

        EditorGUILayout.Space(5f);

        serializedObject.ApplyModifiedProperties();
    }
} 
#endregion
