using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mveEngine;

namespace MetaVideoEditor
{
    public partial class UpdateForm : Form
    {
        public UpdateForm()
        {
            InitializeComponent();
            this.Text = Kernel.Instance.GetString("UpdatesUp");
            button2.Text = Kernel.Instance.GetString("CancelStr");
            button1.Text = Kernel.Instance.GetString("UpdateUp");
            this.BackColor = ColorStyle.GetColors().BackColor;
            if (Updater.AppUpdateVersion > Updater.CurrentVersion)
            {
                TreeNode node = new TreeNode();
                node.Text = "MetaVideoEditor";
                node.Checked = true;
                treeView1.Nodes.Add(node);
            }
            foreach (IPlugin plugin in PluginSourceCollection.Instance.PluginsToUpdate)
            {
                TreeNode node = new TreeNode();
                node.Text = plugin.Name;
                node.Tag = plugin;
                node.Checked = true;
                treeView1.Nodes.Add(node);
            }
        }

        private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            string name;
            string version;
            string description;
            IPlugin plugin = (IPlugin)e.Node.Tag;
            if (plugin != null)
            {
                name = plugin.Name;
                version = plugin.Version.ToString();
                description = plugin.Description;
            }
            else
            {
                name = "MetaVideoEditor";
                version = Updater.AppUpdateVersion.ToString();
                description = "";
            }
            Brush brush = Brushes.Black;
            if (e.Node.IsSelected)
            {
                if (treeView1.Focused)
                    e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.B_click2, e.Bounds);
                else
                    e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.B_on2, e.Bounds);
            }
            if (e.Node.Checked)
            {
                e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.checkbox_checked, e.Bounds.X + 2, e.Bounds.Y + 5);
            }
            else
            {
                brush = Brushes.Gray;
                e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.checkbox, e.Bounds.X + 2, e.Bounds.Y + 5);
            }
            e.Graphics.DrawString(name + " - v" + version, treeView1.Font, brush, e.Bounds.X + 20, e.Bounds.Y + 4);
            e.Graphics.DrawString(description, treeView1.Font, brush, e.Bounds.X + 25, e.Bounds.Y + 20);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<IPlugin> plugins = new List<IPlugin>();
            foreach (TreeNode node in treeView1.Nodes)
            {
                IPlugin p = (IPlugin)node.Tag;
                if (p != null && node.Checked)
                {
                    plugins.Add(p);
                }
            }
            if (plugins.Count > 0)
            {
                var form = new InstallPlugins();
                form.Owner = this.Owner;
                form.BeginInstall(plugins);
                form.ShowDialog();                
            }
            if (treeView1.Nodes[0].Text == "MetaVideoEditor" && treeView1.Nodes[0].Checked)
            {
                var form = new InstallApp();
                form.Owner = this.Owner;
                form.BeginInstall();
                form.ShowDialog();
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
