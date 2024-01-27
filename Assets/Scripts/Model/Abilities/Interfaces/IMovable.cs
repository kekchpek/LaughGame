﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LaughGame.Assets.Scripts.Model.Abilities
{
    public interface IMovable
    {
        Transform MovableTransform { get; }
        bool SelfMovementEnabled { get; }

        Vector2 Direction { get; }
        Rigidbody2D MovableRigidBody { get; }

        void Move(Vector2 movementVelocity);

    }
}