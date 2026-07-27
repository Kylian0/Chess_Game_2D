using UnityEngine;
using UnityEngine.InputSystem;

// Lit les clics du joueur et identifie les éléments de l'échiquier
// situés sous la souris : pièces et cases.
public class PlayerInput : MonoBehaviour
{
    // Référence vers la dernière case sélectionnée.
    // Elle permet notamment de retirer son ancienne surbrillance.
    private ChessSquare selectedSquare;

    // Référence vers la dernière pièce sélectionnée.
    // Elle restera mémorisée pour préparer une future tentative de déplacement.
    private ChessPiece selectedPiece;

    // Update est appelée automatiquement par Unity à chaque image affichée.
    // On y vérifie si le bouton gauche de la souris vient d'être pressé.
    private void Update()
    {
        // wasPressedThisFrame vaut true uniquement pendant l'image
        // où le clic commence, ce qui évite de répéter l'action tant que le bouton reste enfoncé.
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // ReadValue récupère la position actuelle de la souris en pixels sur l'écran.
            // Vector2 contient ici deux valeurs : X horizontal et Y vertical.
            Vector2 mousePosition = Mouse.current.position.ReadValue();

            // ScreenToWorldPoint convertit la position écran de la souris
            // en coordonnées utilisables dans la scène Unity.
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // OverlapPointAll cherche tous les Collider2D présents sous ce point.
            // Un clic sur une pièce peut détecter à la fois le collider de la pièce
            // et celui de la case située juste en dessous.
            Collider2D[] hitColliders = Physics2D.OverlapPointAll(worldPosition);

            // foreach parcourt chaque collider contenu dans le tableau.
            // Contrairement à hitColliders[0], cette boucle ne dépend pas de l'ordre
            // dans lequel Unity retourne les objets détectés.
            foreach (Collider2D hit in hitColliders)
            {
                // Recherche un composant ChessPiece sur l'objet actuellement parcouru.
                ChessPiece chessPiece = hit.GetComponent<ChessPiece>();

                // GetComponent retourne null si l'objet n'est pas une pièce.
                if (chessPiece != null)
                {
                    SelectPiece(chessPiece);
                }

                // Recherche séparément un composant ChessSquare sur le même collider.
                ChessSquare chessSquare = hit.GetComponent<ChessSquare>();

                // Si le collider appartient à une case, celle-ci devient la case sélectionnée.
                if (chessSquare != null)
                {
                    SelectSquare(chessSquare);

                    // Récupération des coordonnées logiques de la case cliquée.
                    Vector2Int boardPosition = chessSquare.GetBoardPosition();

                    // Si une pièce a déjà été sélectionnée, on dispose maintenant
                    // d'une position de départ et d'une position d'arrivée potentielle.
                    if (selectedPiece != null)
                    {
                        Vector2Int startPosition = selectedPiece.GetPiecePosition();
                        Vector2Int targetPosition = boardPosition;

                        // Ce message teste seulement la détection d'une tentative de déplacement.
                        // Aucune pièce n'est encore réellement déplacée à ce stade.
                        Debug.Log(
                            $"Move attempt: {selectedPiece.GetPieceType()} from {startPosition} to {targetPosition}"
                        );
                    }

                    // Conversion de la colonne numérique en lettre d'échecs.
                    // Ajouter boardPosition.x au caractère 'A' produit A, B, C... jusqu'à H.
                    char letter = (char)('A' + boardPosition.x);

                    // Les coordonnées logiques commencent à 0, tandis que la notation d'échecs commence à 1.
                    int number = boardPosition.y + 1;

                    Debug.Log($"Chess notation: {letter}{number}");
                }
            }
        }
    }

    // Change la case actuellement sélectionnée et gère sa surbrillance visuelle.
    private void SelectSquare(ChessSquare newSquare)
    {
        // Si une autre case était déjà sélectionnée, on restaure sa couleur d'origine.
        // La comparaison évite de réinitialiser inutilement la même case.
        if (selectedSquare != null && selectedSquare != newSquare)
        {
            selectedSquare.ResetHighlight();
        }

        // La nouvelle case devient la référence active.
        selectedSquare = newSquare;

        // Cette vérification protège le code au cas où aucune case valide ne serait fournie.
        if (selectedSquare != null)
        {
            selectedSquare.Highlight();
        }
    }

    // Mémorise la pièce cliquée afin qu'un prochain clic sur une case
    // puisse être interprété comme une tentative de déplacement.
    private void SelectPiece(ChessPiece newPiece)
    {
        selectedPiece = newPiece;

        Debug.Log(
            $"Selected piece: {selectedPiece.GetPieceType()} at {selectedPiece.GetPiecePosition()}"
        );
    }
}
