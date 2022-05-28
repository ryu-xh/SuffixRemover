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

        string ustFile;
        string filePath;

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            var args = Environment.GetCommandLineArgs();

            try
            {
                if (args.Length < 2)
                {
                    throw new ArgumentException("읽을 수 있는 파일이 없었습니다.");
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
                    string remainGroupFirstSecondFourthLyric = remover.RemainOnlyGroupFirstSecondFourth(line);
                    string remainGrouptSecondLyric = remover.RemainOnlyGroupSecond(line);
                    string remainGrouptSecondFourthLyric = remover.RemainOnlyGroupSecondFourth(line);

                    Case1.Text = $"{Case1.Text}{originLyric} -> {originLyric}\n";
                    Case2.Text = $"{Case2.Text}{originLyric} -> {remainGroupFirstSecondLyric}\n";
                    Case3.Text = $"{Case3.Text}{originLyric} -> {remainGroupFirstSecondFourthLyric}\n";
                    Case4.Text = $"{Case4.Text}{originLyric} -> {remainGrouptSecondLyric}\n";
                    Case5.Text = $"{Case5.Text}{originLyric} -> {remainGrouptSecondFourthLyric}\n";

                }
            }

        }

        private void Close()
        {
            writer.Close();
            fs.Dispose();

            MessageBox.Show("적용 완료");
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            fs = new FileStream(filePath, FileMode.Create);
            writer = new StreamWriter(fs, Encoding.GetEncoding("shift_jis"));

            reader = new StringReader(ustFile);

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
            fs = new FileStream(filePath, FileMode.Create);
            writer = new StreamWriter(fs, Encoding.GetEncoding("shift_jis"));

            reader = new StringReader(ustFile);

            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (remover.IsLyric(line))
                {
                    string lyric = $"Lyric={remover.RemainOnlyGroupFirstSecondFourth(line)}";

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
            fs = new FileStream(filePath, FileMode.Create);
            writer = new StreamWriter(fs, Encoding.GetEncoding("shift_jis"));

            reader = new StringReader(ustFile);

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
            fs = new FileStream(filePath, FileMode.Create);
            writer = new StreamWriter(fs, Encoding.GetEncoding("shift_jis"));

            reader = new StringReader(ustFile);

            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (remover.IsLyric(line))
                {
                    string lyric = $"Lyric={remover.RemainOnlyGroupSecondFourth(line)}";

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
