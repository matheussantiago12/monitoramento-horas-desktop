using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Win32_API;

namespace MonitoramentoHoras
{
    public class TrayApplicationContext : ApplicationContext
    {
        NotifyIcon notifyIcon = new NotifyIcon();

        public TrayApplicationContext()
        {
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("Sair", null, new EventHandler(Exit));

            notifyIcon.Icon = Properties.Resources.IconeTray;
            notifyIcon.DoubleClick += new EventHandler(ShowMessage);
            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Insert(0, exitMenuItem);
            notifyIcon.Visible = true;
        }

        void ShowMessage(object sender, EventArgs e)
        {
            Win32.GetIdleTime();
            MessageBox.Show("Mensagem de duplo clique");
        }

        void Exit(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;

            Application.Exit();
        }
    }
}
