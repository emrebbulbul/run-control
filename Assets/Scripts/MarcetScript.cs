using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using Emin;
using System;



public class MarcetScript : MonoBehaviour,IStoreListener
{
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_ExtensionProvider;

    private static string Point_250 = "";
    private static string Point_500 = "";
    private static string Point_750 = "";
    private static string Point_1000 = "";
    private static string Point_1500 = "";
    private static string Point_2000 = "";

    DataManagement _DataManagement = new DataManagement();
    MemoryManagement _MemoryManagement = new MemoryManagement();
    void Start()
    {
        if (m_StoreController==null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {

        if (IsInitalized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(Point_250, ProductType.Consumable);
        builder.AddProduct(Point_500, ProductType.Consumable);
        builder.AddProduct(Point_750, ProductType.Consumable);
        builder.AddProduct(Point_1000, ProductType.Consumable);
        builder.AddProduct(Point_1500, ProductType.Consumable);
        builder.AddProduct(Point_2000, ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);
    }
    public void BuyProduct_250()
    {
        BuyProductID(Point_250);
    }
    public void BuyProduct_500()
    {
        BuyProductID(Point_500);
    }
    public void BuyProduct_750()
    {
        BuyProductID(Point_750);
    }
    public void BuyProduct_1000()
    {
        BuyProductID(Point_1000);
    }
    public void BuyProduct_1500()
    {
        BuyProductID(Point_1500);
    }
    public void BuyProduct_2000()
    {
        BuyProductID(Point_2000);
    }

    void BuyProductID(string productID)
    {
        if (IsInitalized())
        {
            Product product = m_StoreController.products.WithID(productID);
            if (product !=null && product.availableToPurchase)
            {
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("Satýn alýrken hata oluþtu");
            }
        }
        else
        {
            Debug.Log("ürün çaðrýlamýyor.");
        }
                   
    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        if (String.Equals(purchaseEvent.purchasedProduct.definition.id,Point_250,StringComparison.Ordinal))
        {
            _MemoryManagement.DataSave_int("Point", _MemoryManagement.DataRead_i("Point") + 250);
        }
        else  if (String.Equals(purchaseEvent.purchasedProduct.definition.id, Point_500, StringComparison.Ordinal))
        {
            _MemoryManagement.DataSave_int("Point", _MemoryManagement.DataRead_i("Point") + 500);
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, Point_750, StringComparison.Ordinal))
        {
            _MemoryManagement.DataSave_int("Point", _MemoryManagement.DataRead_i("Point") + 750);
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, Point_1000, StringComparison.Ordinal))
        {
            _MemoryManagement.DataSave_int("Point", _MemoryManagement.DataRead_i("Point") + 1000);
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, Point_1500, StringComparison.Ordinal))
        {
            _MemoryManagement.DataSave_int("Point", _MemoryManagement.DataRead_i("Point") + 1500);
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, Point_2000, StringComparison.Ordinal))
        {
            _MemoryManagement.DataSave_int("Point", _MemoryManagement.DataRead_i("Point") + 2000);
        }
        return PurchaseProcessingResult.Complete;
    }
    private bool IsInitalized()
    {
        return m_StoreController != null && m_ExtensionProvider != null;
    }
 

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        m_ExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    

    public void TurnBack()
    {
        SceneManager.LoadScene(0);

        
    }
}
