using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Globalization;

namespace MyWpfApp1
{ 
    class ViemModel
    {
        private Model model= new Model();

        private ObservableCollection<Visual> _viewSourceVisual;
        public ObservableCollection<Visual> ViewSourceVisual
        {
            get
            {
                return _viewSourceVisual;
            }
            set
            {
                //model.LoadData();                
                _viewSourceVisual = model.visual;
            }
        }
        public ViemModel()
        {
            _viewSourceVisual = model.visual;
        }
    }

    class HierarchyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var node = value as  Visual;
            return node.Visuals.Where(i => i.parent_id == node.id).ToList();
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
