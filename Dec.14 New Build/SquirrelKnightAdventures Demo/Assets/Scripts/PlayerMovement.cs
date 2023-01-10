using UnityEngine;

#region Main Script
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody target_object;

    [SerializeField]
    private float playerSpeedMultiplier;

    [SerializeField]
    private float jumpForceMultiplier;

    [SerializeField]
    private LayerMask layermask;

    [SerializeField]
    private Vector3 groundBoxRadiusOffset;

    [SerializeField]
    private Vector3 obsicalBoxRadiusOffset;

    [SerializeField]
    private float wallOffset_Value;

    [SerializeField]
    private bool facingLeft;

    //Debugging

    [SerializeField]
    private bool show_obsical_hit;

    [SerializeField]
    private bool show_ground_hit;

    [SerializeField]
    private bool show_obsical_visual;

    [SerializeField]
    private bool show_ground_visual;

    [SerializeField]
    private bool show_mesh;

    [SerializeField]
    private float maxDistance_ground_check;

    [SerializeField]
    private float maxDistance_obstical_check;

    private Vector3 wallOffset;

    private void FixedUpdate()
    {
        CalculateMovement();
    }
    private void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0 && facingLeft)
        {
            Flip();
        }

        if (horizontalInput < 0 && !facingLeft)
        {
            Flip();
        }

        if (CheckIsGrounded() && Input.GetKey(KeyCode.Space))
        {
            target_object.AddForce(Vector3.up * jumpForceMultiplier, ForceMode.Impulse);
        }

        Vector3 direction = new(horizontalInput, 0, 0);

        if (!CheckIfObstacles(direction))
        {
            transform.Translate(direction * playerSpeedMultiplier * Time.deltaTime);
        }
    }
    public bool CheckIsGrounded()
    {
        Vector3 center = target_object.GetComponent<Collider>().bounds.center;

        float horizontalInput = Input.GetAxis("Horizontal");

        RaycastHit hit_ground;
        bool groundCheck = Physics.BoxCast(center + wallOffset, groundBoxRadiusOffset + target_object.transform.lossyScale / 2,
            Vector3.down, out hit_ground, target_object.transform.rotation, maxDistance_ground_check, layermask, QueryTriggerInteraction.UseGlobal);

        if (groundCheck)
        {
            target_object.velocity = target_object.velocity.RemoveComponent(Physics.gravity);
        }

        if (show_ground_hit)
        {
            Debug.Log(groundCheck);
        }

        return groundCheck;
    }
    private void Flip()
    {
        Vector3 currentRotation = gameObject.transform.localScale;

        Vector3 newRotation = new Vector3(currentRotation.x * -1, currentRotation.y, currentRotation.z);

        gameObject.transform.localScale = newRotation;

        facingLeft = !facingLeft;
    }
    public bool CheckIfObstacles(Vector3 moveDirection)
    {
        wallOffset = Vector3.zero;

        if (moveDirection.x > 0) { moveDirection.x = 1f; wallOffset.x = -wallOffset_Value; }
        if (moveDirection.x < 0) { moveDirection.x = -1f; wallOffset.x = wallOffset_Value; }

        Vector3 center = GetComponent<Collider>().bounds.center;

        RaycastHit hit_obstical;
        bool obstacleCheck = Physics.BoxCast(center, obsicalBoxRadiusOffset + target_object.transform.lossyScale / 2, moveDirection, out hit_obstical,
            transform.rotation, maxDistance_obstical_check, layermask, QueryTriggerInteraction.UseGlobal);

        if (obstacleCheck)
        {
            target_object.velocity = target_object.velocity.RemoveComponent(moveDirection);
        }

        if (show_obsical_hit)
        {
            Debug.Log(obstacleCheck);
        }

        return obstacleCheck;
    }

    private void OnDrawGizmos()
    {
        if (show_ground_visual)
        {
            if (show_mesh)
            {
                target_object.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                target_object.GetComponent<MeshRenderer>().enabled = false;
            }

            Gizmos.color = Color.green;

            Gizmos.DrawCube(target_object.position + wallOffset,
                groundBoxRadiusOffset + target_object.transform.lossyScale);
        }

        if (show_obsical_visual)
        {
            if (show_mesh)
            {
                target_object.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                target_object.GetComponent<MeshRenderer>().enabled = false;
            }

            Gizmos.color = Color.blue;

            Gizmos.DrawCube(target_object.position,
                obsicalBoxRadiusOffset + target_object.transform.lossyScale + new Vector3(2, 0, 0));
        }
    }
}

public static class VectorExtensions
{
    public static Vector3 RemoveComponent(this Vector3 vector, Vector3 direction)
    {
        direction = direction.normalized;

        return vector - direction * Vector3.Dot(vector, direction);
    }
}

#endregion
/*
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
*/