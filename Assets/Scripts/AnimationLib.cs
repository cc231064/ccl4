using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class AnimationLib : MonoBehaviour
{
    private Tween currentTween;
    private Vector3 scaleOriginal;
    public Vector3 positionOriginal;
    private void Awake()
    {
        scaleOriginal = transform.localScale;
    }

    public Tween Crunch(Vector3 vector)
    {
        vector.Normalize();
        vector *= 0.25f;
        transform.DOMove(vector + new Vector3(positionOriginal.x, transform.position.y, positionOriginal.z), 0.4f).SetEase(Ease.OutBounce)
            .OnComplete(() => transform.DOMove(new Vector3(positionOriginal.x, transform.position.y, positionOriginal.z), 0.4f).SetEase(Ease.InQuint));
        return transform.DOScale(new Vector3(transform.localScale.x * (1 - math.abs(vector.x)), transform.localScale.y * (1 - math.abs(vector.y)) * 1.25f, transform.localScale.z * (1 - math.abs(vector.z))), 0.4f).SetEase(Ease.OutBounce)
            .OnComplete(() => transform.DOScale(scaleOriginal, 0.4f).SetEase(Ease.InQuint));
    }

    public void Spawn(float time)
    {
        transform.DOMove(positionOriginal, time).SetEase(Ease.OutBounce).SetDelay(1f);
        transform.DOScale(scaleOriginal, time).SetEase(Ease.OutBounce).SetDelay(1f);
    }

    public void DoAnimation(Tween anim)
    {
        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill();
        }

        currentTween = anim;
    }

    public Tween HoverHighlight()
    {
        return transform.DOMove(new Vector3(transform.position.x, positionOriginal.y + 0.25f, transform.position.z), 0.4f).SetEase(Ease.InOutQuint);
    }

    public Tween HoverDeHighlight()
    {
        transform.DOScale(scaleOriginal, 0.4f);
        return transform.DOMove(new Vector3(transform.position.x, positionOriginal.y, transform.position.z), 0.1f).SetEase(Ease.InOutQuint);
    }

    public Tween SelectedHighlight()
    {
        return transform.DOScale(new Vector3(transform.localScale.x, scaleOriginal.y * 1.2f, transform.localScale.z), 0.2f).SetEase(Ease.InQuint)
            .OnComplete(() => transform.DOMove(new Vector3(positionOriginal.x, positionOriginal.y + 0.5f, positionOriginal.z), 0.4f).SetEase(Ease.InOutQuint));
    }

    public Tween SelectedDeHighlight()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        return transform.DOScale(new Vector3(transform.localScale.x, scaleOriginal.y, transform.localScale.z), 0.2f).SetEase(Ease.OutBounce)
            .OnComplete(() => transform.DOMove(positionOriginal, 0.4f).SetEase(Ease.OutBounce));
    }

    public Tween Wobble()
    {
        wobbleKillTimer = 0;
        Vector3 rand = Random.onUnitSphere * 0.025f;
        return transform.DOScale(new Vector3(scaleOriginal.x + rand.x, scaleOriginal.y + rand.y * 8, scaleOriginal.z + +rand.z), 0.1f).SetEase(Ease.OutBounce)
            .OnComplete(() => transform.DOScale(scaleOriginal, 0.4f).SetEase(Ease.OutBounce));
    }

    public void JumpSpin()
    {
        transform.DORotate(new Vector3(0, 180 + Random.Range(0,5)*90, 0), 1.5f, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutCubic);
        transform.DOMove(new Vector3(transform.position.x, positionOriginal.y + 2, transform.position.z), 0.5f).SetEase(Ease.OutQuad) 
        .OnComplete(() => transform.DOMove(new Vector3(transform.position.x, positionOriginal.y, transform.position.z), 1f).SetEase(Ease.OutBounce));
    }

    public void BurnDowwn()
    {
        transform.DORotate(new Vector3(0, Random.Range(0, 360), 90), 3f).SetEase(Ease.OutBounce);
        transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutQuad)
            .OnComplete(() => Destroy(gameObject));
    }

    public float Spinning = 0;

    public float wobbleKillTimer;
    void Update()
    {
        wobbleKillTimer += Time.deltaTime;
        transform.Rotate(0, Time.deltaTime * Spinning, 0);
    }
}
