using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data;

namespace DataAccessLayer
{
    /// <summary>
    /// Eric Bostwick
    /// Created: 2/14/2019
    /// i
    /// Mock DataAccessor implements IItemSupplierAccessor used for testing
    /// </summary>
    public class ItemSupplierAccessorMock : IItemSupplierAccessor
    {
        public List<Supplier> _suppliers = new List<Supplier>();
        public List<ItemSupplier> _itemSuppliers = new List<ItemSupplier>(); 

        public ItemSupplierAccessorMock()
        {
            _suppliers.Add(new Supplier()
            { SupplierID = 100000, Name = "Dunder Soaps", Address = "1234 Washington St.", City = "Cedar Rapids", State = "IA", PostalCode = "52242",
                Country = "USA", PhoneNumber = "13195551111", Email = "dunder@soaps.com", ContactFirstName = "Jim", ContactLastName = "Halpert",
                DateAdded = DateTime.Parse("3/14/2002"), Description = "All of the tiny soaps for the hotel rooms are supplied by them.", Active = true
            });
            _suppliers.Add(new Supplier()
            { SupplierID = 100001, Name = "Soaps and Stuff", Address = "1234 Jefferson St.", City = "Cedar Rapids", State = "IA", PostalCode = "52242",
                Country = "USA", PhoneNumber = "13195551111", Email = "Bart @soapsandstuff.com", ContactFirstName = "Bart", ContactLastName = "Smith",
                DateAdded = DateTime.Parse("2002-03-14") , Description = "Cuz we needed another soap supplier.", Active = true
            });
            _suppliers.Add(new Supplier()
            { SupplierID = 100002, Name = "Bob Vance Fruit", Address = "1334 Madison St.", City = "Cedar Rapids", State = "IA", PostalCode = "52242",
                Country = "USA", PhoneNumber = "13195551111", Email = "Bob-Vance @fruit.com", ContactFirstName = "Bob", ContactLastName = "Vance",
                DateAdded = DateTime.Parse("1998-03-14") , Description = "All of our fruits for the kitchen and catering come from them.", Active = true
            });
            _suppliers.Add(new Supplier()
            { SupplierID = 100003, Name = "Pete's Produce", Address = "5555 Red Pepper St.", City = "Iowa City", State = "IA", PostalCode = "52240",
                Country = "USA", PhoneNumber = "13195551111", Email = "petepiper@petes.com", ContactFirstName = "Pete", ContactLastName = "Piper",
                DateAdded = DateTime.Parse("2005-05-25") , Description = "Pete picks the best pickled peppers.", Active = true
            });
            _suppliers.Add(new Supplier()
            { SupplierID = 100004, Name = "Plates and Silverware", Address = "5214 CupsAndSaucers Lane", City = "Iowa City", State = "IA", PostalCode = "52240",
                Country = "USA", PhoneNumber = "13195551111", Email = "plates @silverware.com", ContactFirstName = "Carly", ContactLastName = "Jones",
                DateAdded = DateTime.Parse("2000-06-25") , Description = "The finest plates and silverware you will ever find.", Active = true
            });
            _suppliers.Add(new Supplier()
            { SupplierID = 100005, Name = "Cheap Dishes", Address = "5214 Chipped St", City = "Cedar Rapids", State = "IA", PostalCode = "52240",
                Country = "USA", PhoneNumber = "13195551111", Email = "hilda @cheapdishes.com", ContactFirstName = "Hilda", ContactLastName = "Hope",
                DateAdded = DateTime.Parse("2010-04-25") , Description = "What do you expect for this much", Active = true
            });
             _suppliers.Add(new Supplier()
            { SupplierID = 100006, Name = "Pets Plus", Address = "5444 Droopy Court", City = "Marengo", State = "IA", PostalCode = "52240",
                Country = "USA", PhoneNumber = "13195551111", Email = "pets @plus.com", ContactFirstName = "Kevin", ContactLastName = "Bentley",
                DateAdded = DateTime.Parse("2010-04-25") , Description = "They give us the pet supplies so we can feed them and make them look nice.", Active = true
            });
             _suppliers.Add(new Supplier()
            { SupplierID = 100007, Name = "Pet Stuff", Address = "555 Jungle Blvd", City = "Des Moines", State = "IA", PostalCode = "55544",
                Country = "USA", PhoneNumber = "13195551111", Email = "marlin @mutualofomaha.com", ContactFirstName = "Marlin", ContactLastName = "Perkins",
                DateAdded = DateTime.Parse("2004-10-25") , Description = "We don't believe in having pets, but we have to make a living somehow.", Active = true
            });
             _suppliers.Add(new Supplier()
            { SupplierID = 100008, Name = "Vending Machines", Address = "555 Coin Slot Blvd", City = "North Liberty", State = "IA", PostalCode = "52446",
                Country = "USA", PhoneNumber = "13195551111", Email = "vending @machines.com", ContactFirstName = "Harry", ContactLastName = "Plath",
                DateAdded = DateTime.Parse("2014-06-15") , Description = "They put the snacks in the vending machines.", Active = true
            });
            _suppliers.Add(new Supplier()
            { SupplierID = 100009, Name = "Alcohol Whole Supply", Address = "2455 Staggering Home St", City = "North Liberty", State = "IA", PostalCode = "52446",
                Country = "USA", PhoneNumber = "13195551111", Email = "alcohol @supply.com", ContactFirstName = "Frank", ContactLastName = "Welsh",
                DateAdded = DateTime.Parse("2011-05-15") , Description = "They supply us with all of our alcohol for the bars and the kitchen", Active = true
            });
 
            _itemSuppliers.Add(new ItemSupplier()
            {
                Name = "Cheap Dishes",
                ContactFirstName = "Hilda",
                ContactLastName = "Hope",
                PhoneNumber = "13195551212",
                Email = "Hilda@cheapdishes.com",
                DateAdded = new DateTime(2009, 10, 10),
                Address = "1234 Chipped Glass St.",
                City = "Eldora",
                State = "IA",
                Country = "USA",
                PostalCode = "52680",
                Description = "Don't complain, there cheap",
                Active = true,
                ItemID = 100002,
                SupplierID = 100005,
                PrimarySupplier = false,
                LeadTimeDays = 100,
                UnitPrice = 0.75M,
                ItemSupplierActive = true
            });
            _itemSuppliers.Add(new ItemSupplier()
            {
                Name = "Plates and Silverware",
                ContactFirstName = "Carly",
                ContactLastName = "Jones",
                PhoneNumber = "13195551212",
                Email = "plates@silverware.com",
                DateAdded = new DateTime(2005, 8, 10),
                Address = "1234 China St.",
                City = "Algona",
                State = "IA",
                Country = "USA",
                PostalCode = "52785",
                Description = "We got the plates and stuff",
                Active = true,
                ItemID = 100002,
                SupplierID = 100004,
                PrimarySupplier = true,
                LeadTimeDays = 25,
                UnitPrice = 1.45M,
                ItemSupplierActive = true
            });
            _itemSuppliers.Add(new ItemSupplier()
            {
                Name = "Pete''s Produce",
                ContactFirstName = "Pete",
                ContactLastName = "Piper",
                PhoneNumber = "13195551212",
                Email = "petepiper@petes.com",
                DateAdded = new DateTime(1997, 6, 10),
                Address = "1234 Red Pepper St.",
                City = "Pickled",
                State = "Wisconsin",
                Country = "USA",
                PostalCode = "54543",
                Description = "Buy em by the peck",
                Active = true,
                ItemID = 100001,
                SupplierID = 100003,
                PrimarySupplier = false,
                LeadTimeDays = 5,
                UnitPrice = 0.25M,
                ItemSupplierActive = true
            });
            _itemSuppliers.Add(new ItemSupplier()
            {
                Name = "Bob Vance Fruit",
                ContactFirstName = "Bob",
                ContactLastName = "Vance",
                PhoneNumber = "13195551212",
                Email = "Bob-Vance@fruit.com",
                DateAdded = new DateTime(2008, 3, 14),
                Address = "1234 Melon St.",
                City = "Lisbon",
                State = "IA",
                Country = "USA",
                PostalCode = "52316",
                Description = "aafafafa",
                Active = true,
                ItemID = 100001,
                SupplierID = 100002,
                PrimarySupplier = true,
                LeadTimeDays = 5,
                UnitPrice = 0.25M,
                ItemSupplierActive = true
            });
            _itemSuppliers.Add(new ItemSupplier()
            {
                Name = "Dunder Soaps",
                ContactFirstName = "Jim",
                ContactLastName = "Halpert",
                PhoneNumber = "13195551212",
                Email = "dunder@soaps.com",
                DateAdded = new DateTime(2004, 3, 14),
                Address = "1234 Washington St.",
                City = "Cedar Rapids",
                State = "IA",
                Country = "USA",
                PostalCode = "52242",
                Description = "aafafafa",
                Active = true,
                ItemID = 100000,
                SupplierID = 100000,
                PrimarySupplier = true,
                LeadTimeDays = 3,
                UnitPrice = 0.50M,
                ItemSupplierActive = true
            });
            _itemSuppliers.Add(new ItemSupplier()
            {
                Name = "Soaps and Stuff",
                ContactFirstName = "Art",
                ContactLastName = "Vandalay",
                PhoneNumber = "13195551212",
                Email = "soaps@soaps.com",
                DateAdded = new DateTime(2004, 3, 14),
                Address = "1234 Washington St.",
                City = "Cedar Rapids",
                State = "IA",
                Country = "USA",
                PostalCode = "52242",
                Description = "aafafafa",
                Active = true,
                ItemID = 100000,
                SupplierID = 100001,
                PrimarySupplier = false,
                LeadTimeDays = 5,
                UnitPrice = 0.51M,
                ItemSupplierActive = true
            });

        }

        public int DeactivateItemSupplier(int itemID, int supplierID)
        {
            bool found = false;
            foreach (var itemSupplier in _itemSuppliers)
            {
                if (itemSupplier.ItemID == itemID && itemSupplier.SupplierID == supplierID)
                {
                    itemSupplier.ItemSupplierActive = false;
                    found = true;                                       
                }

            }
            if (found)
            {
                return 1;                
            }
            return 0;
        }

        public int DeleteItemSupplier(int itemID, int supplierID)
        {
            if(_itemSuppliers.Remove(_itemSuppliers.Find(x => x.ItemID == itemID && x.SupplierID == supplierID)))
            {
                return 1;
            }
            else
            {
                return 0;
            }            
        }

        public int InsertItemSupplier(ItemSupplier itemSupplier)
        {
            int listCount = _itemSuppliers.Count;
            _itemSuppliers.Add(itemSupplier);

            if(_itemSuppliers.Count == listCount + 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }            
        }


        public List<Supplier> SelectAllSuppliersForItemSupplierManagement(int itemID)
        {
            List<ItemSupplier> usedItemSuppliers = _itemSuppliers;
            usedItemSuppliers = _itemSuppliers.FindAll(x => x.ItemID == itemID);

            foreach (var supplier in usedItemSuppliers)
                {                
                   _suppliers.Remove(_suppliers.Find(x => x.SupplierID == supplier.SupplierID));
                }

            return _suppliers;
            
        }

        public ItemSupplier SelectItemSupplierByItemIDandSupplierID(int itemID, int supplierID)
        {            
            ItemSupplier i = new ItemSupplier();
            i = _itemSuppliers.Find(x => x.ItemID == itemID & x.SupplierID == supplierID);
            if (i == null)
            {
                throw new ArgumentException("Item Supplier Not Found");
            }
            return i;
        }

        public List<ItemSupplier> SelectItemSuppliersByItemID(int itemID)
        {
            return _itemSuppliers.FindAll(x => x.ItemID == itemID);
        }

        public int UpdateItemSupplier(ItemSupplier newItemSupplier, ItemSupplier oldItemSupplier)
        {
            int result = 0;
            foreach (var itemSupplier in _itemSuppliers)
            {
                if (itemSupplier.ItemID == oldItemSupplier.ItemID && itemSupplier.SupplierID == oldItemSupplier.SupplierID)
                {
                    itemSupplier.PrimarySupplier = newItemSupplier.PrimarySupplier;
                    itemSupplier.LeadTimeDays = newItemSupplier.LeadTimeDays;
                    itemSupplier.UnitPrice = newItemSupplier.UnitPrice;
                    itemSupplier.ItemSupplierActive = newItemSupplier.ItemSupplierActive;
                    result = 1;
                }
            }

            return result;
        }
    }
}
