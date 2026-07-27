using UnityEngine;
using TMPro;

public class ChessBoard : MonoBehaviour
{
    public GameObject casePrefab;
    public TMP_Text textPrefab;
    
    char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

    void Start()
    {
        CreateChessBoard();
        CreateChessBoardLabel();
    }

    public void CreateChessBoard()
    {
        // Créer un plateau d'échecs de 8x8 cases
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                float positionX = i - 3.5f;
                float positionY = j - 3.5f;

                // Instancier la case à la position calculée
                GameObject caseInstance = Instantiate(casePrefab, new Vector3(positionX, positionY, 0), Quaternion.identity, parent: transform);

                // Récupérer le composant ChessSquare de la case instanciée
                ChessSquare chessSquare = caseInstance.GetComponent<ChessSquare>();
                // Définir la position de la case sur le plateau
                chessSquare.SetBoardPosition(new Vector2Int(i, j));

                SpriteRenderer spriteRenderer = caseInstance.GetComponent<SpriteRenderer>();

                // Colorer les cases en noir et blanc
                if ((i + j) % 2 == 0)
                {
                    spriteRenderer.color = Color.black;
                    chessSquare.SetOriginalColor(spriteRenderer.color);
                }
                else
                {
                    spriteRenderer.color = Color.white;
                    chessSquare.SetOriginalColor(spriteRenderer.color);
                }
            }
        }
    }

    public void CreateChessBoardLabel()
    {
        // Créer les lettres A à H en dessous du plateau
        for (int i = 0; i < 8; i++)
        {
            TMP_Text letterLabel = Instantiate(textPrefab, transform);

            letterLabel.transform.localPosition = new Vector3(i - 3.5f, -4.5f, 0);

            letterLabel.text = letters[i].ToString();


            // Créer les chiffres 1 à 8 à gauche du plateau
            TMP_Text numberLabel = Instantiate(textPrefab, transform);

            numberLabel.transform.localPosition = new Vector3(-4.5f, i - 3.5f, 0);

            numberLabel.text = (i + 1).ToString();
        }
    }

    public void SpawnPawnRow(GameObject pawnPrefab, int row) 
    {
        for (int i = 0; i < 8; i++)
        {
            // Définir la position de la pièce sur le plateau
            Vector2Int boardPosition = new Vector2Int(i, row);
            // Convertir la position du plateau en position dans le monde
            Vector3 worldPosition = BoardPositionToWorldPosition(boardPosition);
            // Instancier les pions à la position calculée
            GameObject pawnInstance = Instantiate(pawnPrefab, worldPosition, Quaternion.identity, parent: transform);
            // Récupérer le composant ChessPiece du pion instancié
            ChessPiece chessPiece = pawnInstance.GetComponent<ChessPiece>();


            if (chessPiece != null)
            {
                // Définir la position de la pièce sur le plateau
                chessPiece.SetPiecePosition(boardPosition);
            }
        }
    }

    // Méthode pour faire apparaître les pièces sur le plateau
    public Vector3 BoardPositionToWorldPosition(Vector2Int boardPosition)
    {
        float positionX = boardPosition.x - 3.5f;
        float positionY = boardPosition.y - 3.5f;

        Vector3 worldPosition = new Vector3(positionX, positionY, 0);
        return worldPosition;
    }
}
