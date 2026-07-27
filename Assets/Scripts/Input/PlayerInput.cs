using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    ChessSquare selectedSquare;
    ChessPiece selectedPiece;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Récupérer la position de la souris
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Vérifier si un collider est présent à la position de la souris
            Collider2D[] hitCollider = Physics2D.OverlapPointAll(worldPosition);

            foreach (Collider2D hit in hitCollider)
            {
                ChessPiece chessPiece = hit.GetComponent<ChessPiece>();

                if (chessPiece != null)
                {
                    SelectPiece(chessPiece);
                }

                ChessSquare chessSquare = hit.GetComponent<ChessSquare>();

                // Si une case d'échecs est trouvée, la sélectionner et afficher sa notation d'échecs
                if (chessSquare != null)
                {
                    SelectSquare(chessSquare);

                    Vector2Int boardPosition = chessSquare.GetBoardPosition();

                    // Convertir la position du plateau en notation d'échecs
                    char letter = (char)('A' + boardPosition.x);
                    int number = boardPosition.y + 1;

                    Debug.Log($"Chess notation: {letter}{number}");
                }
            }
        }
    }

    // Méthode pour sélectionner une case d'échecs
    private void SelectSquare(ChessSquare newSquare)
    {
        // Réinitialiser la couleur de la case précédemment sélectionnée si elle existe
        if (selectedSquare != null && selectedSquare != newSquare)
        {
            selectedSquare.ResetHighlight();
        }

        selectedSquare = newSquare;

        // Mettre en surbrillance la nouvelle case sélectionnée
        if (selectedSquare != null)
        {

            selectedSquare.Highlight();
        }
    }

    private void SelectPiece(ChessPiece newPiece)
    {
        selectedPiece = newPiece;

        Debug.Log($"Selected piece: {selectedPiece.GetPieceType()} at {selectedPiece.GetPiecePosition()}");
    }
}