using UnityEngine;

// Représente les données propres à une pièce d'échecs.
// Ce script mémorise son type, sa couleur et sa position logique sur le plateau.
public class ChessPiece : MonoBehaviour
{
    // Une enum permet de limiter une variable à une liste précise de valeurs possibles.
    // Ici, chaque valeur correspond à un type de pièce d'échecs.
    public enum PieceType
    {
        Pawn,
        Rook,
        Knight,
        Bishop,
        Queen,
        King
    }

    // Cette enum décrit les deux camps possibles d'une pièce.
    public enum PieceColor
    {
        White,
        Black
    }

    // SerializeField conserve la variable privée tout en la rendant visible dans l'Inspector.
    // Cela permet de choisir le type directement sur chaque prefab de pièce.
    [SerializeField]
    private PieceType pieceType;

    // Modifie le type de la pièce depuis un autre script.
    public void SetPieceType(PieceType type)
    {
        pieceType = type;
    }

    // Retourne le type actuel de la pièce.
    public PieceType GetPieceType()
    {
        return pieceType;
    }

    // Couleur de la pièce, configurée dans l'Inspector du prefab.
    [SerializeField]
    private PieceColor pieceColor;

    // Modifie la couleur logique de la pièce depuis un autre script.
    public void SetPieceColor(PieceColor color)
    {
        pieceColor = color;
    }

    // Position logique de la pièce sur le plateau.
    // Vector2Int utilise deux entiers : X pour la colonne et Y pour la rangée.
    [SerializeField]
    private Vector2Int piecePosition;

    // Enregistre une nouvelle position logique pour la pièce.
    // Cette méthode sera utile lors de l'apparition puis du déplacement des pièces.
    public void SetPiecePosition(Vector2Int position)
    {
        piecePosition = position;
    }

    // Retourne la position logique actuelle de la pièce.
    public Vector2Int GetPiecePosition()
    {
        return piecePosition;
    }
}
