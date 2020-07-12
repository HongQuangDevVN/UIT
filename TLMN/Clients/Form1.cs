using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Clients
{
    public partial class Form1 : Form
    {
        public Form1(TCPModel tcp, int P)
        {
            InitializeComponent();
            this.tcp = tcp;
            Phong = P.ToString();
        }
        private TCPModel tcp;
        //private BoBai CardSet = new BoBai();
        private List<Card> CardLeft = new List<Card>();
        private List<Card> CardPresent = new List<Card>();
        private List<Card> CardOnDeck = new List<Card>();
        string Phong;
        string PositionInRoom;
        Thread check1;
        Thread Update;

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            ChoiLai.Visible = false;
            /*tcp = new TCPModel("127.0.0.1", int.Parse("13000"));
            tcp.ConnectToServer();*/
            Danh_Bai.Visible = false;
            Bo_Luot.Visible = false;
            panel2.Visible = false;
            Update = new Thread(UpdateDesk);
            Update.Start();
        }

        private int ktradoithong(List<Card> Cards)
        {
            int res= 0;
            if ((Cards.Count >= 6) && (Cards.Count % 2 == 0))
            {
                if (Cards[Cards.Count - 1].GetNumber() == 15) return 0;
                int i = 2;
                while (i <= Cards.Count)
                {
                    if (Cards[i - 1].GetNumber() != Cards[i - 2].GetNumber()) return 0;
                    else ++res;
                    if (i > 2)
                    {
                        if (Cards[i - 1].GetNumber() != Cards[i - 3].GetNumber()+1)
                            return 0;
                    }
                    i = i + 2;
                }
            }
            else return 0;
            return res;
        }

        private bool checkdoi(List<Card> Cards)
        {
            if (Cards.Count == 2)
            {
                if (Cards[0].GetNumber() == Cards[1].GetNumber()) return true;
                return false;
            }
            else return false;
        }

        private bool ktratuquy(List<Card> Cards)
        {
            if (Cards.Count != 4)
                return false;
            else
            {
                int x1 = Cards[0].GetNumber();
                int x2 = Cards[1].GetNumber();
                int x3 = Cards[2].GetNumber();
                int x4 = Cards[3].GetNumber();
                if (x1 == x2 && x2 == x3 && x3 == x4) return true;
                else return false;
            }
        }

        private bool checkSamCo(List<Card> Cards)
        {
            if (Cards.Count != 3) return false;
            else
            {
                int x1 = Cards[0].GetNumber();
                int x2 = Cards[1].GetNumber();
                int x3 = Cards[2].GetNumber();
                if (x1 == x2 && x2 == x3) return true;
                else return false;
            }
        }

        private int checkSanh(List<Card> Cards)
        {
            int res = 1;
            if (Cards.Count >= 3)
            {
                if (Cards[Cards.Count - 1].GetNumber() == 15) return 0;
                int i = 1;
                while (i <= Cards.Count)
                {
                    if (i >= 2)
                    {
                        if (Cards[i - 1].GetNumber() != Cards[i - 2].GetNumber() + 1)
                            return 0;
                        else res++;
                    }
                    i++;
                }
            }
            else return 0;
            return res;
        }

        private void CheckBeforeDo()
        {
            while (true)
            {
                lock (CardOnDeck)
                {
                    if (CardOnDeck.Count == 0)
                    {
                        lock (CardPresent)
                        {
                            CardPresent.Sort(delegate (Card x1, Card x2)
                            {
                                return ((x1.GetNumber() * 4 + x1.GetType()).CompareTo(x2.GetNumber() * 4 + x2.GetType()));
                            });
                            if (CardPresent.Count == 1) Danh_Bai.Enabled = true;
                            else if (checkdoi(CardPresent)) Danh_Bai.Enabled = true;
                            else if (checkSamCo(CardPresent)) Danh_Bai.Enabled = true;
                            else if (ktratuquy(CardPresent)) Danh_Bai.Enabled = true;
                            else if (ktradoithong(CardPresent) >= 3) Danh_Bai.Enabled = true;
                            else if (checkSanh(CardPresent) >= 3) Danh_Bai.Enabled = true;
                            else Danh_Bai.Enabled = false;
                        }
                    }
                    if (CardOnDeck.Count == 1)
                    {
                        int x = CardOnDeck[0].GetNumber() * 4 + CardOnDeck[0].GetType();
                        lock (CardPresent)
                        {
                            int y = 0;
                            if (CardPresent.Count == 1)
                            {
                                y = CardPresent[0].GetNumber() * 4 + CardPresent[0].GetType();
                                if (y > x) Danh_Bai.Enabled = true;
                                else Danh_Bai.Enabled = false;
                            }
                            else if (CardPresent.Count >= 4)
                            {
                                if (x > 60)
                                {
                                    if ((ktradoithong(CardPresent) >= 3) || (ktratuquy(CardPresent)))
                                        Danh_Bai.Enabled = true;
                                    else Danh_Bai.Enabled = false;
                                }
                                else
                                {
                                    if (ktradoithong(CardPresent) > 3)
                                        Danh_Bai.Enabled = true;
                                    else Danh_Bai.Enabled = false;
                                }
                            }
                            else Danh_Bai.Enabled = false;
                        }
                    }
                    if (CardOnDeck.Count == 2)
                    {
                        int x = CardOnDeck[1].GetNumber() * 4 + CardOnDeck[1].GetType();
                        lock (CardPresent)
                        {
                            CardPresent.Sort(delegate (Card x1, Card x2)
                            {
                                return ((x1.GetNumber() * 4 + x1.GetType()).CompareTo(x2.GetNumber() * 4 + x2.GetType()));
                            });
                            if (checkdoi(CardPresent))
                            {
                                int y = CardPresent[1].GetNumber() * 4 + CardPresent[1].GetType();
                                if (y > x) Danh_Bai.Enabled = true;
                                else Danh_Bai.Enabled = false;
                            }
                            else if (CardPresent.Count > 4)
                            {
                                if (ktradoithong(CardPresent) >= 4) Danh_Bai.Enabled = true;
                                else Danh_Bai.Enabled = false;
                            }
                            else Danh_Bai.Enabled = false;
                        }
                    }
                    if (CardOnDeck.Count == 3)
                    {
                        CardPresent.Sort(delegate (Card x1, Card x2)
                        {
                            return ((x1.GetNumber() * 4 + x1.GetType()).CompareTo(x2.GetNumber() * 4 + x2.GetType()));
                        });
                        int x = CardOnDeck[2].GetNumber() * 4 + CardOnDeck[2].GetType();
                        lock (CardPresent)
                        {
                            if (checkSamCo(CardOnDeck))
                            {
                                if (checkSamCo(CardPresent))
                                {
                                    int y = CardPresent[2].GetNumber() * 4 + CardPresent[2].GetType();
                                    if (y > x) Danh_Bai.Enabled = true;
                                    else Danh_Bai.Enabled = false;
                                }
                                else Danh_Bai.Enabled = false;
                            }
                            else if (checkSanh(CardOnDeck) == 3)
                            {
                                if (checkSanh(CardPresent) == 3)
                                {
                                    int y = CardPresent[2].GetNumber() * 4 + CardPresent[2].GetType();
                                    if (y > x) Danh_Bai.Enabled = true;
                                    else Danh_Bai.Enabled = false;
                                }
                                else Danh_Bai.Enabled = false;
                            }
                            else Danh_Bai.Enabled = false;
                        }
                    }
                    if (CardOnDeck.Count >= 4)
                    {
                        CardPresent.Sort(delegate (Card x1, Card x2)
                        {
                            return ((x1.GetNumber() * 4 + x1.GetType()).CompareTo(x2.GetNumber() * 4 + x2.GetType()));
                        });
                        lock (CardPresent)
                        {
                            int x = CardOnDeck[CardOnDeck.Count - 1].GetNumber() * 4 + CardOnDeck[CardOnDeck.Count - 1].GetType();
                            if (ktratuquy(CardOnDeck))
                            {
                                if (ktratuquy(CardPresent))
                                {
                                    int y = CardPresent[3].GetNumber() * 4 + CardPresent[3].GetType();
                                    if (y > x) Danh_Bai.Enabled = true;
                                    else Danh_Bai.Enabled = false;
                                }
                                else Danh_Bai.Enabled = false;
                            }
                            else if (ktradoithong(CardOnDeck) >= 3)
                            {
                                if (ktradoithong(CardPresent) == ktradoithong(CardOnDeck))
                                {
                                    int y = CardPresent[CardPresent.Count - 1].GetNumber() * 4 + CardPresent[CardPresent.Count - 1].GetType();
                                    if (y > x) Danh_Bai.Enabled = true;
                                    else Danh_Bai.Enabled = false;
                                }
                                else Danh_Bai.Enabled = false;
                            }
                            else if (checkSanh(CardOnDeck) >= 4)
                            {
                                if (checkSanh(CardPresent) == checkSanh(CardOnDeck))
                                {
                                    int y = CardPresent[CardPresent.Count - 1].GetNumber() * 4 + CardPresent[CardPresent.Count - 1].GetType();
                                    if (y > x) Danh_Bai.Enabled = true;
                                    else Danh_Bai.Enabled = false;
                                }
                                else Danh_Bai.Enabled = false;
                            }
                            else Danh_Bai.Enabled = false;
                        }
                    }
                }
            }
        }

        private void UpdateDesk()
        {
            while (true)
            {
                string st = tcp.ReadData();
                if (st[0] == 'C')
                {
                    CardLeft.Clear();
                    lock (CardOnDeck)
                    {
                        CardOnDeck.Clear();
                    }
                    lock (CardPresent)
                    {
                        CardPresent.Clear();
                    }
                    Desk_Start(st);
                    panel1.Visible = true;
                    panel2.Visible = true;
                }

                if (st[0] == 'D')
                {
                    label1.Visible = false;
                    Danh_Bai.Enabled = false;
                    Bo_Luot.Enabled = false;
                    panel2.Refresh();
                    lock (CardOnDeck)
                    {
                        CardOnDeck.Clear();
                    }
                    Deck_Update(st);
                    if (st[st.Length - 1] == 'W')
                    {
                        panel1.Visible = false;
                        /*panel2.Visible = false;*/
                        ChoiLai.Visible = true;
                        st.Remove(st.Length - 1, 1);
                        Update.Abort();
                        Update.Join();
                    }
                    else if(st[st.Length - 1] == 'L')
                    {
                        panel1.Visible = false;
                        /*panel2.Visible = false;*/
                        ChoiLai.Visible = true;
                        st.Remove(st.Length - 1, 1);
                        Update.Abort();
                        Update.Join();
                    }
                    else if (st[st.Length - 1] == 'P')
                    {
                        label1.Visible = true;
                        //Danh_Bai.Enabled = true;
                        Bo_Luot.Enabled = true;
                        st.Remove(st.Length - 1, 1);
                        lock (CardOnDeck)
                        {
                            if (CardOnDeck.Any())
                            {
                                check1 = new Thread(CheckBeforeDo);
                                check1.Start();
                            }
                        }
                    }                  
                    //Deck_Update(st);
                }
                else if (st[0] == 'N')
                {
                    //Danh_Bai.Enabled = true;
                    Bo_Luot.Enabled = true;
                    label1.Visible = true;
                    if (st[st.Length - 1] == 'G')
                    {
                        label1.Visible = true;
                        Bo_Luot.Enabled = false;
                        lock (CardOnDeck)
                        {
                            CardOnDeck.Clear();
                        }
                        lock (CardOnDeck)
                        {
                            if (!CardOnDeck.Any())
                            {
                                check1 = new Thread(CheckBeforeDo);
                                check1.Start();
                            }
                        }
                    }
                    else
                    {
                        lock (CardOnDeck)
                        {
                            if (CardOnDeck.Any())
                            {
                                check1 = new Thread(CheckBeforeDo);
                                check1.Start();
                            }
                        }
                    }
                }    
                
            }
        }

        private void Desk_Start(string st)
        {
            Danh_Bai.Visible = true;
            Bo_Luot.Visible = true;
            Danh_Bai.Enabled = false;
            Bo_Luot.Enabled = false;
            label1.Visible = false;
            if (st[st.Length - 1] == 'P')
            {
                //Danh_Bai.Enabled = true;
                label1.Visible = true;
                Bo_Luot.Enabled = false;
                lock (CardOnDeck)
                {
                    if (!CardOnDeck.Any())
                    {
                        check1 = new Thread(CheckBeforeDo);
                        check1.Start();
                    }
                }
                st.Remove(st.Length - 1, 1);
            }
            /*if (st[st.Length - 1] == 'G')
            {
                Bo_Luot.Enabled = false;
                st.Remove(st.Length - 1, 1);
            }*/
            int pos1 = st.IndexOf('|');
            int pos2 = st.IndexOf('/');
            string room = "";
            string PSR = "";
            for (int i = pos1+1; i < pos2; i++)
                room += st[i];
            Phong = room;
            PSR+= st[pos2 + 1];
            PositionInRoom = PSR;
            string set = st.Remove(pos1, room.Length+3);
            set = st.Remove(0, 1);
            string QB = "";
            int update = 0;
            for (int i = 0; i < set.Length; i++)
            {
                if (set[i] == ';')
                {
                    update++;
                    char ch = '.';
                    int pos = QB.IndexOf(ch);
                    string num1 = QB.Remove(pos, 2);
                    string num2 = "" + QB[pos + 1];
                    //CardSet.Set(update, Int32.Parse(num1), Int32.Parse(num2));
                    Card a = new Card(Int32.Parse(num1), Int32.Parse(num2));
                    CardLeft.Add(a);
                    QB = "";
                }
                else QB += set[i];
            }
            CardLeft.Sort(delegate (Card x1, Card x2)
            {
                return ((x1.GetNumber() * 4 + x1.GetType()).CompareTo(x2.GetNumber() * 4 + x2.GetType()));
            });
            UpdateBoBai();
        }
        private void Deck_Update(string st)
        {
            pictureBox14.ImageLocation = null;
            pictureBox15.ImageLocation = null;
            pictureBox16.ImageLocation = null;
            pictureBox17.ImageLocation = null;
            pictureBox18.ImageLocation = null;
            pictureBox19.ImageLocation = null;
            pictureBox20.ImageLocation = null;
            pictureBox21.ImageLocation = null;
            pictureBox22.ImageLocation = null;
            pictureBox23.ImageLocation = null;
            pictureBox24.ImageLocation = null;
            pictureBox25.ImageLocation = null;
            pictureBox26.ImageLocation = null;
            pictureBox14.Visible = false;
            pictureBox15.Visible = false;
            pictureBox16.Visible = false;
            pictureBox17.Visible = false;
            pictureBox18.Visible = false;
            pictureBox19.Visible = false;
            pictureBox20.Visible = false;
            pictureBox21.Visible = false;
            pictureBox22.Visible = false;
            pictureBox23.Visible = false;
            pictureBox24.Visible = false;
            pictureBox25.Visible = false;
            pictureBox26.Visible = false;
            string set = st.Remove(0, 1);
            //CardOnDeck.Clear();
            
            
            string QB = "";
            for (int i = 0; i < set.Length; i++)
            {
                if (set[i] == ';')
                {
                    char ch = '.';
                    int pos = QB.IndexOf(ch);
                    string num1 = QB.Remove(pos, 2);
                    string num2 = "" + QB[pos + 1];
                    Card a = new Card(Int32.Parse(num1), Int32.Parse(num2));
                    lock (CardOnDeck)
                    {
                        CardOnDeck.Add(a);
                    }
                    QB = "";
                }
                else QB += set[i];
            }
            if (CardOnDeck.Count == 1)
            {
                pictureBox20.Visible = true;
                foreach (Card a in CardOnDeck)
                    pictureBox20.ImageLocation = a.GetLocation();
            }
            else if (CardOnDeck.Count == 2)
            {
                pictureBox21.Visible = true;
                pictureBox20.Visible = true;
                int i = 1;
                foreach (Card a in CardOnDeck)
                {
                    if (i == 1)
                        pictureBox21.ImageLocation = a.GetLocation();
                    if (i == 2)
                        pictureBox20.ImageLocation = a.GetLocation();
                    i++;
                }
                
            }
            else if (CardOnDeck.Count == 3)
            {
                int i = 1;
                pictureBox21.Visible = true;
                pictureBox20.Visible = true;
                pictureBox19.Visible = true;
                foreach (Card a in CardOnDeck)
                {
                    if (i == 1)
                        pictureBox21.ImageLocation = a.GetLocation();
                    if (i == 2)
                        pictureBox20.ImageLocation = a.GetLocation();
                    if (i == 3)
                        pictureBox19.ImageLocation = a.GetLocation();
                    i++;
                }
                
            }
            else if (CardOnDeck.Count == 4)
            {
                int i = 1;
                pictureBox21.Visible = true;
                pictureBox20.Visible = true;
                pictureBox19.Visible = true;
                pictureBox22.Visible = true;
                foreach (Card a in CardOnDeck)
                {
                    if (i == 1)
                        pictureBox22.ImageLocation = a.GetLocation();
                    if (i == 2)
                        pictureBox21.ImageLocation = a.GetLocation();
                    if (i == 3)
                        pictureBox20.ImageLocation = a.GetLocation();
                    if (i == 4)
                        pictureBox19.ImageLocation = a.GetLocation();
                    i++;
                }
            }
            else if (CardOnDeck.Count == 5)
            {
                int i = 1;
                pictureBox21.Visible = true;
                pictureBox20.Visible = true;
                pictureBox19.Visible = true;
                pictureBox22.Visible = true;
                pictureBox18.Visible = true;
                foreach (Card a in CardOnDeck)
                {

                    if (i == 1)
                        pictureBox22.ImageLocation = a.GetLocation();
                    if (i == 2)
                        pictureBox21.ImageLocation = a.GetLocation();
                    if (i == 3)
                        pictureBox20.ImageLocation = a.GetLocation();
                    if (i == 4)
                        pictureBox19.ImageLocation = a.GetLocation();
                    if (i == 5)
                        pictureBox18.ImageLocation = a.GetLocation();
                    i++;
                }
               
            }
            else if (CardOnDeck.Count == 6)
            {
                int i = 1;
                pictureBox21.Visible = true;
                pictureBox20.Visible = true;
                pictureBox19.Visible = true;
                pictureBox22.Visible = true;
                pictureBox18.Visible = true;
                pictureBox23.Visible = true;
                foreach (Card a in CardOnDeck)
                {
                    if (i == 1)
                        pictureBox23.ImageLocation = a.GetLocation();
                    if (i == 2)
                        pictureBox22.ImageLocation = a.GetLocation();
                    if (i == 3)
                        pictureBox21.ImageLocation = a.GetLocation();
                    if (i == 4)
                        pictureBox20.ImageLocation = a.GetLocation();
                    if (i == 5)
                        pictureBox19.ImageLocation = a.GetLocation();
                    if (i == 6)
                        pictureBox18.ImageLocation = a.GetLocation();
                    i++;
                }
                
            }
            else if (CardOnDeck.Count == 7)
            {
                int i = 1;
                pictureBox21.Visible = true;
                pictureBox20.Visible = true;
                pictureBox19.Visible = true;
                pictureBox22.Visible = true;
                pictureBox18.Visible = true;
                pictureBox23.Visible = true;
                pictureBox17.Visible = true;
                foreach (Card a in CardOnDeck)
                {
                    if (i == 1)
                        pictureBox23.ImageLocation = a.GetLocation();
                    if (i == 2)
                        pictureBox22.ImageLocation = a.GetLocation();
                    if (i == 3)
                        pictureBox21.ImageLocation = a.GetLocation();
                    if (i == 4)
                        pictureBox20.ImageLocation = a.GetLocation();
                    if (i == 5)
                        pictureBox19.ImageLocation = a.GetLocation();
                    if (i == 6)
                        pictureBox18.ImageLocation = a.GetLocation();
                    if (i == 7)
                        pictureBox17.ImageLocation = a.GetLocation();
                    i++;
                }
                
            }
            else if (CardOnDeck.Count == 8)
            {
                int i = 1;
                pictureBox21.Visible = true;
                pictureBox20.Visible = true;
                pictureBox19.Visible = true;
                pictureBox22.Visible = true;
                pictureBox18.Visible = true;
                pictureBox23.Visible = true;
                pictureBox17.Visible = true;
                pictureBox24.Visible = true;
                foreach (Card a in CardOnDeck)
                {
                    if (i == 1)
                        pictureBox24.ImageLocation = a.GetLocation();
                    if (i == 2)
                        pictureBox23.ImageLocation = a.GetLocation();
                    if (i == 3)
                        pictureBox22.ImageLocation = a.GetLocation();
                    if (i == 4)
                        pictureBox21.ImageLocation = a.GetLocation();
                    if (i == 5)
                        pictureBox20.ImageLocation = a.GetLocation();
                    if (i == 6)
                        pictureBox19.ImageLocation = a.GetLocation();
                    if (i == 7)
                        pictureBox18.ImageLocation = a.GetLocation();
                    if (i == 8)
                        pictureBox17.ImageLocation = a.GetLocation();
                    i++;
                }
                
            }
            else if (CardOnDeck.Count == 9)
            {
                pictureBox21.Visible = true;
                pictureBox20.Visible = true;
                pictureBox19.Visible = true;
                pictureBox22.Visible = true;
                pictureBox18.Visible = true;
                pictureBox23.Visible = true;
                pictureBox17.Visible = true;
                pictureBox24.Visible = true;
                pictureBox16.Visible = true;
                int i = 1;
                foreach (Card a in CardOnDeck)
                {
                    if (i == 1)
                        pictureBox24.ImageLocation = a.GetLocation();
                    if (i == 2)
                        pictureBox23.ImageLocation = a.GetLocation();
                    if (i == 3)
                        pictureBox22.ImageLocation = a.GetLocation();
                    if (i == 4)
                        pictureBox21.ImageLocation = a.GetLocation();
                    if (i == 5)
                        pictureBox20.ImageLocation = a.GetLocation();
                    if (i == 6)
                        pictureBox19.ImageLocation = a.GetLocation();
                    if (i == 7)
                        pictureBox18.ImageLocation = a.GetLocation();
                    if (i == 8)
                        pictureBox17.ImageLocation = a.GetLocation();
                    if (i == 9)
                        pictureBox16.ImageLocation = a.GetLocation();
                    i++;
                }
                
            }
            else if (CardOnDeck.Count == 10)
            {
                int i = 1;
                pictureBox21.Visible = true;
                pictureBox20.Visible = true;
                pictureBox19.Visible = true;
                pictureBox22.Visible = true;
                pictureBox18.Visible = true;
                pictureBox23.Visible = true;
                pictureBox17.Visible = true;
                pictureBox24.Visible = true;
                pictureBox16.Visible = true;
                pictureBox25.Visible = true;
                foreach (Card a in CardOnDeck)
                {
                    if (i == 1)
                        pictureBox25.ImageLocation = a.GetLocation();
                    if (i == 2)
                        pictureBox24.ImageLocation = a.GetLocation();
                    if (i == 3)
                        pictureBox23.ImageLocation = a.GetLocation();
                    if (i == 4)
                        pictureBox22.ImageLocation = a.GetLocation();
                    if (i == 5)
                        pictureBox21.ImageLocation = a.GetLocation();
                    if (i == 6)
                        pictureBox20.ImageLocation = a.GetLocation();
                    if (i == 7)
                        pictureBox19.ImageLocation = a.GetLocation();
                    if (i == 8)
                        pictureBox18.ImageLocation = a.GetLocation();
                    if (i == 9)
                        pictureBox17.ImageLocation = a.GetLocation();
                    if (i == 10)
                        pictureBox16.ImageLocation = a.GetLocation();
                    i++;
                }
                
            }
            else if (CardOnDeck.Count == 11)
            {
                int i = 1;
                pictureBox21.Visible = true;
                pictureBox20.Visible = true;
                pictureBox19.Visible = true;
                pictureBox22.Visible = true;
                pictureBox18.Visible = true;
                pictureBox23.Visible = true;
                pictureBox17.Visible = true;
                pictureBox24.Visible = true;
                pictureBox16.Visible = true;
                pictureBox25.Visible = true;
                pictureBox15.Visible = true;
                foreach (Card a in CardOnDeck)
                {
                    if (i == 1)
                        pictureBox25.ImageLocation = a.GetLocation();
                    if (i == 2)
                        pictureBox24.ImageLocation = a.GetLocation();
                    if (i == 3)
                        pictureBox23.ImageLocation = a.GetLocation();
                    if (i == 4)
                        pictureBox22.ImageLocation = a.GetLocation();
                    if (i == 5)
                        pictureBox21.ImageLocation = a.GetLocation();
                    if (i == 6)
                        pictureBox20.ImageLocation = a.GetLocation();
                    if (i == 7)
                        pictureBox19.ImageLocation = a.GetLocation();
                    if (i == 8)
                        pictureBox18.ImageLocation = a.GetLocation();
                    if (i == 9)
                        pictureBox17.ImageLocation = a.GetLocation();
                    if (i == 10)
                        pictureBox16.ImageLocation = a.GetLocation();
                    if (i == 11)
                        pictureBox15.ImageLocation = a.GetLocation();
                    i++;
                }
                
            }
            else if (CardOnDeck.Count == 12)
            {
                int i = 1;
                pictureBox21.Visible = true;
                pictureBox20.Visible = true;
                pictureBox19.Visible = true;
                pictureBox22.Visible = true;
                pictureBox18.Visible = true;
                pictureBox23.Visible = true;
                pictureBox17.Visible = true;
                pictureBox24.Visible = true;
                pictureBox16.Visible = true;
                pictureBox25.Visible = true;
                pictureBox15.Visible = true;
                pictureBox26.Visible = true;
                foreach (Card a in CardOnDeck)
                {
                    if (i == 1)
                        pictureBox26.ImageLocation = a.GetLocation();
                    if (i == 2)
                        pictureBox25.ImageLocation = a.GetLocation();
                    if (i == 3)
                        pictureBox24.ImageLocation = a.GetLocation();
                    if (i == 4)
                        pictureBox23.ImageLocation = a.GetLocation();
                    if (i == 5)
                        pictureBox22.ImageLocation = a.GetLocation();
                    if (i == 6)
                        pictureBox21.ImageLocation = a.GetLocation();
                    if (i == 7)
                        pictureBox20.ImageLocation = a.GetLocation();
                    if (i == 8)
                        pictureBox19.ImageLocation = a.GetLocation();
                    if (i == 9)
                        pictureBox18.ImageLocation = a.GetLocation();
                    if (i == 10)
                        pictureBox17.ImageLocation = a.GetLocation();
                    if (i == 11)
                        pictureBox16.ImageLocation = a.GetLocation();
                    if (i == 12)
                        pictureBox15.ImageLocation = a.GetLocation();
                    i++;
                }
                
            }
        }//CardOnDeck update
        private void Start_Click(object sender, EventArgs e)
        {
            Start.Visible = false;
            panel2.Visible = true;
            string str = "C"+Phong;
            int flag = tcp.SendData(str);           
        }

        public void UpdateBoBai()
        {
            int i= 1;
            panel1.Refresh();
            pictureBox1.ImageLocation = null;
            pictureBox2.ImageLocation = null;
            pictureBox3.ImageLocation = null;
            pictureBox4.ImageLocation = null;
            pictureBox5.ImageLocation = null;
            pictureBox6.ImageLocation = null;
            pictureBox7.ImageLocation = null;
            pictureBox8.ImageLocation = null;
            pictureBox9.ImageLocation = null;
            pictureBox10.ImageLocation = null;
            pictureBox11.ImageLocation = null;
            pictureBox12.ImageLocation = null;
            pictureBox13.ImageLocation = null;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            pictureBox7.Visible = false;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            pictureBox10.Visible = false;
            pictureBox11.Visible = false;
            pictureBox12.Visible = false;
            pictureBox13.Visible = false;
            foreach (Card a in CardLeft)
            {
                string st = a.GetLocation();
                if (i == 1) { pictureBox1.ImageLocation = st; pictureBox1.Visible = true; pictureBox1.Location = new System.Drawing.Point(3, 25); }
                if (i == 2) { pictureBox2.ImageLocation = st; pictureBox2.Visible = true; pictureBox2.Location = new System.Drawing.Point(46, 25); }
                if (i == 3) { pictureBox3.ImageLocation = st; pictureBox3.Visible = true; pictureBox3.Location = new System.Drawing.Point(89, 25); }
                if (i == 4) { pictureBox4.ImageLocation = st; pictureBox4.Visible = true; pictureBox4.Location = new System.Drawing.Point(132, 25); }
                if (i == 5) { pictureBox5.ImageLocation = st; pictureBox5.Visible = true; pictureBox5.Location = new System.Drawing.Point(175, 25); }
                if (i == 6) { pictureBox6.ImageLocation = st; pictureBox6.Visible = true; pictureBox6.Location = new System.Drawing.Point(218, 25); }
                if (i == 7) { pictureBox7.ImageLocation = st; pictureBox7.Visible = true; pictureBox7.Location = new System.Drawing.Point(261, 25); }
                if (i == 8) { pictureBox8.ImageLocation = st; pictureBox8.Visible = true; pictureBox8.Location = new System.Drawing.Point(304, 25); }
                if (i == 9) { pictureBox9.ImageLocation = st; pictureBox9.Visible = true; pictureBox9.Location = new System.Drawing.Point(347, 25); }
                if (i == 10) { pictureBox10.ImageLocation = st; pictureBox10.Visible = true; pictureBox10.Location = new System.Drawing.Point(390, 25); }
                if (i == 11) { pictureBox11.ImageLocation = st; pictureBox11.Visible = true; pictureBox11.Location = new System.Drawing.Point(433, 25); }
                if (i == 12) { pictureBox12.ImageLocation = st; pictureBox12.Visible = true; pictureBox12.Location = new System.Drawing.Point(476, 25); }
                if (i == 13) { pictureBox13.ImageLocation = st; pictureBox13.Visible = true; pictureBox13.Location = new System.Drawing.Point(519, 25); }
                i++;
            }
        }//CareLeft Update

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            lock (CardPresent)
            {
                System.Drawing.Point a = new System.Drawing.Point(3, 25);
                int x = CardLeft[0].GetNumber();
                int y = CardLeft[0].GetType();
                Card tmp = new Card(x, y);
                if (a == pictureBox1.Location)
                {
                    pictureBox1.Location = new System.Drawing.Point(3, 0);
                    CardPresent.Add(tmp);
                }
                else
                {
                    pictureBox1.Location = a;
                    foreach (Card b in CardPresent)
                    {
                        if ((b.GetNumber() == tmp.GetNumber()) && (b.GetType() == tmp.GetType()))
                        {
                            CardPresent.Remove(b);
                            break;
                        }
                    }

                }
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            lock (CardPresent)
            {
                System.Drawing.Point a = new System.Drawing.Point(46, 25);
                int x = CardLeft[1].GetNumber();
                int y = CardLeft[1].GetType();
                Card tmp = new Card(x, y);
                if (a == pictureBox2.Location)
                {
                    pictureBox2.Location = new System.Drawing.Point(46, 0);
                    CardPresent.Add(tmp);
                }
                else
                {
                    pictureBox2.Location = a;
                    foreach (Card b in CardPresent)
                    {
                        if ((b.GetNumber() == tmp.GetNumber()) && (b.GetType() == tmp.GetType()))
                        {
                            CardPresent.Remove(b);
                            break;
                        }
                    }
                }
            }
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            lock (CardPresent)
            {
                System.Drawing.Point a = new System.Drawing.Point(89, 25);
                int x = CardLeft[2].GetNumber();
                int y = CardLeft[2].GetType();
                Card tmp = new Card(x, y);
                if (a == pictureBox3.Location)
                {
                    pictureBox3.Location = new System.Drawing.Point(89, 0);
                    CardPresent.Add(tmp);
                }
                else
                {
                    pictureBox3.Location = a;
                    foreach (Card b in CardPresent)
                    {
                        if ((b.GetNumber() == tmp.GetNumber()) && (b.GetType() == tmp.GetType()))
                        {
                            CardPresent.Remove(b);
                            break;
                        }
                    }
                }
            }
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            lock (CardPresent)
            {
                System.Drawing.Point a = new System.Drawing.Point(132, 25);
                int x = CardLeft[3].GetNumber();
                int y = CardLeft[3].GetType();
                Card tmp = new Card(x, y);
                if (a == pictureBox4.Location)
                {
                    pictureBox4.Location = new System.Drawing.Point(132, 0);
                    CardPresent.Add(tmp);
                }
                else
                {
                    pictureBox4.Location = a;
                    foreach (Card b in CardPresent)
                    {
                        if ((b.GetNumber() == tmp.GetNumber()) && (b.GetType() == tmp.GetType()))
                        {
                            CardPresent.Remove(b);
                            break;
                        }
                    }
                }
            }
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            lock (CardPresent)
            {
                System.Drawing.Point a = new System.Drawing.Point(175, 25);
                int x = CardLeft[4].GetNumber();
                int y = CardLeft[4].GetType();
                Card tmp = new Card(x, y);
                if (a == pictureBox5.Location)
                {
                    pictureBox5.Location = new System.Drawing.Point(175, 0);
                    CardPresent.Add(tmp);
                }
                else
                {
                    pictureBox5.Location = a;
                    foreach (Card b in CardPresent)
                    {
                        if ((b.GetNumber() == tmp.GetNumber()) && (b.GetType() == tmp.GetType()))
                        {
                            CardPresent.Remove(b);
                            break;
                        }
                    }
                }
            }
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            lock (CardPresent)
            {
                System.Drawing.Point a = new System.Drawing.Point(218, 25);
                int x = CardLeft[5].GetNumber();
                int y = CardLeft[5].GetType();
                Card tmp = new Card(x, y);
                if (a == pictureBox6.Location)
                {
                    pictureBox6.Location = new System.Drawing.Point(218, 0);
                    CardPresent.Add(tmp);
                }
                else
                {
                    pictureBox6.Location = a;
                    foreach (Card b in CardPresent)
                    {
                        if ((b.GetNumber() == tmp.GetNumber()) && (b.GetType() == tmp.GetType()))
                        {
                            CardPresent.Remove(b);
                            break;
                        }
                    }
                }
            }
        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            lock (CardPresent)
            {
                System.Drawing.Point a = new System.Drawing.Point(261, 25);
                int x = CardLeft[6].GetNumber();
                int y = CardLeft[6].GetType();
                Card tmp = new Card(x, y);
                if (a == pictureBox7.Location)
                {
                    pictureBox7.Location = new System.Drawing.Point(261, 0);
                    CardPresent.Add(tmp);
                }
                else
                {
                    pictureBox7.Location = a;
                    foreach (Card b in CardPresent)
                    {
                        if ((b.GetNumber() == tmp.GetNumber()) && (b.GetType() == tmp.GetType()))
                        {
                            CardPresent.Remove(b);
                            break;
                        }
                    }
                }
            }
        }
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            lock (CardPresent)
            {
                System.Drawing.Point a = new System.Drawing.Point(304, 25);
                int x = CardLeft[7].GetNumber();
                int y = CardLeft[7].GetType();
                Card tmp = new Card(x, y);
                if (a == pictureBox8.Location)
                {
                    pictureBox8.Location = new System.Drawing.Point(304, 0);
                    CardPresent.Add(tmp);
                }
                else
                {
                    pictureBox8.Location = a;
                    foreach (Card b in CardPresent)
                    {
                        if ((b.GetNumber() == tmp.GetNumber()) && (b.GetType() == tmp.GetType()))
                        {
                            CardPresent.Remove(b);
                            break;
                        }
                    }
                }
            }
        }
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            lock (CardPresent)
            {
                System.Drawing.Point a = new System.Drawing.Point(347, 25);
                int x = CardLeft[8].GetNumber();
                int y = CardLeft[8].GetType();
                Card tmp = new Card(x, y);
                if (a == pictureBox9.Location)
                {
                    pictureBox9.Location = new System.Drawing.Point(347, 0);
                    CardPresent.Add(tmp);
                }
                else
                {
                    pictureBox9.Location = a;
                    foreach (Card b in CardPresent)
                    {
                        if ((b.GetNumber() == tmp.GetNumber()) && (b.GetType() == tmp.GetType()))
                        {
                            CardPresent.Remove(b);
                            break;
                        }
                    }
                }
            }
        }
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            lock (CardPresent)
            {
                System.Drawing.Point a = new System.Drawing.Point(390, 25);
                int x = CardLeft[9].GetNumber();
                int y = CardLeft[9].GetType();
                Card tmp = new Card(x, y);
                if (a == pictureBox10.Location)
                {
                    pictureBox10.Location = new System.Drawing.Point(390, 0);
                    CardPresent.Add(tmp);
                }
                else
                {
                    pictureBox10.Location = a;
                    foreach (Card b in CardPresent)
                    {
                        if ((b.GetNumber() == tmp.GetNumber()) && (b.GetType() == tmp.GetType()))
                        {
                            CardPresent.Remove(b);
                            break;
                        }
                    }
                }
            }
        }
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            lock (CardPresent)
            {
                System.Drawing.Point a = new System.Drawing.Point(433, 25);
                int x = CardLeft[10].GetNumber();
                int y = CardLeft[10].GetType();
                Card tmp = new Card(x, y);
                if (a == pictureBox11.Location)
                {
                    pictureBox11.Location = new System.Drawing.Point(433, 0);
                    CardPresent.Add(tmp);
                }
                else
                {
                    pictureBox11.Location = a;
                    foreach (Card b in CardPresent)
                    {
                        if ((b.GetNumber() == tmp.GetNumber()) && (b.GetType() == tmp.GetType()))
                        {
                            CardPresent.Remove(b);
                            break;
                        }
                    }
                }
            }
        }
        private void pictureBox12_Click(object sender, EventArgs e)
        {
            lock (CardPresent)
            {
                System.Drawing.Point a = new System.Drawing.Point(476, 25);
                int x = CardLeft[11].GetNumber();
                int y = CardLeft[11].GetType();
                Card tmp = new Card(x, y);
                if (a == pictureBox12.Location)
                {
                    pictureBox12.Location = new System.Drawing.Point(476, 0);
                    CardPresent.Add(tmp);
                }
                else
                {
                    pictureBox12.Location = a;
                    foreach (Card b in CardPresent)
                    {
                        if ((b.GetNumber() == tmp.GetNumber()) && (b.GetType() == tmp.GetType()))
                        {
                            CardPresent.Remove(b);
                            break;
                        }
                    }
                }
            }
        }
        private void pictureBox13_Click(object sender, EventArgs e)
        {
            lock (CardPresent)
            {
                System.Drawing.Point a = new System.Drawing.Point(519, 25);
                int x = CardLeft[12].GetNumber();
                int y = CardLeft[12].GetType();
                Card tmp = new Card(x, y);
                if (a == pictureBox13.Location)
                {
                    pictureBox13.Location = new System.Drawing.Point(519, 0);
                    CardPresent.Add(tmp);
                }
                else
                {
                    pictureBox13.Location = a;
                    foreach (Card b in CardPresent)
                    {
                        if ((b.GetNumber() == tmp.GetNumber()) && (b.GetType() == tmp.GetType()))
                        {
                            CardPresent.Remove(b);
                            break;
                        }
                    }
                }
            }
        }

        private void Danh_Bai_Click(object sender, EventArgs e)
        {
            if (check1 != null) {
                check1.Abort();
                //check1.Join();
            }
            lock (CardPresent)
            {
                CardPresent.Sort(delegate (Card x1, Card x2)
                {
                    return ((x1.GetNumber() * 4 + x1.GetType()).CompareTo(x2.GetNumber() * 4 + x2.GetType()));
                });
                string st = "";
                foreach (Card a in CardPresent)
                {
                    st += a.GetNumber().ToString() + '.' + a.GetType().ToString() + ';';
                    foreach (Card b in CardLeft)
                    {
                        if ((b.GetNumber() == a.GetNumber()) && (a.GetType() == b.GetType()))
                        {
                            CardLeft.Remove(b);
                            break;
                        }
                    }
                }
                
                CardPresent.Clear();
                st = 'D' + st + '|' + Phong + '/' + PositionInRoom;
                UpdateBoBai();
                //Gui khi het bai
                if (!CardLeft.Any())
                {
                    st += 'W'; 
                }
                int flag = tcp.SendData(st);
            }
        }

        private void Bo_Luot_Click(object sender, EventArgs e)
        {
            if (check1 != null)
            {
                check1.Abort();
                //check1.Join();
            }
            label1.Visible = false;
            string st = "B|" + Phong + '/' + PositionInRoom;
            int flag = tcp.SendData(st);
            Danh_Bai.Enabled = false;
            Bo_Luot.Enabled = false;
        }

        private void ChoiLai_Click(object sender, EventArgs e)
        {
            if (check1 != null)
            {
                check1.Abort();
                check1.Join();
            }
            pictureBox14.ImageLocation = null;
            pictureBox15.ImageLocation = null;
            pictureBox16.ImageLocation = null;
            pictureBox17.ImageLocation = null;
            pictureBox18.ImageLocation = null;
            pictureBox19.ImageLocation = null;
            pictureBox20.ImageLocation = null;
            pictureBox21.ImageLocation = null;
            pictureBox22.ImageLocation = null;
            pictureBox23.ImageLocation = null;
            pictureBox24.ImageLocation = null;
            pictureBox25.ImageLocation = null;
            pictureBox26.ImageLocation = null;
            ChoiLai.Visible = false;
            string st="A|"+ Phong + '/' + PositionInRoom;
            int flag = tcp.SendData(st);
            Update = new Thread(UpdateDesk);
            Update.Start();
            //string st1 = tcp.ReadData();
        }
    }
}
