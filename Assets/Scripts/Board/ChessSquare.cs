using UnityEngine;

public class ChessSquare : MonoBehaviour
{
    Vector2Int boardPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log($"La case est bien présente : {gameObject.name}");

        // Il faut que je récupère la position x et y de la case sur le plateau et les affichers dans la console à l'aide de boardPosition et de Vector2Int
        boardPosition = Vector2Int.RoundToInt(transform.position);

        // Afficher la position X et Y de la case dans la console en appelant boardPosition
        Debug.Log("Position de la case : " + boardPosition);
    }
}
