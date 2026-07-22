using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    ChessSquare selectedSquare;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Récupérer la position de la souris
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Vérifier si un collider est présent à la position de la souris
            Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition);

            if (hitCollider != null)
            {
                ChessSquare chessSquare = hitCollider.GetComponent<ChessSquare>();

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
        selectedSquare = newSquare;

        if (selectedSquare != null)
        {
            Debug.Log($"Square selected: {selectedSquare.GetBoardPosition()}");
        }
    }
}
