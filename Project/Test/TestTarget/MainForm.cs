using System.Collections.Generic;
using System.Windows.Forms;

namespace TestTarget
{
    public partial class MainForm : Form
    {
        public int _field;

        public MainForm()
        {
            InitializeComponent();
        }

        public int InstanceMethod(string value)
        {
            return int.Parse(value);
        }

        public static int StaticMethod(string value)
        {
            return int.Parse(value);
        }

        public static int StaticProperty { get; set; }

        public static int _staticField;

        Dictionary<string, Dictionary<string, int>> _dic = new Dictionary<string, Dictionary<string, int>>();

        int this[string key1, string key2]
        {
            get
            {
                return _dic[key1][key2];
            } 
            set
            {
                Dictionary<string, int> core;
                if (!_dic.TryGetValue(key1, out core))
                {
                    core = new Dictionary<string, int>();
                    _dic.Add(key1, core);
                }
                core.Add(key2, value);
            }
        }

        public string OverLoadMethod(Control c)
        {
            return typeof(Control).FullName;
        }

        public string OverLoadMethod(Form c)
        {
            return typeof(Form).FullName;
        }


        public static string OverLoadMethodStatic(Control c)
        {
            return typeof(Control).FullName;
        }

        public static string OverLoadMethodStatic(Form c)
        {
            return typeof(Form).FullName;
        }
    }
}
