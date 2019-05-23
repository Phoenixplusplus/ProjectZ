using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable<T>
{
    void TakeDamage(T damageTaken, bool critical);
}

public interface IKillable
{
    void Killed();
}
