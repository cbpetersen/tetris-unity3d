using UnityEngine;

using System.Collections;

using Tetris.Engine;
using Tetris.Engine.AI;
using Tetris.Engine.AI.Algorithms;
using Tetris.Engine.AI.Algorithms.Weights;

using UnityEngine.SceneManagement;

using Move = Tetris.Engine.Move;

public class Manager : MonoBehaviour
{
    private Engine ai;
    private GameManager gameManager;
    private GUIScript gui;

    private int blockSpawned = -1;
    private bool gameover;

    public GameObject[][] Blocks;
    public GameObject Cube;
    public Transform LeftWall;
    public Transform RightWall;

    private IEnumerator moveIterator;

    private void Start()
    {
        this.gui = this.GetComponent<GUIScript>();
        this.gameManager = new GameManager(20, 10);
        this.ai = new Engine(
                new TetrisAi(
                    new TetrisAiWeights
                        {
                            ColumnTransitions = 0.8024363520000000f,
                            LandingHeight = -0.1958289440000000f,
                            NumberOfHoles = 5.0289489999999999f,
                            RowTransitions = -0.4794300500000000f,
                            RowsCleared = -2.0772042300000000f,
                            WellSums = 0.4410647000000000f
                        }));

        this.Blocks = new GameObject[20][];
        for (var i = 0; i < 20; i++)
        {
            this.Blocks[i] = new GameObject[10];
        }

        for (var row = this.gameManager.BoardManager.GameBoard.GetLength(0) - 1; row >= 0; row--)
        {
            for (var column = 0; column < this.gameManager.BoardManager.GameBoard[row].Length; column++)
            {
                var cube = Instantiate(this.Cube);
                cube.transform.position = new Vector3(column / 3.2f, row / 3.2f, 0);
                this.Blocks[row][column] = cube;
            }
        }

        this.LeftWall.transform.position = new Vector3(-0.32f, 0f);
        this.RightWall.transform.position = new Vector3(10f/3.2f, 0f);
    }

    private void Update()
    {
        var gameBoard = this.gameManager.BoardManager.GameBoard;
        var activeBlock = this.gameManager.ActiveBlock;

        for (var row = gameBoard.GetLength(0) - 1; row >= 0; row--)
        {
            for (var column = 0; column < gameBoard[row].Length; column++)
            {
                if (activeBlock != null && row - activeBlock.Position.Row >= 0 && row - activeBlock.Position.Row < activeBlock.BlockMatrixSize && column - activeBlock.Position.Column >= 0 && column - activeBlock.Position.Column < activeBlock.BlockMatrixSize && activeBlock.BlockMatrix[row - activeBlock.Position.Row][column - activeBlock.Position.Column])
                {
                    this.Blocks[row][column].SetActive(true);
                }
                else
                {
                    this.Blocks[row][column].SetActive(gameBoard[row][column]);
                }
            }
        }

        this.gui.UpdateScore(this.gameManager.GameStats);
    }

    private void FixedUpdate()
    {
        for (var i = 0; i < this.gui.GetGameSpeed(); i++)
        {
            if (this.gameover)
            {
                return;
            }

            this.GameStep();
        }
    }

    private void GameStep()
    {
        if (this.gui.IsAutoPlay())
        {
            if (this.moveIterator != null && this.moveIterator.MoveNext())
            {
                this.gameManager.MoveBlock((Move)this.moveIterator.Current);
            }

            if (this.blockSpawned != this.gameManager.GameStats.BlocksSpawned)
            {
                var steps = this.ai.GetNextMove(this.gameManager.BoardManager);
                this.moveIterator = steps.Moves.GetEnumerator();
                this.blockSpawned = this.gameManager.GameStats.BlocksSpawned;
            }
        }

        if (this.gameManager.GameState.IsGameOver())
        {
            this.gameover = true;
            this.StartCoroutine(this.GameOver());
        }
        else
        {
            this.gameManager.OnGameLoopStep();
        }
    }

    public void MoveBlock(Move move)
    {
        this.gameManager.MoveBlock(move);
    }

    public IEnumerator GameOver()
    {
        Debug.Log ("Game Over!");
        Debug.Log (this.gameManager.GameStats.Fitness);

        this.gui.ToogleGameover();
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("game");
    }
}
