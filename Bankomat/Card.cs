using System;

namespace Bankomat
{
    class Card
    {
        private double _cardNumber; //поле номер карты
        private int _cardPin;    //поле ПИН-код карты
        private int _attempts;   //поле количества попыток ввода ПИН-кода

        /// <summary>
        /// Конструктор для создания объекта кредитная карта и установка ПИН-кода 
        /// </summary>
        /// <param name="cardNumber">Номер карты передается как параметр из введенного в TextBox</param>
        public Card(double cardNumber)
        {
            _cardNumber = cardNumber;
            _cardPin = SetCardPin();
        }

        //методы доступа к приватным полям класса
        public double GetCardNumber() { return _cardNumber; }
        public int GetCardPin() { return _cardPin; }
        public int GetAttempts() { return _attempts; }

        public int SetAttempts() { return _attempts++; } //метод счетчика попыток ввода пин-кода

        //метод установки пин-кода карты первые 4 цифры номера карты
        public int SetCardPin() 
        {
            return _cardPin = int.Parse(_cardNumber.ToString().Substring(0, 4));  
        }
    }
}
