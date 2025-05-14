using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResourceCollectEffect : MonoBehaviour
{
    [SerializeField] private Image iconImage;

    public void Init(Sprite icon, Vector3 startScreenPos, Vector3 endScreenPos, float duration = .75f)
    {
        iconImage.sprite = icon;
        transform.position = startScreenPos;

        transform.DOJump(endScreenPos, -1, 1, duration).SetEase(Ease.InQuad)
            .OnComplete(() => Destroy(gameObject));
    }
}
