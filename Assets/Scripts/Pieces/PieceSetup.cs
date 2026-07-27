using UnityEngine;

// Installe toutes les pièces dans leur position de départ.
// Ce script sépare la création des pièces de la création visuelle du plateau.
public class PieceSetup : MonoBehaviour
{
    // Référence vers ChessBoard utilisée pour convertir les coordonnées logiques
    // en positions du monde Unity et pour ranger les pièces sous le bon parent.
    [SerializeField]
    private ChessBoard chessBoard;

    // Prefabs utilisés pour créer les rangées complètes de pions.
    [SerializeField]
    private GameObject whitePawn;

    [SerializeField]
    private GameObject blackPawn;

    // Tableaux contenant les huit prefabs des pièces de la rangée arrière.
    // L'index du tableau correspond directement à la colonne du plateau.
    [SerializeField]
    private GameObject[] whiteBackRow;

    [SerializeField]
    private GameObject[] blackBackRow;

    // Start est appelée automatiquement une fois au lancement de la scène.
    // Elle place les 32 pièces dans leur configuration initiale.
    private void Start()
    {
        SpawnPawnRow(whitePawn, 1);
        SpawnPawnRow(blackPawn, 6);
        SpawnBackRow(whiteBackRow, 0);
        SpawnBackRow(blackBackRow, 7);
    }

    // Crée une rangée complète de huit pions.
    // pawnPrefab indique quel prefab utiliser et row indique la rangée logique ciblée.
    public void SpawnPawnRow(GameObject pawnPrefab, int row)
    {
        // La boucle parcourt les huit colonnes du plateau, de 0 à 7.
        // Chaque répétition crée un pion dans la colonne correspondant à i.
        for (int i = 0; i < 8; i++)
        {
            // Vector2Int représente la position logique du pion :
            // i pour la colonne et row pour la rangée reçue en paramètre.
            Vector2Int boardPosition = new Vector2Int(i, row);

            // ChessBoard transforme les coordonnées logiques en coordonnées visibles dans la scène.
            Vector3 worldPosition = chessBoard.BoardPositionToWorldPosition(boardPosition);

            // Instantiate crée une copie du prefab à la position calculée.
            // Quaternion.identity signifie qu'aucune rotation n'est appliquée.
            // chessBoard.transform devient le parent de la pièce dans la Hierarchy.
            GameObject pawnInstance = Instantiate(
                pawnPrefab,
                worldPosition,
                Quaternion.identity,
                parent: chessBoard.transform
            );

            // GetComponent récupère les données ChessPiece de l'objet créé.
            ChessPiece chessPiece = pawnInstance.GetComponent<ChessPiece>();

            // La vérification protège le jeu si le prefab ne possède pas le composant attendu.
            if (chessPiece != null)
            {
                chessPiece.SetPiecePosition(boardPosition);
            }
        }
    }

    // Crée une rangée arrière complète à partir d'un tableau de prefabs.
    // L'ordre du tableau doit correspondre à l'ordre des colonnes A à H.
    public void SpawnBackRow(GameObject[] piecePrefabs, int row)
    {
        // Length retourne le nombre d'éléments présents dans le tableau.
        // La boucle s'adapte donc automatiquement à sa taille au lieu d'utiliser 8 en dur.
        for (int i = 0; i < piecePrefabs.Length; i++)
        {
            // L'index i sert à la fois à choisir le prefab et à définir sa colonne logique.
            Vector2Int boardPosition = new Vector2Int(i, row);

            // Conversion de la position du plateau en position du monde Unity.
            Vector3 worldPosition = chessBoard.BoardPositionToWorldPosition(boardPosition);

            // piecePrefabs[i] sélectionne le prefab stocké à l'index actuel du tableau.
            GameObject pieceInstance = Instantiate(
                piecePrefabs[i],
                worldPosition,
                Quaternion.identity,
                parent: chessBoard.transform
            );

            // Récupération du composant qui stocke le type, la couleur et la position de la pièce.
            ChessPiece chessPiece = pieceInstance.GetComponent<ChessPiece>();

            if (chessPiece != null)
            {
                chessPiece.SetPiecePosition(boardPosition);
            }
        }
    }
}
