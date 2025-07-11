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
        public ItemType ItemType { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int ItemAtk { get; set; }
        public int ItemDef { get; set; }
        public float ItemGold { get; set; }

        public bool IsEquip {  get; set; }
        public bool IsSell {  get; set; }

        public Items() { }

        public Items(string itemName, string descript, ItemType type, int atk, int def, int price)
        {
            this.ItemType = type;
            this.ItemName = itemName;
            this.ItemAtk = atk;
            this.ItemDef = def;
            this.ItemDescription = descript;
            this.ItemGold = price;
            this.IsEquip = false;
            this.IsSell = false;
        }

        public void ItemShow()
        {
            Console.Write(IsEquip ? "[E]" : "");
            Console.Write($"{ItemName}\t|");
            if(ItemType == ItemType.Weapon)
            {
                Console.Write($" 공격력 + {ItemAtk}\t| ");
            }
            if (ItemType == ItemType.Armor)
            {
                Console.Write($" 방어력 + {ItemDef}\t| ");
            }
            Console.Write($"{ItemDescription}");
        }

        public void ItemShopShow()
        {
            Console.Write($"{ItemName}\t|");
            if (ItemType == ItemType.Weapon)
            {
                Console.Write($" 공격력 + {ItemAtk}\t| ");
            }
            if (ItemType == ItemType.Armor)
            {
                Console.Write($" 방어력 + {ItemDef}\t| ");
            }
            Console.Write($"{ItemDescription}\t|");
            Console.Write(IsSell ? "구매완료" : $"{ItemGold} G");
        }

        public void ItemShopSellShow()
        {
            ItemShow();
            Console.Write($"| {ItemGold * 0.85f:F0} G");
        }
    }
}
