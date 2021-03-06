// see https://www.youtube.com/watch?v=211t6r12XPQ Matt Gambell "Game Dev Guide"
// Video and presumably the original version of this script released under CC Attribution license

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;



[RequireComponent(typeof(Image))]

public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
public Image background;

public UnityEvent onSelectEvent;
public UnityEvent onDeselectEvent;


TabGroup group;


  public void Start()
  {
    group = GetComponentInParent<TabGroup>();
    if (null == background) background = GetComponent<Image>();
  }
  
  
  public void OnPointerEnter( PointerEventData ped )
  {
    group.onHover( this );
  }

  public void OnPointerExit( PointerEventData ped )
  {
    group.onLeave( this );
  }

  public void OnPointerClick( PointerEventData ped )
  {
    group.onSelect( this );
  }
  
  
  public void onSelect()
  {
    if (null != onSelectEvent)
      onSelectEvent.Invoke();
  }
  
  public void onDeselect()
  {
    if (null != onDeselectEvent)
      onDeselectEvent.Invoke();
  }
}

