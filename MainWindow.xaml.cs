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
using System.Windows.Forms;
using System.Threading;

namespace WhereisMouse
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private  System.Timers.Timer timer = new System.Timers.Timer();
        private const int MAIN_WINDOW_WIDTH = 300;
        private const int MAIN_WINDOW_HEIGHT = 150;
        public MainWindow()
        {
            InitializeComponent();

            Loaded += delegate
            {

                timer.Elapsed += delegate
                {
                    try
                    {
                        Dispatcher.Invoke(new Action(delegate
                        {
                            Mouse.Capture(this);
                            Point pointToWindow = Mouse.GetPosition(this);
                            Mouse.Capture(null);
                            try
                            {
                                Point pointToScreen = PointToScreen(pointToWindow);
                                xBox.Text = $"{pointToScreen.X}";
                                yBox.Text = $"{pointToScreen.Y}";
                                if (pointToScreen.X <= MAIN_WINDOW_WIDTH && pointToScreen.Y <= MAIN_WINDOW_HEIGHT)
                                {
                                    Left = SystemParameters.WorkArea.Width - MAIN_WINDOW_WIDTH;
                                    Top = SystemParameters.WorkArea.Height - MAIN_WINDOW_HEIGHT;
                                }
                                else
                                {
                                    Left = 0;
                                    Top = 0;
                                }
                        }
                            catch(InvalidOperationException)
                    {
                        Console.WriteLine("");
                    }


                }));
                    }
                    catch (TaskCanceledException)
                    {

                    }
                

                };
                timer.Interval = 100;
                timer.Start();
            };

            Closing += delegate
            {

            };

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Stop timer");
        }

    }
}
