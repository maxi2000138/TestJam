using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy = 1;
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _rayCastDistance = 1;
    [SerializeField] private ContactFilter2D contactFilter2D;
    private RaycastHit2D[] raycastHit2D = new RaycastHit2D[1];
    void Start()
    {
        StartCoroutine(Destroyer());
    }

    IEnumerator Destroyer() {
        yield return new WaitForSeconds(_timeToDestroy);
        Destroy(this.gameObject);
	}

    
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * _speed * Time.fixedDeltaTime);

        int hits = Physics2D.Raycast(transform.position, Vector2.right, contactFilter2D, raycastHit2D, _rayCastDistance);
        if (hits > 0) {
            if (raycastHit2D[0].collider.CompareTag("Enemy"))
                raycastHit2D[0].collider.GetComponent<IEnemy>().TakeDamage();
             
            Destroy(gameObject);
        }
            
        
    }

}
