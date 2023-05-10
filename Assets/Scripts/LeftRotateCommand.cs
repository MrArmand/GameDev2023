using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRotateCommand : CommandClass
{
    private float _rotationSpeed;

    public LeftRotateCommand(IEntity entity, float rotationSpeed) : base(entity)
    {
        _rotationSpeed = rotationSpeed;
    }

    public override void Execute()
    {
        _entity.transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
    }
}
