using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DragDropTreeViewUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            fillTree1();
        }

        private void fillTree1()
        {
            treeView1.RootNodes.Clear();

            TreeViewNode node = new TreeViewNode { Content = "Root", IsExpanded = true };
            treeView1.RootNodes.Add(node);

            TreeViewNode node1 = new TreeViewNode { Content = "Level 1 - A", IsExpanded = true };
            node1.Children.Add(new TreeViewNode { Content = "Level 2 - A" });
            node1.Children.Add(new TreeViewNode { Content = "Level 2 - B" });
            node.Children.Add(node1);

            TreeViewNode node2 = new TreeViewNode { Content = "Level 1 - B", IsExpanded = true };
            node2.Children.Add(new TreeViewNode { Content = "Level 2 - C" });
            node2.Children.Add(new TreeViewNode { Content = "Level 2 - D" });
            node.Children.Add(node2);
        }

        private void treeView1_DragItemsStarting(TreeView sender, TreeViewDragItemsStartingEventArgs args)
        {
            foreach (TreeViewNode node in args.Items)
            {
                string content = node.Content.ToString();
                int depth = node.Depth;
                string parent = node.Parent.Content.ToString();

                if (depth == 0)
                {
                    // Don't allow dragging the root noot
                    args.Cancel = true;
                }
                else
                {
                    treeViewActionsTextBlock.Text += 
                        "Dragging Node '" + content + "'" + 
                        ", Depth = " + depth.ToString() + 
                        ", Parent = '" + parent + Environment.NewLine;
                }
            }
        }

        private void treeView1_DragItemsCompleted(TreeView sender, TreeViewDragItemsCompletedEventArgs args)
        {
            foreach (TreeViewNode node in args.Items)
            {
                string content = node.Content.ToString();
                int depth = node.Depth;
                string parent = node.Parent.Content.ToString();

                treeViewActionsTextBlock.Text +=
                    "Dropped Node '" + content + "'" +
                    ", Depth = " + depth.ToString() +
                    ", Parent = '" + parent + Environment.NewLine;
            }
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            fillTree1();
            treeViewActionsTextBlock.Text = string.Empty;
        }

    }
}
