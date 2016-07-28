using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using RandomMovie.Properties;

namespace RandomMovie.src.view
{
    public partial class MainWindow : Form
    {

        private FolderBrowserDialog fbd;
        private OpenFileDialog ofd;

        // File types to look for, folders to exclude
        private String[] extensions = new String[] { ".mkv", ".mp4", ".m4p", ".m4v", ".vob", ".avi", ".mov", ".wmv", ".mpg", ".mpeg", ".m2v" },
                         excFolder = new String[] { "behind the scenes", "deleted scenes", "interviews", "trailers" };

        public MainWindow()
        {
            InitializeComponent();
        }

        // First Browse Button
        private void button1_Click(object sender, EventArgs e)
        {
            fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath;
            }
        }

        // GO! Button
        private void button2_Click(object sender, EventArgs e)
        {
            String fileName, arguments;

            if (radioButton1.Checked)
            {
                fileName = textBox2.Text;
                arguments = "\"" + chooseFile();
                if (checkBox1.Checked)
                {
                    arguments += "\" /startpos 00:20:00";
                }
                if (checkBox2.Checked)
                {
                    arguments += " /fullscreen";
                }
            }
            else
            {
                fileName = textBox3.Text;
                arguments = "\"" + chooseFile();
                if (checkBox1.Checked)
                {
                    arguments += "\" --start-time=1200";
                }
                if (checkBox2.Checked)
                {
                    arguments += " -f";
                }
            }
            Process.Start(fileName, arguments);

        }

        // MPC-HC Path Button
        private void button3_Click(object sender, EventArgs e)
        {
            ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox2.Text = ofd.FileName;
            }
        }

        // VLC Path Button
        private void button4_Click(object sender, EventArgs e)
        {
            ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox3.Text = ofd.FileName;
            }
        }

        // Second
        private void button5_Click(object sender, EventArgs e)
        {
            fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox4.Text = fbd.SelectedPath;
            }
        }

        // Third
        private void button6_Click(object sender, EventArgs e)
        {
            fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox5.Text = fbd.SelectedPath;
            }
        }

        // Fourth
        private void button7_Click(object sender, EventArgs e)
        {
            fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox6.Text = fbd.SelectedPath;
            }
        }

        // Method for choosing the random files
        private String chooseFile()
        {
            var rand = new Random();

            // Get the files from each directory
            var files = Directory.EnumerateFiles(textBox1.Text, ".", SearchOption.AllDirectories).Where(f => extensions.Contains(Path.GetExtension(f).ToLower()) &&
                                                                                                             !excFolder.Any(d => Path.GetDirectoryName(f).ToLower().Contains(d))).ToList();
            if(textBox4.Text != "")
                files.AddRange(Directory.EnumerateFiles(textBox4.Text, ".", SearchOption.AllDirectories).Where(f => extensions.Contains(Path.GetExtension(f).ToLower()) &&
                                                                                                                    !excFolder.Any(d => Path.GetDirectoryName(f).ToLower().Contains(d))));
            if(textBox5.Text != "")
                files.AddRange(Directory.EnumerateFiles(textBox5.Text, ".", SearchOption.AllDirectories).Where(f => extensions.Contains(Path.GetExtension(f).ToLower()) &&
                                                                                                                    !excFolder.Any(d => Path.GetDirectoryName(f).ToLower().Contains(d))));
            if(textBox6.Text != "")
                files.AddRange(Directory.EnumerateFiles(textBox6.Text, ".", SearchOption.AllDirectories).Where(f => extensions.Contains(Path.GetExtension(f).ToLower()) &&
                                                                                                                    !excFolder.Any(d => Path.GetDirectoryName(f).ToLower().Contains(d))));
            // Choose a file at random
            return files.ElementAt(rand.Next(0, files.Count()));
        }

        // Settings on Close and Load
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.browse1 = textBox1.Text;
            Settings.Default.browse2 = textBox4.Text;
            Settings.Default.browse3 = textBox5.Text;
            Settings.Default.browse4 = textBox6.Text;
            Settings.Default.mpcText = textBox2.Text;
            Settings.Default.vlcText = textBox3.Text;
            if (radioButton1.Checked)
            {
                Settings.Default.defPlay = "mpc";
            }
            else
            {
                Settings.Default.defPlay = "vlc";
            }
            if (checkBox1.Checked)
            {
                Settings.Default.checkStatus1 = true;
            }
            else
            {
                Settings.Default.checkStatus1 = false;
            }
            if (checkBox2.Checked)
            {
                Settings.Default.checkStatus2 = true;
            }
            else
            {
                Settings.Default.checkStatus2 = false;
            }
            Settings.Default.Save();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            textBox1.Text = Settings.Default.browse1;
            textBox4.Text = Settings.Default.browse2;
            textBox5.Text = Settings.Default.browse3;
            textBox6.Text = Settings.Default.browse4;
            textBox2.Text = Settings.Default.mpcText;
            textBox3.Text = Settings.Default.vlcText;
            if (Settings.Default.defPlay == "mpc")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
            checkBox1.Checked = Settings.Default.checkStatus1;
            checkBox2.Checked = Settings.Default.checkStatus2;
        }
    }
}