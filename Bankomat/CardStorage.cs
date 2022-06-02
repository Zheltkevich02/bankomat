using System;
using System.Linq;

namespace Bankomat
{
    //Класс хранилище карт статический т.к. хранилище одно в банкомате и все его члены общие.
    static class CardStorage
    {
        //поле типа массив Card, для хранения объектов конфискованных карты
        private static Card[] _cards; 

        //метод доступа к приватному полю класса
        public static Card[] GetCard() { return _cards; } 

        //метод добавления конфискованной карты в хранилище
        public static Card[] AddCard(Card card) 
        {
            _cards = new Card[] { }; //инициализируем массив объектов
            _cards.Append(card);     //добавляем конфискованную карту в массив(хранилище)
            
            return _cards; //возвращаем получившийся массив
        }
    }
}
