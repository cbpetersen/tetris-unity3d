using UnityEngine;
using System.Collections;

using Tetris.Engine;

public class PlayControls : MonoBehaviour
{
    private Manager manager;

    void Start()
    {
        this.manager = this.GetComponent<Manager>();
    }

    void Update()
    {
        var horizontalMovement = Vector3.left * Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        if (horizontalMovement.x > 0)
        {
            this.manager.MoveBlock(Move.Left);
        }
        else if (horizontalMovement.x < 0)
        {
            this.manager.MoveBlock(Move.Right);
        }

        if (Input.GetKeyDown("space"))
        {
            this.manager.MoveBlock(Move.Fall);
        }
    }
}
