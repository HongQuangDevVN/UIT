using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients
{
    public class Card
    {
        int Number;
        int Type;
        public Card()
        {
            Number = 0;
            Type = 0;

        }
        public Card(int x, int y)
        {
            Number = x;
            Type = y;
        }
        public int GetNumber()
        {
            return Number;
        }
        public int GetType()
        {
            return Type;
        }
        public string GetLocation()
        {
            string st = "";
            if (Number == 3)
            {
                if (Type == 1) st = @"E:\\image\3B.JPG";
                if (Type == 2) st = @"E:\\image\3H.JPG";
                if (Type == 3) st = @"E:\\image\3R.JPG";
                if (Type == 4) st = @"E:\\image\3C.JPG";
            }
            if (Number == 4)
            {
                if (Type == 1) st = @"E:\\image\4B.JPG";
                if (Type == 2) st = @"E:\\image\4H.JPG";
                if (Type == 3) st = @"E:\\image\4R.JPG";
                if (Type == 4) st = @"E:\\image\4C.JPG";
            }
            if (Number == 5)
            {
                if (Type == 1) st = @"E:\\image\5B.JPG";
                if (Type == 2) st = @"E:\\image\5H.JPG";
                if (Type == 3) st = @"E:\\image\5R.JPG";
                if (Type == 4) st = @"E:\\image\5C.JPG";
            }
            if (Number == 6)
            {
                if (Type == 1) st = @"E:\\image\6B.JPG";
                if (Type == 2) st = @"E:\\image\6H.JPG";
                if (Type == 3) st = @"E:\\image\6R.JPG";
                if (Type == 4) st = @"E:\\image\6C.JPG";
            }
            if (Number == 7)
            {
                if (Type == 1) st = @"E:\\image\7B.JPG";
                if (Type == 2) st = @"E:\\image\7H.JPG";
                if (Type == 3) st = @"E:\\image\7R.JPG";
                if (Type == 4) st = @"E:\\image\7C.JPG";
            }
            if (Number == 8)
            {
                if (Type == 1) st = @"E:\\image\8B.JPG";
                if (Type == 2) st = @"E:\\image\8H.JPG";
                if (Type == 3) st = @"E:\\image\8R.JPG";
                if (Type == 4) st = @"E:\\image\8C.JPG";
            }
            if (Number == 9)
            {
                if (Type == 1) st = @"E:\\image\9B.JPG";
                if (Type == 2) st = @"E:\\image\9H.JPG";
                if (Type == 3) st = @"E:\\image\9R.JPG";
                if (Type == 4) st = @"E:\\image\9C.JPG";
            }
            if (Number == 10)
            {
                if (Type == 1) st = @"E:\\image\10B.JPG";
                if (Type == 2) st = @"E:\\image\10H.JPG";
                if (Type == 3) st = @"E:\\image\10R.JPG";
                if (Type == 4) st = @"E:\\image\10C.JPG";
            }
            if (Number == 11)
            {
                if (Type == 1) st = @"E:\\image\JB.JPG";
                if (Type == 2) st = @"E:\\image\JH.JPG";
                if (Type == 3) st = @"E:\\image\JR.JPG";
                if (Type == 4) st = @"E:\\image\JC.JPG";
            }
            if (Number == 12)
            {
                if (Type == 1) st = @"E:\\image\QB.JPG";
                if (Type == 2) st = @"E:\\image\QH.JPG";
                if (Type == 3) st = @"E:\\image\QR.JPG";
                if (Type == 4) st = @"E:\\image\QC.JPG";
            }
            if (Number == 13)
            {
                if (Type == 1) st = @"E:\\image\KB.JPG";
                if (Type == 2) st = @"E:\\image\KH.JPG";
                if (Type == 3) st = @"E:\\image\KR.JPG";
                if (Type == 4) st = @"E:\\image\KC.JPG";
            }
            if (Number == 14)
            {
                if (Type == 1) st = @"E:\\image\1B.JPG";
                if (Type == 2) st = @"E:\\image\1H.JPG";
                if (Type == 3) st = @"E:\\image\1R.JPG";
                if (Type == 4) st = @"E:\\image\1C.JPG";
            }
            if (Number == 15)
            {
                if (Type == 1) st = @"E:\\image\2B.JPG";
                if (Type == 2) st = @"E:\\image\2H.JPG";
                if (Type == 3) st = @"E:\\image\2R.JPG";
                if (Type == 4) st = @"E:\\image\2C.JPG";
            }
            return st;
        }
    }
    class BoBai
    {
        Card[] QuanBai = new Card[14];
        public void KhoiTao()
        {
            for (int i = 1; i <= 13; i++)
                QuanBai[i] = new Card(0, 0);
        }
        public void Set(int i, int num1, int num2)
        {
            QuanBai[i] = new Card(num1, num2);
        }
        public int GetNum(int i)
        {
            int x=QuanBai[i].GetNumber();
            return x;
        }
        public int GetT(int i)
        {
            int x = QuanBai[i].GetType();
            return x;
        }
        public string GetL(int i)
        {
            string x = QuanBai[i].GetLocation();
            return x;
        }

        

        public void XepBai()
        {
            for (int i = 1; i <= 12; i++)
            {
                for (int j = i + 1; j <= 13; j++)
                {
                    int x1 = QuanBai[i].GetNumber();
                    int x2 = QuanBai[i].GetType();
                    int y1 = QuanBai[j].GetNumber();
                    int y2 = QuanBai[j].GetType();
                    int value1 = 4 * x1 + x2;
                    int value2 = 4 * y1 + y2;
                    if (value1 > value2)
                    {
                        Card tmp = new Card();
                        tmp = QuanBai[i];
                        QuanBai[i] = QuanBai[j];
                        QuanBai[j] = tmp;
                    }
                    
                }
            }
        }
      
    }
}
