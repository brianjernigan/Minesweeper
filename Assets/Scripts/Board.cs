using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Board : SerializedMonoBehaviour
{
    private int _numMines;
    private int _numRows;
    private int _numColumns;

    public int NumRows => _numRows;

    [TableMatrix(HorizontalTitle = "Game Board", SquareCells = true, DrawElementMethod = "DrawElement", Transpose = true)]
    [SerializeField] private Cell[,] _gameBoard;
    
    public Cell GetCellData(int index)
    {
        var coordinates = GetCoordinatesFromIndex(index);
        return _gameBoard[coordinates.row, coordinates.column];
    }
    
    [OnInspectorInit]
    private void Start()
    {
        InitializeGame(5, 5, 5);
        FillBoard(_gameBoard);

        var mineIndices = GetMineIndices(_numRows * _numColumns);
        PlaceMines(mineIndices);
        SetBoardTouchingCounts(_gameBoard);
    }
    
    private void InitializeGame(int numberOfMines, int numberOfRows, int numberOfColumns)
    {
        _numMines = numberOfMines;
        _numRows = numberOfRows;
        _numColumns = numberOfColumns;

        _gameBoard = new Cell[_numRows, _numColumns];
    }
    
    private void FillBoard(Cell[,] grid)
    {
        for (int r = 0; r < _numRows; r++)
        {
            for (int c = 0; c < _numColumns; c++)
            {
                grid[r, c] = new Cell(r, c, false, false);
            }
        }
    }
    
    private List<int> GetMineIndices(int totalCells)
    {
        var mineCoordinates = new List<int>();
        for (int i = 0; i < _numMines; i++)
        {
            var coordinate = Random.Range(0, totalCells);
            while (mineCoordinates.Contains(coordinate))
            {
                coordinate = Random.Range(0, totalCells);
            }
            mineCoordinates.Add(coordinate);
        }

        return mineCoordinates;
    }
    
    private void PlaceMines(List<int> mineCoordinates)
    {
        foreach (var coordinate in mineCoordinates)
        {
            var xyCoordinates = GetCoordinatesFromIndex(coordinate);
            var mineCell = _gameBoard[xyCoordinates.row, xyCoordinates.column];
            mineCell.HasBomb = true;
        }
    }

    private (int row, int column) GetCoordinatesFromIndex(int index)
    {
        var row = index / _numRows;
        var col = index % _numRows;
        return (row, col);
    }

    private void SetBoardTouchingCounts(Cell[,] board)
    {
        for (int r = 0; r < _numRows; r++)
        {
            for (int c = 0; c < _numColumns; c++)
            {
                var currentCell = board[r, c];
                currentCell.NumAdjacentBombs = GetCellTouchingCount(currentCell);
            }
        }
    }
    
    private int GetCellTouchingCount(Cell currentCell)
    {
        var numBombs = 0;
        for (int r = -1; r <= 1; r++)
        {
            for (int c = -1; c <= 1; c++)
            {
                // Don't count cell itself
                if (!(r == 0 && c == 0))
                {
                    numBombs += CheckForSurroundingBombs(currentCell, r, c);
                }
            }
        }

        return numBombs;
    }

    private int CheckForSurroundingBombs(Cell currentCell, int rowOffset, int colOffset)
    {
        var rowToCheck = currentCell.RowNumber + rowOffset;
        var colToCheck = currentCell.ColNumber + colOffset;

        var isOutOfBounds = (rowToCheck < 0 || colToCheck < 0 || rowToCheck >= _numRows || colToCheck >= _numColumns);
        if (isOutOfBounds) return 0;

        return _gameBoard[rowToCheck, colToCheck].HasBomb ? 1 : 0;
    }
    
    static Cell DrawElement(Rect rect, Cell cell)
    {
        if (cell.HasBomb)
        {
            SirenixEditorGUI.DrawColorField(rect, Color.red);
        }
        else
        {
            SirenixEditorGUI.DrawColorField(rect, Color.white);
        }

        return cell;
    }
}


