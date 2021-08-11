
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerFire : MonoBehaviour
{
    [SerializeField] private Transform _pistol;
    [SerializeField] private Transform _firePos;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float StartTimeBtwShoots;
    [SerializeField] Player player;
    private float TimeBtwShoots = 0;

	private void Start() {
        TimeBtwShoots = StartTimeBtwShoots;
	}


	void Update()
    {

        if (Input.GetKeyDown(KeyCode.D)) {
            _pistol.eulerAngles = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            _pistol.eulerAngles = new Vector3(0f,0f, 180f);
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            _pistol.eulerAngles = new Vector3(0f, 0f, 90f);
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            _pistol.eulerAngles = new Vector3(0f,0f, -90f);
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) {
            _pistol.eulerAngles = new Vector3(0f, 0f, -45f);
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) {
            _pistol.eulerAngles = new Vector3(0f, 0f, 225f);
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) {
            _pistol.eulerAngles = new Vector3(0f, 0f, 135f);
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) {
            _pistol.eulerAngles = new Vector3(0f, 0f, 45f);
        }

        if (Input.GetKeyDown(KeyCode.RightShift)) {
            Instantiate(_bulletPrefab, _firePos.position, _firePos.rotation);
        }
        else if (Input.GetKey(KeyCode.RightShift)) {
            if (TimeBtwShoots <= 0) {
                Instantiate(_bulletPrefab, _firePos.position, _firePos.rotation);
                TimeBtwShoots = StartTimeBtwShoots;
            }
            else {
                TimeBtwShoots -= Time.deltaTime;
            }
        }
        


        if (Input.GetKeyDown(KeyCode.RightControl)) {
            player.enabled = false;
		}
        else if (Input.GetKeyUp(KeyCode.RightControl)) {
            player.enabled = true;
        }



    }
}
