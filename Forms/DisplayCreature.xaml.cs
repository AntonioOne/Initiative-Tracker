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
using System.Windows.Shapes;

namespace Initiative_Tracker.Forms
{
    /// <summary>
    /// Interaction logic for DisplayCreature.xaml
    /// </summary>
    public partial class DisplayCreature : Window
    {
        public Creature target;
        public DisplayCreature(Creature _target)
        {
            InitializeComponent();
            target = _target;
            if(target.name.Length > 0)
            {
                this.Title = target.name;
                UserControl uc = new Initiative_Tracker.UserControls.Output.Field("Name", target.name);
                Contents.Children.Add(uc);
            }
            if(target.type.Length > 0)
            {
                UserControl uc = new Initiative_Tracker.UserControls.Output.Field("Type", target.type, insignificant: true);
                Contents.Children.Add(uc);
            }
        }
    }
}
