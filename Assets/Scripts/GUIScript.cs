using UnityEngine;
using System.Collections;
using System.Linq;

// ReSharper disable once InconsistentNaming
// ReSharper disable once CheckNamespace
public class GUIScript : MonoBehaviour
{
    private int rowscleared;
    private float startTime;
    private float rowsPerMin = 0;
    private float oneRowsCleared;
    private float twoRowsCleared;
    private float threeRowsCleared;
    private float fourRowsCleared;
    private float points = 0;
    private int fitness;
    private int block = 0;
    private float blockSpawned = 1;
    private float avgBlockPerMin;
    private LineRenderer lineRenderer;
    private readonly ArrayList evalList = new ArrayList();
    private Vector2 scrollViewVector = Vector2.zero;
    private static float speedHSliderValue = 1;
    private static float blockHSliderValue;
    private float avgRowsPerMin;
    private float time;
    private Rect timeRunningRect;
    private Rect oneRowRect;
    private Rect twoRowRect;
    private Rect threeRowRect;
    private Rect fourRowRect;
    private Rect totalRowRect;
    private Rect fitnessRowRect;
    private Rect avgRowRect;
    private Rect blockRect;
    private Rect avgBlockRect;
    private Rect speedRect;
    private Rect autoplayRect;
    private float labelLeftMargin;
    private bool gameover;
    private Rect creditwwwRect;
    private Rect creditRect;
    private static bool autoplayBool = true;

    // ReSharper disable once UnusedMember.Local
    private void Start()
    {
        this.lineRenderer = this.GetComponent<LineRenderer>();
        for (var i = 0; i < 100; i++)
        {
            this.evalList.Add(0.0f);
        }

        if (Screen.width == 600)
        {
            this.labelLeftMargin = Screen.width / 3 - 190;
        }
        else
        {
            this.labelLeftMargin = Screen.width / 3 - 250;
        }

        var labelwidth = 500f;
        var labelHieght = 25f;
        this.timeRunningRect = new Rect(this.labelLeftMargin, 20, labelwidth, labelHieght);

        this.oneRowRect = new Rect(this.labelLeftMargin, 40, labelwidth, labelHieght);
        this.twoRowRect = new Rect(this.labelLeftMargin, 60, labelwidth, labelHieght);
        this.threeRowRect = new Rect(this.labelLeftMargin, 80, labelwidth, labelHieght);
        this.fourRowRect = new Rect(this.labelLeftMargin, 100, labelwidth, labelHieght);
        this.totalRowRect = new Rect(this.labelLeftMargin, 120, labelwidth, labelHieght);
        this.fitnessRowRect = new Rect(this.labelLeftMargin, 140, labelwidth, labelHieght);
        this.avgRowRect = new Rect(this.labelLeftMargin, 160, labelwidth, labelHieght);
        this.blockRect = new Rect(this.labelLeftMargin, 180, labelwidth, labelHieght);
        this.avgBlockRect = new Rect(this.labelLeftMargin, 200, labelwidth, labelHieght);
        this.speedRect = new Rect(this.labelLeftMargin, 260, labelwidth, labelHieght);
        this.autoplayRect = new Rect(this.labelLeftMargin, 310, labelwidth, labelHieght);
        this.creditRect = new Rect(this.labelLeftMargin, 350, labelwidth, labelHieght);
        this.creditwwwRect = new Rect(this.labelLeftMargin, 370, labelwidth, labelHieght);
    }

    // ReSharper disable once UnusedMember.Local
    // ReSharper disable once InconsistentNaming
    private void OnGUI()
    {
        this.time = Time.time - this.startTime;
        this.avgBlockPerMin = this.blockSpawned / this.time * 60;

        if (this.gameover)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2f, 200, 30), "Gameover starting over soon");
        }

        if (autoplayBool)
        {
            autoplayBool = GUI.Toggle(this.autoplayRect, autoplayBool, "Auto play ON");
        }
        else
        {
            autoplayBool = GUI.Toggle(this.autoplayRect, autoplayBool, "Auto play OFF");
        }

        this.avgRowsPerMin = this.rowscleared / this.time * 60;
        GUI.Label(this.timeRunningRect, "Time Running: " + PrettyTime((int)this.time));
        GUI.Label(this.oneRowRect, "1 Rows: " + this.oneRowsCleared);
        GUI.Label(this.twoRowRect, "2 Rows: " + this.twoRowsCleared);
        GUI.Label(this.threeRowRect, "3 Rows: " + this.threeRowsCleared);
        GUI.Label(this.fourRowRect, "4 Rows: " + this.fourRowsCleared);
        GUI.Label(this.totalRowRect, "Total Rows Cleared: " + this.rowscleared);
        GUI.Label(this.fitnessRowRect, "Fitness: " + this.fitness);

        GUI.Label(this.avgBlockRect, "Avg Blocks per min: " + (int)this.avgBlockPerMin);
        GUI.Label(this.blockRect, "Blocks spawned: " + this.blockSpawned);
        GUI.Label(this.avgRowRect, "Avg rows per min: " + (int)this.avgRowsPerMin);
        speedHSliderValue = GUI.HorizontalSlider(new Rect(Screen.width / 3 - 160, 265, 95, 30), speedHSliderValue, 1.0f, 1000.0f);
        GUI.Label(this.speedRect, "Speed: " + (int)speedHSliderValue);
        GUI.Box(new Rect(this.labelLeftMargin - 10, 260, 200, 220), string.Empty);
        GUI.Box(new Rect(this.labelLeftMargin - 10, 10, 200, 220), string.Empty);

        GUI.Label(this.creditRect, "\u00a9 Christoffer Bo Petersen");
        GUI.Label(this.creditwwwRect, "www.cb-p.dk");
    }

    public bool IsAutoPlay()
    {
        return autoplayBool;
    }

    public void ToogleGameover()
    {
        this.gameover = true;
    }

    public void UpdateScore(Tetris.Engine.GameStats gameResult)
    {
        if (this.startTime == 0)
        {
            this.startTime = Time.time;
        }

        this.rowscleared = gameResult.TotalRowClearings;
        this.fitness = gameResult.Fitness;
        this.oneRowsCleared = gameResult.OneRowClearings;
        this.twoRowsCleared = gameResult.TwoRowsClearings;
        this.threeRowsCleared = gameResult.ThreeRowsClearings;
        this.fourRowsCleared = gameResult.FourRowsClearings;
        this.blockSpawned = gameResult.BlocksSpawned;
    }

    public void SetEval(float eval)
    {
        this.evalList.RemoveAt(0);
        this.evalList.Add(eval);
    }

    public float GetGameSpeed()
    {
        return speedHSliderValue;
    }

    private int GetNumberofLines(string str)
    {
        return str.Count(x => x == '\n');
    }

    private static string PrettyTime(int time)
    {
        var sec = time % 60;
        int min;
        if (time > 3600)
        {
            min = time % 3600 / 60;
        }
        else
        {
            min = time / 60;
        }

        var hour = time / 3600;

        return string.Format(
            "{0}:{1}:{2}",
            hour.ToString().PadLeft(2, '0'),
            min.ToString().PadLeft(2, '0'),
            sec.ToString().PadLeft(2, '0'));
    }
}
