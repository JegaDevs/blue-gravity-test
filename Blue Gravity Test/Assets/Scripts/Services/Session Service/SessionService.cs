using Jega.BlueGravity.PreWrittenCode;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jega.BlueGravity
{
    public class SessionService : IService
    {
        public int Priority => 0;
        private ShopInventory currentShopInventory;

        public ShopInventory CurrentShopInventory => currentShopInventory;
        public bool IsShopActive => currentShopInventory != null;
        public void Preprocess()
        {
        }
        public void Postprocess()
        {
        }


        public void RegisterActiveShopInventory(ShopInventory shopInventory) 
        {
            currentShopInventory = shopInventory;
        }

        public void UnregisterActiveShopInventory()
        {
            currentShopInventory = null;
        }

    }
}
