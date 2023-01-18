using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class partChange : MonoBehaviour
{
    GameObject partPanel;
    GameObject unusedPartsPanel;

    public GameObject[] locks;
    List<GameObject> lockedParts;

    private void Start()
    {
        unusedPartsPanel = GameObject.Find("unusedPartsPanel");
        partPanel = GameObject.Find("partsBackdrop");
        partPanel.SetActive(false);
        locks = GameObject.FindGameObjectsWithTag("partLock");
    }

    int lastChildCount = 0;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(partPanel.activeSelf)
            {
                foreach (GameObject Lock in locks)
                    lockedParts.Add(Lock.transform.GetChild(0).gameObject);

                GetComponent<Pmovement>().change(lockedParts);
            }


            partPanel.SetActive(!partPanel.activeSelf);
        }
            

        if (lastChildCount != unusedPartsPanel.transform.childCount && unusedPartsPanel.transform.childCount%2 == 1)
            unusedPartsPanel.GetComponent<RectTransform>().position -= new Vector3(0, 130, 0);

        lastChildCount = unusedPartsPanel.transform.childCount;
    }
}
