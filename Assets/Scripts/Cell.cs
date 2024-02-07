using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cell
{
    private int _rowNumber;
    private int _colNumber;
    private int _numAdjacentBombs;
    private bool _hasBomb;
    private bool _isFlagged;
    private bool _isRevealed;

    public int RowNumber
    {
        get => _rowNumber;
        set => _rowNumber = value;
    }

    public int ColNumber
    {
        get => _colNumber;
        set => _colNumber = value;
    }

    public int NumAdjacentBombs
    {
        get => _numAdjacentBombs;
        set => _numAdjacentBombs = value;
    }

    public bool HasBomb
    {
        get => _hasBomb;
        set => _hasBomb = value;
    }

    public bool IsFlagged
    {
        get => _isFlagged;
        set => _isFlagged = value;
    }

    public bool IsRevealed
    {
        get => _isRevealed;
        set => _isRevealed = value;
    }

    public Cell(int rowNumber, int colNumber, bool hasBomb, bool isRevealed)
    {
        _rowNumber = rowNumber;
        _colNumber = colNumber;
        _hasBomb = hasBomb;
        _isRevealed = isRevealed;
    }
}
    