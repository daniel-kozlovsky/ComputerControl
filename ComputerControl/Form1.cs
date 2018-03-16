using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerControl
{
    public partial class Form1 : Form
    {
        Button btnExtra;
        Button btnMain;
        ContextMenu mnuOptions;
        public Form1()
        {
            InitializeComponent();
            InitMyComponents();
        }
        void InitMyComponents()
        {
            //
            //Button main
            //
            btnMain = new Button();
            btnMain.Height = this.ClientSize.Height;
            btnMain.Width = this.ClientSize.Width - 20;
            btnMain.Left = 0;
            btnMain.Top = 0;
            btnMain.Text = "Shut Down";
            btnMain.Click += btnMain_Click;

            //
            //Button extra
            //
            btnExtra = new Button();
            btnExtra.Height = this.ClientSize.Height;
            btnExtra.Width = 20;
            btnExtra.Left = btnMain.Right;
            btnExtra.Top = 0;
            btnExtra.Text = ">";
            btnExtra.Click += btnExtra_Click;



            this.Controls.Add(btnExtra);
            this.Controls.Add(btnMain);

            AddContextMenuAndItems();
        }
        //context
        public void AddContextMenuAndItems()
        {

            MenuItem[] ComputerOptions = new MenuItem[4];
            for (int x = 0; x < ComputerOptions.Length; x++)
            {
                ComputerOptions[x] = new MenuItem();
            }
            ComputerOptions[0].Text = "Restart";
            ComputerOptions[1].Text = "Standby";
            ComputerOptions[2].Text = "Log Off";
            ComputerOptions[3].Text = "Hibernate";

            ComputerOptions[0].Click += Form1_Click;
            ComputerOptions[1].Click += Form1_Click2;
            ComputerOptions[2].Click += Form1_Click3;
            ComputerOptions[3].Click += Form1_Click4;

            mnuOptions = new ContextMenu(ComputerOptions);


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Cursor.Position.Y + 100 > Screen.PrimaryScreen.Bounds.Height)
            {
                this.Location = new Point(Cursor.Position.X, Screen.PrimaryScreen.Bounds.Height-110);
            }
            else
            {
                this.Location = Cursor.Position;
            }
            
        }

        //Shutdown
        void btnMain_Click(object sender, System.EventArgs e)
        {
            Process.Start("shutdown", "/s /t 0");
        }
        //Context
        void btnExtra_Click(object sender, System.EventArgs e)
        {

            mnuOptions.Show(btnExtra, new Point(btnExtra.Width, 0));
            
            
        }
        //restart
        void Form1_Click(object sender, System.EventArgs e)
        {
            Process.Start("shutdown", "/r /t 0");
        }
        //standby
        void Form1_Click2(object sender, System.EventArgs e)
        {
            Application.Exit();
            SetSuspendState(false, true, true);

        }
        //log off
        void Form1_Click3(object sender, System.EventArgs e)
        {
            ExitWindowsEx(0, 0);

        }
        //hibernate
        void Form1_Click4(object sender, System.EventArgs e)
        {
            SetSuspendState(true, true, true);
            Application.Exit();
        }
        [DllImport("user32")]
        public static extern bool ExitWindowsEx(uint uFlags, uint dwReason);
        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);
    }
}
