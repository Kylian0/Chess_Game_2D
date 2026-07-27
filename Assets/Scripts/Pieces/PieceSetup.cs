using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class PieceSetup : MonoBehaviour
{
    [SerializeField]
    ChessBoard chessBoard;

    [SerializeField]
    private GameObject whitePawn;
    [SerializeField]
    private GameObject blackPawn;

    [SerializeField]
    private GameObject[] whiteBackRow;
    [SerializeField]
    private GameObject[] blackBackRow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Fais apparaitre les pièces de la première et dernière rangée
        SpawnPawnRow(whitePawn, 1);
        SpawnPawnRow(blackPawn, 6);
        SpawnBackRow(whiteBackRow, 0);
        SpawnBackRow(blackBackRow, 7);
    }

    // Méthode pour convertir la position du plateau en position dans le monde
    public void SpawnPawnRow(GameObject pawnPrefab, int row)
    {
        for (int i = 0; i < 8; i++)
        {
            // Définir la position de la pièce sur le plateau
            Vector2Int boardPosition = new Vector2Int(i, row);
            // Convertir la position du plateau en position dans le monde
            Vector3 worldPosition = chessBoard.BoardPositionToWorldPosition(boardPosition);
            // Instancier les pions à la position calculée
            GameObject pawnInstance = Instantiate(pawnPrefab, worldPosition, Quaternion.identity, parent: chessBoard.transform);
            // Récupérer le composant ChessPiece du pion instancié
            ChessPiece chessPiece = pawnInstance.GetComponent<ChessPiece>();


            if (chessPiece != null)
            {
                // Définir la position de la pièce sur le plateau
                chessPiece.SetPiecePosition(boardPosition);
            }
        }
    }

    public void SpawnBackRow(GameObject[] piecePrefab, int row)
    {
        for (int i = 0; i < piecePrefab.Length; i++)
        {
            // Définir la position de la pièce sur le plateau
            Vector2Int boardPosition = new Vector2Int(i, row);
            // Convertir la position du plateau en position dans le monde
            Vector3 worldPosition = chessBoard.BoardPositionToWorldPosition(boardPosition);
            // Instancier les pièces à la position calculée
            GameObject pieceInstance = Instantiate(piecePrefab[i], worldPosition, Quaternion.identity, parent: chessBoard.transform);
            // Récupérer le composant ChessPiece de la pièce isntaniée
            ChessPiece chessPiece = pieceInstance.GetComponent<ChessPiece>();

            if (chessPiece != null)
            {
                // Définir la position de la pièce sur le plateau
                chessPiece.SetPiecePosition(boardPosition);
            }
        }
    }
}
