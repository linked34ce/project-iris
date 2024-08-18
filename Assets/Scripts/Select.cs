using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Select : MonoBehaviour
{

    [SerializeField] GameObject select1;
    [SerializeField] GameObject select2;

    private bool isSelect1 = true;

    public void ChangeOpcaity(GameObject select, float opacity)
    {
        Image selectBackground = select.transform.Find("Background").GetComponent<Image>();
        Color oldColor = selectBackground.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, opacity);
        selectBackground.color = newColor;
    }

    public bool GetIsSelect1()
    {
        return isSelect1;
    }

    public void SetIsSelect1(bool isSelect1)
    {
        this.isSelect1 = isSelect1;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (GetIsSelect1())
                Initiate.Fade("Scenes/Dungeons/Nijigasaki Old School Building/1st Floor", Color.black, 1f);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (GetIsSelect1())
            {
                SetIsSelect1(false);
                ChangeOpcaity(select2, 1);
                ChangeOpcaity(select1, 0);
            }
            else
            {
                SetIsSelect1(true);
                ChangeOpcaity(select1, 1);
                ChangeOpcaity(select2, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (GetIsSelect1())
            {
                SetIsSelect1(false);
                ChangeOpcaity(select2, 1);
                ChangeOpcaity(select1, 0);
            }
            else
            {
                SetIsSelect1(true);
                ChangeOpcaity(select1, 1);
                ChangeOpcaity(select2, 0);
            }
        }
    }
}
