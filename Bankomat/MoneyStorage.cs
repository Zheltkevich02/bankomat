using System;

namespace Bankomat
{
    //Класс хранилище денег статический т.к. хранилище одно в банкомате и все его члены общие.
    static class MoneyStorage
    {
        private static int _count100;  //поле количества купюр достоинством 100 руб
        private static int _count500;  //поле количества купюр достоинством 500 руб
        private static int _count1000; //поле количества купюр достоинством 1000 руб

        //методы доступа к приватным полям  
        public static int GetCount100()  { return _count100; }
        public static int GetCount500()  { return _count500; }
        public static int GetCount1000() { return _count1000; }


        //метод установки случайного количества купюр в хранилище разного достоинства
        public static void SetCount() 
        {
            var random = new Random(); //создаем объект типа Random

            _count100  = random.Next(0, 20); //присваиваем случайное количество (от 0 до 20 шт.) купюр достоинством 100 руб
            _count500  = random.Next(0, 20); //присваиваем случайное количество (от 0 до 20 шт.) купюр достоинством 500 руб
            _count1000 = random.Next(0, 20); //присваиваем случайное количество (от 0 до 20 шт.) купюр достоинством 1000 руб
        }

        //метод возвращающий общую сумму денег имеющихся в банкомате
        public static double SumCount() 
        {
            return _count100 * 100 + _count500 * 500 + _count1000 * 1000;
        }
    }
}
