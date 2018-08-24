using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoCaRo
{
    public partial class Form1 : Form
    {
        bool LuotCo=true;
        bool bolTinhTrangGame = true;
        Oco [,] arrOCo;  //mảng 2 chiều ánh xạ bàn cờ 20x20
        List<Oco> lstWin=new List<Oco>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(20*40,20*32);
            TaoBanCo();
            ResetGame();
        }

        private void ResetGame()
        {
            Button btnReset = new Button();
            btnReset.Text = "Chơi Lại";
            btnReset.Click += btnReset_Click;
            btnReset.Size = new Size(100,50);
            btnReset.Top = 100;
            btnReset.Left = this.Width - 150;
            this.Controls.Add(btnReset);
        }

        void btnReset_Click(object sender, EventArgs e)
        {
            XuLySulyBtnReset();
        }

        private void XuLySulyBtnReset()
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Oco)
                    if (this.Controls[i].Text.Length > 0)
                    {
                        this.Controls[i].ResetText();
                        this.Controls[i].BackColor = SystemColors.Control;
                    }
            }
            lstWin.Clear();//Xóa ds đường thắng
            bolTinhTrangGame = true;
        }

        private void TaoBanCo()
        {
            arrOCo = new Oco[20, 20];
           for(int i=0;i<20;i++)
               for (int j = 0; j < 20; j++)
               {
                 
                   Oco btn=new Oco();
                   btn.Size=new Size(30,30);
                   btn.Top=i*30;
                   btn.Left=j*30;
                   btn.Dong = i;  //đặt vị trí ô cơ
                   btn.Cot = j;
                   btn.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                   btn.Click += btn_Click;
                   arrOCo[i, j] = btn;
                   this.Controls.Add(btn);

               }
        }

        void btn_Click(object sender, EventArgs e)
        {
            
            Oco btn=(Oco)sender;
            if (btn.Text.Length > 0)  //ràng buộc khong cho nhấn 2 lần vào 1 ô cờ
                return;
            if (!bolTinhTrangGame)  //nếu game đã tìm ra người chiến thắng thì k chơi nữa
            {
                MessageBox.Show("Ván Game Đã Kết Thúc");
                return;
            }
            XuLySuKienChon(btn);
        }

        private void XuLySuKienChon(Oco btn)
        {
            if (LuotCo) //nếu Lươt cờ true là lượt của X ngược lại là lược của O
            {
               
                btn.Text = "X";
                btn.ForeColor = Color.Red;
                LuotCo = false; //thay đổi lượt cờ
            }
            else
            {
                btn.Text = "O";
                btn.ForeColor = Color.Green;
                LuotCo = true;
              
            }

            testWin(btn);
               
        }

        private void testWin(Oco btn)
        {

         bool bolHuong=false;
         if (KT_ThangThua(btn, "Doc"))
             bolHuong=true;
         if (KT_ThangThua(btn, "Ngang"))
             bolHuong = true;
         if (KT_ThangThua(btn, "TreoTrai"))
             bolHuong = true;
         if (KT_ThangThua(btn, "TreoPhai"))
             bolHuong = true;
         if (bolHuong) MessageBox.Show("Win");
            
            //đổi màu ô cờ đúng
            foreach (Oco co in lstWin)
            {
                co.BackColor = Color.Yellow;
            }
            
           
        }

       

        private  bool KT_ThangThua(Oco btn,string Huong)
        {
            int LuotDi=2;
            int demQuanCo=0;
            int chanDau = 0;
            Oco btnVitriDau=btn;
            Oco btn2;
            List<Oco> lstWint = new List<Oco>(); //dùng để lưu trữ đường cơ chiến thắng
            lstWint.Add(btn);
            while(LuotDi>0)
            {
                try
                {
                    if (LuotDi == 2)
                        btn2 = XuLyHuong_DiToi(btnVitriDau, Huong);
                    else btn2 = XuLyHuong_DiLui(btnVitriDau, Huong);
                    if (!btnVitriDau.Text.Equals(btn2.Text))
                    {
                        LuotDi--;
                        btnVitriDau = btn;
                        if (btn2.Text.Length > 0)
                            chanDau++;
                    }
                    else
                    {
                        demQuanCo++;
                        btnVitriDau = btn2;
                        lstWint.Add(btn2);
                    }
                }
                catch
                {
                    LuotDi--;
                    btnVitriDau = btn;
                }      
           }
            if (demQuanCo >= 4 && chanDau<2)
            {
                lstWin.AddRange(lstWint);
                return true;
            }
            else

                return false;
        }

        private Oco XuLyHuong_DiToi(Oco btn,string Huong)
        {
            if (Huong.Equals("Doc"))
                return arrOCo[btn.Dong + 1, btn.Cot];
            if (Huong.Equals("Ngang"))
                return arrOCo[btn.Dong, btn.Cot+1];
            if (Huong.Equals("TreoTrai"))
                return arrOCo[btn.Dong - 1, btn.Cot-1];
            else return arrOCo[btn.Dong - 1, btn.Cot + 1];

        }

        private Oco XuLyHuong_DiLui(Oco btn, string Huong)
        {
            if (Huong.Equals("Doc"))
                return arrOCo[btn.Dong - 1, btn.Cot];
            if (Huong.Equals("Ngang"))
                return arrOCo[btn.Dong , btn.Cot - 1];
            if (Huong.Equals("TreoTrai"))
                return arrOCo[btn.Dong + 1, btn.Cot + 1];
            return arrOCo[btn.Dong + 1, btn.Cot - 1];

        }


    

       

    }
}
