using System;
using System.Windows;

namespace _dee_coder
{
    public partial class MainWindow : Window
    {
        //Initialize here
        public byte i, j, k = 0;
        public byte addError, addError2;
        public int tempFillBArr = 0;
        public byte[,] H = new byte[3, 7]
        {
            {0, 0, 0, 1, 1, 1, 1},
            {0, 1, 1, 0, 0, 1, 1},
            {1, 0, 1, 0, 1, 0, 1}
        };

        public byte[,] Vstar = new byte[7, 1]
        {
            {0},
            {0},
            {1},
            {0},
            {1},
            {1},
            {1}
        };

        public string CheckBytes;
        
        public byte[,] Bc = new byte[3, 1];
        public bool Parsedorno;
        public byte[,] Bd = new byte[3, 1];
        //end initialize
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InputButton_OnClick(object sender, RoutedEventArgs e)
        {
            string String = InputBox.Text; 
            string[] outputString = new string[7] { "2", "2", "2", "2", "2", "2", "2" };
            if (String.Length != 4) {MessageBox.Show("Uncorrect imput");}
            Vstar[2, 0] = Byte.Parse(String.Substring(0, String.Length - 3));
            Vstar[4, 0] = Byte.Parse(String.Substring(1, String.Length - 3));
            Vstar[5, 0] = Byte.Parse(String.Substring(2, String.Length - 3));
            Vstar[6, 0] = Byte.Parse(String.Substring(3));

            CalculateB(Vstar,H, Bc);

            i = 0;
            j = 0;

            Vstar[1, 0] = Bc[1, 0];
            Vstar[3, 0] = Bc[0, 0];
            Vstar[0, 0] = Bc[2, 0];

            CheckBytes = ShortVstarToString();
            Out1.Text = VstarToString();        
        }

        private void InputButton2_OnClick(object sender, RoutedEventArgs e)
        {
            Parsedorno = Byte.TryParse(ErrBox.Text, out addError);
            if (Parsedorno) { addError = Byte.Parse(ErrBox.Text); }

            Parsedorno = Byte.TryParse(ErrBox2.Text, out addError2);
            if (Parsedorno) { addError2 = Byte.Parse(ErrBox2.Text); }
            
           
            if (addError > 0 & addError < 7){ ChangeV(addError); }
            if(addError2 > 0 & addError2 < 7) { ChangeV(addError2); }

            Answer1.Text = VstarToString();
            CalculateB(Vstar, H, Bd);
            byte bug = (byte)((4 * Bd[0, 0]) + (2 * Bd[1, 0]) + (1 * Bd[2, 0]));
            
            switch (bug)
            {
                case 1:
                    
                    break;
                case 2:
                    ChangeV(bug);
                    break;
                case 3:
                    
                    break;
                case 4:
                    ChangeV(bug);
                    break;
                case 5:
                    ChangeV(bug);
                    break;
                case 6:
                    ChangeV(bug);
                    break;
                case 0:
                    
                    break;

            }

            
            Answer.Text = bug.ToString();
            Answer2.Text = VstarToString();
            Answer3.Text = ShortVstarToString();
            if(CheckBytes != ShortVstarToString()) { MessageBox.Show("Декодовані дані - неправильні. Оскільки помилка що виникла підчас передачі - не одинична."); }

        }

        public string VstarToString()
        {
            string myString;
            string[] outputString = new string[7] { "2", "2", "2", "2", "2", "2", "2" };
            k = 0;
            foreach (var i in Vstar)
            {
                outputString[k] = i.ToString();
                k++;
            }
            
            myString = String.Join("", outputString);
            return myString;
        }

        public string ShortVstarToString()
        {
            string myString;
            string[] outputString = new string[4];
            k = 0;
            j = 0;
            foreach (var i in Vstar)
            {
                if (j == 0 || j == 1 || j == 3)
                {
                    j++;
                }
                else
                {
                    outputString[k] = i.ToString();
                    k++;
                    j++;
                }
            }   
            myString = String.Join(" ", outputString);
            j = 0;
            return myString;
        }
        public void ChangeV(byte index)
        {
            index--;



            if (Vstar[index, 0] == 1)
                {
                    Vstar[index, 0] = 0;
                }
            else Vstar[index, 0] = 1;
            
           
        }
        public void CalculateB(byte[,] Vstar, byte[,] H, byte[,] B) {
            while (i <= 2)
            {
                while (j <= 6)
                {
                    tempFillBArr = tempFillBArr + (Vstar[j, 0] * H[i, j]);
                    j++;
                }

                if ((tempFillBArr % 2) != 0) tempFillBArr = 1;
                else tempFillBArr = tempFillBArr % 2;

                B[i, 0] = (byte)tempFillBArr;

                i++;
                j = 0;
                tempFillBArr = 0;
            }

            
        }

        private void ResetButton_OnClick(object sender, RoutedEventArgs e)
        {
             k = 0;
            i = 0;
            j = 0;
            tempFillBArr = 0;
            H = new byte[3, 7]
            {
                {0, 0, 0, 1, 1, 1, 1},
                {0, 1, 1, 0, 0, 1, 1},
                {1, 0, 1, 0, 1, 0, 1}
            };
            Vstar = new byte[7, 1]
            {
                {0},
                {0},
                {1},
                {0},
                {1},
                {1},
                {1}
            };
            CheckBytes = "";

        }
    }
}