using System;

namespace Bankomat
{
    class RequestCenterBank
    {
        private double _accountNumber; //номер счета, будет как номер карты клиента
        private int _balance;       //сумма денег на счете 

        //конструктор класса для создания счета
        public RequestCenterBank(double numberCard)
        {
            _accountNumber = numberCard;
            _balance = SetBalance();
        }

        //метод доступа к приватному полю класса
        public double GetAccauntNumber() { return _accountNumber; }
        public double GetBalance() { return _balance; }

        //метод установки баланса счета
        public int SetBalance() { return new Random().Next(500, 50000); }
    }
}
