using System.Collections.Generic;
using UnityEngine;

public class BoidMovement : MonoBehaviour
{
    [Header("Fish Entities List")]
    [SerializeField] private ListGameObjectVariable _fishesList;

    [Header("Movement and Perception Settings")]
    [SerializeField] private FloatVariable _turnSpeed;
    [SerializeField] private FloatVariable _viewRadius;
    [SerializeField] private FloatVariable _forwardSpeed;

    [Header("Boid Behavior Weights")]
    [SerializeField] private FloatVariable _weightForward;
    [SerializeField] private FloatVariable _weightCohesion;
    [SerializeField] private FloatVariable _weightSeparation;
    [SerializeField] private FloatVariable _weightAlignment;
    [SerializeField] private FloatVariable _weightAvoidace;

    [Header("Player")]
    [SerializeField] private FloatVariable _playerViewRadius;
    [SerializeField] private Transform _playerTransform;
    public Vector3 velocity { get; private set; }
    private void Start()
    {
        _playerTransform = FindObjectOfType<PlayerMovement>().transform;
    }
    private void FixedUpdate()
    {
        velocity = Vector2.Lerp(velocity, CalculateVelocity(), _turnSpeed.Value * Time.fixedDeltaTime);
        transform.position += velocity * Time.fixedDeltaTime;
        LookRotation();
    }

    private void LookRotation()
    {
        Quaternion lookRotaion = Quaternion.LookRotation(velocity);
        transform.rotation = Quaternion.Slerp(transform.localRotation, lookRotaion, Time.fixedDeltaTime * (_turnSpeed.Value / 2f));
    }

    //private void FaceFront()
    //{
    //    float step = Time.fixedDeltaTime * 5f;
    //    Vector3 newDir = Vector3.RotateTowards(-transform.forward, _rigidbody2D.velocity, step, 0);

    //    float offset = Vector2.SignedAngle(-transform.forward, newDir);
    //    transform.Rotate(Vector3.right, offset);
    //    var rotation = transform.eulerAngles;
    //    if (rotation.x >= -90f && rotation.x <= 90f) transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);
    //    else transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 180);

    //}
    private Vector2 CalculateVelocity()
    {
        List<GameObject> neighboringFishesList = GetNeighboringFishesList();

        Vector2 velocity = (_weightForward.Value * (Vector2)transform.forward
            + _weightCohesion.Value * Cohesion(neighboringFishesList)
            + _weightSeparation.Value * Separation(neighboringFishesList)
            + _weightAlignment.Value * Aligment(neighboringFishesList)
            + _weightAvoidace.Value * Avoidace()
            ).normalized * _forwardSpeed.Value;

        return velocity;
    }

    private List<GameObject> GetNeighboringFishesList()
    {
        List<GameObject> neighboringFishes = new List<GameObject>();

        foreach (var fish in _fishesList.value)
        {
            if (fish == this.gameObject) continue;

            if (Vector2.Distance(transform.position, fish.transform.position) <= _viewRadius.Value)
                neighboringFishes.Add(fish);
        }
        return neighboringFishes;
    }
    private Vector2 Avoidace()
    {
        Vector2 avoidVector = new Vector2();
        avoidVector += RunAway();
        return avoidVector.normalized;
    }
    private Vector2 RunAway()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, _playerTransform.position);
        if (distanceToPlayer <= _playerViewRadius.Value)
        {
            float avoidanceStrength = Mathf.SmoothStep(0.1f, 1f, 1 / distanceToPlayer);
            Vector2 desiredVelocity = (transform.position - _playerTransform.position).normalized * avoidanceStrength * 2f;
            return Vector2.Lerp(velocity, desiredVelocity, Time.fixedDeltaTime * _turnSpeed.Value * 2f);
        }
        else return Vector2.zero;
    }
    #region Rule 1: Cohesion
    private Vector2 Cohesion(List<GameObject> neighboringFishesList)
    {
        Vector2 direction;
        Vector2 centerPos = Vector2.zero;

        foreach (var fish in neighboringFishesList)
            centerPos += (Vector2)fish.transform.position;

        if (neighboringFishesList.Count != 0) centerPos /= neighboringFishesList.Count;
        else centerPos = transform.position;

        direction = (centerPos - (Vector2)transform.position).normalized;
        return direction;
    }
    #endregion
    #region Rule 2: Aligment
    private Vector2 Aligment(List<GameObject> neighboringFishesList)
    {
        Vector2 direction;
        Vector2 centrolVelocity = Vector2.zero;

        foreach (var fish in neighboringFishesList) centrolVelocity += (Vector2)fish.GetComponent<BoidMovement>().velocity;

        if (neighboringFishesList.Count != 0) centrolVelocity /= neighboringFishesList.Count;
        else centrolVelocity = velocity;

        direction = centrolVelocity.normalized;
        return direction;
    }
    #endregion
    #region Rule 3: Separation
    private Vector2 Separation(List<GameObject> neighboringFishesList)
    {
        Vector2 direction = Vector2.zero;

        foreach (var fish in neighboringFishesList)
        {
            Vector2 awayFishVector = (Vector2)transform.position - (Vector2)fish.transform.position;
            float weight = 1f;
            direction += awayFishVector.normalized * weight;
        }

        direction.Normalize();
        return direction;
    }
    #endregion
}
