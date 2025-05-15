using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResourceCollectEffect : MonoBehaviour
{
    [SerializeField] private Image iconImage;

    public void Init(Sprite icon, Vector3 startScreenPos, Vector3 endScreenPos, float duration = .5f)
    {
        iconImage.sprite = icon;
        transform.position = startScreenPos;
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, .1f).OnComplete(() =>
        transform.DOJump(endScreenPos, -1, 1, duration).SetEase(Ease.InQuad)
            .OnComplete(() => transform.DOScale(0, .1f).OnComplete(() => Destroy(gameObject))));
    }
}
