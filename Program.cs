using System;
using System.Collections.Generic;

internal class Program
{
    static void Main(string[] args)
    {
        Aquarium aquarium = new Aquarium();
        aquarium.Start();
    }
}

class Aquarium
{
    private FishList _fishList = new FishList();
    private List<Fish> _fishes = new List<Fish>();
    private int _maxNumberFish = 15;
    private int _numberPlaces = 15;
    public void Start()
    {
        const string CommandExit = "exit";
        const string CommandAddFish = "add";
        const string CommandRemoveFish = "remove";
        bool isExit = false;

        while (isExit == false)
        {
            if (_fishes.Count == 0)
            {
                Console.WriteLine("Аквариум пуст, запустите рыбок :з");
            }
            else
            {
                Console.Clear();
                ShowInfo();
            }

            RunCycle();

            Console.SetCursorPosition(0, 25);
            Console.WriteLine("Чтобы добавить рыб, введите: " + CommandAddFish);
            Console.WriteLine("Чтобы убрать определённых рыб, введите: " + CommandRemoveFish);
            Console.WriteLine("Чтобы выйти из программы, введите: " + CommandExit);

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case CommandAddFish:
                    AddFish();
                    break;

                case CommandRemoveFish:
                    RemoveFish();
                    break;

                case CommandExit:
                    isExit = true;
                    break;
            }
        }
    }

    private void AddFish()
    {
        if (_maxNumberFish > 0)
        {            
            _fishList.ShowFishSpecies();
            int fishIndex = GetNumber(_fishList.GetCount());
            _fishes.Add(_fishList.TakeFish(fishIndex));
            _numberPlaces--;
        }
        else
        {
            Console.WriteLine("В аквариум больше нельзя запустить рыбок, это будет опасно для их жизни");
            Console.ReadKey();
        }
    }

    private void RemoveFish()
    {
        Console.Clear();

        if (_fishes.Count > 0)
        {
            ShowInfo();
            Console.SetCursorPosition(0, 25);
            Console.WriteLine("Чтобы достать рыбку, введите её номер");
            _fishes.RemoveAt(GetNumber(_fishes.Count));
            _numberPlaces++;
        }

        Console.Clear();
    }

    private void ShowInfo()
    {
        int numberFish = 0;
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Мест в аквариуме: " + _numberPlaces);
        Console.WriteLine("В аквариуме плавают: ");

        foreach (Fish fish in _fishes)
        {
            Console.WriteLine(numberFish + ": " + "Рыбка - " + fish.Name + " будет жить еще примерно: " + fish.Age + ", она.. " + fish.FindStatus());
            numberFish++;
        }
    }

    private void RunCycle()
    {
        foreach (var fish in _fishes)
        {
            fish.SubtractYear();
        }
    }

    private int GetNumber(int listCount)
    {
        bool isParse = false;
        int numberForReturn = 0;

        while (isParse == false)
        {
            string userNumber = Console.ReadLine();
            int.TryParse(userNumber, out numberForReturn);

            if (numberForReturn < 0 || numberForReturn >= listCount)
            {
                Console.WriteLine("Вводи одну из предложенных цифр, а не из космоса =_=");
            }
            else
            {
                isParse = true;
            }
        }

        return numberForReturn;
    }
}

class FishList
{
    private List<Fish> _fishSpecies = new List<Fish>();

    public FishList()
    {
        AddSpecies();
    }

    public Fish TakeFish(int fishIndex)
    {
        return _fishSpecies[fishIndex];
    }

    public void ShowFishSpecies()
    {
        int numberFish = 0;

        foreach (Fish fish in _fishSpecies)
        {
            Console.WriteLine(numberFish + " - " + fish.Name + " её продолжительность жизни в годах: " + fish.Age);
            numberFish++;
        }
    }

    public int GetCount()
    {
        return _fishSpecies.Count;
    }

    private void AddSpecies()
    {
        _fishSpecies.Add(new Fish("Плавает", "PearlGourami", 5));
        _fishSpecies.Add(new Fish("Плавает", "SilverAngelfish", 10));
        _fishSpecies.Add(new Fish("Плавает", "NeonTetra", 5));
        _fishSpecies.Add(new Fish("Плавает", "Zebrafish", 6));
        _fishSpecies.Add(new Fish("Плавает", "CommonGoldfish", 20));
        _fishSpecies.Add(new Fish("Плавает", "RainbowPlaty", 4));
    }
}

class Fish
{
    public Fish(string status, string name, int age)
    {
        Status = status;
        Name = name;
        Age = age;
    }

    public string Status { get; protected set; }
    public string Name { get; protected set; }
    public int Age { get; protected set; }

    public string FindStatus()
    {
        if (Age <= 0)
        {
            Status = "Плавает кверху брюхом";
        }

        return Status;
    }

    public void SubtractYear()
    {
        if (Age > 0)
        {
            Age--;
        }
        else
        {
            Age = 0;
        }
    }
}
