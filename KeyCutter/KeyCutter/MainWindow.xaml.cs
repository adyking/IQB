
using KeyCutter.ServiceRefUser;

using System;
using System.Collections.Generic;
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

namespace KeyCutter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            

            try
            {
                // User user = new User();

                ServiceRefUser.UserServiceClient service = new ServiceRefUser.UserServiceClient();

                // Call the method to start listening (it should create a new user if \bacldoor post is requested)
                service.CreateUser();

                // user = service.CreateUser();
                //MessageBox.Show(user.Username + user.Password);
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message.ToString());
            }
          
        }
    }
}
