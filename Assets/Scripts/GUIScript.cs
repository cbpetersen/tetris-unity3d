using UnityEngine;

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
    private Rect speedLabelRect;
    private Rect autoplayRect;
    private float labelLeftMargin;
    private bool gameover;
    private Rect creditwwwRect;
    private Rect creditRect;
    private static bool autoplayBool = true;
    private Rect gameoverRect;
    private Rect speedRect;
    private Rect statsBoxRect;
    private Rect settingsBoxRect;

    // ReSharper disable once UnusedMember.Local
    private void Start()
    {
        this.startTime = Time.time;

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
        this.speedRect = new Rect(Screen.width / 3 - 160, 265, 95, 30);
        this.speedLabelRect = new Rect(this.labelLeftMargin, 260, labelwidth, labelHieght);
        this.autoplayRect = new Rect(this.labelLeftMargin, 310, labelwidth, labelHieght);
        this.creditRect = new Rect(this.labelLeftMargin, 350, labelwidth, labelHieght);
        this.gameoverRect = new Rect(Screen.width / 2 - 100, Screen.height / 2f, 200, 30);
        this.creditwwwRect = new Rect(this.labelLeftMargin, 370, labelwidth, labelHieght);

        this.statsBoxRect = new Rect(this.labelLeftMargin - 10, 10, 200, 220);
        this.settingsBoxRect = new Rect(this.labelLeftMargin - 10, 260, 200, 220);
    }

    // ReSharper disable once UnusedMember.Local
    // ReSharper disable once InconsistentNaming
    private void OnGUI()
    {
        this.time = Time.time - this.startTime;
        this.avgBlockPerMin = this.blockSpawned / this.time * 60;

        if (this.gameover)
        {
            GUI.Label(this.gameoverRect, "Gameover starting over soon");
        }

        GUI.Box(this.statsBoxRect, string.Empty);
        autoplayBool = GUI.Toggle(this.autoplayRect, autoplayBool, string.Format("Auto play {0}", autoplayBool ? "ON" : "OFF"));
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

        GUI.Box(this.settingsBoxRect, string.Empty);
        speedHSliderValue = GUI.HorizontalSlider(this.speedRect, speedHSliderValue, 1.0f, 1000.0f);
        GUI.Label(this.speedLabelRect, "Speed: " + (int)speedHSliderValue);
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
        this.rowscleared = gameResult.TotalRowClearings;
        this.fitness = gameResult.Fitness;
        this.oneRowsCleared = gameResult.OneRowClearings;
        this.twoRowsCleared = gameResult.TwoRowsClearings;
        this.threeRowsCleared = gameResult.ThreeRowsClearings;
        this.fourRowsCleared = gameResult.FourRowsClearings;
        this.blockSpawned = gameResult.BlocksSpawned;
    }

    public float GetGameSpeed()
    {
        return speedHSliderValue;
    }

    private static string PrettyTime(int time)
    {
        var sec = time % 60;
        var min = time % 3600 / 60;
        var hour = time / 3600;

        return string.Format(
            "{0}:{1}:{2}",
            hour.ToString().PadLeft(2, '0'),
            min.ToString().PadLeft(2, '0'),
            sec.ToString().PadLeft(2, '0'));
    }
}
