using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BafsSeller : MonoBehaviour
{
    [SerializeField] private BafProduct[] bafProducts;

    private void OnValidate()
    {
        foreach (var item in bafProducts)
        {
            item.Text.text = item.Price.ToString();
        }
    }

    public void ActiveBaf(int index)
    {
        BafProduct bafProduct = FindBaf(index);
        if(ScoreAndMoney.SingleTone.TryToBye(bafProduct.Price) == false) return;

        bafProduct.OnBuyEvent.Invoke();
    }

    private BafProduct FindBaf(int index)
    {
        foreach (var item in bafProducts)
        {
            if (item.BafName.GetHashCode() == index) return item;
        }
        return null;
    }
}

[System.Serializable]
public class BafProduct
{
    public BafName BafName;
    public UnityEvent OnBuyEvent;
    public int Price;
    public Text Text;
}

public enum BafName
{
    Immortality,
    Lopata,
    Star,
    TimeCrio,
    Bomb
}
