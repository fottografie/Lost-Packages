using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kachel : MonoBehaviour
{
    public int index;
    public bool clear;
    public bool package;

    public GameObject pfeilObject;
    private GameObject pfeil;
    public GameObject[] neighbours = new GameObject[6];

    public GameObject flow;
    public int flowAngle;

    public GameObject altPfeilObject;
    private GameObject altPfeil;
    public int altFlowAngle;
    public bool altFlowBool;
    public GameObject altFlow;

    // Start is called before the first frame update
    void Start()
    {
        SetIndex();
        flowAngle = (int)transform.rotation.eulerAngles.z;
        pfeil = Instantiate(pfeilObject, transform.position, transform.rotation);
        flow = GetFlow((int)transform.eulerAngles.z);
        if (altFlowBool)
        {
            altFlow = GetFlow(altFlowAngle);
            altPfeil = Instantiate(altPfeilObject, transform.position, Quaternion.Euler(0, 0, altFlowAngle));
        }
    }

    public void ChangePfeil()
    {
        DestroyImmediate(pfeil, true);
        DestroyImmediate(altPfeil, true);

        int temp = flowAngle;
        flowAngle = altFlowAngle;
        altFlowAngle = temp;

        pfeil = Instantiate(pfeilObject, transform.position, Quaternion.Euler(0, 0, flowAngle));
        altPfeil = Instantiate(altPfeilObject, transform.position, Quaternion.Euler(0, 0, altFlowAngle));
    }

    //Setzt den Index der Kachel auf den Namen des GameObjects
    void SetIndex()
    {
        string[] nameO = gameObject.name.Split(' ');
        index = int.Parse(nameO[1]);
        clear = true;
        package = false;
    }


    //Berechnet abhängig von der Drehung der Kachel das nächste Feld in Flussrichtung
    public GameObject GetFlow(int z)
    {
        GameObject flow_;
        if (z == 0)
        {
            flow_ = neighbours[3];
            return flow_;
        }
        else if (z > 59 && z < 61)
        {
            flow_ = neighbours[1];
            return flow_;
        }
        else if (z > 119 && z < 121)
        {
            flow_ = neighbours[0];
            return flow_;
        }
        else if (z == 180)
        {
            flow_ = neighbours[2];
            return flow_;
        }
        else if (z == 240)
        {
            flow_ = neighbours[4];
            return flow_;
        }
        else if (z == 300)
        {
            flow_ = neighbours[5];
            return flow_;
        }
        Debug.Log(flow);
        return null;

    }


    //Berechnet anhandt der Drehnung der Kachel die Optionsfelder des Spielers
    public GameObject[] PlayerNextField()
    {
        GameObject[] playerNextField = new GameObject[3];
        if (flowAngle == 0)
        {
            playerNextField[0] = neighbours[5];
            playerNextField[1] = neighbours[3];
            playerNextField[2] = neighbours[1];
        }
        else if (flowAngle > 59 && flowAngle < 61)
        {
            playerNextField[0] = neighbours[3];
            playerNextField[1] = neighbours[1];
            playerNextField[2] = neighbours[0];
        }
        else if (flowAngle > 119 && flowAngle < 121)
        {
            playerNextField[0] = neighbours[1];
            playerNextField[1] = neighbours[0];
            playerNextField[2] = neighbours[2];
        }
        else if (flowAngle == 180)
        {
            playerNextField[0] = neighbours[0];
            playerNextField[1] = neighbours[2];
            playerNextField[2] = neighbours[4];
        }
        else if (flowAngle == 240)
        {
            playerNextField[0] = neighbours[2];
            playerNextField[1] = neighbours[4];
            playerNextField[2] = neighbours[5];
        }
        else if (flowAngle == 300)
        {
            playerNextField[0] = neighbours[4];
            playerNextField[1] = neighbours[5];
            playerNextField[2] = neighbours[3];
        }
        return playerNextField;
    }

}
