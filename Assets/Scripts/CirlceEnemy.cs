using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CirlceEnemy : MonoBehaviour, IEnemy
{
	[SerializeField] private float TimeDamageEffect = 0.1f;
	private SpriteRenderer spriteRenderer;
	private Color curColor;
	public event UnityAction OnDeath;

	private int _health;

	

	public int Health { get { return _health; } set { _health = Mathf.Clamp(value,0, 10000); } }





	private void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		curColor = spriteRenderer.color;
		_health = 10;
		Debug.Log(Health);
	}

	public void TakeDamage() {
		Health -= 1;
		Debug.Log(Health);
		if (Health == 0) {
			OnDeath?.Invoke();
			Destroy(gameObject);
		}
		StartCoroutine(AnimDamage());
	}

	IEnumerator AnimDamage() {
		spriteRenderer.color = Color.white;
		yield return new WaitForSeconds(TimeDamageEffect);
		spriteRenderer.color = curColor;
	}


	

}
