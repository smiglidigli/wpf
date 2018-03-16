using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pt_lab_1
{
    class Backend
    {

        internal static string LoadDirectoryView()
        {
            var dlg = new FolderBrowserDialog() { Description = "Select directory to open" };
            dlg.ShowDialog();
            return dlg.SelectedPath;
        }


        internal static void AppendDirectoriesToTreeNode(TreeNode node, string root)
        {
            DirectoryInfo rootDir = new DirectoryInfo(root);

            foreach (DirectoryInfo subDir in rootDir.GetDirectories())
            {
                TreeNode subdirNode = new TreeNode(subDir.Name);
                AppendDirectoriesToTreeNode(subdirNode, subDir.FullName);

                foreach (FileInfo fileInfo in subDir.GetFiles())
                {
                    subdirNode.Nodes.Add(fileInfo.Name);
                }

                node.Nodes.Add(subdirNode);
            }
        }

    }
}
