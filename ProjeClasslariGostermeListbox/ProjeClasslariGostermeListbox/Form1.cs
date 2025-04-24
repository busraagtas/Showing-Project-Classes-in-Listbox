using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjeClasslariGostermeListbox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();
                string rootPath = dialog.SelectedPath;
                string[] directories = Directory.GetDirectories(rootPath);

                foreach (string dir in directories)
                {
                    string folderName = Path.GetFileName(dir);
                    listBox1.Items.Add($"📁 {folderName}");

                    string[] csFiles = Directory.GetFiles(dir, "*.cs", SearchOption.TopDirectoryOnly);
                    foreach (string file in csFiles)
                    {
                        string[] lines = File.ReadAllLines(file);
                        foreach (string line in lines)
                        {
                            if (line.Trim().Contains("class "))
                            {
                                string className = GetClassName(line);
                                if (!string.IsNullOrEmpty(className))
                                {
                                    listBox1.Items.Add($"   📄 {className}");
                                }
                            }
                        }
                    }
                }
            }
        }

        private string GetClassName(string line)
        {
            Match match = Regex.Match(line, @"class\s+(\w+)");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return null;
        }
    }
}
