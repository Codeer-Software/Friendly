using System.Windows.Forms;

namespace TestTarget
{
    public class GuiData
    {
        Control _core;
        public GuiData(Control core)
        {
            _core = core;
        }
        public GuiData(Form core)
        {
            _core = core;
        }
    }
}
