using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour
{
    int rowscleared = 0;
    float startTime = 0;
    float rowsPerMin = 0;
    float OneRowsCleared = 0;
    float twoRowsCleared = 0;
    float threeRowsCleared = 0;
    float fourRowsCleared = 0;
    float points = 0;
    int fitness = 0;
    int block = 0;
    float blockSpawned = 1;
    float avgBlockPerMin = 0;
    public string stringToEdit = "";
    public GameObject lineRender;
    ArrayList evalList = new ArrayList();
    Vector2 scrollViewVector = Vector2.zero;
    private static float speedHSliderValue = 1;
    private static float blockHSliderValue = 0;
    float avgRowsPerMin = 0;
    float time;
    Rect timeRunningRect;
    Rect oneRowRect;
    Rect twoRowRect;
    Rect threeRowRect;
    Rect fourRowRect;
    Rect totalRowRect;
    Rect fitnessRowRect;
    Rect avgRowRect;
    Rect blockRect;
    Rect avgBlockRect;
    Rect speedRect;
    Rect autoplayRect;
    float alignLabelLeft;
    bool gameover = false;
    private Rect creditwwwRect;
    private Rect creditRect;
    static bool autoplayBool = true;


    void Start()
    {
        lineRender = (GameObject)Instantiate(lineRender);
        for (int i = 0; i < 100; i++)
        {
            evalList.Add(0.0f);
        }

        if (Screen.width == 600)
        {
            alignLabelLeft = Screen.width/3 - 190;
        }
        else
        {
            alignLabelLeft = Screen.width / 3 - 250;
        }

        float labelwidth = 500;
        float labelHieght = 25;
        timeRunningRect = new Rect(alignLabelLeft, 20, labelwidth, labelHieght);

        oneRowRect = new Rect(alignLabelLeft, 40, labelwidth, labelHieght);
        twoRowRect = new Rect(alignLabelLeft, 60, labelwidth, labelHieght);
        threeRowRect = new Rect(alignLabelLeft, 80, labelwidth, labelHieght);
        fourRowRect = new Rect(alignLabelLeft, 100, labelwidth, labelHieght); ;
        totalRowRect = new Rect(alignLabelLeft, 120, labelwidth, labelHieght);
        fitnessRowRect = new Rect(alignLabelLeft, 140, labelwidth, labelHieght);
        avgRowRect = new Rect(alignLabelLeft, 160, labelwidth, labelHieght);
        blockRect = new Rect(alignLabelLeft, 180, labelwidth, labelHieght);
        avgBlockRect = new Rect(alignLabelLeft, 200, labelwidth, labelHieght);
        speedRect = new Rect(alignLabelLeft, 260, labelwidth, labelHieght);
        autoplayRect = new Rect(alignLabelLeft, 310, labelwidth, labelHieght);
        creditRect = new Rect(alignLabelLeft, 350, labelwidth, labelHieght);
        creditwwwRect = new Rect(alignLabelLeft, 370, labelwidth, labelHieght);
    }


    void OnGUI()
    {
        time = ((Time.time - startTime));
        avgBlockPerMin = ((blockSpawned / time ) * 60);

        if (gameover)
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.width / 3, 200, 30), "Gameover starting over soon");

        if (Screen.width == 600)
        {
            avgRowsPerMin = ((rowscleared / time) * 60);
            GUI.Box(new Rect(alignLabelLeft - 10, 10, 200, 220), "");
            GUI.Label(timeRunningRect, "Time Running: " + prettyTime((int)time));
            GUI.Label(oneRowRect, "1 Rows: " + OneRowsCleared);
            GUI.Label(twoRowRect, "2 Rows: " + twoRowsCleared);
            GUI.Label(threeRowRect, "3 Rows: " + threeRowsCleared);
            GUI.Label(fourRowRect, "4 Rows: " + fourRowsCleared);
            GUI.Label(totalRowRect, "Total Rows Cleared: " + rowscleared);
            GUI.Label(fitnessRowRect, "Fitness: " + fitness);
            GUI.Label(avgBlockRect, "Avg Blocks per min: " + (int)avgBlockPerMin);
            GUI.Label(blockRect, "Blocks spawned: " + blockSpawned);
            GUI.Label(avgRowRect, "Avg rows per min: " + (int)avgRowsPerMin);

            GUI.Box(new Rect(alignLabelLeft - 10, 260, 200, 220), "");
            speedHSliderValue = UnityEngine.GUI.HorizontalSlider(new Rect(Screen.width / 3 - 100, 265, 95, 30), speedHSliderValue, 1.0f, 1000.0f);
            GUI.Label(speedRect, "Speed: " + ((int)speedHSliderValue));

            blockHSliderValue = UnityEngine.GUI.HorizontalSlider(new Rect(Screen.width / 3 - 100, 295, 95, 30), blockHSliderValue, 0.0f, 3.0f);
            if (autoplayBool)
                autoplayBool = GUI.Toggle(autoplayRect, autoplayBool, "Auto play ON");
            else
                autoplayBool = GUI.Toggle(autoplayRect, autoplayBool, "Auto play OFF");

            GUI.Label(creditRect, "\u00a9 Christoffer Bo Petersen");
            GUI.Label(creditwwwRect, "www.cb-p.dk");

            blockHSliderValue = 0;

            int consoleLength = linesInString(stringToEdit) * 15;
            scrollViewVector.y = consoleLength;

            scrollViewVector = GUI.BeginScrollView(new Rect(Screen.width / 2 + 110, 10, 175, 275), scrollViewVector, new Rect(0, 0, 150, consoleLength));

            stringToEdit = UnityEngine.GUI.TextArea(new Rect(0, 0, 160, consoleLength), stringToEdit, int.MaxValue);
            GUI.EndScrollView();


            GUI.Box(new Rect(Screen.width / 2 + 110, 300, 180, 420), "Last 100 Moves");

            LineRenderer lineRenderer = lineRender.GetComponent<LineRenderer>();
            lineRenderer.SetVertexCount(evalList.Count);
            int i = 0;
            while (i < evalList.Count)
            {
                Vector3 pos;
                float eval = (float)evalList[i];
                pos = new Vector3(i / 14.0f + 14.3f, (eval / 70.0f) + 1, -2.0f);
                lineRenderer.SetPosition(i, pos);
                i++;
            }
            lineRenderer.SetWidth(0.06F, 0.06F);
        }
        else
        {
            avgRowsPerMin = ((rowscleared / time) * 60);
            GUI.Box(new Rect(alignLabelLeft - 10, 10, 200, 220), "");
            GUI.Label(timeRunningRect, "Time Running: " + prettyTime((int)time));
            GUI.Label(oneRowRect, "1 Rows: " + OneRowsCleared);
            GUI.Label(twoRowRect, "2 Rows: " + twoRowsCleared);
            GUI.Label(threeRowRect, "3 Rows: " + threeRowsCleared);
            GUI.Label(fourRowRect, "4 Rows: " + fourRowsCleared);
            GUI.Label(totalRowRect, "Total Rows Cleared: " + rowscleared);
            GUI.Label(fitnessRowRect, "Fitness: " + fitness);

            GUI.Label(avgBlockRect, "Avg Blocks per min: " + (int)avgBlockPerMin);
            GUI.Label(blockRect, "Blocks spawned: " + blockSpawned);
            GUI.Label(avgRowRect, "Avg rows per min: " + (int)avgRowsPerMin);

            GUI.Box(new Rect(alignLabelLeft - 10, 260, 200, 220), "");
            speedHSliderValue = UnityEngine.GUI.HorizontalSlider(new Rect(Screen.width / 3 - 160, 265, 95, 30), speedHSliderValue, 1.0f, 1000.0f);
            GUI.Label(speedRect, "Speed: " + ((int)speedHSliderValue));

            int consoleLength = linesInString(stringToEdit) * 15;
            scrollViewVector.y = consoleLength;
            scrollViewVector = GUI.BeginScrollView(new Rect(Screen.width / 2 + 200, 10, 300, 500), scrollViewVector, new Rect(0, 0, 280, consoleLength));

            stringToEdit = UnityEngine.GUI.TextArea(new Rect(0, 0, 280, consoleLength), stringToEdit, int.MaxValue);
            GUI.EndScrollView();


            GUI.Box(new Rect(Screen.width / 2 + 200, 520, 280, 420), "Last 100 Moves");
            LineRenderer lineRenderer = lineRender.GetComponent<LineRenderer>();
            lineRenderer.SetVertexCount(evalList.Count);
            int i = 0;
            while (i < evalList.Count)
            {
                Vector3 pos;
                float eval = (float)evalList[i];
                pos = new Vector3(i / 14.0f + 14.5f, (eval / 70.0f) + 1, -2.0f);
                lineRenderer.SetPosition(i, pos);
                i++;
            }
            lineRenderer.SetWidth(0.06F, 0.06F);
        }
    }

    public bool isAutoPlay()
    {
        return autoplayBool;
    }


    public void toogleGameover()
    {
        gameover = true;
    }

    public void updateScore(Tetris.Engine.GameStats gameResult)
    {
        if (startTime == 0)
            startTime = Time.time;

        this.rowscleared = gameResult.TotalRowClearings;
        this.fitness = gameResult.Fitness;
        OneRowsCleared = gameResult.OneRowClearings;
        twoRowsCleared = gameResult.TwoRowsClearings;
        threeRowsCleared = gameResult.ThreeRowsClearings;
        fourRowsCleared = gameResult.FourRowsClearings;
        blockSpawned = gameResult.BlocksSpawned;
    }

    public void setEval(float eval)
    {
        evalList.RemoveAt(0);
        evalList.Add(eval);
    }

    public float getGameSpeed()
    {
        return speedHSliderValue;
    }

    public float getLookAhead()
    {
        return blockHSliderValue;
    }

    int linesInString(string str)
    {
        int count = 1;
        int start = 0;
        while ((start = str.IndexOf('\n', start)) != -1)
        {
            count++;
            start++;
        }
        return count;
    }

    string prettyTime(int time){

        int sec = time%60;
        int min = 0;
        if (time > 3600)
            min = (time % 3600) / 60;
        else
            min = time / 60;
        int hour = time/3600;

        return hour+":"+min+":"+sec;
    }
}
