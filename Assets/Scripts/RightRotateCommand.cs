using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightRotateCommand : CommandClass
{
    private float _rotationSpeed;

    public RightRotateCommand(IEntity entity, float rotationSpeed) : base(entity)
    {
        _rotationSpeed = rotationSpeed;
    }

    public override void Execute()
    {
        _entity.transform.Rotate(Vector3.back, _rotationSpeed * Time.deltaTime);
    }
}
