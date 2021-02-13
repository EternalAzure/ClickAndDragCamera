using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam; //Add in Unity editor
    [SerializeField] private Transform target; //Add in Unity editor
    [SerializeField] private float distanceToTarget = 6; //Configure in Unity editor

    private Vector3 previousPosition;


    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
            float rotationAroundXAxis = direction.y * 180; // camera moves vertically

            cam.transform.position = target.position;

            cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); // <— This is what makes it work!

            cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

            previousPosition = newPosition;
        }
        else
        {
            if (Input.mouseScrollDelta.y < 0)
            {
                float newDist = distanceToTarget - Input.mouseScrollDelta.y;
                cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));
                distanceToTarget = newDist;
            }
            else if (Input.mouseScrollDelta.y > 0)
            {
                float newDist = distanceToTarget - Input.mouseScrollDelta.y;
                cam.transform.Translate(new Vector3(0, 0, distanceToTarget));
                distanceToTarget = newDist;
            }

            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            cam.transform.position = target.position;
            cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

            previousPosition = newPosition;
        }
    }
}
