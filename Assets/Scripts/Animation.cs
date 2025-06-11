using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using System.Collections;
using System;

public class Animation : MonoBehaviour
{
    Snapper snap;
    private Tween currentTween;
    private Vector3 scaleOriginal;
    public Vector3 positionOriginal;
    private void Awake()
    {
        snap = GetComponent<Snapper>();
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

    IEnumerator Spawn(Vector3 vector, float time, float delay)
    {
        Vector3 endSize = transform.localScale;
        transform.localScale = new Vector3(0, 1, 0);
        transform.position += vector;
        yield return new WaitForSeconds(delay);
        transform.DOMove(transform.position - vector, time).SetEase(Ease.OutBounce);
        transform.DOScale(endSize, time).SetEase(Ease.OutBounce);
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
}
