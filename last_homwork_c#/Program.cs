using System;
using System.Collections.Generic;

namespace homework
{
    // 캐릭터 정보 클래스
    class Character
    {
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public Item weapon { get; private set; } // 장착된 아이템(무기)

        public Character(string name, int hp, int atk, int def)
        {
            Name = name;
            Hp = hp;
            Atk = atk;
            Def = def;
        }

        // 아이템 장착
        public void EquipItem(Item item)
        {
            if (weapon != null)
            {
                Console.WriteLine($"{Name}이/가 이미 {weapon.Name}을(를) 장착하고 있습니다. 먼저 탈착하세요.");
                return;
            }

            weapon = item;
            item.IsEquipped = true;
            Console.WriteLine($"{item.Name}이/가 {Name}에게 장착되었습니다.");
        }

        // 아이템 탈착
        public void UnEquipItem()
        {
            if (weapon != null)
            {
                Console.WriteLine($"{weapon.Name}이/가 {Name}에게서 탈착되었습니다.");
                weapon.IsEquipped = false;
                weapon = null;
            }
            else
            {
                Console.WriteLine($"{Name}은/는 현재 장착한 아이템이 없습니다.");
            }
        }

        // 정보출력
        public override string ToString()
        {
            int totalAtk = Atk + (weapon?.AtkPlus ?? 0);
            int totalDef = Def + (weapon?.DefPlus ?? 0);
            return $"이름: {Name}, 체력: {Hp}, 공격력: {totalAtk}, 방어력: {totalDef} (장착 아이템: {(weapon?.Name ?? "없음")})";
        }
    }

    // 아이템 정보 클래스
    class Item
    {
        public string Name { get; set; }
        public int AtkPlus { get; set; }
        public int DefPlus { get; set; }
        public bool IsEquipped { get; set; }

        public Item(string name, int atkPlus, int defPlus)
        {
            Name = name;
            AtkPlus = atkPlus;
            DefPlus = defPlus;
            IsEquipped = false;
        }

        public void DisplayInfo()
        {
            string equippedStatus = IsEquipped ? "장착됨" : "미장착";
            Console.WriteLine($"아이템: {Name}, 공격력 증가: {AtkPlus}, 방어력 증가: {DefPlus}, 상태: {equippedStatus}");
        }
    }

    // 캐릭터 관리 클래스
    class CharacterManager
    {
        public List<Character> chList = new List<Character>();

        //캐릭터 생성
        public void AddCharacter()
        {
            int hp;
            int atk;
            int def;
            Console.Clear();
            Console.Write("이름: ");
            string name = Console.ReadLine();
            while (true)
            {
                Console.WriteLine("체력 + 공격력 + 방어력 = 20입니다.");
                Console.Write("체력 (x10): ");
                hp = int.Parse(Console.ReadLine()) * 10;
                Console.Write("공격력: ");
                atk = int.Parse(Console.ReadLine());
                def = 20 - atk - (hp / 10);
                Console.WriteLine($"체력 : {hp} 공격력 : {atk} 방어력 : {def} 입니다");
                Console.WriteLine("생성 : 1 , 재입력 : 2 ");
                int input = int.Parse(Console.ReadLine());
                if (input == 1)
                {
                    break;
                }
            }
            Character newCh = new Character(name, hp, atk, def);
            chList.Add(newCh);
            Console.WriteLine($"{newCh.Name} 캐릭터가 생성되었습니다.");
        }

        //캐릭터 제거
        public void RemoveCharacter()
        {
            Console.Clear();
            Console.Write("삭제할 캐릭터 이름: ");
            string name = Console.ReadLine();
            Character delCh = chList.Find(c => c.Name == name);
            if (delCh != null)
            {
                chList.Remove(delCh);
                Console.WriteLine($"{name} 캐릭터가 삭제되었습니다.");
            }
            else
            {
                Console.WriteLine("캐릭터를 찾을 수 없습니다.");
            }
        }

        //캐릭터 검색
        public void SearchCharacter()
        {
            Console.Clear();
            Console.Write("검색할 캐릭터 이름: ");
            string name = Console.ReadLine();
            Character searchCh = chList.Find(c => c.Name == name);
            if (searchCh != null)
            {
                Console.WriteLine(searchCh);
            }
            else
            {
                Console.WriteLine("캐릭터를 찾을 수 없습니다.");
            }
        }
        // 모든 캐릭터캐릭터 출력
        public void PrintAllCharacters()
        {
            Console.Clear();
            if (chList.Count != 0)
            {
                Console.WriteLine("캐릭터 목록:");
                foreach (var character in chList)
                {
                    Console.WriteLine(character);
                }
            }
            else
            {
                Console.WriteLine("캐릭터가 없습니다.");
            }
        }
    }

    // 아이템 관리 클래스
    class ItemManager
    {
        // 딕셔너리로 아이템을 관리
        public Dictionary<string, Item> itemDictionary = new Dictionary<string, Item>();

        public ItemManager()
        {
            AddItem("무기 1", 5, 1);
            AddItem("무기 2", 10, 2);
            AddItem("무기 3", 15, 5);
        }

        // 딕셔너리에 아이템 추가
        public void AddItem(string name, int atkplus, int defplus)
        {
            Item newItem = new Item(name, atkplus, defplus);
            itemDictionary[name] = newItem;  // 아이템을 이름(key)으로 추가
            Console.WriteLine($"{name} 아이템이 추가되었습니다.");
        }

        // 캐릭터에게 아이템 장착
        public void EquipItemToCharacter(Character character)
        {
            Console.Clear();
            if (itemDictionary.Count == 0)
            {
                Console.WriteLine("장착할 수 있는 아이템이 없습니다.");
                return;
            }

            // 딕셔너리에서 아이템 목록을 번호와 함께 출력
            Console.WriteLine("장착 가능한 아이템 목록:");
            List<Item> itemList = new List<Item>(itemDictionary.Values); // 아이템을 리스트로 변환
            for (int i = 0; i < itemList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {itemList[i].Name} - 공격력: {itemList[i].AtkPlus}, 방어력: {itemList[i].DefPlus}, 상태: {(itemList[i].IsEquipped ? "장착됨" : "미장착")}");
            }

            // 사용자에게 번호 입력받기
            Console.Write("장착할 아이템 번호를 입력하세요: ");
            int itemIndex = int.Parse(Console.ReadLine()) - 1; // 1부터 시작하는 번호를 리스트 인덱스로 변환

            // 번호가 유효한지 확인
            if (itemIndex >= 0 && itemIndex < itemList.Count)
            {
                Item selectedItem = itemList[itemIndex];
                if (selectedItem.IsEquipped)
                {
                    Console.WriteLine($"{selectedItem.Name}은(는) 이미 장착 중입니다.");
                }
                else
                {
                    character.EquipItem(selectedItem);
                }
            }
            else
            {
                Console.WriteLine("잘못된 번호입니다.");
            }
        }

        public void UnEquipItemFromCharacter(Character character)
        {
            Console.Clear();
            character.UnEquipItem();
        }
    }

    // 게임의 흐름을 관리하는 클래스
    class Game
    {
        CharacterManager characterManager = new CharacterManager();
        ItemManager itemManager = new ItemManager();

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("메뉴 선택");
                Console.WriteLine("1. 캐릭터 생성");
                Console.WriteLine("2. 캐릭터 삭제");
                Console.WriteLine("3. 캐릭터 검색");
                Console.WriteLine("4. 모든 캐릭터 보기");
                Console.WriteLine("5. 캐릭터에게 아이템 장착");
                Console.WriteLine("6. 캐릭터에게서 아이템 탈착");
                Console.WriteLine("7. 종료");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        characterManager.AddCharacter();
                        break;
                    case "2":
                        characterManager.RemoveCharacter();
                        break;
                    case "3":
                        characterManager.SearchCharacter();
                        break;
                    case "4":
                        characterManager.PrintAllCharacters();
                        break;
                    case "5":
                        Console.Write("아이템을 장착할 캐릭터 이름: ");
                        string equipName = Console.ReadLine();
                        Character equipCh = characterManager.chList.Find(c => c.Name == equipName);
                        if (equipCh != null)
                        {
                            itemManager.EquipItemToCharacter(equipCh);
                        }
                        else
                        {
                            Console.WriteLine("캐릭터를 찾을 수 없습니다.");
                        }
                        break;
                    case "6":
                        Console.Write("아이템을 탈착할 캐릭터 이름: ");
                        string unequipName = Console.ReadLine();
                        Character unequipCh = characterManager.chList.Find(c => c.Name == unequipName);
                        if (unequipCh != null)
                        {
                            itemManager.UnEquipItemFromCharacter(unequipCh);
                        }
                        else
                        {
                            Console.WriteLine("캐릭터를 찾을 수 없습니다.");
                        }
                        break;
                    case "7":
                        Console.WriteLine("게임을 종료합니다.");
                        return;
                    default:
                        Console.WriteLine("잘못된 선택입니다. 다시 선택하세요.");
                        break;
                }

                Console.WriteLine("아무 키나 누르면 메뉴로 돌아갑니다.");
                Console.ReadKey();
            }
        }
    }

    // 메인 클래스
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }
}
