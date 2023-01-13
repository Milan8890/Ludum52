using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

[System.Serializable]  
public class part
{
    // 1=head, 2=body, 3=arm, 4=utilarm, 5=leg
    public int type;
    public int typeSquared; // nem jó név de a type-on belüli type pl lánctalp vagy sima láb

    public Sprite sprite;
    public float rarity;

    public float condition;

    //head/body
    public float hp;
    public float weight;

    //arm
    public bool melee;
    public float damage;
    //melle
    public float reach;
    //ranged
    public float fireRate;
    Sprite bulettSprite;

    //leg
    public bool stopIm;
    public float speed;
}

public class partScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public part part;

    GameObject unusedPartsPanel;
    GameObject canvas;

    public GameObject[] locks;

    private void Start()
    {
        unusedPartsPanel = GameObject.Find("unusedPartsPanel");
        canvas = GameObject.Find("bigCanvas");
        locks = GameObject.FindGameObjectsWithTag("partLock");
    }


    private void Update()
    {
     
    } 

    public void OnPointerEnter(PointerEventData pointerEventData)
    {

    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent = canvas.transform;
        transform.localScale = new Vector3(1, 1, 1);
    }

    bool newPlaceSet = false;
    public void OnEndDrag(PointerEventData eventData)
    {
        newPlaceSet = false;
        foreach (GameObject Lock in locks)
        {
            if (Vector2.Distance(GetComponent<RectTransform>().position, Lock.GetComponent<RectTransform>().position) <= 160f)
            {
                if(Lock == locks[part.type - 1])
                {
                    if (Lock.transform.childCount == 0)
                    {
                        transform.parent = Lock.transform;
                        GetComponent<RectTransform>().position = Lock.GetComponent<RectTransform>().position;
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    else
                    {
                        Transform equiped = Lock.transform.GetChild(0);
                        equiped.parent = unusedPartsPanel.transform;

                        equiped.localScale = new Vector3(1, 1, 1);
                        equiped.SetAsFirstSibling();

                        transform.parent = Lock.transform;
                        GetComponent<RectTransform>().position = Lock.GetComponent<RectTransform>().position;
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    newPlaceSet = true;
                }
            }
        }

        if(!newPlaceSet)
        {
            transform.parent = unusedPartsPanel.transform;
            transform.localScale = new Vector3(1, 1, 1);
            transform.SetAsFirstSibling();
        }

    }
}