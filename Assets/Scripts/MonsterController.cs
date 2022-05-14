using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class MonsterController : NetworkBehaviour
{
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
    public NetworkVariable<Quaternion> Rotation = new NetworkVariable<Quaternion>();

    //This code must be executed SERVER SIDE only
    private Monster _entity;

    private GameObject _target;

    public bool IsCasting = false;
    private float _currentCastTime = 0;

    // Start is called before the first frame update
    void Awake()
    {
        _entity = GetComponent<Monster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null)
        {
            SearchTarget();
            return;
        }

        if (IsCasting)
        {
            _currentCastTime -= Time.deltaTime;
            if (_currentCastTime < 0)
            {
                IsCasting = false;
            }
            return;
        }

        transform.LookAt(_target.transform);

        if (Vector3.Distance(transform.position, _target.transform.position) < _entity.Range)
        {
            StartCast();
        }

        transform.position += transform.forward * Time.deltaTime * _entity.moveSpeed;

        SetNetworkParams(transform.position, transform.rotation);
    }

    private void StartCast()
    {
        IsCasting = true;
        _currentCastTime = _entity.CastTime;

        _entity.DealDamage(_target.GetComponent<Entity>());
    }

    private void SetNetworkParams(Vector3 newPos, Quaternion newRot)
    {
        Position.Value = Vector3.Lerp(transform.position, newPos, 50f * Time.deltaTime);
        Rotation.Value = newRot;
    }

    public void SearchTarget()
    {
        var entities = FindObjectsOfType<Entity>();
        float minDistance = _entity.AggroDistance;

        foreach (var entity in entities.Where(x => x.team != _entity.team))
        {
            GameObject gameObjectEntity = entity.gameObject;
            float distance = Vector3.Distance(gameObjectEntity.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                _target = gameObjectEntity;
            }
        }
    }
}
