using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg
{
    internal class Player
    {
        private static Player instance;
        public static Player Instance 
        {
            get 
            { 
                if(instance == null)
                {
                    instance = new Player();
                }
                return instance;
            } 
        }

        public int Level { get; set; }
        public string Name { get; set; }
        public string Job {get; set;}
        public float Hp { get; set; }
        public float Gold { get; set; }
        public int ClearTime { get; set; }
        public float BaseStrengh { get; set; }
        public float BaseDefence { get; set; }

        public float TotalStrengh
        { 
            get {
                return BaseStrengh + AddStrengh;
            }
        }
        public float TotalDefence
        {
            get {  
                return BaseDefence + AddDefence; 
            }
        }

        private float _addStrengh;
        private float _addDefence;
        public float AddStrengh {
            get
            {
                return _addStrengh;
            }
        }
        public float AddDefence {
            get
            {
                return _addDefence;
            }
        }

        public Items EquipWeapon { get; private set; }
        public Items EquipArmor { get; private set; }

        public List<Items> Inv { get; set; }

        public void EquipItem(Items item)
        {
            if (!Inv.Contains(item)) return;

            if (item.ItemType == ItemType.Weapon) 
            {
                if(EquipWeapon != null)
                {
                    EquipWeapon.IsEquip = false;
                }

                if(EquipWeapon == item)
                {
                    EquipWeapon = null;
                    item.IsEquip = false;
                }
                else
                {
                    EquipWeapon = item;
                    item.IsEquip = true;
                }
            }
            else if(item.ItemType == ItemType.Armor)
            {
                if (EquipArmor != null)
                {
                    EquipArmor.IsEquip = false;
                }

                if(EquipArmor == item)
                {
                    EquipArmor = null;
                    item.IsEquip = false;
                }
                else
                {
                    EquipArmor = item;
                    item.IsEquip = true;
                }
            }
            UpdateStatus();
        }

        public void UpdateStatus()
        {
            _addStrengh = 0;
            _addDefence = 0;
            foreach (var item in Inv)
            {
                if (item.IsEquip)
                {
                    _addStrengh += item.ItemAtk;
                    _addDefence += item.ItemDef;
                }
            }
        }

        public DungeonResult DClearCheck(Dungeon dungeon)
        {
            DungeonResult result = new DungeonResult()
            {
                BeforeHp = this.Hp,
                BeforeGold = this.Gold,
            };

            bool IsClear = false;
            if (this.TotalDefence < dungeon.defLimit)
            {
                int rand = new Random().Next(0, 10);
                IsClear = rand < 4;
            }
            else
            {
                IsClear = true;
            }

            result.IsClear = IsClear;

            if (IsClear)
            {
                Random rand = new Random();
                float hp = (dungeon.defLimit - this.TotalDefence) + (float)rand.NextDouble() * ((35.0f + (dungeon.defLimit - this.TotalDefence)) - (dungeon.defLimit - this.TotalDefence));
                this.Hp -= hp;

                float gold = (this.TotalStrengh) + (float)rand.NextDouble() * ((this.TotalStrengh * 2) - (this.TotalStrengh));
                this.Gold += dungeon.clearGold + gold;

                this.Level++;
                this.BaseStrengh += 0.5f;
                this.BaseDefence += 1f;
                result.LevelUp = true;
            }
            else
            {
                this.Hp /= 2;
            }

            if (this.Hp < 0) this.Hp = 0;

            result.AfterHp = this.Hp;
            result.AfterGold = this.Gold;

            return result;
        }

        private Player(){
            Level = 1;
            BaseStrengh = 10;
            BaseDefence = 5;
            Hp = 100f;
            Gold = 1500f;
            Inv = new List<Items>();
            UpdateStatus();
        }
    }
}
