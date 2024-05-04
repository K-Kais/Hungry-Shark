using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed;
    private Vector3 _initialMousePos;
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _initialMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _rb.velocity = Vector3.zero;
        }
        if (Input.GetMouseButton(0))
        {
            _rb.velocity = (Input.mousePosition - _initialMousePos).normalized * _speed;
        }
    }
    private void FixedUpdate()
    {
        LookRotation();
    }
    private void LookRotation()
    {
        if (_rb.velocity.sqrMagnitude > 0.001f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(_rb.velocity, Vector3.up);
            Quaternion additionalRotation = Quaternion.Euler(0, -90f, 0);
            Quaternion targetRotation = lookRotation * additionalRotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 8f);
        }
    }

    //private void FaceFront()
    //{
    //    float step = Time.fixedDeltaTime * 15f;
    //    Vector3 newDir = Vector3.RotateTowards(transform.right, _rb.velocity, step, 0);

    //    float zOffset = Vector2.SignedAngle(transform.right, newDir);
    //    transform.rotation *= Quaternion.AngleAxis(zOffset, Vector3.forward);
    //}
}
