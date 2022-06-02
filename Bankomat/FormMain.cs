using System;
using System.Windows.Forms;

namespace Bankomat
{
    public partial class F_Main : Form
    {
        public F_Main()
        {
            InitializeComponent();
        }

        private string keyBoard;    //вводимое значение с клавиатура банкомата
        private double wordEnter;   //вводимое значение с клавиатура в int
        private int options = 0;    //варианты выбора для кнопки Ok на клавиатуре банкомата

        Card card;
        RequestCenterBank reqest;

        //метод для хранения вводимых чисел с клавиатуры банкомата
        void Keyboard(string textButton)
        {
            RichTextBoxScreen.AppendText(textButton);
            keyBoard += textButton;
        }

        //метод для выдачи денег
        void Money()
        {
            MoneyStorage.SetCount();  //Наполняем банкомат деньгами

            if (reqest.GetBalance() < wordEnter)
            {
                RichTextBoxScreen.Text = "Недостаточно средств на счете";
                options = 0;

                ButtonTakeCard.PerformClick();
            }
            else if (MoneyStorage.SumCount() < wordEnter)
            {
                RichTextBoxScreen.Text = "Недостаточно средств в банкомате";

                options = 0;
                ButtonTakeCard.PerformClick();
            }
            else
            {
                 RichTextBoxScreen.Text = "Заберите карту";
            }
        }

        //поле для временного хранения номера счета, для безналичного перевода
        string numberRemittance = "";  

        //метод для возвращения банкомата в состояние ожидания
        private void ClearAll()
        {
            RichTextBoxScreen.Text = "Вставьте карту";
            LabelTicket.Text = "Печать справок";

            TextBoxCard.Enabled = true;
            TextBoxCard.Text = "";
            ButtonInsertCard.Enabled = true;
            ButtonTakeCard.Enabled = false;
            keyBoard = "";
            wordEnter = 0;
        }

        //метод показывающий главное меню на экране банкомата
        private void MainMemu()
        {
            RichTextBoxScreen.Clear();

            RichTextBoxScreen.AppendText("Выберите операцию:\n");
            RichTextBoxScreen.AppendText("- Снять наличные\n\n");
            RichTextBoxScreen.AppendText("- Проверить баланс\n\n");
            RichTextBoxScreen.AppendText("- Безналичный платеж");
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            if (keyBoard != "")  //если есть введенное значение с клавиатуры банкомата,
            {
                wordEnter = double.Parse(keyBoard);  //то парсим его в тип int и присваиваем переменной wordEnter
            }

            if (options == 1) //если выбрана операция снятия денег
            {
                DialogResult result = MessageBox.Show("Напечатать справку по операции?",
                                                      "Всплывающее сообщение",
                                                      MessageBoxButtons.YesNo);
               
                if (result == DialogResult.Yes) //если выбрана функция напечатать справку
                {
                    //выдаем деньги
                    Money();

                    //печатаем справку по операции
                    LabelTicket.Text += $"\n{DateTime.Now}" +
                                        $"\nСумма выдачи: {wordEnter}" +
                                        $"\nОстаток: {reqest.GetBalance() - wordEnter}";

                }
                else
                {
                    //выдаем деньги
                    Money();
                }
            }
            else if (options == 2)  //если выбрана операция безналичного перевода
            {
                if (wordEnter != 0)
                {
                    RichTextBoxScreen.AppendText("\nВведите сумму перевода:\n");

                    numberRemittance = keyBoard;
                    keyBoard = "";
                    options = 3;
                }
            }
            else if (options == 3) //продолжение безналичного перевода
            {
                RichTextBoxScreen.Text = $"Перевод на счет №{numberRemittance} " +
                                         $"на сумму {wordEnter} рублей выполнен успешно";


                numberRemittance = "";
                keyBoard = "";
                options = 0;
            }
            else
            {
                if (card.GetCardPin() == wordEnter)
                {
                    MainMemu();
                }
                else
                {
                    card.SetAttempts();

                    if (card.GetAttempts() < 3)
                    {
                        RichTextBoxScreen.AppendText($"\n\nПИН-код не верный!" +
                                                      "\nВведите ПИН-код:");
                        keyBoard = "";
                        wordEnter = 0;
                    }
                    else
                    {
                        ClearAll();

                        RichTextBoxScreen.AppendText($"\n\nВы 3 раза ввели неправильный ПИН-код," +
                                                     $"ваша карта заблокирована и помещена в хранилище," +
                                                     $"для возврата обратитесь в банк.");

                        CardStorage.AddCard(card);
                    }
                }
            }
        }

        private void ButtonInsertCard_Click(object sender, EventArgs e)
        {
            RichTextBoxScreen.Clear();

            if (TextBoxCard.Text != "")
            {
                card = new Card(double.Parse(TextBoxCard.Text));
                reqest = new RequestCenterBank(double.Parse(TextBoxCard.Text));

                TextBoxCard.Enabled = false;
                ButtonInsertCard.Enabled = false;
                ButtonTakeCard.Enabled = true;

                RichTextBoxScreen.AppendText("Введите ПИН-код: ");
            }
            else MessageBox.Show("Введите номер карты", "Ошибка!", MessageBoxButtons.OK);
        }

        private void ButtonTakeCard_Click(object sender, EventArgs e)
        {
            if (options == 1)
            {
                RichTextBoxScreen.Text = "Заберите деньги";
                TextBoxCash.Text = wordEnter.ToString();
                TextBoxCard.Text = "";

                TextBoxCash.Enabled = true;
                ButtonTakeCash.Enabled = true;
            }
            else ClearAll();
        }

        private void ButtonCash_Click(object sender, EventArgs e)
        {
            keyBoard = "";
            options = 1;
            RichTextBoxScreen.Text = "Введите сумму для снятия: ";
        }

        private void ButtonTakeCash_Click(object sender, EventArgs e)
        {
            TextBoxCash.Text = "";
            TextBoxCash.Enabled = false;
            ButtonTakeCash.Enabled = false;
            options = 0;
            LabelTicket.Text = "Печать справок";
            ButtonTakeCard.Enabled = true;

            ClearAll();
        }

        private void ButtonBalance_Click(object sender, EventArgs e)
        {

            if (options == 0)
            {
                RichTextBoxScreen.Text = $"Баланс вашего счета: \n{reqest.GetBalance()}" +
                                     $"\n\n- напечатать справку" +
                                     $"\n\n\n\n- вернуться в главное меню";
                options = 4;
            }
            else
            {
                LabelTicket.Text += $"\n{DateTime.Now}" +
                                    $"\nБаланс: {reqest.GetBalance()}";
            }
            
        }

        private void ButtonBeznal_Click(object sender, EventArgs e)
        {
            RichTextBoxScreen.Text = "Введите номер счета для перевода средств: \n";

            options = 2;
            keyBoard = "";
        }

        private void ButtonMainMenu_Click(object sender, EventArgs e)
        {
            MainMemu();
            LabelTicket.Text = "Печать справок";
        }

        #region События нажатие кнопок-цифр
        private void Button1_Click(object sender, EventArgs e) { Keyboard(Button1.Text); }

        private void Button2_Click(object sender, EventArgs e) { Keyboard(Button2.Text); }

        private void Button3_Click(object sender, EventArgs e) { Keyboard(Button3.Text); }

        private void Button4_Click(object sender, EventArgs e) { Keyboard(Button4.Text); }

        private void Button5_Click(object sender, EventArgs e) { Keyboard(Button5.Text); }

        private void Button6_Click(object sender, EventArgs e) { Keyboard(Button6.Text); }

        private void Button7_Click(object sender, EventArgs e) { Keyboard(Button7.Text); }

        private void Button8_Click(object sender, EventArgs e) { Keyboard(Button8.Text); }

        private void Button9_Click(object sender, EventArgs e) { Keyboard(Button9.Text); }

        private void Button0_Click(object sender, EventArgs e) { Keyboard(Button0.Text); }
        #endregion

    }
}
