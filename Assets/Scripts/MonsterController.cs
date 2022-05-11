using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MonsterController : NetworkBehaviour
{
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
    public NetworkVariable<Quaternion> Rotation = new NetworkVariable<Quaternion>();

    //This code must be executed SERVER SIDE only
    private Entity _entity;

    private GameObject _target;

    // Start is called before the first frame update
    void Awake()
    {
        _entity = GetComponent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null)
        {
            SearchTarget();
            return;
        }

        transform.Translate(Vector3.Normalize(_target.transform.position - transform.position) * Time.fixedDeltaTime * _entity.moveSpeed);

        SetNetworkParams(transform.position, transform.rotation);
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

        foreach (var entity in entities)
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