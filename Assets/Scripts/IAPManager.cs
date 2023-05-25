using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HmsPlugin;
using HuaweiMobileServices.IAP;
using HuaweiMobileServices.Utils;

using System.Linq;
using HuaweiMobileServices.Base;

public class IAPManager : MonoBehaviour
{
    GameManager gm;



    public void OnBuyProductSuccess(PurchaseResultInfo obj)
    {
        string myProductId = obj.InAppPurchaseData.ProductId;


        if (myProductId.Equals("250D"))
        {
            gm.SetCoins(250);
        }
        else if (myProductId.Equals("500"))
        {
            gm.SetCoins(500);
        }
        else if (myProductId.Equals("750"))
        {
            gm.SetCoins(750);
        }
        else if (myProductId.Equals("1000"))
        {
            gm.SetCoins(1000);
        }
        else if (myProductId.Equals("premium"))
        {
            gm.SetGoldBought(1);
        }
    }

    public void BuyProduct(string id)
    {
        Debug.Log(id);
        HMSIAPManager.Instance.PurchaseProduct(id);
    }

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();


        HMSIAPManager.Instance.OnBuyProductSuccess = OnBuyProductSuccess;
    }


}