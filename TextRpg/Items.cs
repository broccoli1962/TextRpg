using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg
{
    public enum ItemType{
        None,
        Weapon,
        Armor,
    }
    internal class Items
    {
        public ItemType itemType;
        public string ItemName { get;}
        public string ItemDescription { get; }
        public int ItemAtk { get; set; }
        public int ItemDef { get; set; }
        public float ItemGold { get; }


        public Items[] item;
        public bool isEquip = false;
        public bool isSell = false;
        public Items(string itemName, string descript, ItemType type, int atk, int def, int price)
        {
            this.itemType = type;
            this.ItemName = itemName;
            this.ItemAtk = atk;
            this.ItemDef = def;
            this.ItemDescription = descript;
            this.ItemGold = price;
        }

        public void ItemShow()
        {
            if (isEquip)
            {
                Console.Write("[E]");
            }
            Console.Write($"{ItemName}\t|");

            if( itemType == ItemType.Weapon)
            {
                Console.Write($" 공격력 + {ItemAtk}\t| {ItemDescription}");
            }
            else
            {
                Console.Write($" 방어력 + {ItemDef}\t| {ItemDescription}");
            }
        }

        public void ItemShopShow()
        {
            Console.Write($"{ItemName}\t|");

            if (itemType == ItemType.Weapon)
            {
                Console.Write($" 공격력 + {ItemAtk}\t| {ItemDescription}\t");
            }
            else
            {
                Console.Write($" 방어력 + {ItemDef}\t| {ItemDescription}\t");
            }

            if (isSell) {
                Console.Write("| 구매완료");
            }
            else
            {
                Console.Write($"| {ItemGold} G");
            }
        }

        public void ItemShopSellShow()
        {
            Console.Write($"{ItemName}\t|");

            if (itemType == ItemType.Weapon)
            {
                Console.Write($" 공격력 + {ItemAtk}\t| {ItemDescription}\t");
            }
            else
            {
                Console.Write($" 방어력 + {ItemDef}\t| {ItemDescription}\t");
            }
            Console.Write($"| {ItemGold * 0.85f} G");
        }
    }
}
