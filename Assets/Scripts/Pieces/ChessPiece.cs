using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    public enum PieceType
    {
        Pawn,
        Rook,
        Knight,
        Bishop,
        Queen,
        King
    }

    public enum PieceColor
    {
        White,
        Black
    }

    [SerializeField]
    private PieceType pieceType;
    public void SetPieceType(PieceType type)
    {
        pieceType = type;
    }

    public PieceType GetPieceType()
    {
        return pieceType;
    }

    [SerializeField]
    private PieceColor pieceColor;
    public void SetPieceColor(PieceColor color)
    {
        pieceColor = color;
    }

    [SerializeField]
    private Vector2Int piecePosition;
    public void SetPiecePosition(Vector2Int position)
    {
        piecePosition = position;
    }

    public Vector2Int GetPiecePosition()
    {
        return piecePosition;
    }
}
