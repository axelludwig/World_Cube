using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    protected float speed;
    protected float gravity;
    protected float startTime = -1;

    private Vector3 startForward;
    private Vector3 startPosition;

    private Entity BulletOriginEntity;

    public void SetUp(Entity _entity)
    {
        BulletOriginEntity = _entity;
        speed = 10;
        gravity = 1;
    }

    private void Start()
    {
        startForward = transform.forward;
        startPosition = transform.position;
        Destroy(this, 20f);
    }

    private void FixedUpdate()
    {
        if (startTime < 0) startTime = Time.time;

        RaycastHit hit;
        float currentTime = Time.time - startTime;
        float prevTime = currentTime - Time.fixedDeltaTime;
        float nextTime = currentTime + Time.fixedDeltaTime;

        Vector3 currentPoint = FindPointOnParabola(currentTime);
        Vector3 nextPoint = FindPointOnParabola(nextTime);

        if (prevTime > 0)
        {
            Vector3 prevPoint = FindPointOnParabola(prevTime);
            if (CastRayBetweenPoints(prevPoint, currentPoint, out hit))
            {
                OnHit(hit);
                return;
            }
        }

        if (CastRayBetweenPoints(currentPoint, nextPoint, out hit))
        {
            OnHit(hit);
        }
    }

    protected void OnHit(RaycastHit hit)
    {
        var entity = hit.transform.gameObject.GetComponentInChildren<Entity>();
        if (entity != null && entity.team != BulletOriginEntity.team)
        {
            BulletOriginEntity.DealDamage(entity);
        }

        Destroy(gameObject);
    }



    private void Update()
    {
        if (startTime < 0) return;
        float currentTime = Time.time - startTime;
        Vector3 currentPoint = FindPointOnParabola(currentTime);
        transform.position = currentPoint;
    }

    private Vector3 FindPointOnParabola(float time)
    {
        Vector3 point = startPosition + (startForward * speed * time);
        Vector3 gravityVec = Vector3.down * gravity * time * time;

        return point + gravityVec;
    }

    private bool CastRayBetweenPoints(Vector3 startPoint, Vector3 endPoint, out RaycastHit hit)
    {
        return Physics.Linecast(startPoint, endPoint, out hit);
    }
}
