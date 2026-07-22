using UnityEngine;

public class ChessSquare : MonoBehaviour
{
    private Vector2Int boardPosition;

    private Color originalColor;

    // Méthode pour mettre en surbrillance la case sélectionnée
    public void Highlight()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = Color.blue;
    }

    // Méthode pour réinitialiser la couleur de la case à sa couleur d'origine
    public void ResetHighlight()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = originalColor;
    }
    
    public Vector2Int GetBoardPosition()
    {
        return boardPosition;
    }

    public void SetBoardPosition(Vector2Int newBoardPosition)
    {
        boardPosition = newBoardPosition;
    }

    public void SetOriginalColor(Color color)
    {
        originalColor = color;
    }

}
