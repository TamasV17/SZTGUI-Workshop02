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

namespace Workshop02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ResultWindow rw = new ResultWindow();

        private GameController gameController;

        string proba1 = null;
        string proba2 = null;
        public int paircounter = 0;
        List<Word> cards;
        List<Label> pair;

        public MainWindow()
        {
            InitializeComponent();
            ResultWindow asd = new ResultWindow();
            gameController = new GameController(paircounter);
            //asd.RestartGameRequested += RestartGame;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cards = File.ReadAllLines("data.txt").Select(t => new Word(t)).ToList();
            pair = new List<Label>();

            cards.ForEach(t =>
            {
                Label label = new Label();
                label.Tag = t;
                label.Background = Brushes.LightBlue;
                label.Margin = new Thickness(20);
                label.FontSize = 24;
                label.Width = this.ActualWidth / 6;
                label.Height = this.ActualHeight / 6;
                wrap.Children.Add(label);
                pair.Add(label);

                label.MouseLeftButtonDown += L_MouseLeftButtonDown;
            }
            );
           // rw.Owner = this;
        }

        private void L_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (proba1 == null && proba2 == null)
            {
                foreach (Label label in pair)
                {
                    if (label.Background.ToString() != "#FF90EE90")
                    {
                        label.Background = Brushes.LightBlue;
                        label.Foreground = Brushes.LightBlue;
                    }
                }
            }
            Label l = sender as Label;
            Word w = (Word)(sender as Label).Tag;
            l.Background = Brushes.Yellow;
            l.Foreground = Brushes.Black;
            l.Content = w.ToString();
            l.HorizontalContentAlignment = HorizontalAlignment.Center;
            l.VerticalContentAlignment = VerticalAlignment.Center;
            if (proba1 == null && proba2 == null)
            {
                proba1 = w.ToString();
            }
            else
            {
                proba2 = w.ToString();
            }
            if (proba1 != null && proba2 != null)
            {
                if (proba1 == proba2)
                {

                    foreach (Label label in pair)
                    {
                        if (label.Tag.ToString() == proba1)
                        {
                            label.Background = Brushes.LightGreen;
                            paircounter++;
                        }
                    }
                    proba1 = null;
                    proba2 = null;
                }
                else
                {
                    proba1 = null;
                    proba2 = null;
                    
                }
            }
            if (paircounter == pair.Count)
            {
                rw = new ResultWindow();
                rw.Owner = this;
                rw.ShowDialog();
                
            }         

        }

        public void UjJatek()
        {
            foreach (Label label in pair)
            {
               
                    label.Background = Brushes.LightBlue;
                    label.Foreground = Brushes.LightBlue;
                
            }
            paircounter = 0;
        }

        //private void RestartGame()
        //{
             
        //}

    }

    internal class GameController
    {
        private int foundPairs;
        private int totalPairs;

        public GameController(int totalPairs)
        {
            this.totalPairs = totalPairs;
        }

        public void FoundPair()
        {
            foundPairs++;
        }

        public bool IsGameWon()
        {
            return foundPairs == totalPairs;
        }

        public void ResetGame()
        {
            foundPairs = 0;
        }
    }

    public class Pair
    {
        private string p;

        public Pair(string p)
        {
            this.p = p;
        }

        public string P { get => p; set => p = value; }
    }

    public class Word
    {
        private string szo;

        public Word(string szo)
        {
            this.szo = szo;
        }

        public string Szo { get => szo; set => szo = value; }

        public override string ToString()
        {
            return szo;
        }
    }
}
