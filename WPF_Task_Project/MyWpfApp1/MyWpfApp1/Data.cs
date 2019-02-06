using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.ObjectModel;
using System.Xml;
using System.Windows;
using System.Windows.Data;
//using System.Collections.Specialized;


namespace MyWpfApp1
{
    /// <summary>
    /// Модель данных
    /// </summary>
    public class Model 
    {
        /// <summary>
        /// График работ
        /// </summary>
        public ObservableCollection<Graph> graph;
        /// <summary>
        /// 5 уровней визуализации
        /// </summary>
        public ObservableCollection<Visual> visual;

        public Model()
        {            
            graph = new ObservableCollection<Graph>();
            visual = new ObservableCollection<Visual>();            
            LoadData();
        }
        /// <summary>
        /// Загрузка статичных XML данных
        /// </summary>
        public void LoadData()
        {
            try
            {
                
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("Model/test_task_model");
                XmlElement xRoot = xDoc.DocumentElement;
                foreach (XmlElement xnode in xRoot)
                {
                    foreach (XmlElement xnode1 in xnode)
                    {
                        if (xnode1.LocalName == "Graph")
                        {
                            Graph _graph = new Graph();
                            foreach (XmlNode childnode in xnode1.ChildNodes)
                            {
                                switch (childnode.LocalName)
                                {
                                    case "id_sublayer1":
                                        _graph.id_sublayer1 = Int32.Parse(childnode.InnerText);
                                        break;
                                    case "id_technology_card":
                                        _graph.id_technology_card = Int32.Parse(childnode.InnerText);
                                        break;
                                    case "start_date":
                                        _graph.start_date = DateTime.Parse(childnode.InnerText);
                                        break;
                                    case "end_date":
                                        _graph.end_date = DateTime.Parse(childnode.InnerText);
                                        break;
                                }
                            }
                            graph.Add(_graph);
                        }

                        if (xnode1.LocalName == "Visual")
                        {
                            Visual _visual = new Visual();
                            foreach (XmlNode childnode in xnode1.ChildNodes)
                            {
                                switch (childnode.LocalName)
                                {
                                    case "id":
                                        _visual.id = Int32.Parse(childnode.InnerText);
                                        break;
                                    case "parent_id":
                                        XmlNode attr1 = childnode.Attributes.GetNamedItem("xsi:nil");
                                        if (attr1 == null)
                                            _visual.parent_id = Int32.Parse(childnode.InnerText);
                                        break;
                                    case "name":
                                        _visual.name = childnode.InnerText;
                                        break;
                                    case "id_technology_card":
                                        XmlNode attr2 = childnode.Attributes.GetNamedItem("xsi:nil");
                                        if (attr2 == null)
                                            _visual.id_technology_card = Int32.Parse(childnode.InnerText);
                                        break;
                                    case "start_date":
                                        _visual.start_date = DateTime.Parse(childnode.InnerText);
                                        break;
                                    case "end_date":
                                        _visual.end_date = DateTime.Parse(childnode.InnerText);
                                        break;
                                }
                            }
                            _visual.Visuals = visual;
                            _visual.vvvvvvvvvv = _visual;
                            //_visual.Graphs = graph.Where(x => x.id_technology_card == _visual.id_technology_card).ToArray();
                            _visual.Graphs = new ObservableCollection<Graph>(graph.Where(x => x.id_technology_card == _visual.id_technology_card));

                            Period temp = new Period();
                            temp.mount = "Январь";
                            Graph tempgraph = _visual.Graphs.FirstOrDefault(x => x.start_date.Month ==1);
                            if (tempgraph != null)
                            {
                                temp.isGraphs = true;
                                if (_visual.p == null)
                                {
                                    _visual.p = new List<Period>();
                                    _visual.p.Add(temp);
                                }
                                else
                                {
                                    _visual.p.Add(temp);
                                }
                            }
                            


                            visual.Add(_visual);
                            
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + e.StackTrace);
            }

        }
    }

    public class Graph 
    {
        public int id_sublayer1 { get; set; } //идентификатор этажа
        public int id_technology_card { get; set; } //идентификатор технологической карты
        public DateTime start_date { get; set; } //дата начала
        public DateTime end_date { get; set; } //дата окончания
    }

    public class Visual
    {
        public int id{ get; set; } //идентификатор уровня
        public int? parent_id { get; set; } //идентификатор родителя
        public string name { get; set; } //название этапа, или технологической карты (если уровень 5)
        public int? id_technology_card { get; set; } //идентификатор технологической карты, если это уровень 5
        public DateTime start_date { get; set; } //дата начала (минимальная дата начала его детей, если это не уровень 5)
        public DateTime end_date { get; set; } //дата окончания (максимальная дата окончания его детей, если это не уровень 5)

        public virtual ObservableCollection<Visual> Visuals { get; set; }
        public virtual ObservableCollection<Graph> Graphs { get; set; }

        public Visual vvvvvvvvvv { get; set; }

        public List<Period> p { get ; set; }        
    }

    public class Period
    {
        public string mount { get; set; }
        public bool isGraphs { get; set; }
    }

}
