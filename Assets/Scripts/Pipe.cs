using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using DG.Tweening;

[ExecuteInEditMode]
public class Pipe : MonoBehaviour
{

    public PipeType currentPipeType;

    public Image pipeImg, waterImg;

    public PipeManager pipeManager;


    bool isFilled;

    public Pipe north_pipe, east_pipe, west_pipe, south_pipe;

    private void OnEnable()
    {
        pipeManager = FindObjectOfType<PipeManager>();
        // pipeManager.AddPipe(this);

    }

    // Start is called before the first frame update
    void Start()
    {

        // InvokeRepeating(nameof(RotatePipe), 2, 2);
    }


    public void RotatePipe()
    {
        pipeImg.transform.RotateAround(transform.position, Vector3.forward, 90);
        pipeManager.CheckBoardStart();
    }

    public void ToggleWaterImg(bool enabled, bool isHorizontal, bool isFiltered = false)
    {
        waterImg.color = isFiltered ? Color.white : Color.green;
        waterImg.fillMethod = isHorizontal ? Image.FillMethod.Horizontal : Image.FillMethod.Vertical;

        if(name == "01")
        {
            int a = 9;
        }

        if (!isFilled && enabled)
        {
            isFilled = true;
            DOTween.To(() => waterImg.fillAmount, x => waterImg.fillAmount = x, 1, .25f);
        }
        else if (isFilled && !enabled)
        {
            isFilled = false;
            DOTween.To(() => waterImg.fillAmount, x => waterImg.fillAmount = x, 0, .25f);
        }


        // waterImg.fillAmount = enabled ? 1 : 0;
    }



    public void ToggleWater()
    {
        waterImg.fillAmount = 0;
    }


    // Update is called once per frame
    void Update()
    {

    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Pipe))]
public class PipeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Pipe pipe = (Pipe)target;

        if (GUILayout.Button("Rotate"))
        {
            pipe.RotatePipe();
        }

        if (GUILayout.Button("AssignPipe"))
        {
            switch (pipe.currentPipeType)
            {
                case PipeType.Horizontal:
                    pipe.pipeImg.sprite = pipe.pipeManager.horizontalPipe;
                    pipe.waterImg.sprite = pipe.pipeManager.horizontalPipeWater;
                    break;

                case PipeType.CurveLeft:
                    pipe.pipeImg.sprite = pipe.pipeManager.curveleftPipe;
                    pipe.waterImg.sprite = pipe.pipeManager.curveLeftWater;
                    break;

                case PipeType.CurveRight:
                    pipe.pipeImg.sprite = pipe.pipeManager.curveRight;
                    pipe.waterImg.sprite = pipe.pipeManager.curveRightWater;
                    break;
            }
        }

    }
}
#endif