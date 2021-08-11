using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEnemy 
{
    public int Health { get; set; }

    public event UnityAction OnDeath;
    public void TakeDamage();

}
