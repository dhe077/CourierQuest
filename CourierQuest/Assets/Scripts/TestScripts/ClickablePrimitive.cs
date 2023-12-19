using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickablePrimitive : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{
   public void OnPointerClick(PointerEventData eventData)
   {
      Debug.Log("Clicked");
   }

   public void OnPointerDown(PointerEventData eventData)
   {
      Debug.Log("Down");
   }

}
