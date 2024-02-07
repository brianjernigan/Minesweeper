using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Board _gameBoard;
    [SerializeField] private Sprite _bombSprite;
    
    public void OnButtonClick(GameObject button)
    {
        var buttonCell = GetCorrespondingCell(button);

        if (buttonCell.IsFlagged || buttonCell.IsRevealed)
        {
            return;
        }

        if (buttonCell.HasBomb)
        {
            button.GetComponent<Image>().sprite = _bombSprite;
            // Game over
            // Reveal all bombs
            // Mark incorrect flags
        }
        else
        {
            RevealCell(button, buttonCell);
            if (buttonCell.NumAdjacentBombs == 0)
            {
                // Flood fill
            }
        }
    }

    private void RevealCell(GameObject button, Cell buttonCell)
    {
        button.GetComponentInChildren<TMP_Text>().text = buttonCell.NumAdjacentBombs.ToString();
        buttonCell.IsRevealed = true;
    }

    private int GetIndexFromCoordinates(int rowNum, int colNum)
    {
        return (rowNum * _gameBoard.NumRows) + colNum;
    }

    public Cell GetCorrespondingCell(GameObject button)
    {
        var buttonIndexString = button.name;
        var buttonIndexInt = int.Parse(buttonIndexString);
        return _gameBoard.GetCellData(buttonIndexInt);
    }

}
