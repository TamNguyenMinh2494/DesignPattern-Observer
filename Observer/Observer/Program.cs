using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer
{
    abstract class Stock

    {
        private string _symbol;
        private double _price;
        private List<IInvestor> _investors = new List<IInvestor>();

        // Constructor

        public Stock(string symbol, double price)
        {
            this._symbol = symbol;
            this._price = price;
        }

        public void Attach(IInvestor investor)
        {
            _investors.Add(investor);
        }

        public void Detach(IInvestor investor)
        {
            _investors.Remove(investor);
        }

        public void Notify()
        {
            foreach (IInvestor investor in _investors)
            {
                investor.Update(this);
            }

            Console.WriteLine("");
        }

        // Gets or sets the price

        public double Price
        {
            get { return _price; }
            set

            {
                if (_price != value)
                {
                    _price = value;
                    Notify();
                }
            }
        }

        // Gets the symbol

        public string Symbol
        {
            get { return _symbol; }
        }
    }

    class Apple : Stock

    {
        // Constructor

        public Apple(string symbol, double price)
          : base(symbol, price)
        {
        }
    }

    interface IInvestor

    {
        void Update(Stock stock);
    }

    class Investor : IInvestor

    {
        private string _name;
        private Stock _stock;

        public Investor(string name)
        {
            this._name = name;
        }

        public void Update(Stock stock)
        {
            Console.WriteLine("Notified {0} of {1}'s " +
              "change to {2:C}", _name, stock.Symbol, stock.Price);
        }

        public Stock Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {


            Apple apple = new Apple("Apple", 120.00);
            apple.Attach(new Investor("Google"));
            apple.Attach(new Investor("Facebook"));
            apple.Attach(new Investor("Tam"));


            apple.Price = 999;
            apple.Price = 256;
            
            Console.ReadKey();
        }
    }
}
