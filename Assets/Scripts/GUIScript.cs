﻿using UnityEngine;
// ReSharper disable once InconsistentNaming
// ReSharper disable once CheckNamespace
public class GUIScript : MonoBehaviour
{
    private bool gameover;
    private float avgBlockPerMin,avgRowsPerMin,blockSpawned = 1,fourRowsCleared,labelLeftMargin,threeRowsCleared,oneRowsCleared,points = 0,rowsPerMin = 0,startTime,twoRowsCleared,time;
    private int block = 0,rowscleared,fitness;
    private Rect autoplayRect,avgBlockRect,avgRowRect,blockRect,creditRect,creditwwwRect,fitnessRowRect,twoRowRect,totalRowRect;
    private Rect fourRowRect,timeRunningRect,threeRowRect,statsBoxRect,speedRect,speedLabelRect,settingsBoxRect,oneRowRect,gameoverRect;
    private static bool autoPlayEnabled = true;
    private static float speedHSliderValue = 1;
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
        autoPlayEnabled = GUI.Toggle(this.autoplayRect, autoPlayEnabled, string.Format("Auto play {0}", autoPlayEnabled ? "ON" : "OFF"));
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
        speedHSliderValue = GUI.HorizontalSlider(this.speedRect, speedHSliderValue, 0.01f, 200.0f);
        GUI.Label(this.speedLabelRect, "Speed: " + (int)speedHSliderValue);
        GUI.Label(this.creditRect, "\u00a9 Christoffer Bo Petersen");
        GUI.Label(this.creditwwwRect, "www.cb-p.dk");
    }
    public bool IsAutoPlay()
    {
        return autoPlayEnabled;
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
