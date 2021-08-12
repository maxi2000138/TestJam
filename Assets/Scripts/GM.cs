using UnityEngine;

public class GM : MonoBehaviour
{
[SerializeField] private GameObject _winPanel;
	private IEnemy[] _enemies;


	private void OnEnable() {
		_enemies = GetComponentsInChildren<IEnemy>();
		foreach (IEnemy enemy in _enemies) {
			enemy.OnDeath += CheckWin;
		}
	}

	private void OnDisable() {
		foreach (IEnemy enemy in _enemies) {
			enemy.OnDeath -= CheckWin;
		}
	}

	public void CheckWin() {
		foreach (IEnemy enemy in _enemies) {
			if (enemy.Health != 0)
				return;
		}
		WinGame();
	}

	private void WinGame() {
        _winPanel.SetActive(true);
	}
    
}
