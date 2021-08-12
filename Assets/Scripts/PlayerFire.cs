
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerFire : MonoBehaviour
{
    [SerializeField] private Transform _pistol;
    [SerializeField] private Transform _firePos;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _startTimeBtwShoots;
    [SerializeField] private Player _player;
    private float _timeBtwShoots = 0;

	private void Start() 
    {
        ChangeBulletCharge();
	}
    private void ChangeBulletCharge() =>  _timeBtwShoots = _startTimeBtwShoots;
    private void ChangeRotation(Vector3 _vector) => _pistol.eulerAngles = _vector;
    private void CreateBullet() => Instantiate(_bulletPrefab, _firePos.position, _firePos.rotation);
     
	private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) {
            ChangeRotation(Vector3.zero);
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            ChangeRotation(new Vector3(0f,0f, 180f));
        }
        if (Input.GetKeyDown(KeyCode.W)) {
           ChangeRotation(new Vector3(0f, 0f, 90f));
        }
        if (Input.GetKeyDown(KeyCode.S)) {
             ChangeRotation(new Vector3(0f, 0f, 90f));
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) {
            ChangeRotation( new Vector3(0f, 0f, -45f));
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) {
             ChangeRotation( new Vector3(0f, 0f, 225f));
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) {
             ChangeRotation(new Vector3(0f, 0f, 135f));
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) {
           ChangeRotation(new Vector3(0f, 0f, 45f));
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            CreateBullet();
        }
        else if (Input.GetKey(KeyCode.RightShift)) {
            if (_timeBtwShoots <= 0) 
            {
                CreateBullet();
                ChangeBulletCharge();
            }
            else 
            {
                _timeBtwShoots -= Time.deltaTime;
            }
        }
        


        if (Input.GetKeyDown(KeyCode.RightControl))
            _player.enabled = false;
        else if (Input.GetKeyUp(KeyCode.RightControl)) {
            _player.enabled = true;
       }



    }
}
