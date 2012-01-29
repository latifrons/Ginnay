using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Ginnay.Pac;
using Ginnay.Proxy;
using Ginnay.Proxy.SocketManager;
using Ginnay.ProxySpider;
using Ginnay.ProxySpider.ProxyProviders;

namespace GinnayGUI
{
	public partial class MainForm : Form
	{
		
		#region Fields (8) 

		private Instances ins;
		Dictionary<string,DataRow> proxiesRows = new Dictionary<string, DataRow>();
		DataTable proxiesTable = new DataTable();
		private ManualResetEvent proxyManagerLock = new ManualResetEvent(true);
		private bool proxyValidateConditionChanged = false;
		DataTable proxyValidateConditionTable = new DataTable();
		private bool urlPatternChanged = false;
		DataTable urlPatternTable = new DataTable();
		DataTable proxyProviderTable = new DataTable();
		private bool IEPacSet = false;

		#endregion Fields 

		#region Constructors (1) 

		public MainForm()
		{
			InitializeComponent();
			{
				proxiesTable.Columns.Add("Address");
				proxiesTable.Columns.Add("Latency", typeof(LatencyColumnValue));
				proxiesTable.Columns.Add("Location");
				dgProxyList.Columns["Latency"].ValueType = typeof(LatencyColumnValue);

				dgProxyList.DataSource = proxiesTable;
				this.dgProxyList.Sort(this.dgProxyList.Columns["Latency"], ListSortDirection.Ascending);
			}
			{
				proxyValidateConditionTable.Columns.Add("TargetURL");
				proxyValidateConditionTable.Columns.Add("Keywords");
				proxyValidateConditionTable.Columns.Add("ForbiddenKeywords");
				this.dgProxyValidateCondition.DataSource = proxyValidateConditionTable;
				this.dgProxyValidateCondition.Sort(this.dgProxyValidateCondition.Columns["TargetURL"], ListSortDirection.Ascending);
			}
			{
				urlPatternTable.Columns.Add("Enabled", typeof(bool));
				urlPatternTable.Columns.Add("NeedValidation", typeof(bool));
				urlPatternTable.Columns.Add("Classification");
				urlPatternTable.Columns.Add("URLPattern");
				urlPatternTable.Columns.Add("NecessaryKeywords");
				urlPatternTable.Columns.Add("ForbiddenKeywords");
				urlPatternTable.Columns["Enabled"].DefaultValue = true;
				urlPatternTable.Columns["NeedValidation"].DefaultValue = false;
				this.dgURLPattern.DataSource = urlPatternTable;
				this.dgURLPattern.Sort(this.dgURLPattern.Columns["Classification"], ListSortDirection.Ascending);
			}
			{
				proxyProviderTable.Columns.Add("Type");
				proxyProviderTable.Columns.Add("Enabled", typeof(bool));
				this.dgProxyProvider.DataSource = proxyProviderTable;
			}
			
		}

		#endregion Constructors 

		#region Properties (1) 

		public Instances Ins
		{
			get { return ins; }
			set
			{
				ins = value;
				FormInit();
			}
		}

		#endregion Properties 

		#region Delegates and Events (7) 

		// Delegates (7) 

		private delegate void Del1(object sender, ListenerStateChangedEventArgs args);
		private delegate void Del2(object sender, ExceptionRaisedEventArgs args);
		private delegate void Del3(object sender, DownloadCompletedEventHandlerArgs args);
		private delegate void Del4(object sender, ProgressChangedEventArgs args);
		private delegate void Del5(object sender, ProxyUsingChangedEvnetHandlerArgs args);
		private delegate void Del6(object sender, RunWorkerCompletedEventArgs args);
		private delegate void Del7(object sender, ProxyRemovedEventHandlerArgs args);

		private delegate void Del8(string s);

		#endregion Delegates and Events 

		#region Methods (42) 

		// Private Methods (42) 

		private void btnStart_Click(object sender, EventArgs e)
		{
			int port;
			//UpdatePacFile();
			if (ins.ProxyManager.GoodProxiesQueue.Count <= 10)
			{
				ins.ProxyManager.StartDownloadProxies(true);
			}
			else
			{
				ins.ProxyManager.StartValidateProxies();
			}

			if (Int32.TryParse(txtPort.Text, out port))
			{
				ins.StartListenerThread(port);
			}
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
//			ResetPAC();
			ins.StopListenerThread();
		}

		private void btnStopDownloadProxies_Click(object sender, EventArgs e)
		{
			if (proxyManagerLock.WaitOne(0))
			{
				ins.ProxyManager.StopDownloadProxies();
			}
		}

		private void btnStopValidateProxies_Click(object sender, EventArgs e)
		{
			if (proxyManagerLock.WaitOne(0))
			{
				ins.ProxyManager.StopValidateProxies();
			}
		}

		private void btnValidateProxies_Click(object sender, EventArgs e)
		{
			if (proxyManagerLock.WaitOne(0))
			{
				btnDownloadProxies.Enabled = false;
				btnStopDownloadProxies.Enabled = false;
				btnValidateProxies.Enabled = false;
				btnStopValidateProxies.Enabled = true;
				progressBarProxyManager.Value = 0;
				lblProxyManagerStatus.Text = "";
				ins.ProxyManager.StartValidateProxies();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			ins.ProxyManager.StartDownloadProxies(false);
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			SaveProxyValidations();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			//reset
			ResetProxyValidateCondition();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			SaveURLPattern();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			TryExit();
		}
		private void TryExit()
		{
			DialogResult dr = MessageBox.Show("Do you really want to shutdown Ginnay and exit?", "Ginnay",
											  MessageBoxButtons.OKCancel);
			if (dr == DialogResult.OK)
			{
//				ResetPAC();
				this.Dispose();
				Exit();
			}
		}

		private void dgProxyValidateCondition_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			proxyValidateConditionChanged = true;
		}

		private void dgProxyValidateCondition_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
		{
			proxyValidateConditionChanged = true;
		}

		private void dgURLPattern_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			urlPatternChanged = true;
		}

		private void dgURLPattern_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
		{
			urlPatternChanged = true;
		}

		private void DoDownloadCompleted(object sender, DownloadCompletedEventHandlerArgs args)
		{
			proxyManagerLock.Set();
			try
			{
				this.BeginInvoke(new Del3(UpdateWhenDownloadCompleted), new object[] { sender, args });
			}
			catch (Exception)
			{
				
			}
			
		}

		private void DoDownloadProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			try
			{
				this.progressBarProxyManager.BeginInvoke(new Del4(UpdateDownloadProgressBarProxyManager), new object[] { sender, e });
			}
			catch (Exception)
			{
				
			}
		}

		private void DoDownloadStarted(object sender, DownloadStartedEventHandlerArgs args)
		{
			if (proxyManagerLock.WaitOne(0))
			{
				try
				{
					this.BeginInvoke(new MethodInvoker(UpdateDownloadStarted));
				}
				catch (Exception)
				{
					
				}
			}
		}

		private void UpdateDownloadStarted()
		{
			btnDownloadProxies.Enabled = false;
			btnStopDownloadProxies.Enabled = true;
			btnValidateProxies.Enabled = false;
			btnStopValidateProxies.Enabled = false;
			progressBarProxyManager.Value = 0;
			lblProxyManagerStatus.Text = "";
			lock (proxiesTable)
			{
				proxiesTable.Clear();
				proxiesRows.Clear();
			}
		}

		private void DoExceptionRaised(object sender, ExceptionRaisedEventArgs e)
		{
			try{
				this.BeginInvoke(new Del2(UpdateStatus), new object[] {sender, e});
			}
			catch (Exception)
			{

			}
		}

		private void DoListenerStateChanged(object sender, ListenerStateChangedEventArgs args)
		{
			try
			{
				this.BeginInvoke(new Del1(UpdateGinnayStatus), new object[] { sender, args });
			}
			catch (Exception)
			{
				
			}
		}

		private void DoPendingProxyListChanged(object sender, PendingProxyListChangedEventHandlerArgs args)
		{
			lock (proxiesTable)
			{
				proxiesRows.Clear();
				proxiesTable.Rows.Clear();
				List<ProxyInfo> proxies = new List<ProxyInfo>(ins.ProxyManager.PendingProxies);
				foreach (ProxyInfo pi in proxies)
				{
					if (!proxiesRows.ContainsKey(pi.ProxyAddress))
					{
						DataRow dr = proxiesTable.NewRow();
						dr["Address"] = pi.ProxyAddress;
						dr["Latency"] = new LatencyColumnValue(pi.RTT);
						dr["Location"] = pi.Location;
						proxiesTable.Rows.Add(dr);
						proxiesRows[pi.ProxyAddress] = dr;
					}
				}
			}
		}

		private void DoProxyRemoved(object sender, ProxyRemovedEventHandlerArgs args)
		{
			try
			{
				this.dgProxyList.BeginInvoke(new Del7(UpdateProxyRemoved), new object[] { sender, args });
			}
			catch (Exception)
			{

			}
		}

		private void DoProxyUsingChanged(object sender, ProxyUsingChangedEvnetHandlerArgs args)
		{
			try
			{
				this.lblProxyAddress.BeginInvoke(new Del5(UpdateProxyUsingInfo), new object[] { sender, args });
			}
			catch (Exception)
			{

			}
		}

		private void DoProxyValidateConditionChanged(object sender, ProxyValidateConditionChangedEventHandlerArgs args)
		{
			ResetProxyValidateCondition();
		}

		private void DoValidateCompleted(object sender, RunWorkerCompletedEventArgs args)
		{
			proxyManagerLock.Set();
			try
			{
				this.BeginInvoke(new Del6(UpdateWhenValidateCompleted), new object[] { sender, args });
			}
			catch (Exception)
			{

			}
		}

		private void DoValidateProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			try
			{
				this.progressBarProxyManager.BeginInvoke(new Del4(UpdateValidateProgressBarProxyManager), new object[] { sender, e });
			}
			catch (Exception)
			{

			}
		}

		private void FormInit()
		{
			ins.Listener.OnListenerStateChanged += new ListenerStateChangedEventHandler(DoListenerStateChanged);
			ins.Listener.OnExceptionRaised += new ExceptionRaisedEventHandler(DoExceptionRaised);
			ins.Listener.OnNewRequest += new NewRequestEventHandler(DoNewRequest);
			ins.ProxyManager.OnDownloadCompleted += new DownloadCompletedEventHandler(DoDownloadCompleted);
			ins.ProxyManager.OnPendingProxyListChanged += new PendingProxyListChangedEventHandler(DoPendingProxyListChanged);
			ins.ProxyManager.OnValidateCompleted += new RunWorkerCompletedEventHandler(DoValidateCompleted);
			ins.ProxyManager.OnDownloadProgressChanged += new ProgressChangedEventHandler(DoDownloadProgressChanged);
			ins.ProxyManager.OnValidateProgressChanged += new ProgressChangedEventHandler(DoValidateProgressChanged);
			ins.SelectiveProxyGuide.OnProxyUsingChanged += new ProxyUsingChangedEvnetHandler(DoProxyUsingChanged);
			ins.ProxyManager.OnDownloadStarted += new DownloadStartedEventHandler(DoDownloadStarted);
			ins.ProxyManager.OnProxyRemoved += new ProxyRemovedEventHandler(DoProxyRemoved);
			ins.ProxyManager.ProxyValidator.OnProxyValidateConditionChanged += new ProxyValidateConditionChangedEventHandler(DoProxyValidateConditionChanged);
			ins.ProxyManager.OnProxyProviderListChanged += new ProxyProviderListChangedEventHandler(DoProxyProviderListChanged);
			ins.PacSetting.OnThroughMePatternsChanged += new ThroughMePatternsChangedEventHandler(DoThroughMePatternsChanged);
			{
				this.textBox1.Text = ins.PacFilePath;
			}
			//init logo

		}

		private void DoNewRequest(object sender, NewRequestEventHandlerArgs args)
		{
			Log(args.Url);
		}

		private void DoProxyProviderListChanged(object sender, ProxyProviderListChangedEventHandlerArgs args)
		{
			ResetProxyProviders();
		}

		private void ResetProxyProviders()
		{
			proxyProviderTable.Rows.Clear();
			foreach (AbstractProxyProvider pp in ins.ProxyManager.ProxyProviders)
			{
				DataRow dr = proxyProviderTable.NewRow();
				dr["Enabled"] = pp.Enabled;
				dr["Type"] = pp.ClassName;
				proxyProviderTable.Rows.Add(dr);
			}

			this.dgProxyProvider.BeginInvoke(new MethodInvoker(UpdateProxyProviderGridView));
		}

		private void UpdateProxyProviderGridView()
		{
			this.dgProxyValidateCondition.Update();
		}

		private void DoThroughMePatternsChanged(object sender, ThroughMePatternsChangedEventHandlerArgs args)
		{
			ResetURLPatterns();
		}
		private void CheckUpdate()
		{
			int ver;
			string vers;
			ins.CheckUpdate(out ver, out vers);
			if (ver > Instances.VER)
			{
				DialogResult dr = MessageBox.Show("New Version " + vers + " Available. See?","Update Available for Ginnay",MessageBoxButtons.YesNo);
				if (dr == DialogResult.Yes)
				{
					System.Diagnostics.Process.Start("https://github.com/latifrons/Ginnay/wiki");
				}
			}

		}

		private void Exit()
		{
			ins.StopListenerThread();
			ins.ProxyManager.StopDownloadProxies();
			ins.ProxyManager.StopValidateProxies();
			ins.TargetConnectionPool.StopDaemon();
			//restore IE proxy
			Application.Exit();
		}

		private void ResetProxyValidateCondition()
		{
			proxyValidateConditionTable.Rows.Clear();
			foreach (ProxyValidateCondition pvc in ins.ProxyManager.ProxyValidator.ValidateConditions)
			{
				DataRow dr = proxyValidateConditionTable.NewRow();
				dr["TargetURL"] = pvc.Url;
				dr["Keywords"] = pvc.KeywordsStr;
				dr["ForbiddenKeywords"] = pvc.ForbiddenKeywordsStr;
				proxyValidateConditionTable.Rows.Add(dr);
			}
			this.dgProxyValidateCondition.BeginInvoke(new MethodInvoker(UpdateProxyValidateConditionGridView));
		}

		private void ResetURLPatterns()
		{
			urlPatternTable.Rows.Clear();
			foreach (URLPattern up in ins.PacSetting.ThroughMePatterns)
			{
				DataRow dr = urlPatternTable.NewRow();
				dr["Classification"] = up.Classification;
				dr["URLPattern"] = up.UrlPattern;
				dr["NecessaryKeywords"] = up.NecessaryKeywordsStr;
				dr["ForbiddenKeywords"] = up.ForbiddenKeywordsStr;
				dr["Enabled"] = up.Enabled;
				dr["NeedValidation"] = up.NeedValidation;
				urlPatternTable.Rows.Add(dr);
			}
			this.dgProxyValidateCondition.BeginInvoke(new MethodInvoker(UpdateURLPatternGridView));
		}

		private void SaveProxyValidations()
		{
			List<ProxyValidateCondition> conditions = new List<ProxyValidateCondition>();
			foreach (DataRow row in proxyValidateConditionTable.Rows)
			{
				ProxyValidateCondition pvc = new ProxyValidateCondition();
				pvc.Url = row["TargetURL"].ToString();
				string[] keywords = row["Keywords"].ToString().Split(new char[] { ',', ';', '，', '；' });
				pvc.Keywords.AddRange(keywords);
				string[] forbiddenKeywords = row["ForbiddenKeywords"].ToString().Split(new char[] { ',', ';', '，', '；' });
				pvc.ForbiddenKeywords.AddRange(forbiddenKeywords);
				conditions.Add(pvc);
			}
			ins.ProxyManager.ProxyValidator.ReplaceProxyValidations(conditions);
			bool success = ins.SaveProxyValidateConditionConfig();
			ResetProxyValidateCondition();
			if (success)
			{
				MessageBox.Show("Done");
			}
			else
			{
				MessageBox.Show("Failed");
			}
		}

		private void SaveURLPattern()
		{
			List<URLPattern> patterns = new List<URLPattern>();
			foreach (DataRow row in urlPatternTable.Rows)
			{
				URLPattern p = new URLPattern(row["URLPattern"].ToString());
				string[] keywords = row["NecessaryKeywords"].ToString().Split(new char[] { ',', ';', '，', '；' });
				p.NecessaryKeywords.AddRange(keywords);

				string[] forbiddenKeywords = row["ForbiddenKeywords"].ToString().Split(new char[] { ',', ';', '，', '；' });
				p.ForbiddenKeywords.AddRange(forbiddenKeywords);
				
				p.Enabled = (bool)row["Enabled"];
				p.NeedValidation = (bool) row["NeedValidation"];
				p.Classification = row["Classification"].ToString();
				if (string.IsNullOrEmpty(p.Classification))
				{
					p.Classification = "default";
					row["Classification"] = "default";
				}
				patterns.Add(p);
			}
			ins.PacSetting.Clear();
			ins.PacSetting.LoadURLPatterns(patterns);

			bool success = ins.SaveURLPatterns();

			ResetURLPatterns();
			if (success)
			{
				MessageBox.Show("Done");
			}
			else
			{
				MessageBox.Show("Failed");
			}
		}

		private void tabControl1_Deselecting(object sender, TabControlCancelEventArgs e)
		{
			if (e.TabPage == tabPage5 && proxyValidateConditionChanged)
			{
				DialogResult dr =MessageBox.Show("Save?", "Save?", MessageBoxButtons.YesNoCancel);
				if (dr == DialogResult.Yes)
				{
					SaveProxyValidations();
					proxyValidateConditionChanged = false;
				}
				else if (dr == DialogResult.No)
				{
					ResetProxyValidateCondition();
					proxyValidateConditionChanged = false;
				}
				else if (dr == DialogResult.Cancel)
				{
					e.Cancel = true;
				}
			}
			if (e.TabPage == tabPage3 && urlPatternChanged)
			{
				DialogResult dr = MessageBox.Show("Save?", "Save?", MessageBoxButtons.YesNoCancel);
				if (dr == DialogResult.Yes)
				{
					SaveURLPattern();
					urlPatternChanged = false;
				}
				else if (dr == DialogResult.No)
				{
					ResetURLPatterns();
					urlPatternChanged = false;
				}
				else if (dr == DialogResult.Cancel)
				{
					e.Cancel = true;
				}
			}
		}

		private void UpdateDownloadProgressBarProxyManager(object sender, ProgressChangedEventArgs args)
		{
			this.progressBarProxyManager.Value = args.ProgressPercentage;
			if (args.UserState != null)
			{
				lblProxyManagerStatus.Text = args.UserState.ToString();
			}
		}

		private void UpdateGinnayStatus(object sender, ListenerStateChangedEventArgs args)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Ginnay is ");
			if (ins.Listener.Running)
			{
				sb.Append("running on port ").Append(ins.Listener.Port);
				btnStop.Enabled = true;
				btnStart.Enabled = false;
			}
			else
			{
				sb.Append("stopped");
				btnStart.Enabled = true;
				btnStop.Enabled = false;
			}
			lblStatus.Text = sb.ToString();
			Log(lblStatus.Text);
		}

		private void UpdateProxyRemoved(object sender, ProxyRemovedEventHandlerArgs args)
		{
			ProxyInfo pi = args.ProxyInfo;
			lock (proxiesTable)
			{
				DataRow dr;
				if (proxiesRows.TryGetValue(pi.ProxyAddress, out dr))
				{
					proxiesTable.Rows.Remove(dr);
					proxiesRows.Remove(pi.ProxyAddress);
				}
			}
		}

		private void UpdateProxyUsingInfo(object sender, ProxyUsingChangedEvnetHandlerArgs args)
		{
			ProxyInfo pi = ins.SelectiveProxyGuide.CurrentProxyInfo;
			if (pi != null)
			{
				this.lblProxyAddress.Text = pi.ProxyAddress;
				this.lblProxyLatency.Text = "(" + pi.RTT + " ms)";
				this.lblProxySource.Text = pi.Location;
				double per = (double)pi.RTT / 2000;
				if (per > 1)
				{
					per = 1;
				}
				Color c = Color.FromArgb((int)(0xff * per),
										(int)(0xff * (1.0 - per)), 0);
				this.lblProxyColor.BackColor = c;

				this.lblProxyStatusWorkingProxy.Text = pi.ProxyAddress;
				Log("Using Proxy: " + this.lblProxyAddress.Text);
			}
			else
			{
				this.lblProxyAddress.Text = "No Proxy Working";
				this.lblProxyStatusWorkingProxy.Text = "No Proxy Working";
				this.lblProxyLatency.Text = "";
				this.lblProxySource.Text = "";
				this.lblProxyColor.BackColor = Color.Red;
				Log(this.lblProxyAddress.Text);
			}
		}

		private void UpdateProxyValidateConditionGridView()
		{
			this.dgProxyValidateCondition.Update();
			proxyValidateConditionChanged = false;
		}

		private void UpdateStatus(object sender, ExceptionRaisedEventArgs e)
		{
			lblStatus.Text = e.Explaination;
			Log(e.Explaination);
		}

		private void UpdateURLPatternGridView()
		{
			this.dgURLPattern.Update();
			urlPatternChanged = false;
		}

		private void UpdateValidateProgressBarProxyManager(object sender, ProgressChangedEventArgs args)
		{
			this.progressBarProxyManager.Value = args.ProgressPercentage;
			
			ProxyInfo pi = args.UserState as ProxyInfo;
			lock (proxiesTable)
			{
				if (pi != null && pi.RTT < 0)
				{
					
					DataRow dr;
					if (proxiesRows.TryGetValue(pi.ProxyAddress,out dr))
					{
						proxiesTable.Rows.Remove(dr);
						proxiesRows.Remove(pi.ProxyAddress);
					}
					
				}
				else if (pi != null)
				{
					DataRow dr;
					if (proxiesRows.TryGetValue(pi.ProxyAddress,out dr))
					{
						if (dr != null)
						{
							dr["Latency"] = new LatencyColumnValue(pi.RTT);
						}
					}
				}
			}
			if (dgProxyList.Rows.Count > 0)
			{
				dgProxyList.FirstDisplayedScrollingRowIndex = 0;
			}
		}

		private void UpdateWhenDownloadCompleted(object sender, DownloadCompletedEventHandlerArgs args)
		{
			btnDownloadProxies.Enabled = true;
			btnStopDownloadProxies.Enabled = true;
			btnValidateProxies.Enabled = true;
			btnStopValidateProxies.Enabled = true;
		
			
			lblProxyManagerStatus.Text = args.Count + " proxies downloaded successfully";
			Log(lblProxyManagerStatus.Text);
			
		}

		private void UpdateWhenValidateCompleted(object sender, RunWorkerCompletedEventArgs args)
		{
			btnDownloadProxies.Enabled = true;
			btnStopDownloadProxies.Enabled = true;
			btnValidateProxies.Enabled = true;
			btnStopValidateProxies.Enabled = true;
			lblProxyManagerStatus.Text = "Proxies validate finished";
			Log(lblProxyManagerStatus.Text);
		}

		#endregion Methods 

		private void btnSetIEPac_Click(object sender, EventArgs e)
		{
			SetPAC();
		}

		private void SetPAC()
		{
			DialogResult dr = MessageBox.Show("You should ONLY use this feature in LOCAL NETWORK SETTINGS.\r\nDON'T press Yes unless you know what you are doing.\r\nContinue?", "Experiment feature",
			                MessageBoxButtons.YesNo);
			if (dr == DialogResult.No)
			{
				return;
			}
			lock (ins.PacSetting)
			{
				if (!IEPacSet)
				{
					bool success = ins.SetIEPac();
					if (success)
					{
						IEPacSet = true;
						Log("IE PAC successfully set.");
						btnSetIEPac.Text = "Restore IE PAC";
					}
					else
					{
						Log("IE PAC failed to be set.");
					}
				}
				else
				{
					bool success = ins.CancelIEPac();
					if (success)
					{
						IEPacSet = false;
						Log("IE PAC successfully restored.");
						btnSetIEPac.Text = "Set IE PAC";
					}
					else
					{
						Log("IE PAC failed to be restored.");
					}
				}
				setIEPACToolStripMenuItem.Checked = IEPacSet;
			}
		}

		private void Log(string message)
		{
			if (txtLog.InvokeRequired)
			{
				txtLog.Invoke(new Del8(Log), new object[] { message });
			}
			else
			{
				txtLog.AppendText(DateTime.Now.ToString("HH:mm:ss "));
				txtLog.AppendText(message);
				txtLog.AppendText(Environment.NewLine);
				txtLog.SelectionStart = txtLog.Text.Length;
				txtLog.ScrollToCaret();
			}
		}

		private void setIEPACToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetPAC();
		}

		private void btnGenerate_Click(object sender, EventArgs e)
		{
			UpdatePacFile();
		}

		private void UpdatePacFile()
		{
			lock (ins.PacSetting)
			{
				bool success = ins.UpdateIEPac();
				if (success)
				{
					Log("IE PAC successfully generated.");
				}
			}
		}

		private void generatePACFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UpdatePacFile();
		}

		private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.Visible = !this.Visible;
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			this.Visible = false;
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TryExit();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutForm af = new AboutForm();
			af.Show();
		}

		private void button6_Click(object sender, EventArgs e)
		{
			ins.SelectiveProxyGuide.ChangeProxy(ProxyChangePolicy.DELETE);
		}

		private void contentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start("http://ginnay.codeplex.com");
			}catch (Exception)
			{
				MessageBox.Show("You do not have a browser????");
			}
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			Thread t = new Thread(new ThreadStart(CheckUpdate));
			t.Start();
		}

		private void button7_Click(object sender, EventArgs e)
		{
			ins.SelectiveProxyGuide.ChangeProxy(ProxyChangePolicy.DELETE);
		}

		private void button8_Click(object sender, EventArgs e)
		{
			Process.Start(
				@"mailto:latifrons88@gmail.com?subject=Help me visit this website&body=Website: [Youku]<br />Sample page: [http://v.youku.com/v_show/id_XMzMzMTUzMzMy.html]<br /> Comments: [Your comments here.]<br />");
		}
	}
	class LatencyColumnValue:IComparable
	{
		#region Fields (1) 

		private int rtt;

		#endregion Fields 

		#region Constructors (1) 

		public LatencyColumnValue(int rtt)
		{
			this.rtt = rtt;
		}

		#endregion Constructors 

		#region Properties (1) 

		public int RTT
		{
			get { return rtt; }
			set { rtt = value; }
		}

		#endregion Properties 

		#region Methods (2) 

		// Public Methods (2) 

		public int CompareTo(object other)
		{
			LatencyColumnValue o = other as LatencyColumnValue;
			// 1 < 2 < 3 < -1 < 0
			if ((rtt > 0 && o.rtt <= 0) || (rtt <= 0 && o.rtt > 0))
			{
				return o.rtt - rtt;
			}
			else
			{
				return rtt - o.rtt;
			}
		}

		public override string ToString()
		{
			if (rtt == 0)
			{
				return "N/A";
			}
			else if (rtt == -1)
			{
				return "Fail";
			}
			else
			{
				return rtt.ToString();
			}
			
		}

		#endregion Methods 
	}
}