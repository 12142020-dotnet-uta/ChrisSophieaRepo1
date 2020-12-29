using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace P0_ChrisSophiea
{
    public class DAOMethodsImpl : DAOMethods
    {
        DAOUtility db = new DAOUtility();

        public DAOMethodsImpl() { }
        public DAOMethodsImpl(DAOUtility context)
        {
            this.db = context;
        }
        public Customer AddCustomer(Customer c)
        {
            Customer customer = db.customers.Where(x => x.EmailAddress == c.EmailAddress).FirstOrDefault();
            if (customer == null)
            {
                db.customers.Add(c);

            }
            else
            {
                Console.WriteLine("Adding customer failed. Customer already exists in DB.");
            }
            db.SaveChanges();
            return db.customers.Where(x => x.EmailAddress == c.EmailAddress).First();
        }

        public void DeleteCustomer(Customer c)
        {

            Customer customer = db.customers.Where(x => x.CustomerId == c.CustomerId).FirstOrDefault();
            if (customer != null)
            {
                db.customers.Remove(c);
            }
            else
            {
                Console.WriteLine("Removing customer failed. Customer doesn't exist in the DB.");
            }
            db.SaveChanges();
        }

        public ICollection<Customer> GetAllCustomers()
        {
            // db.SaveChanges();
            return db.customers.ToList();
        }

        public void PrintAllCustomers()
        {

            foreach (Customer c in db.customers.ToList())
            {
                Console.WriteLine(c);
            }
            // db.SaveChanges();
        }

        public ICollection<Customer> GetCustomersByEmail(string email)
        {
            ICollection<Customer> customers = new List<Customer>();
            foreach (Customer c in db.customers.ToList())
            {
                if (c.EmailAddress == email)
                {
                    customers.Add(c);
                }
            }
            return customers;
        }

        public ICollection<Customer> GetCustomersByName(string fname, string lname)
        {
            ICollection<Customer> customers = new List<Customer>();
            foreach (Customer c in db.customers.ToList())
            {
                if (c.Fname == fname && c.Lname == lname)
                {
                    customers.Add(c);
                }
            }
            // db.SaveChanges();
            return customers;
        }

        public ICollection<Purchase> CustomerPurchases(Customer customer)
        {
            return customer.Purchases;
        }


        public void UpdateCustomer(Customer c, string newfname, string newlname, string newemail)
        {
            Customer customer = db.customers.Where(x => x.CustomerId == c.CustomerId).FirstOrDefault();
            if (customer == null)
            {
                Console.WriteLine("Could not update customer. It doesn't exist in the DB.");
            }
            else
            {
                customer.Fname = newfname;
                customer.Lname = newlname;
                customer.EmailAddress = newemail;
                db.customers.Update(customer);
            }
            db.SaveChanges();
        }

        public void AddInventory(Store store, Item item, int amount)
        {
            Inventory i = new Inventory();
            i = db.inventories.Include(a => a.Store1).Include(a => a.Item1).Where(x => x.Store1.StoreAddress == store.StoreAddress && x.Item1.ItemName == item.ItemName).FirstOrDefault();
            if (i == null)
            {
                i = new Inventory();
                i.Store1 = store;
                i.Item1 = item;
                i.InventoryAmount += amount;
                // db.stores.Attach(store);
                // db.items.Attach(item);
                db.inventories.Add(i);

            }
            else
            {
                i.InventoryAmount += amount;
                // db.stores.Attach(i.Store1);
                // db.items.Attach(i.Item1);
                db.inventories.Update(i);

            }

            db.SaveChanges();
        }

        public ICollection<Inventory> GetInventory(Store store)
        {

            ICollection<Inventory> allInventory = new List<Inventory>();
            foreach (Inventory i in db.inventories.ToList())
            {
                if (i.Store1.StoreId == store.StoreId)
                {
                    allInventory.Add(i);
                    //db.Entry(i.Store1).State = 0;
                }
            }
            //db.SaveChanges();
            return allInventory;
        }
        public ICollection<Inventory> GetInventoryByType(Store store, string itemType)
        {

            ICollection<Inventory> inventories = new List<Inventory>();
            foreach (Inventory i in db.inventories.Include(a => a.Store1).Include(a => a.Item1).ToList())
            {
                if (i.Store1.StoreId == store.StoreId && i.Item1.ItemType == itemType)
                {
                    inventories.Add(i);
                    // db.Entry(i.Store1).State = 0;
                    // db.Entry(i.Item1).State = 0;
                }
            }
            // db.SaveChanges();
            return inventories;
        }

        public Inventory GetInventoryById(int Id)
        {

            Inventory inventory = new Inventory();
            foreach (Inventory i in db.inventories.Include(a => a.Item1).Include(a => a.Store1).ToList())
            {
                if (i.InventoryId == Id)
                {
                    inventory = i;
                }
            }
            //db.SaveChanges();
            return inventory;
        }

        public void PrintAllInventory(Store store)
        {

            foreach (Inventory i in db.inventories.Include(a => a.Store1).Include(a => a.Item1).ToList())
            {
                if (i.Store1.StoreId == store.StoreId)
                {
                    Console.WriteLine(i);
                }
            }
            // db.SaveChanges();

        }

        public void PrintInventoryByType(Store store, string type)
        {

            foreach (Inventory i in db.inventories.Include(a => a.Store1).Include(a => a.Item1).ToList())
            {
                if (i.Store1.StoreId == store.StoreId && i.Item1.ItemType == type)
                {
                    Console.WriteLine(i);

                }
            }
            //db.SaveChanges();

        }
        public void ReduceInventory(Inventory i, int amount)
        {
            Inventory inventory = new Inventory();
            inventory = db.inventories.Where(x => x.InventoryId == i.InventoryId).FirstOrDefault();
            if (i.InventoryAmount - amount >= 0)
            {
                inventory.InventoryAmount -= amount;
                db.inventories.Update(inventory);


            }
            else
            {
                Console.WriteLine("Could not reduce inventory. Not that much instock.");
            }
            db.SaveChanges();

        }

        public void AddItem(Item i)
        {
            Item item = new Item();
            item = db.items.Where(x => x.ItemName == i.ItemName).FirstOrDefault();
            if (item == null)
            {
                db.items.Add(i);

            }
            else
            {
                Console.WriteLine("Adding item failed. Item already exists in DB.");
            }
            db.SaveChanges();
        }

        public void DeleteItem(Item i)
        {
            Item item = new Item();
            item = db.items.Where(x => x.ItemName == i.ItemName).FirstOrDefault();
            if (item != null)
            {
                db.items.Remove(i);
            }
            else
            {
                Console.WriteLine("Removing item failed. Item doesn't exist in DB.");
            }
            db.SaveChanges();
        }

        public Item GetItemById(int id)
        {

            return db.items.Where(x => x.ItemId == id).FirstOrDefault();
        }

        public List<Item> GetAllItems()
        {
            //db.SaveChanges();
            return db.items.ToList();
        }

        public List<Item> GetItemsByType(string type)
        {
            //db.SaveChanges();
            return db.items.Where(x => x.ItemType == type).ToList();
        }

        public List<Item> GetItemsByName(string itemName)
        {

            List<Item> itemsListByName = new List<Item>();
            foreach (Item i in db.items)
            {
                if (i.ItemName.Equals(itemName, StringComparison.OrdinalIgnoreCase))
                {
                    itemsListByName.Add(i);
                }
            }
            //db.SaveChanges();
            return itemsListByName;
        }

        public void UpdateItem(Item i, string newItemName, string newItemDescrition, double newItemPrice)
        {

            Item item = new Item();
            item = db.items.Where(x => x.ItemName == i.ItemName).FirstOrDefault();
            if (item == null)
            {
                Console.WriteLine("Could not update item. It doesn't exist in the DB.");
            }
            else
            {
                item.ItemName = newItemName;
                item.ItemPrice = newItemPrice;
                db.items.Update(item);
            }
            db.SaveChanges();
        }

        public void AddPurchase(Purchase p)
        {
            Purchase purchase = new Purchase();
            purchase = db.purchases.Include(a => a.Item1).Include(a => a.Store1).Where(x => x.PurchaseId == p.PurchaseId).FirstOrDefault();
            if (purchase == null)
            {
                if (p.TotalPrice != 0)
                {
                    db.purchases.Add(p);

                }
                else
                {
                    Console.WriteLine("You didn't select any items to purchase.");
                }
            }
            else
            {
                Console.WriteLine("This purchase already went through.");
            }
            db.SaveChanges();


        }


        public void DeletePurchase(Purchase p)
        {

            Purchase purchase = new Purchase();
            purchase = db.purchases.Where(x => x.PurchaseId == p.PurchaseId).FirstOrDefault();
            if (purchase != null)
            {
                db.purchases.Remove(purchase);
            }
            else
            {
                Console.WriteLine("Can't remove purchase. It doesn't exist in the DB.");
            }

        }

        public ICollection<Purchase> GetAllPurchases()
        {
            return db.purchases.ToList();
        }

        public ICollection<Purchase> GetPurchasesByCustomer(Customer c)
        {
            ICollection<Purchase> purchaseListByCustomer = new List<Purchase>();
            foreach (Purchase purchase in db.purchases.Include(a => a.Item1).Include(a => a.Store1).Include(a => a.Customer1).ToList())
            {
                if (purchase.Customer1.CustomerId == c.CustomerId)
                {
                    purchaseListByCustomer.Add(purchase);
                }
            }
            return purchaseListByCustomer;
        }

        public ICollection<Purchase> GetPurchasesByStore(Store store)
        {
            ICollection<Purchase> purchaseListByStore = new List<Purchase>();
            foreach (Purchase purchase in db.purchases.Include(a => a.Item1).Include(a => a.Store1).Include(a => a.Customer1).ToList())
            {
                if (purchase.Store1.StoreId == store.StoreId)
                {
                    purchaseListByStore.Add(purchase);
                }
            }
            return purchaseListByStore;
        }

        public ICollection<Purchase> GetPurchasesByItem(Item item)
        {
            ICollection<Purchase> purchaseListByItem = new List<Purchase>();
            foreach (Purchase purchase in db.purchases.Include(a => a.Item1).Include(a => a.Store1).Include(a => a.Customer1).ToList())
            {
                foreach (Item i in purchase.Item1)
                {
                    if (i.ItemId == item.ItemId)
                    {
                        purchaseListByItem.Add(purchase);
                    }
                }
            }
            return purchaseListByItem;
        }


        public void AddStore(Store s)
        {
            Store store = new Store();
            store = db.stores.Where(x => x.StoreAddress == s.StoreAddress).FirstOrDefault();
            if (store == null)
            {
                db.stores.Add(s);
                //
            }
            else
            {
                Console.WriteLine("Adding store failed. Store already exists in DB.");
            }
            db.SaveChanges();
        }

        public void DeleteStore(Store s)
        {
            Store store = new Store();
            store = db.stores.Where(x => x.StoreAddress == s.StoreAddress).FirstOrDefault();
            if (store != null)
            {
                db.stores.Remove(s);
                // db.SaveChanges();
            }
            else
            {
                Console.WriteLine("Store deletion failed. Store doesn't exist in DB.");
            }
            db.SaveChanges();
        }

        public ICollection<Store> GetAllStores()
        {
            ICollection<Store> stores = new List<Store>();
            foreach (Store s in db.stores.ToList())
            {
                stores.Add(s);
            }
            return stores;
        }

        public void PrintAllStores()
        {
            foreach (Store s in db.stores.ToList())
            {
                Console.WriteLine(s);
            }
        }

        public Store GetStoreByAddress(string s)
        {
            Store store = new Store();
            foreach (Store s1 in db.stores.ToList())
            {
                if (s1.StoreAddress == s)
                {
                    store = s1;
                }
            }
            return store;
        }

        public Store GetStoreById(int id)
        {
            Store store = new Store();
            foreach (Store s in db.stores.ToList())
            {
                if (s.StoreId == id)
                {
                    store = s;
                }
            }
            return store;
        }

        public Store GetStoreByPhone(string p)
        {
            Store store = db.stores.Where(x => x.PhoneNumber == p).FirstOrDefault();
            return store;
        }

        public ICollection<Inventory> GetInventoriesByStore(Store store)
        {
            return store.Inventories;
        }

        public void UpdateStore(Store s, string address, string phone)
        {
            Store store = new Store();
            store = db.stores.Where(x => x.StoreAddress == s.StoreAddress).FirstOrDefault();
            if (store == null)
            {
                Console.WriteLine("Could not update store. It doesn't exist in the DB.");
            }
            else
            {
                store.StoreAddress = address;
                store.PhoneNumber = phone;
                db.stores.Update(store);
                //
            }
            db.SaveChanges();

        }


    }
}