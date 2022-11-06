using UnityEngine;
using System.Collections;
using Entities;
using System;

public class PlayerController : MonoBehaviour
{
    public Action<bool> OnFinishGame;
    [Header("PlayerSetings")]
    [SerializeField] private int _pointsReserve;
    [SerializeField] private int _minPointsReserve;
    [Space]
    [SerializeField] private Player _player;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Road _road;
    [SerializeField] private GameObject _finalDoor;
    [SerializeField] private Obstacle _obstacle;
    private BulletPool _bulletPool;
    private float _touchcount = 0f;
    private const float _coolDownTouch = 0.7f;
    private float _leftToCoolDownTouch;

    private void Start()
    {
        _bulletPool = new BulletPool(_bullet, 3, _player.transform);
        _player.SetPointsReserve(_pointsReserve, _minPointsReserve);
        _leftToCoolDownTouch = 0;
    }
    private void FixedUpdate()
    {
        if (_leftToCoolDownTouch <= 0)
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                Touch touch = Input.GetTouch(0);
                _touchcount += touch.deltaTime;
            }
            else if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Stationary)
            {
                Touch touch = Input.GetTouch(0);
                _touchcount += touch.deltaTime;
            }
            else if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
            {
                MakeShot();
                _touchcount = 0f;
                _leftToCoolDownTouch = _coolDownTouch;
            }
        }
        else
        {
            _leftToCoolDownTouch -= Time.deltaTime;
        }
    }
    private void MakeShot()
    {
        Bullet bullet = _bulletPool.GetFreeElement();
        int numberOfPoints;
        if (_touchcount < bullet.GetMinNumberOfPoints() / 10f)
        {
            numberOfPoints = bullet.GetMinNumberOfPoints();
        }
        else if (_touchcount > bullet.GetMaxNumberOfPoints() / 10f)
        {
            numberOfPoints = bullet.GetMaxNumberOfPoints();
        }
        else
        {
            numberOfPoints = (int)(bullet.GetMinNumberOfPoints() * _touchcount);
        }
        if(_player.TakePoints(numberOfPoints))
        {
            _road.ScaleRoad(_player.transform.localScale.x);
            bullet.SetBulletPower(numberOfPoints);
            bullet.Move();
            StartCoroutine(WaitForHit(bullet));
        }
        else
        {
            GameOver(false);
            return;
        }
    }
    private IEnumerator WaitForHit(Bullet bullet)
    {
        yield return new WaitUntil(() => !bullet.gameObject.activeInHierarchy);
        yield return new WaitForSeconds(_obstacle.GetLifeTimeOfInfected() + 0.1f);
        if(_player.Move(_finalDoor.transform.localPosition.z))
        {
            GameOver(true);
        }
    }
    private void GameOver(bool isWin)
    {
        OnFinishGame?.Invoke(isWin);
        StopAllCoroutines();
    }
}
