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

namespace Initiative_Tracker.UserControls.Output
{
    /// <summary>
    /// Interaction logic for Field.xaml
    /// </summary>
    public partial class Field : UserControl
    {
        public Field(string field, string content, bool insignificant = false)
        {
            InitializeComponent();
            fieldName.Text = field + ":";
            fieldContent.Text = content;
            if(insignificant)
            {
                fieldName.FontSize = 8;
                fieldContent.FontSize = 8;
            }
        }
    }
}
