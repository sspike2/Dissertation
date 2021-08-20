using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AutoScale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Sequence s = DOTween.Sequence();

        s.Append(transform.DOScale(new Vector3(.9f, .9f, .9f), .25f));
        s.AppendInterval(.25f);
        s.Append(transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), .25f));
        s.SetLoops(-1);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
