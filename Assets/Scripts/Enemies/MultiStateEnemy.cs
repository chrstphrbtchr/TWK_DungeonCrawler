using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiStateEnemy : Enemy
{
    public enum EnemyState
    {
        Idle,
        GoToPlayer,
        ReturnHome
    }

    public EnemyState state = EnemyState.Idle;

    public abstract void OnAggroBegin();
    public abstract void OnAggroEnd();
}
