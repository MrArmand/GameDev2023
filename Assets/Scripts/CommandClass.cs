using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Principal;

public abstract class CommandClass
{
    protected IEntity _entity;

    public CommandClass(IEntity entity)
    {
        this._entity = entity;
    }

    public abstract void Execute();
}
