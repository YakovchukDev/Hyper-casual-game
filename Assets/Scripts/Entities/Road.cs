using UnityEngine;
using DG.Tweening;

public class Road : MonoBehaviour
{
    [SerializeField] GameObject _mainRoad;
    [SerializeField] GameObject _circleUnderPlayer;

    public void ScaleRoad(float scaleValue)
    {
        _mainRoad.transform.DOScaleX(scaleValue, 0.3f);
        _circleUnderPlayer.transform.DOScale(scaleValue / 8f, 0.3f);
    }
}
