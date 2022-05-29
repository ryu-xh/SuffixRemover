using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
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

namespace suffixRemover
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string GetLocalizaionString(string key)
        {
            string uiString;

            ResourceManager rm = new ResourceManager("suffixRemover.Strings", Assembly.GetExecutingAssembly());

            uiString = rm.GetString(key);

            return uiString;
        }

        string ustFile;
        string filePath;

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            var args = Environment.GetCommandLineArgs();

            try
            {
                if (args.Length < 2)
                {
                    throw new ArgumentException(message: GetLocalizaionString("NoArgs"));
                }
                filePath = args[1];

                using (var sr = new StreamReader(filePath, System.Text.Encoding.GetEncoding("shift_jis")))
                {
                    ustFile = sr.ReadToEnd();

                    sr.Close();
                }

                Init();
            }
            catch (ArgumentException err)
            {
                Console.WriteLine(err.Message);

                MessageBox.Show(err.Message);

                Environment.Exit(0);
            }

        }

        Remover remover = new Remover();
        StringReader reader;
        FileStream fs;
        StreamWriter writer;

        private void Init()
        {
            
            reader = new StringReader(ustFile);
            string line;

            Case1.Text = "";
            Case2.Text = "";
            Case3.Text = "";
            Case4.Text = "";
            Case5.Text = "";

            while ((line = reader.ReadLine()) != null)
            {
                if (remover.IsLyric(line))
                {
                    string originLyric = remover.GetLyric(line);
                    string remainGroupFirstSecondLyric = remover.RemainOnlyGroupFirstSecond(line);
                    string remainGroupFirstSecondFifthLyric = remover.RemainOnlyGroupFirstSecondFifth(line);
                    string remainGrouptSecondLyric = remover.RemainOnlyGroupSecond(line);
                    string remainGrouptSecondFifthLyric = remover.RemainOnlyGroupSecondFifth(line);

                    Case1.Text = $"{Case1.Text}{originLyric} → {originLyric}\n";
                    Case2.Text = $"{Case2.Text}{originLyric} → {remainGroupFirstSecondLyric}\n";
                    Case3.Text = $"{Case3.Text}{originLyric} → {remainGroupFirstSecondFifthLyric}\n";
                    Case4.Text = $"{Case4.Text}{originLyric} → {remainGrouptSecondLyric}\n";
                    Case5.Text = $"{Case5.Text}{originLyric} → {remainGrouptSecondFifthLyric}\n";

                }
            }

        }

        private new void Open()
        {
            fs = new FileStream(filePath, FileMode.Create);
            writer = new StreamWriter(fs, Encoding.GetEncoding("shift_jis"));

            reader = new StringReader(ustFile);
        }

        private new void Close()
        {
            writer.Close();
            fs.Dispose();

            MessageBox.Show(GetLocalizaionString("ItWasApplied"));
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Open();

            string line;

            while ((line = reader.ReadLine()) != null)
            {
                writer.WriteLine(line);
            }

            Close();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Open();

            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (remover.IsLyric(line))
                {
                    string lyric = $"Lyric={remover.RemainOnlyGroupFirstSecond(line)}";

                    writer.WriteLine(lyric);
                } else
                {
                    writer.WriteLine(line);
                }
            }

            Close();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Open();

            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (remover.IsLyric(line))
                {
                    string lyric = $"Lyric={remover.RemainOnlyGroupFirstSecondFifth(line)}";

                    writer.WriteLine(lyric);
                }
                else
                {
                    writer.WriteLine(line);
                }
            }

            Close();
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            Open();

            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (remover.IsLyric(line))
                {
                    string lyric = $"Lyric={remover.RemainOnlyGroupSecond(line)}";

                    writer.WriteLine(lyric);
                }
                else
                {
                    writer.WriteLine(line);
                }
            }

            Close();
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            Open();

            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (remover.IsLyric(line))
                {
                    string lyric = $"Lyric={remover.RemainOnlyGroupSecondFifth(line)}";

                    writer.WriteLine(lyric);
                }
                else
                {
                    writer.WriteLine(line);
                }
            }

            Close();
        }
    }
}
