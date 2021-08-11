using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    [SerializeField] private GameObject WinPanel;
	private IEnemy[] enemies;


	private void OnEnable() {
		enemies = GetComponentsInChildren<IEnemy>();
		foreach (IEnemy item in enemies) {
			item.OnDeath += CheckWin;
		}
	}

	private void OnDisable() {
		foreach (IEnemy item in enemies) {
			item.OnDeath -= CheckWin;
		}
	}

	void CheckWin() {
		foreach (IEnemy item in enemies) {
			if (item.Health != 0)
				return;
		}
		WinGame();
	}

	void WinGame() {
        WinPanel.SetActive(true);
	}
    
}
