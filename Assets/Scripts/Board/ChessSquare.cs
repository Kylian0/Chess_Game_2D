using UnityEngine;

public class ChessSquare : MonoBehaviour
{
    private Vector2Int boardPosition;

    public Vector2Int GetBoardPosition()
    {
        return boardPosition;
    }

    public void SetBoardPosition(Vector2Int newBoardPosition)
    {
        boardPosition = newBoardPosition;
    }
}
