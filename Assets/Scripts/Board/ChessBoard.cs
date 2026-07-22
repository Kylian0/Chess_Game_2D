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
                }
                else
                {
                    spriteRenderer.color = Color.white;
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
}
