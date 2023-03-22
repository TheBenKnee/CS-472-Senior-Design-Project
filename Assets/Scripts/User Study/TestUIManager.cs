using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIManager : MonoBehaviour
{
    public GameObject PawnHover;
    public GameObject CoinHover;
    public GameObject OreHover;

    public void EnableHover(int type)
    {
        switch(type)
        {
            case 0:
            {
                PawnHover.SetActive(true);
                break;
            }
            case 1:
            {
                CoinHover.SetActive(true);
                break;
            }
            case 2:
            {
                OreHover.SetActive(true);
                break;
            }
        }
    }

    public void DisableHover(int type)
    {
        switch(type)
        {
            case 0:
            {
                PawnHover.SetActive(false);
                break;
            }
            case 1:
            {
                CoinHover.SetActive(false);
                break;
            }
            case 2:
            {
                OreHover.SetActive(false);
                break;
            }
        }
    }
}
