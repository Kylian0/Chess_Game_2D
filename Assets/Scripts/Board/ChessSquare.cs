using UnityEngine;

public class ChessSquare : MonoBehaviour
{
    private Vector2Int boardPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public Vector2Int GetBoardPosition()
    {
        return boardPosition;
    }

    public void SetBoardPosition(Vector2Int newBoardPosition)
    {
        boardPosition = newBoardPosition;
    }
}
