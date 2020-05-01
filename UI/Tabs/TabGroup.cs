// see https://www.youtube.com/watch?v=211t6r12XPQ
// Video and presumably the original version of this script released under CC Attribution license

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TabGroup : MonoBehaviour
{
public TabButton selectedButton = null;

public Sprite hoverBackground = null;
public Sprite selectBackground = null;
public Sprite normalBackground = null;

public GameObject tabObjectGroup = null;


TabButton[] allButtons;


  public void Start()
  {
    allButtons = GetComponentsInChildren<TabButton>();
  }


  public void onHover( TabButton button )
  {
    resetAllTabs();
    
    if (button != selectedButton)
      button.background.sprite = hoverBackground;
  }

  public void onLeave( TabButton button )
  {
    resetAllTabs();
  }
  
  public void onSelect( TabButton button )
  {
    if (null != selectedButton)
      selectedButton.onDeselect();

    selectedButton = button;
    button.background.sprite = selectBackground;
    button.onSelect();

    resetAllTabs();


  int index = button.transform.GetSiblingIndex();

    if (null != tabObjectGroup)
    {
      for (int i=0; i<tabObjectGroup.transform.childCount; i++)
        tabObjectGroup.transform.GetChild(i).gameObject.SetActive( i == index );
    }
  }
  

  void resetAllTabs()
  {
    foreach( TabButton tb in allButtons )
    {
      if (tb != selectedButton)
        tb.background.sprite = normalBackground;
    }
  }
}

