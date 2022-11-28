using UnityEngine;
using UnityEngine.UI;



public class CameraMovement : MonoBehaviour
{
    public GameObject Crosshair;
    public float lookSpeedH = 2f;
    public float lookSpeedV = 2f;
    public float ZoomMoveAcceleration = 1.5f;
    public float moveSpeed = 3f;


    private float yaw = 0f;
    private float pitch = 0f;
    private bool ShowCursor = false;
    private void Start()
    {
        Screen.SetResolution(900, 700, false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        ToggleCameraMovement();
        Move();
        lookAndZoom();
    }
    void ToggleCameraMovement()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (ShowCursor == false)
            {
                Crosshair.SetActive(false);
                Cursor.visible = true;
                ShowCursor = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                ShowCursor = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Crosshair.SetActive(true);
            }
        }
    }
    void lookAndZoom()
    {
        if (ShowCursor == false)
        {
            yaw += lookSpeedH * Input.GetAxis("Mouse X");
            pitch -= lookSpeedV * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }
    }
    void Move()
    {
        if (moveSpeed >= 3f)
        {
            moveSpeed += moveSpeed * Input.GetAxis("Mouse ScrollWheel") * ZoomMoveAcceleration;
        }
        else
        {
            moveSpeed = 3f;
        }
        Vector3 locationforrward = Input.GetAxisRaw("Horizontal") / 100 * moveSpeed * transform.right;
        Vector3 locationright = Input.GetAxisRaw("Vertical") / 100 * moveSpeed * transform.forward;
        Vector3 locationUpDown = Input.GetAxisRaw("UpDown") / 100 * moveSpeed * transform.up;
        transform.position += locationforrward + locationright + locationUpDown;
    }
}
