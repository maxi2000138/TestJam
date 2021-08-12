using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
   [SerializeField] private float _timeToDestroy = 1;
    [SerializeField] private float _speed = 1;
    private void Start()
    {
        Destroy(gameObject, _timeToDestroy);
    }
    private void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.TryGetComponent<IEnemy>(out IEnemy _enemy))
        {
            _enemy.TakeDamage(1);
            Destroy(gameObject);
        }
    }

}
