using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RightMouseClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private Sprite _flagSprite;
    [SerializeField] private Sprite _defaultSprite;
    
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseClick(eventData);
        }
    }

    private void OnRightMouseClick(PointerEventData eventData)
    {
        var button = GetButtonGameObject(eventData);
        var currentCell = _gameController.GetCorrespondingCell(button);
        FlagOrUnflagCell(button, currentCell);
    }

    private GameObject GetButtonGameObject(PointerEventData eventData)
    {
        var clickedTextObject = eventData.pointerCurrentRaycast.gameObject;
        return clickedTextObject.transform.parent.gameObject;
    }

    private void FlagOrUnflagCell(GameObject button, Cell correspondingCell)
    {
        if (correspondingCell.IsRevealed)
        {
            return;
        }

        var buttonImage = button.GetComponent<Image>();
        buttonImage.sprite = buttonImage.sprite == _defaultSprite ? _flagSprite : _defaultSprite;

        correspondingCell.IsFlagged = !correspondingCell.IsFlagged;
    }
}
