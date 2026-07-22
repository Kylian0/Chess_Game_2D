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

    [SerializeField]
    private PieceType pieceType;

    public enum PieceColor
    {
        White,
        Black
    }

    [SerializeField]
    private PieceColor pieceColor;

    public void SetPieceType(PieceType type)
    {
        pieceType = type;
    }

    public void SetPieceColor(PieceColor color)
    {
        pieceColor = color;
    }
}
