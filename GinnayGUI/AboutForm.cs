using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GinnayGUI
{
	public partial class AboutForm : Form
	{
		public AboutForm()
		{
			InitializeComponent();
			this.label1.Text = "Ginnay " + Instances.VERS + " Build " + Instances.VER;
		}
	}
}
