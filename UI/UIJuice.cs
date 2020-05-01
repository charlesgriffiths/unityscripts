// see https://www.youtube.com/watch?v=Ll3yujn9GVQ
// Video and presumably the original version of this script released under CC Attribution license

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum UIAnimationTypes
{
  Move,
  Scale,
  ScaleX,
  ScaleY,
  Fade
}


public class UIJuice : MonoBehaviour
{
public GameObject objectToAnimate;

public UIAnimationTypes animationType;
public LeanTweenType easeType;
public float duration;
public float delay;

public bool loop;
public bool pingpong;

public bool startPositionOffset;
public Vector3 from;
public Vector3 to;

private LTDescr _tweenObject;

public bool showOnEnable;
public bool workOnDisable;
  
  
  public void OnEnable()
  {
    if (showOnEnable)
      Show();
  }
  
  
  public void Show()
  {
    HandleTween();
  }
  
  
  public void HandleTween()
  {
    if (null == objectToAnimate) objectToAnimate = gameObject;

    switch( animationType )
    {
      case UIAnimationTypes.Fade:
        Fade();
        break;
      case UIAnimationTypes.Move:
        MoveAbsolute();
        break;
      case UIAnimationTypes.Scale:
        Scale();
        break;
      case UIAnimationTypes.ScaleX:
        Scale();
        break;
      case UIAnimationTypes.ScaleY:
        Scale();
        break;
    }
    
    _tweenObject.setDelay( delay );
    _tweenObject.setEase( easeType );
    
    if (loop) _tweenObject.loopCount = int.MaxValue;
    if (pingpong) _tweenObject.setLoopPingPong();
  }
  
  
  public void Fade()
  {
    if (null == objectToAnimate.GetComponent<CanvasGroup>())
      objectToAnimate.AddComponent<CanvasGroup>();
      
  CanvasGroup cg = objectToAnimate.GetComponent<CanvasGroup>();
      
    if (startPositionOffset)
      cg.alpha = from.x;
      
    _tweenObject = LeanTween.alphaCanvas( cg, to.x, duration );
  }
  
  
  public void MoveAbsolute()
  {
  RectTransform rt = objectToAnimate.GetComponent<RectTransform>();

    rt.anchoredPosition = from;
    _tweenObject = LeanTween.move( rt, to, duration );
  }
  
  
  public void Scale()
  {
    if (startPositionOffset)
      objectToAnimate.GetComponent<RectTransform>().localScale = from;
      
    _tweenObject = LeanTween.scale( objectToAnimate, to, duration );
  }
  
  
  void SwapDirection()
  {
  Vector3 tmp = from;
    
    from = to;
    to = tmp;
  }
  
  
  public void OnDisable()
  {
    SwapDirection();
    
    HandleTween();
    
    _tweenObject.setOnComplete(() =>
    {
      SwapDirection();
      objectToAnimate.SetActive( false );
    });
  }
}

