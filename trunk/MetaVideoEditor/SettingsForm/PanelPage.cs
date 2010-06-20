using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SettingsStyle
{
    [ToolboxItem(false)] // dont show up in the toolbox, this will be created by the Add TabStripPage verb on the TabPageSwitcherDesigner
    [Docking(DockingBehavior.Never)]  // dont ask about docking
    [System.ComponentModel.DesignerCategory("Code")] // dont bring up the component designer when opened
    public class PanelPage : Panel {
        public PanelPage() {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.White;
            this.Dock = DockStyle.Fill;
        }


        /// <summary>
        /// Bring this TabStripPage to the front of the switcher.
        /// </summary>
        public void Activate() {
            TabPageSwitcher panelPageSwitcher = this.Parent as TabPageSwitcher;
            if (panelPageSwitcher != null) {
                panelPageSwitcher.SelectedPanelPage = this;
            }
            
        }
    }
}
