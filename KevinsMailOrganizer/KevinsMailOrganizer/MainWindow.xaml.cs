using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Net;
using System.Web;

namespace KevinsMailOrganizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ListBox tempList = new ListBox();
        string relativePath = AppDomain.CurrentDomain.BaseDirectory;
        

        //FUNKAR
        string[] templateArray = Directory.GetFiles(@"..\..\assets\", "*.*", SearchOption.AllDirectories);
        
        

        List<FileInfo> fileCollection = new List<FileInfo>();

        public MainWindow()
        {
            InitializeComponent();
            GetTemplate();
            SortTemplates();
            Console.WriteLine(relativePath);

        }

        public void GetTemplate()
        {
                templateArray = Directory.GetFiles(@"..\..\assets\", "*.*", SearchOption.AllDirectories);
        }

        public void SortTemplates() {

            TreeViewItem tempTreeViewItem = new TreeViewItem() { Header = "Riksbanken" };
            //Används för senare jämförelse
            string tempDirectory = "";
            
            //
            foreach (var directory in templateArray) {

                //Skapar filinfo för att kunna läsa av filen lättare
                FileInfo info = new FileInfo(directory);

                fileCollection.Add(info);
                //Kollar om det redan finns en kategori med det aktiva namnet
                if (info.Directory.Name.ToString() == tempDirectory){

                    //Itererar igenom samtliga obejkt i trädet
                    foreach (TreeViewItem objTreeViewITem in tempTreeViewItem.Items) {

                        //Kollar om vi hittar något med samma namn
                        if (objTreeViewITem.Header.ToString() == info.Directory.Name.ToString()) {

                            //Lägger till en ny fil i den strukturen
                            TreeViewItem sameDir = new TreeViewItem() { Header = info.Name };
                            objTreeViewITem.Items.Add(sameDir);
                        }
                    }

                    //Fortsätter iterationen
                    continue;

                    
                } else {

                    TreeViewItem tempFile = new TreeViewItem() { Header = info.Name.ToString() };
                    TreeViewItem tempItem = new TreeViewItem() { Header = info.Directory.Name.ToString() };
                    
                    tempItem.Items.Add(tempFile);
                    tempTreeViewItem.Items.Add(tempItem);
                    tempDirectory = info.Directory.Name.ToString();
                }

            }

            tV_template_box.Items.Add(tempTreeViewItem);


            foreach (var file in templateArray) {
                FileInfo info = new FileInfo(file);
            }
            
            
        }

        public void OpenFile(string templateURL) {

            var process = new Process();
            string fullPath = @"" + templateURL;

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = fullPath
            };

            Console.WriteLine(startInfo.FileName);
            process.StartInfo = startInfo;
            process.Start();
        }

        public void CheckFilePath(string selectedPath) {
            foreach (var savedPath in fileCollection) {
                if (savedPath.Name.ToString() == selectedPath)
                {
                    OpenFile(savedPath.FullName);
                    break;
                }
                else {
                    Console.WriteLine("Not the same file " + savedPath.FullName);
                }
            }
        }

        public void Refresh() {
            tV_template_box.Items.Clear();
            GetTemplate();
            fileCollection.Clear();
            SortTemplates();
        }

        public void OpenSelected() {
            TreeViewItem tVSelectedItem = tV_template_box.SelectedItem as TreeViewItem;
            string selectedFile = tVSelectedItem.Header.ToString();

            CheckFilePath(selectedFile);
        }

        private void btn_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenSelected();
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void btn_Help_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
