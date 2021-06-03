using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Buoyancy : MonoBehaviour
{

    Sequence sequence;
    [SerializeField] float height;
    [SerializeField] float delay;
    // Start is called before the first frame update
    void Start()
    {
        sequence = DOTween.Sequence();
        StartBuoyancy();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartBuoyancy()
    {
        sequence.Append(transform.DOLocalMoveY(height, delay)).SetEase(Ease.OutSine);
        sequence.PrependInterval(delay +.1f);
        sequence.Append(transform.DOLocalMoveY(0, delay)).SetEase(Ease.OutSine);
        sequence.PrependInterval(delay +.1f);
        sequence.Append(transform.DOLocalMoveY(-height, delay)).SetEase(Ease.OutSine);
        sequence.PrependInterval(delay +.1f);
        sequence.Append(transform.DOLocalMoveY(0, delay)).SetEase(Ease.OutSine);


        sequence.Play();

        sequence.SetLoops(-1);
    }

    public void StopBuoyancy()
    {
        sequence.Kill();
    }
}
