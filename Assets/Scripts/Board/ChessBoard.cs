using UnityEngine;
using TMPro;

// Construit l'échiquier visuel et convertit les coordonnées logiques du jeu
// en positions utilisables dans la scène Unity.
public class ChessBoard : MonoBehaviour
{
    // Prefab utilisé pour créer chacune des 64 cases du plateau.
    public GameObject casePrefab;

    // Prefab TextMeshPro utilisé pour afficher les lettres et les chiffres autour du plateau.
    public TMP_Text textPrefab;

    // Tableau de caractères contenant les lettres des colonnes de l'échiquier.
    // L'index 0 correspond à A, l'index 1 à B, etc.
    private char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

    // Start est appelée automatiquement une seule fois par Unity au lancement de la scène.
    // Elle déclenche ici la création du plateau et de ses coordonnées visuelles.
    private void Start()
    {
        CreateChessBoard();
        CreateChessBoardLabel();
    }

    // Crée les 64 cases de l'échiquier, leur attribue une position logique
    // et alterne leur couleur entre noir et blanc.
    public void CreateChessBoard()
    {
        // La première boucle parcourt les 8 colonnes du plateau.
        // La variable i représente donc la coordonnée logique X, de 0 à 7.
        for (int i = 0; i < 8; i++)
        {
            // Pour chaque colonne, cette seconde boucle parcourt les 8 rangées.
            // La variable j représente la coordonnée logique Y, de 0 à 7.
            // Les deux boucles imbriquées produisent 8 x 8, soit 64 cases.
            for (int j = 0; j < 8; j++)
            {
                // Les coordonnées logiques vont de 0 à 7, mais le plateau doit être centré
                // autour de l'origine de la scène. Retirer 3,5 transforme donc 0..7 en -3,5..3,5.
                float positionX = i - 3.5f;
                float positionY = j - 3.5f;

                // Vector3 représente une position dans l'espace Unity avec les axes X, Y et Z.
                // Z reste à 0 car le jeu utilise un affichage en 2D.
                Vector3 worldPosition = new Vector3(positionX, positionY, 0);

                // Instantiate crée une copie du prefab dans la scène.
                // Quaternion.identity signifie qu'aucune rotation n'est appliquée.
                // Le transform du ChessBoard devient le parent de la nouvelle case dans la Hierarchy.
                GameObject caseInstance = Instantiate(
                    casePrefab,
                    worldPosition,
                    Quaternion.identity,
                    parent: transform
                );

                // GetComponent recherche le composant ChessSquare présent sur la case instanciée.
                // Ce composant contient les données et comportements propres à une case.
                ChessSquare chessSquare = caseInstance.GetComponent<ChessSquare>();

                // Vector2Int stocke deux nombres entiers.
                // Ici, il représente la position logique de la case sur l'échiquier : colonne i, rangée j.
                chessSquare.SetBoardPosition(new Vector2Int(i, j));

                // Le SpriteRenderer est le composant Unity responsable de l'affichage du sprite
                // et permet notamment de modifier sa couleur.
                SpriteRenderer spriteRenderer = caseInstance.GetComponent<SpriteRenderer>();

                // La somme de la colonne et de la rangée alterne entre paire et impaire.
                // L'opérateur % retourne le reste d'une division : un reste de 0 signifie que la somme est paire.
                if ((i + j) % 2 == 0)
                {
                    spriteRenderer.color = Color.black;
                }
                else
                {
                    spriteRenderer.color = Color.white;
                }

                // La couleur choisie est mémorisée afin que ChessSquare puisse la restaurer
                // après avoir affiché une surbrillance temporaire.
                chessSquare.SetOriginalColor(spriteRenderer.color);
            }
        }
    }

    // Crée les lettres A à H sous le plateau et les chiffres 1 à 8 sur sa gauche.
    public void CreateChessBoardLabel()
    {
        // Une seule boucle suffit, car chaque index permet de créer à la fois
        // une lettre de colonne et un chiffre de rangée.
        for (int i = 0; i < 8; i++)
        {
            // Création du label de colonne en tant qu'enfant du ChessBoard.
            TMP_Text letterLabel = Instantiate(textPrefab, transform);

            // localPosition place le label relativement au transform de son parent.
            // X suit la colonne tandis que Y reste fixé sous le plateau.
            letterLabel.transform.localPosition = new Vector3(i - 3.5f, -4.5f, 0);

            // ToString convertit le caractère du tableau en texte affichable par TextMeshPro.
            letterLabel.text = letters[i].ToString();

            // Création du label de rangée en tant qu'enfant du ChessBoard.
            TMP_Text numberLabel = Instantiate(textPrefab, transform);

            // X reste fixé à gauche du plateau tandis que Y suit la rangée.
            numberLabel.transform.localPosition = new Vector3(-4.5f, i - 3.5f, 0);

            // Les index commencent à 0, alors que les rangées d'échecs commencent à 1.
            numberLabel.text = (i + 1).ToString();
        }
    }

    // Crée une rangée complète de huit pions à partir d'un prefab et d'une rangée logique.
    // Cette méthode est actuellement conservée ici, même si PieceSetup possède aussi cette responsabilité.
    public void SpawnPawnRow(GameObject pawnPrefab, int row)
    {
        // La boucle parcourt les huit colonnes, de A à H.
        for (int i = 0; i < 8; i++)
        {
            // Vector2Int associe la colonne variable i à la rangée fixe reçue en paramètre.
            Vector2Int boardPosition = new Vector2Int(i, row);

            // Conversion de la position logique en coordonnées visibles dans la scène Unity.
            Vector3 worldPosition = BoardPositionToWorldPosition(boardPosition);

            // Création du pion à la position calculée, sans rotation, sous le ChessBoard.
            GameObject pawnInstance = Instantiate(
                pawnPrefab,
                worldPosition,
                Quaternion.identity,
                parent: transform
            );

            // Récupération du composant qui représente les données de la pièce.
            ChessPiece chessPiece = pawnInstance.GetComponent<ChessPiece>();

            // GetComponent peut retourner null si le prefab ne possède pas ChessPiece.
            // Cette vérification évite donc une NullReferenceException.
            if (chessPiece != null)
            {
                chessPiece.SetPiecePosition(boardPosition);
            }
        }
    }

    // Convertit une coordonnée logique du plateau, comprise entre 0 et 7,
    // en position centrée dans le monde Unity, comprise entre -3,5 et 3,5.
    public Vector3 BoardPositionToWorldPosition(Vector2Int boardPosition)
    {
        float positionX = boardPosition.x - 3.5f;
        float positionY = boardPosition.y - 3.5f;

        // Vector3 est utilisé car le Transform d'un GameObject possède trois axes,
        // même dans un jeu 2D. La profondeur Z reste ici égale à 0.
        Vector3 worldPosition = new Vector3(positionX, positionY, 0);

        // return renvoie la position calculée au code qui a appelé cette méthode.
        return worldPosition;
    }
}
