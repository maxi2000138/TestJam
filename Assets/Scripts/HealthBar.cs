using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// [ExecuteAlways]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Direction _DrawDirection = Direction.Left;
    [SerializeField] private GameObject _HeartPrefab;
    [SerializeField] private int _HealthCount;
    [SerializeField] private int _DistanceBetweenHealth;

    private Dictionary<GameObject, bool> _HealthPoints = new Dictionary<GameObject, bool>();
    // Переменные, отвечающие за динамическое обновление полосы здоровья
    private Direction _CachedDrawDirection;
    private GameObject _CachedHeartPrefab;
    private int _CachedHealthCount;
    private int _CachedDistanceBetweenHealth;

    private void Start()
    {
        DrawHealthBar();
    }

    private void Update()
    {
        if ((_DrawDirection != _CachedDrawDirection) ||
            (_CachedHeartPrefab != _HeartPrefab) ||
            (_CachedHealthCount != _HealthCount) ||
            (_CachedDistanceBetweenHealth != _DistanceBetweenHealth))
        {
            ClearHealthBar();
            DrawHealthBar();
        }
        if (Input.GetMouseButtonDown(0))
        {
            OnGetDamage(2);
        }
        if (Input.GetMouseButtonDown(1))
        {
            OnGetHealth(10);
        }
    }

    private void OnGetDamage(int damage_capacity)
    {
        if (damage_capacity <= _HealthPoints.Count(x => x.Value))
        {
            // Если выключены не все элементы массива
            if (_HealthPoints.Count(x => !x.Value) != _HealthCount)
            {
                for (int i = 0; i < damage_capacity; i++)
                {
                    var now_point = _HealthPoints.Keys.ToList()[_HealthPoints.Count - i - 1];
                    var count = 0;
                    // Ищем следующий элемент сердца, который не был выключен
                    while (_HealthPoints[now_point] != true)
                    {
                        count += 1;
                        now_point = _HealthPoints.Keys.ToList()[_HealthPoints.Count - i - 1 - count];
                    }
                    _HealthPoints[now_point] = false;
                }
            }
        } 
        else
        {
            // Берём оставшиеся включенные элементы массива и выключаем их
            for (int i = 0; i < _HealthPoints.Count; i++)
            {
                if (_HealthPoints[_HealthPoints.Keys.ToList()[i]])
                {
                    _HealthPoints[_HealthPoints.Keys.ToList()[i]] = false;
                }
            }
        }
        DrawHealthBar();
    }

    private void OnGetHealth(int refill_capacity)
    {
        if (refill_capacity <= _HealthPoints.Count(x => !x.Value))
        {
            // Если включены не все элементы массива
            if (_HealthPoints.Count(x => x.Value) != _HealthCount)
            {
                for (int i = 0; i < refill_capacity; i++)
                {
                    var now_point = _HealthPoints.Keys.ToList()[i];
                    var count = 0;
                    // Ищем следующий элемент сердца, который не был включен
                    while (_HealthPoints[now_point])
                    {
                        count += 1;
                        now_point = _HealthPoints.Keys.ToList()[i + count];
                    }
                    _HealthPoints[now_point] = true;
                }
            }
        }
        else
        {
            // Берём оставшиеся включенные элементы массива и включаем их
            for (int i = 0; i < _HealthPoints.Count; i++)
            {
                if (!_HealthPoints[_HealthPoints.Keys.ToList()[i]])
                {
                    _HealthPoints[_HealthPoints.Keys.ToList()[i]] = true;
                }
            }
        }
        DrawHealthBar();
    }

    private void DrawHealthBar()
    {
        // Если в массив _HealthPoints добавлены все сердца, то отрисовываем сердца, по информации из массива,
        // иначе рисуем сердца и добавляем в массив
        if (_HealthPoints.Count == _HealthCount)
        {
            foreach (var pair in _HealthPoints)
            {
                pair.Key.SetActive(pair.Value);// заменить на изменение спрайта
            }
        }
        else
        {
            for (int i = 0; i < _HealthCount; i++)
            {
                Vector3 position = new Vector3(transform.position.x + (i * _DistanceBetweenHealth * (int)_DrawDirection), transform.position.y);
                var heart = Instantiate(_HeartPrefab, position, Quaternion.identity);
                heart.transform.SetParent(transform, true);
                _CachedDrawDirection = _DrawDirection;
                _CachedHealthCount = _HealthCount;
                _CachedHeartPrefab = _HeartPrefab;
                _CachedDistanceBetweenHealth = _DistanceBetweenHealth;
                _HealthPoints.Add(heart, true);
            }
        }
    }

    private void ClearHealthBar()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        _HealthPoints = new Dictionary<GameObject, bool>();

    }

    // [ExecuteInEditMode]
    // private void DeleteChilds()
    // {
    //     for (int i = 0; i < transform.childCount; i++)
    //     {
    //         DestroyImmediate(transform.GetChild(i).gameObject);
    //     }
    // }
}

public enum Direction
{
    Right = 1,
    Left = -1
}
