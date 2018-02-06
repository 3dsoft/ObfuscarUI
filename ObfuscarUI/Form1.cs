using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ObfuscarUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // 선택한 파일 경로를 기본 출력 경로로 사용하기 위한 변수
        string path = string.Empty;

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists("Obfuscar.Console.exe"))
            {
                PrintMessage("File Not Found - Obfuscar.Console.exe");
                MessageBox.Show("File Not Found - Obfuscar.Console.exe");
            }
        }

        // 파일 선택
        private void btnAdd_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string s in openFileDialog1.FileNames)
                {
                    listboxFiles.Items.Add(s.Trim());
                    path = Path.GetDirectoryName(s.Trim());
                }
            }
        }

        // 선택 파일 제외
        private void btnExcept_Click(object sender, EventArgs e)
        {
            if (listboxFiles.Items.Count < 1) return;

            try
            {
                int x = listboxFiles.SelectedIndex;
                if (x < 0) return;

                listboxFiles.Items.RemoveAt(x);
                listboxFiles.SelectedIndex = listboxFiles.Items.Count - 1;
            }
            catch { }
        }


        // 출력 경로 선택
        private void btnOutputPath_Click(object sender, EventArgs e)
        {
            if(path != string.Empty) folderBrowserDialog1.SelectedPath = path;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtOutputPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        // 난독화 시작
        private void btnRun_Click(object sender, EventArgs e)
        {
            if(listboxFiles.Items.Count < 1 || txtOutputPath.Text == string.Empty)
            {
                MessageBox.Show("난독화 할 파일과 출력 경로를 설정해주세요.");
                return;
            }

            PrintMessage("Start....");

            string body1 = "<?xml version='1.0'?>\r\n<Obfuscator >\r\n";

            File.WriteAllText("config.xml", body1);

            File.AppendAllText("config.xml", $"<Var name = \"OutPath\" value = \"{txtOutputPath.Text.Trim()}\" />");
            for (int i = 0; i < listboxFiles.Items.Count; i++)
            {
                File.AppendAllText("config.xml", $"<Module file = \"{listboxFiles.Items[i]}\" />\r\n");
            }
            File.AppendAllText("config.xml", "</Obfuscator >\r\n");

            try
            {
                ProcessStartInfo ps = new ProcessStartInfo();
                ps.FileName = "Obfuscar.Console.exe";
                ps.Arguments = "config.xml";
                ps.CreateNoWindow = true;
                ps.UseShellExecute = false;

                Process pro = new Process();
                pro.StartInfo = ps;
                pro.Start();

                pro.WaitForExit(10000);
                Thread.Sleep(1000);

                PrintMessage("Obfuscar Finished!");
                PrintMessage(" ");
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }                
        }

        // 안내문 출력
        void PrintMessage(string msg)
        {
            if(msg.StartsWith("-") || msg.Trim() == string.Empty) listBox1.Items.Add(msg);
            else listBox1.Items.Add(DateTime.Now.ToString("MM-dd HH:mm:ss") + " " + msg);

            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        // 모든 항목 초기화
        private void btnClear_Click(object sender, EventArgs e)
        {
            listboxFiles.Items.Clear();
            txtOutputPath.Clear();
            PrintMessage("--------------------------------------");
        }

        // 출력 경로 열기
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (txtOutputPath.Text == string.Empty) return;

            try { Process.Start(txtOutputPath.Text); } catch { }
        }
    }
}
