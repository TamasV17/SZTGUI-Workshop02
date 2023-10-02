using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace Workshop02
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public delegate void RestartGameHandler();/// 

    public partial class ResultWindow : Window
    {
        //public event RestartGameHandler RestartGameRequested;

        public ResultWindow()
        {
            InitializeComponent();

            if (File.Exists("name.json"))
            {
                var names = JsonConvert.DeserializeObject<Name[]>(File.ReadAllText("name.json"));
                names.ToList().ForEach(x => lbox.Items.Add(x));
            }
        }

        private void bt_ment_Click(object sender, RoutedEventArgs e)
        {
            var p = new Name(tb_name.Text);

            lbox.Items.Add(p);

            List<Name> names = new List<Name>();

            foreach (var item in lbox.Items)
            {
                names.Add(item as Name);
            }

            string json = JsonConvert.SerializeObject(names);
            File.WriteAllText("name.json", json);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            

        }

        internal class Name
        {
            private string nev;

            public Name(string nev)
            {
                this.Nev = nev;
            }
            public override string ToString()
            {
                return Nev;
            }
            public string Nev { get => nev; set => nev = value; }
        }    
        public void bt_ujgame_Click(object sender, RoutedEventArgs e)
        {
            //RestartGameRequested?.Invoke()
            //;
            ((MainWindow)this.Owner).UjJatek();

            Close();
        }
    }
}
