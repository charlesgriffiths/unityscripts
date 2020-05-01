// see https://www.youtube.com/watch?v=CGsEJToeXmA Matt Gambell "Game Dev Guide"
// Video and presumably the original version of this script released under CC Attribution license

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class FancyGridLayout : LayoutGroup
{
  public enum FitType
  {
    Uniform,
    Width,
    Height,
    FixedRows,
    FixedColumns
  }
  
public FitType fittype;
  
public int rows;
public int columns;
  
public Vector2 cellsize;
public Vector2 spacing;

public bool fitX;
public bool fitY;


  public override void CalculateLayoutInputHorizontal()
  {
    base.CalculateLayoutInputHorizontal();

    if (FitType.Uniform == fittype || FitType.Width == fittype || FitType.Height == fittype)
    {
    float size = Mathf.Sqrt( transform.childCount );
    
      rows = Mathf.CeilToInt( size );
      columns = Mathf.CeilToInt( size );
      fitX = true;
      fitY = true;
    }
    
    if (FitType.Width == fittype || FitType.FixedColumns == fittype)
      rows = Mathf.CeilToInt( transform.childCount / (float) columns );
      
    if (FitType.Height == fittype || FitType.FixedRows == fittype)
      columns = Mathf.CeilToInt( transform.childCount / (float) rows );
    
  float parentWidth = rectTransform.rect.width - spacing.x * (columns-1) - padding.left - padding.right;
  float parentHeight = rectTransform.rect.height - spacing.y * (rows-1) - padding.top - padding.bottom;

  float cellWidth = parentWidth / (float) columns;
  float cellHeight = parentHeight / (float) rows;
    
    cellsize.x = fitX ? cellWidth : cellsize.x;
    cellsize.y = fitY ? cellHeight : cellsize.y;
    
    for (int i=0; i<rectChildren.Count; i++)
    {
    int row = i / (0==columns?1:columns);
    int column = i % (0==columns?1:columns);
    RectTransform item = rectChildren[i];

    float xPos = (cellsize.x + spacing.x) * column + padding.left;
    float yPos = (cellsize.y + spacing.y) * row + padding.top;
    
      if (rows-1 == row)
      {
        xPos += (rows * columns - rectChildren.Count) * (cellsize.x + spacing.x) * 0.5f;
      }
      
      SetChildAlongAxis( item, 0, xPos, cellsize.x );
      SetChildAlongAxis( item, 1, yPos, cellsize.y );
    }
  }


  public override void CalculateLayoutInputVertical() {}
  public override void SetLayoutHorizontal() {}
  public override void SetLayoutVertical() {}
}

