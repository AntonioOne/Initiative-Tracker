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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;

namespace Initiative_Tracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public static ObservableCollection<Creature> InitiativeList;
        static int ind = 0;
        public MainWindow()
        {
            InitializeComponent();
            
            InitiativeList = new ObservableCollection<Creature>();
            //
            InitiativeList.Add(new Creature() { turn = true});
            InitiativeList.Add(new Creature() { HP = 15 });
            InitiativeList.Add(new Creature() { tempHP = 10, type="some type"});
            dgInitiative.ItemsSource = InitiativeList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InitiativeList.Add(new Creature() { HP = ind});
            ind++;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedCreatures = dgInitiative.SelectedItems.Cast<object>().ToList();
            foreach (Creature o in selectedCreatures)
            {
                InitiativeList.Remove(o);
            }
        }

        private void Damage_Click(object sender, RoutedEventArgs e)
        {
            foreach (Creature o in dgInitiative.SelectedItems)
            {
                o.DealDamage(new Damage() { damageType = DamageType.Raw, damage = 10 });
            }
        }

        private void Display_Click(object sender, RoutedEventArgs e)
        {
            var selectedCreatures = dgInitiative.SelectedItems.Cast<object>().ToList();
            if (selectedCreatures.Count > 0) {
                Forms.DisplayCreature dc = new Forms.DisplayCreature(((Creature)selectedCreatures[0]));
                dc.Show();
            }
        }
    }


    
}
