using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using DG.Tweening;

public enum PipeEntry { north, east, south, west }
public enum PipeType { Horizontal, CurveLeft, CurveRight, Plus, Start, End, Filter }
[ExecuteInEditMode]
public class PipeManager : MonoBehaviour
{

    [SerializeField] GameObject[] LevelObj;
    [SerializeField] int currentLevel;

    [SerializeField] GameObject LevelSelectObj;

    [SerializeField] GameObject currentLevelObj;
    public List<Pipe> allPipes = new List<Pipe>();

    [SerializeField] bool[] levelsCompleted = new bool[3];
    [SerializeField] Image[] LevelCompletedIMG;
    Pipe StartPipe;

    public Sprite horizontalPipe, horizontalPipeWater, curveleftPipe, curveLeftWater, curveRight, curveRightWater;

    int waterProgress = 0, currentWaterProgress = 0;


    private void OnEnable()
    {
        // AddPipes();
    }
    // Start is called before the first frame update
    void Start()
    {
        // yield return new WaitForSeconds(.1f);
        // SetupNeighbours();
        // CheckBoardStart();
        // ClearBoard();

    }


    public void LevelSelect(int id)
    {
        currentLevel = id;
        allPipes.Clear();
        currentLevelObj = Instantiate(LevelObj[currentLevel], Vector3.zero, Quaternion.identity);
        currentLevelObj.transform.SetParent(transform, false);

        // LevelObj[currentLevel].SetActive(true);
        AddPipes();
        LevelSelectObj.SetActive(false);

        currentLevelObj.transform.DOScale(Vector3.one, .25f);
    }



    public void AddPipes()
    {

        bool startPipeAssigned = false;
        var pipes = currentLevelObj.GetComponentsInChildren<Pipe>();

        for (int i = 0; i < pipes.Length; i++)
        {

            if (!startPipeAssigned)
            {
                if (pipes[i].name == "00")
                {
                    StartPipe = pipes[i];
                    startPipeAssigned = true;
                }
            }

            if (!allPipes.Contains(pipes[i]))
                allPipes.Add(pipes[i]);

        }
        SetupNeighbours();
        ClearBoard();
        // currentPipe
    }

    public void SetupNeighbours()
    {
        for (int i = 0; i < allPipes.Count; i++)
        {
            SetupNeighbors(allPipes[i]);
        }
    }
    public void SetupNeighbors(Pipe CurrentPipe)
    {

        var xNode = int.Parse(CurrentPipe.name[0].ToString());
        var yNode = int.Parse(CurrentPipe.name[1].ToString());

        int west = xNode - 1;
        int east = xNode + 1;
        int south = yNode - 1;
        int north = yNode + 1;

        if (west > 0)
        {
            string tmpname = west.ToString() + yNode.ToString();
            for (int i = 0; i < allPipes.Count; i++)
            {
                if (allPipes[i].name == tmpname)
                {
                    CurrentPipe.west_pipe = allPipes[i];
                    break;
                }
            }
        }

        if (east < 9)
        {
            string tmpname = east.ToString() + yNode.ToString();
            for (int i = 0; i < allPipes.Count; i++)
            {
                if (allPipes[i].name == tmpname)
                {
                    CurrentPipe.east_pipe = allPipes[i];
                    break;
                }
            }
        }

        if (north > 0)
        {
            string tmpname = xNode.ToString() + south.ToString();
            for (int i = 0; i < allPipes.Count; i++)
            {
                if (allPipes[i].name == tmpname)
                {
                    CurrentPipe.north_pipe = allPipes[i];
                    break;
                }
            }
        }

        if (south < 4)
        {
            string tmpname = xNode.ToString() + north.ToString();
            for (int i = 0; i < allPipes.Count; i++)
            {
                if (allPipes[i].name == tmpname)
                {
                    CurrentPipe.south_pipe = allPipes[i];
                    break;
                }
            }
        }
    }

    void ClearBoard()
    {
        for (int i = 0; i < allPipes.Count; i++)
        {

            allPipes[i].ToggleWaterImg(false, true);
            // allPipes[i].ToggleWater();
        }
    }

    public void CheckBoardStart()
    {
        // if (waterProgress < currentWaterProgress)
        ClearBoard();
        waterProgress = currentWaterProgress;
        currentWaterProgress = 0;
        CheckBoard(StartPipe, PipeEntry.north);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckBoard(Pipe currentPipe, PipeEntry entrySide, bool isFiltered = false)
    {
        if (currentPipe == null) return;

        var angles = currentPipe.pipeImg.transform.rotation.eulerAngles.z;
        var anglesAbs = Mathf.Abs(angles);
        currentWaterProgress++;

        if (currentPipe.name.StartsWith("73"))
        {
            int a = 0;
        }
        switch (currentPipe.currentPipeType)
        {
            case PipeType.Start:
                CheckBoard(currentPipe.south_pipe, PipeEntry.north, isFiltered);
                break;

            case PipeType.Horizontal:
                if (anglesAbs == 90 || anglesAbs == 270)
                {
                    if (entrySide == PipeEntry.north)
                    {
                        CheckBoard(currentPipe.south_pipe, PipeEntry.north, isFiltered);
                        currentPipe.ToggleWaterImg(true, false, isFiltered);
                    }
                    else if (entrySide == PipeEntry.south)
                    {
                        CheckBoard(currentPipe.north_pipe, PipeEntry.south, isFiltered);
                        currentPipe.ToggleWaterImg(true, false, isFiltered);
                    }
                }
                else if (anglesAbs == 180 || anglesAbs == 0)
                {
                    if (entrySide == PipeEntry.east)
                    {
                        CheckBoard(currentPipe.west_pipe, PipeEntry.east, isFiltered);
                        currentPipe.ToggleWaterImg(true, true, isFiltered);
                    }
                    else if (entrySide == PipeEntry.west)
                    {
                        CheckBoard(currentPipe.east_pipe, PipeEntry.west, isFiltered);
                        currentPipe.ToggleWaterImg(true, true, isFiltered);
                    }
                }
                break;

            case PipeType.CurveLeft:
                if (angles == 0)
                {
                    if (entrySide == PipeEntry.east)
                    {
                        currentPipe.ToggleWaterImg(true, true, isFiltered);
                        CheckBoard(currentPipe.south_pipe, PipeEntry.north, isFiltered);
                    }
                    else if (entrySide == PipeEntry.south)
                    {
                        currentPipe.ToggleWaterImg(true, true, isFiltered);
                        CheckBoard(currentPipe.east_pipe, PipeEntry.west, isFiltered);
                    }
                }
                else if (angles == 90)
                {
                    if (entrySide == PipeEntry.west)
                    {
                        CheckBoard(currentPipe.north_pipe, PipeEntry.south, isFiltered);
                        currentPipe.ToggleWaterImg(true, true, isFiltered);
                    }
                    else if (entrySide == PipeEntry.north)
                    {
                        CheckBoard(currentPipe.east_pipe, PipeEntry.west, isFiltered);
                        currentPipe.ToggleWaterImg(true, true, isFiltered);
                    }
                }
                else if (anglesAbs == 180)
                {
                    if (entrySide == PipeEntry.north)
                    {
                        CheckBoard(currentPipe.west_pipe, PipeEntry.east, isFiltered);
                        currentPipe.ToggleWaterImg(true, true, isFiltered);
                    }
                    else if (entrySide == PipeEntry.east)
                    {
                        CheckBoard(currentPipe.north_pipe, PipeEntry.south, isFiltered);
                        currentPipe.ToggleWaterImg(true, true, isFiltered);
                    }
                }
                else if (angles == 270)
                {


                    if (entrySide == PipeEntry.west)
                    {
                        CheckBoard(currentPipe.south_pipe, PipeEntry.north, isFiltered);
                        currentPipe.ToggleWaterImg(true, true, isFiltered);
                    }
                    else if (entrySide == PipeEntry.south)
                    {
                        CheckBoard(currentPipe.west_pipe, PipeEntry.east, isFiltered);
                        currentPipe.ToggleWaterImg(true, true, isFiltered);
                    }
                }
                break;



            case PipeType.CurveRight:

                if (angles == 0)
                {
                    if (entrySide == PipeEntry.west)
                    {
                        currentPipe.ToggleWaterImg(true, true, isFiltered);
                        CheckBoard(currentPipe.south_pipe, PipeEntry.north, isFiltered);
                    }
                    else if (entrySide == PipeEntry.south)
                    {
                        currentPipe.ToggleWaterImg(true, true, isFiltered);
                        CheckBoard(currentPipe.west_pipe, PipeEntry.east, isFiltered);
                    }
                }
                else if (angles == 270)
                {
                    if (entrySide == PipeEntry.west)
                    {
                        currentPipe.ToggleWaterImg(true, true, isFiltered);
                        CheckBoard(currentPipe.north_pipe, PipeEntry.south, isFiltered);
                    }
                    else if (entrySide == PipeEntry.north)
                    {
                        currentPipe.ToggleWaterImg(true, true, isFiltered);
                        CheckBoard(currentPipe.west_pipe, PipeEntry.east, isFiltered);
                    }
                }
                else if (anglesAbs == 180)
                {
                    if (entrySide == PipeEntry.north)
                    {
                        currentPipe.ToggleWaterImg(true, true, isFiltered);
                        CheckBoard(currentPipe.east_pipe, PipeEntry.west, isFiltered);
                    }
                    else if (entrySide == PipeEntry.east)
                    {
                        currentPipe.ToggleWaterImg(true, true, isFiltered);
                        CheckBoard(currentPipe.north_pipe, PipeEntry.south, isFiltered);
                    }
                }

                else if (angles == 90)
                {
                    if (entrySide == PipeEntry.east)
                    {
                        currentPipe.ToggleWaterImg(true, true, isFiltered);
                        CheckBoard(currentPipe.south_pipe, PipeEntry.north, isFiltered);
                    }
                    else if (entrySide == PipeEntry.south)
                    {
                        currentPipe.ToggleWaterImg(true, true, isFiltered);
                        CheckBoard(currentPipe.east_pipe, PipeEntry.west, isFiltered);
                    }
                }
                break;

            case PipeType.Filter:
                if (anglesAbs == 180 || anglesAbs == 0)
                {
                    if (entrySide == PipeEntry.east)
                    {
                        CheckBoard(currentPipe.west_pipe, PipeEntry.east, true);
                        currentPipe.ToggleWaterImg(true, true, true);
                    }
                    else if (entrySide == PipeEntry.west)
                    {
                        CheckBoard(currentPipe.east_pipe, PipeEntry.west, true);
                        currentPipe.ToggleWaterImg(true, true, true);
                    }
                }
                break;

            case PipeType.End:
                levelsCompleted[currentLevel] = true;
                // LevelObj[currentLevel].SetActive(false);
                Destroy(currentLevelObj);
                // UIScript.Instance.StopPipeGame(isFiltered);
                CheckLevelCompletion();
                break;
        }

        return;
    }

    public void CheckLevelCompletion()
    {
        LevelSelectObj.SetActive(true);
        bool allLevelsCompelted = true;
        for (int i = 0; i < levelsCompleted.Length; i++)
        {
            LevelCompletedIMG[i].enabled = levelsCompleted[i];
            if (!LevelCompletedIMG[i].enabled) allLevelsCompelted = false;
        }
        if (allLevelsCompelted)
        {
            GameManager.instance.CompletePipeGame();
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PipeManager))]
public class PipeManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("ClearPipes"))
        {
            ((PipeManager)target).allPipes.Clear();
        }

        if (GUILayout.Button("Add Pipes"))
        {
            ((PipeManager)target).AddPipes();
        }

        if (GUILayout.Button("Setup Neighbours"))
        {
            ((PipeManager)target).SetupNeighbours();
        }




    }
}

#endif