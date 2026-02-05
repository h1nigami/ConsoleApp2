public delegate int MathOperation(int x, int y);

public class Calculator
{
    public static int Add(int x, int y) => x+y;
    public static int Substract(int x, int y) => x-y;
}

//void Действие без возвращаемого значения
//Action<T>

//Делегат для любого метода который возвращает значение
//Func<int , int , int> add = (a, b) => a+b;

//int sum = add(5,5);

//Идеально подходит для фильтрации возвращает true/false
//Predicate<T>

// Паттерн наблюдатель 
public class Stock
{
    //1. Определяем делегат для обработчика событий
    public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice);

    //2. Обьявляем событие этого типа
    public event PriceChangedHandler PriceChanged;

    private decimal _price;
    public decimal Price
    {
        get => _price;
        set
        {
            if(_price == value) return;
            decimal oldPrice = _price;
            //3. Вызываем (генерируем) событие. Это можно сделать только внутри класса Stock
            OnPriceChanged(oldPrice, _price);
        }
    }
    protected virtual void OnPriceChanged(decimal oldPrice, decimal newPrice)
    {
        //Invoke - проверка на наличие прдписчиков и их уведомление
        PriceChanged?.Invoke(oldPrice, newPrice);
    }
}

public class Investor
{
    private string _name;
    public Investor(string name) {_name = name;}
    
    public void OnStockPriceChanged(decimal oldPrice, decimal newPrice)
    {
        Console.WriteLine($"Цена изменилась с {oldPrice} на {newPrice}");
    }
}