using System;
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
    public class ToolStripQueue : ToolStripDropDownButton
    {
        public ToolStripQueue()
        {
            QueueItem item = new QueueItem();
            item.Text = Kernel.Instance.GetString("CurrentActionsMW");
            this.DropDownItems.Add(item);
        }


        public void AddItem(string message)
        {
            QueueItem item = new QueueItem();
            item.Text = message;
            this.DropDownItems.Add(item);
        }

        public void RemoveItem(string message, bool success)
        {
            QueueItem item = FindByText(message);
            if (item == null) return;
            if (success) SetImg(item, global::MetaVideoEditor.Properties.Resources.valid);
            else SetImg(item, global::MetaVideoEditor.Properties.Resources.delete);
            item.Remove();
            //this.DropDownItems.Remove(item);
        }

        public void StartProcess(string message)
        {
            QueueItem item = FindByText(message);
            SetImg(item, global::MetaVideoEditor.Properties.Resources.busy);           
        }

        delegate void SetImgDelegate(QueueItem item, Bitmap img);
        void SetImg(QueueItem item, Bitmap img)
        {
            if (Parent == null) return;
            if (Parent.InvokeRequired)
            {
                Parent.Invoke(new SetImgDelegate(SetImg), new object[] { item, img });
                return;
            }
            item.Image = img;
        }

        QueueItem FindByText(string text)
        {
            foreach (QueueItem qi in this.DropDownItems)
            {
                if (qi.Text == text)
                {
                    return qi;
                }
            }
            return null;
        }
    }

    public class QueueItem : ToolStripMenuItem
    {
        public QueueItem()
        {            
        }

        System.Timers.Timer timer;

        public void Remove()
        {
            timer = new System.Timers.Timer(5000);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            RemoveItem();
            this.Dispose(true);
        }

        delegate void RemoveDelegate();
        void RemoveItem()
        {
            if (Parent != null && Parent.InvokeRequired)
            {
                Parent.Invoke(new RemoveDelegate(RemoveItem), new object[] { });
                return;
            }
            if (GetCurrentParent() != null)
                GetCurrentParent().Items.Remove(this);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Brush brush = Brushes.Black;
            
            if (!Enabled)
                brush = Brushes.Gray;
            e.Graphics.DrawString(this.Text, this.Font, brush, e.ClipRectangle.X + 34, e.ClipRectangle.Y + 2);

            if (this.Image != null)
                e.Graphics.DrawImage(this.Image, 3, 3, 16, 16);
        }

    }

  
    
}