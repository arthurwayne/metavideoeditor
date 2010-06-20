using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace SettingsStyle
{

    [ToolboxItem(typeof(TabStripToolboxItem))]
    

    public partial class TabStrip : ToolStrip
    {
        Font boldFont = new Font(SystemFonts.MenuFont, FontStyle.Bold);
        private const int EXTRA_PADDING = 0;
        
        

        public TabStrip()
        {
            this.AutoSize = false;
            this.Size = new Size(200, this.Height);
            this.BackColor = Color.White;
            this.GripStyle = ToolStripGripStyle.Hidden;
            this.Dock = DockStyle.Left;
            this.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;

            this.ShowItemToolTips = false;
        }
        protected override ToolStripItem CreateDefaultItem(string text, Image image, EventHandler onClick)
        {
            return new Tab(text, image, onClick);
        }

        private Tab currentSelection;

        public Tab SelectedTab
        {
            get { return currentSelection; }
            set
            {
                if (currentSelection != value)
                {
                    currentSelection = value;

                    if (currentSelection != null)
                    {
                        PerformLayout();
                        if (currentSelection.PanelPage != null)
                        {
                            currentSelection.PanelPage.Activate();
                        }
                    }
                }

            }
        }

        protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Tab currentTab = Items[i] as Tab;
                SuspendLayout();
                if (currentTab != null)
                {
                    if (currentTab != e.ClickedItem)
                    {
                        currentTab.Checked = false;
                        currentTab.Font = this.Font;
                        currentTab.b_active = false;
                    }
                    else
                    {
                        // currentTab.Font = boldFont;
                        currentTab.b_active = true;
                    }
                }
                ResumeLayout();
            }
            SelectedTab = e.ClickedItem as Tab;

            base.OnItemClicked(e);

        }

        protected override void SetDisplayedItems()
        {
            base.SetDisplayedItems();
            for (int i = 0; i < DisplayedItems.Count; i++)
            {
                if (DisplayedItems[i] == SelectedTab)
                {
                    DisplayedItems.Add(SelectedTab);
                    break;
                }
            }
        }

        protected override Size DefaultSize
        {
            get
            {
                Size size = base.DefaultSize;
                // size.Height += EXTRA_PADDING*2;
                return size;
            }
        }
        private int tabOverlap = 0;
        [DefaultValue(10)]
        public int TabOverlap
        {
            get { return tabOverlap; }
            set
            {
                if (tabOverlap != value)
                {
                   
                    tabOverlap = value;
                    // call perform layout so we 
                    PerformLayout();
                }
            }
        }



    }
}
