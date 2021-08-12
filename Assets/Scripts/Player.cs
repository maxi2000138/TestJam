using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
  [SerializeField] private float MinGroundNormalY = .65f;
    [SerializeField] private float GravityModifier = 1f;
    [SerializeField] private Vector2 Velocity;
    [SerializeField]private float JumpScale = 1;
    [SerializeField] private LayerMask LayerMask;

    protected Vector2 targetVelocity;
    protected bool isOnGround;
    protected Vector2 groundNormal;
    protected Rigidbody2D _rigidbody2d;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;
    protected PlatformEffector2D _platform;
     private bool isTurned;
 private Vector3 _sizes;


	private void OnEnable() {
        _rigidbody2d = GetComponent<Rigidbody2D>();
	}
	private void Start()
    {
        _sizes = transform.localScale;
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(LayerMask);
        contactFilter.useLayerMask = true;

    }

   
    private void Update()
    {
        float movement = Input.GetAxis("Horizontal");
        targetVelocity = new Vector2(movement, 0) * 5;
        if (Input.GetKey(KeyCode.Space) && isOnGround)
        {
            Velocity.y = 5;
        }
            if(movement != 0)
       {
           isTurned = movement > 0;
           ChangeDirection();
       }
    }

	private void FixedUpdate() {
        Velocity += GravityModifier * Physics2D.gravity * Time.deltaTime;
        Velocity.x = targetVelocity.x;

        isOnGround = false;

        Vector2 deltaPosition = Velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y * JumpScale;

        Movement(move, true);
	}

   private void Movement(Vector2 move, bool yMovement) 
   {
        float distance = move.magnitude;

        if (distance > minMoveDistance) {

            int count = _rigidbody2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);

            hitBufferList.Clear();

            for (int i = 0; i < count; i++) {
                if ((hitBuffer[i].normal == Vector2.up && Velocity.y < 0 && yMovement))
                    hitBufferList.Add(hitBuffer[i]);
			}

			for (int i = 0; i < hitBufferList.Count; i++) {
                Vector2 currentNormal = hitBufferList[i].normal;
                if(currentNormal.y > MinGroundNormalY) {
                    isOnGround = true;
					if (yMovement) {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
					}
				}

                float projection = Vector2.Dot(Velocity, currentNormal);
                if(projection < 0) {
                    Velocity = Velocity - projection * currentNormal;
				}

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
			}
		}

        _rigidbody2d.position = _rigidbody2d.position + move.normalized * distance;
	}
       private void ChangeDirection() 
    {
       float _turn = isTurned ?_sizes.x : -_sizes.x;
      Vector3 scale = transform.localScale;
        scale.x = _turn;
       transform.localScale = scale;
   }
}
