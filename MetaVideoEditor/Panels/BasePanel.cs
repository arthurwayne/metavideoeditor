using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using mveEngine;

namespace CustomControls
{

    public class BasePanel : UserControl
    {

        public BasePanel()
        {
            
        }

        public virtual void RefreshPanel()
        {
            if (SelectedItem != null)
            {
                MainPanel.Visible = true;
                UpdateData();
            }
            else
                MainPanel.Visible = false;     
        }

        public virtual void UpdateData()
        {
        }

        public CustomControls.DoubleBufferPanel MainPanel;

        public Item SelectedItem
        {
            get { return Kernel.Instance.ItemCollection.SelectedItem; }
        }

        public void HasChanged()
        {
            if (!SelectedItem.HasChanged)
            {
                SelectedItem.HasChanged = true;
                Kernel.Instance.ItemCollection.RefreshNode(Kernel.Instance.ItemCollection.SelectedNode);
            }
        }

        public virtual void DisposeControls()
        {
        }

    }
}