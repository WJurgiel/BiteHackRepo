using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform correctTarget;
    public float snapDistance = 10f;
    public bool isPlaced = false;
    public TilesManager tilesManager;

    private RectTransform rectTransform;
    private Canvas canvas;

    private Vector2 startPos;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        tilesManager = FindObjectOfType<TilesManager>();
    }

    private void Start()
    {
        startPos = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float dist = Vector2.Distance(rectTransform.anchoredPosition, correctTarget.anchoredPosition);
        if (dist < snapDistance)
        {
            rectTransform.anchoredPosition = correctTarget.anchoredPosition;
            isPlaced = true;
        }
        else
        {
            isPlaced = false;
        }

        if (tilesManager.AreAllTilesPlaced())
        {
            Debug.Log("All tiles placed");
        }
    }
}
