using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

namespace PokeEngine.Items
{
    public enum ItemType:byte { Misc, Medicine, TMHM, Berry, KeyItem };

    public class Item
    {
        //class fields
        public String name;
        public String description;
        public int itemID;    //unique ID of the item
        public String script; //lua script for item action
        public ItemType type; //category for item
        private static List<Item> itemList;

        public Item()
        {
            name = "Default Name";
            description = "Default Description";
            itemID = 0;
            script = null;
            type = ItemType.Misc;
        }

        public static Item getItem(int ID)
        {
            Item returnedItem = null;

            if (ID < itemList.Count)
            {
                if (itemList[ID].itemID == ID)
                    return itemList[ID];
            }

            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i].itemID == ID)
                {
                    returnedItem = itemList[i];
                    break;
                }
            }

            if (returnedItem == null)
                throw new Exception("Can not find the item in the item list");
            else
                return returnedItem;
        }

        public static Item getItem(String name)
        {
            Item returnedItem = null;

            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i].name == name)
                {
                    returnedItem = itemList[i];
                    break;
                }
            }

            if (returnedItem == null)
                throw new Exception("Can not find the item in the item list");
            else
                return returnedItem;
        }

        public static void addItem(Item item)
        {
            item.itemID = itemList.Count;
            itemList.Add(item);
        }

        public static void removeItem(Item item)
        {
            removeItem(item.itemID);
        }

        public static void removeItem(int ID)
        {
            itemList.RemoveAt(ID);
            for (int i = ID; i < itemList.Count; i++)
            {
                itemList[i].itemID = i;
            }
        }

        //Save and Load methods
        ////////////////////////////////
                public static void SaveItem(Item inItem, BinaryWriter writer)
        {
            writer.Write(inItem.name);
            writer.Write(inItem.description);
            writer.Write(inItem.itemID);
            writer.Write(inItem.script);
            writer.Write((byte)inItem.type);
        }

        public static Item LoadItem(BinaryReader reader)
        {
            Item item = new Item();

            item.name = reader.ReadString();
            item.description = reader.ReadString();
            item.itemID = reader.ReadInt32();
            item.script = reader.ReadString();
            item.type = (ItemType)reader.ReadByte();

            return item;
        }

        public static void SaveItemList(BinaryWriter writer)
        {
            int num = itemList.Count;
            writer.Write(num);
            for (int i = 0; i < num; i++)
            {
                SaveItem(itemList[i], writer);
            }
        }

        public static void LoadItemList(BinaryReader reader)
        {
            int num = reader.ReadInt32();

            itemList.Clear();

            for (int i = 0; i < num; i++)
            {
                itemList.Add(LoadItem(reader));
            }
        }
    }
}
