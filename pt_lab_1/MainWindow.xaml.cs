using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pt_lab_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string path = Backend.LoadDirectoryView();
            ClearTreeView();

            foreach (string s in Directory.EnumerateDirectories(path))
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = s.Substring(s.LastIndexOf('\\') + 1);
                item.Tag = s;
                item.FontWeight = FontWeights.Normal;

                foreach (string f in Directory.GetFiles(s))
                {
                    TreeViewItem tviFile = new TreeViewItem();
                    tviFile.Header = System.IO.Path.GetFileName(f);
                    tviFile.Tag = f;
                    tviFile.FontWeight = FontWeights.Normal;
                    item.Items.Add(tviFile);
                }

                FillTreeView(item, s);

                tvFiles.Items.Add(item);
            }
            //FillTreeView(item, path, firstRun: true);
        }

        private void FillTreeView(TreeViewItem parentItem, string path)
        {
            foreach (string str in Directory.EnumerateDirectories(path))
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = str.Substring(str.LastIndexOf('\\') + 1);
                item.Tag = str;
                item.FontWeight = FontWeights.Normal;
                foreach (string f in Directory.GetFiles(str))
                {
                    TreeViewItem tviFile = new TreeViewItem();
                    tviFile.Header = System.IO.Path.GetFileName(f);
                    tviFile.Tag = f;
                    tviFile.FontWeight = FontWeights.Normal;
                    item.Items.Add(tviFile);
                }
                parentItem.Items.Add(item);

                FillTreeView(item, str);
            }
        }

        private void ClearTreeView()
        {
            this.tvFiles.Items.Clear();
        }

        private void ctxMenu_ShowTxt(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = ((System.Windows.Controls.TreeViewItem)tvFiles.SelectedValue).Tag.ToString();
                if (!File.GetAttributes(path).HasFlag(FileAttributes.Directory)
                    && System.IO.Path.GetExtension(path) == ".txt")
                {
                    string fileContents;
                    using (var textReader = File.OpenText(path))
                    {
                        fileContents = textReader.ReadToEnd();
                    }
                    txtBox_Main.Text = fileContents;
                }
            }
            catch
            {
                //implement
            }
        }

        private void ctxMenu_DeleteFromTree(object sender, RoutedEventArgs e)
        {
            string path = ((System.Windows.Controls.TreeViewItem)tvFiles.SelectedValue).Tag.ToString();
            DialogResult result = System.Windows.Forms.MessageBox.Show(
                "Are you sure you want to delete the resource?","Delete resource?",System.Windows.Forms.MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
                {
                    Directory.Delete(path);
                }
                else
                {
                    File.Delete(path);
                }
            }
            tvFiles.Items.Remove(tvFiles.SelectedItem);
            tvFiles.UpdateLayout();
        }
    }
}

