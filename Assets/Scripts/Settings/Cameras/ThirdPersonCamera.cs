using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform _targer;
    [SerializeField] private float distamce = 5f;
    [SerializeField] private float height = 1f;
    [SerializeField] private float SmoothSpeed = 1f;

    private Vector3 velocity = Vector3.zero;
    
    private void LateUpdate()
    {
        if (_targer == null)
        {
            return;
        }
        
        Vector3 targetPosition = _targer.position + _targer.up * height - _targer.forward * distamce;
        //SmoothDamp is a function that smoothly interpolates between two points
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothSpeed);
        transform.LookAt(_targer.position);
    }
}
