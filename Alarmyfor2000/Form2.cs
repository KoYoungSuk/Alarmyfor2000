using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Alarmyfor2000
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            var appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe";
            SetIE8KeyforWebBrowserControl(appName);
        }
    private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 windows10 = new Form3();
            windows10.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 windows2000 = new Form4();
            windows2000.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 windows7 = new Form5();
            windows7.Show();
        }

        public static int GetBrowserVersion()
        {
            string regpath = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer";
            string[] iestr  = new string[] { "svcVersion", "svcUpdateVersion", "Version", "W2kVersion" };

            int maxVer = 0;
            for (int i = 0; i < iestr.Length; ++i)
            {
                object objVal = Microsoft.Win32.Registry.GetValue(regpath, iestr[i], "0");
                string strVal = System.Convert.ToString(objVal);  
                if (strVal != null)
                {
                    int iPos = strVal.IndexOf('.');
                    if (iPos > 0)
                        strVal = strVal.Substring(0, iPos);

                    int res = 0;
                    if (int.TryParse(strVal, out res))
                        maxVer = Math.Max(maxVer, res);
                } // End if (strVal != null)
            } 
            return maxVer;
        }
        private void SetIE8KeyforWebBrowserControl(string appName)
        {
            RegistryKey Regkey = null;
            try
            {
                // For 64 bit machine
                if (Environment.Is64BitOperatingSystem)
                    Regkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Wow6432Node\\Microsoft\\Internet Explorer\\MAIN\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
                else  //For 32 bit machine
                    Regkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);

                // If the path is not correct or
                // if the user haven't priviledges to access the registry
                if (Regkey == null)
                {
                    MessageBox.Show("Application Settings Failed - Address Not found");
                    return;
                }

                int nIEVer = SysUtil.GetBrowserVersion();
                if (nIEVer > 0 && Convert.ToInt32(Regkey.GetValue(sAppName)) < nIEVer)
                {
                    // Set to Last IE Version No.
                    Regkey.SetValue(sAppName, unchecked((int)nIEVer), RegistryValueKind.DWord);

                    // Check for the key after adding
                    int nFindAppkey = Convert.ToInt32(Regkey.GetValue(sAppName));

                    if (nFindAppkey == nIEVer)
                    {
                        //MessageBox.Show("Application Settings Applied Successfully");
                    }
                    else
                        MessageBox.Show("Application Settings Failed, Ref: " + nFindAppkey.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Application Settings Failed");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Close the Registry
                if (Regkey != null)
                    Regkey.Close();
            }
        }

    }

  }
