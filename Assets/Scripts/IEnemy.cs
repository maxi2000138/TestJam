using UnityEngine.Events;

public interface IEnemy 
{
    int Damage {get; set; }
    int Health { get; set; }

     event UnityAction OnDeath;
     void TakeDamage(int _damage);

}
