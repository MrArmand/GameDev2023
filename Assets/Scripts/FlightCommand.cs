using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class FlightCommand : CommandClass
{
    private float _thrust;
    private Rigidbody2D _rb2D;

    public FlightCommand(IEntity entity, float thrust, Rigidbody2D rb2D) : base(entity)
    {
        _thrust = thrust;
        _rb2D = rb2D;

    }

    public override void Execute()
    {
        Vector2 thrustVector = _entity.transform.up;
        _rb2D.AddForce(_thrust * thrustVector * Time.fixedDeltaTime);
    }
}

