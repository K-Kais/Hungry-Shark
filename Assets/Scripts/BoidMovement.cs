﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GridBrushBase;

public class BoidMovement : MonoBehaviour
{
    [SerializeField] ListGameObjectVariable _fishes;
    [SerializeField] FloatVariable _viewRadius;
    [SerializeField] FloatVariable _weightForward;
    [SerializeField] FloatVariable _forwardSpeed;
    [SerializeField] FloatVariable _backwardSpeed;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        _rigidbody2D.velocity = CalculateVelocity();
        transform.rotation = Quaternion.LookRotation(_rigidbody2D.velocity);
        //FaceFront();
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
        List<GameObject> neighboringFishes = GetNeighboringFishes();
        Vector2 velocity = (_weightForward.Value * (Vector2)transform.forward
            + 0.8f * Cohesion(neighboringFishes)
            + 1f * Separation(neighboringFishes)
            + 0.5f * Aligment(neighboringFishes)
            ).normalized * _forwardSpeed.Value;

        return velocity;
    }

    private List<GameObject> GetNeighboringFishes()
    {
        List<GameObject> neighboringFishes = new List<GameObject>();

        foreach (var fish in _fishes.value)
        {
            if (fish == this.gameObject) continue;

            if (Vector2.Distance(transform.position, fish.transform.position) <= _viewRadius.Value)
                neighboringFishes.Add(fish);
        }
        return neighboringFishes;
    }
    #region Rule 1: Cohesion
    private Vector2 Cohesion(List<GameObject> neighboringFishes)
    {
        Vector2 direction;
        Vector2 centerPos = Vector2.zero;

        foreach (var fish in neighboringFishes)
            centerPos += (Vector2)fish.transform.position;

        if (neighboringFishes.Count != 0) centerPos /= neighboringFishes.Count;
        else centerPos = transform.position;

        direction = (centerPos - (Vector2)transform.position).normalized;
        return direction;
    }
    #endregion
    #region Rule 2: Aligment
    private Vector2 Aligment(List<GameObject> neighboringFishes)
    {
        Vector2 direction;
        Vector2 centrolVelocity = Vector2.zero;

        foreach (var fish in neighboringFishes) centrolVelocity += fish.GetComponent<Rigidbody2D>().velocity;

        if (neighboringFishes.Count != 0) centrolVelocity /= neighboringFishes.Count;
        else centrolVelocity = _rigidbody2D.velocity;

        direction = centrolVelocity.normalized;
        return direction;
    }
    #endregion
    #region Rule 3: Separation
    private Vector2 Separation(List<GameObject> neighboringFishes)
    {
        Vector2 direction = Vector2.zero;

        foreach (var fish in neighboringFishes)
        {
            Vector2 awayFishVector = transform.position - fish.transform.position;
            float weight = 1f;
            direction += awayFishVector.normalized * weight;
        }

        direction.Normalize();
        return direction;
    }
    #endregion
}
