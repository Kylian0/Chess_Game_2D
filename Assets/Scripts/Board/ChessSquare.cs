using UnityEngine;

// Représente une case individuelle de l'échiquier.
// Ce script stocke sa position logique et gère son apparence visuelle.
public class ChessSquare : MonoBehaviour
{
    // Vector2Int contient deux coordonnées entières :
    // X représente la colonne et Y représente la rangée du plateau.
    private Vector2Int boardPosition;

    // Couleur normale de la case, mémorisée afin de pouvoir la restaurer
    // après une surbrillance temporaire.
    private Color originalColor;

    // Change temporairement la couleur de la case pour indiquer
    // qu'elle est actuellement sélectionnée par le joueur.
    public void Highlight()
    {
        // GetComponent récupère le SpriteRenderer attaché au même GameObject.
        // Le SpriteRenderer contrôle l'affichage et la couleur du sprite de la case.
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = Color.blue;
    }

    // Retire la surbrillance et restaure la couleur noire ou blanche
    // enregistrée lors de la création du plateau.
    public void ResetHighlight()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = originalColor;
    }

    // Retourne la position logique actuelle de cette case.
    // Une méthode Get permet à un autre script de lire une donnée privée
    // sans lui permettre de la modifier directement.
    public Vector2Int GetBoardPosition()
    {
        return boardPosition;
    }

    // Enregistre la position logique attribuée par ChessBoard lors de la création de la case.
    // Le paramètre newBoardPosition contient la colonne et la rangée à mémoriser.
    public void SetBoardPosition(Vector2Int newBoardPosition)
    {
        boardPosition = newBoardPosition;
    }

    // Enregistre la couleur normale de la case.
    // Cette valeur sera réutilisée par ResetHighlight après une sélection.
    public void SetOriginalColor(Color color)
    {
        originalColor = color;
    }
}
