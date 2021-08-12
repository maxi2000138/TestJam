using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CirlceEnemy : MonoBehaviour, IEnemy
{
 [SerializeField] private float _timeDamageEffect = 0.1f;
	private SpriteRenderer _spriteRenderer;
	private Color _currentColor;
	public event UnityAction OnDeath;

[SerializeField] private int _health = 10;
[SerializeField]   private int _damage = 1;
	

	public int Health { get { return _health; } set { _health = Mathf.Clamp(value,0, 10000); } }

    public int Damage { get{return _damage;} set {_damage = Mathf.Clamp(value,0, 10000); }}

    private void Start() {
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_currentColor = _spriteRenderer.color;
		_health = 10;
	}

	public void TakeDamage(int _damage) {
		Health -= 1;
		if (Health <= 0) 
        {
			OnDeath?.Invoke();
			Destroy(gameObject);
		}
		StartCoroutine(AnimationTakingDamage());
	}

	private IEnumerator AnimationTakingDamage() {
		_spriteRenderer.color = Color.white;
		yield return new WaitForSeconds(_timeDamageEffect);
		_spriteRenderer.color = _currentColor;
	}


	

}
