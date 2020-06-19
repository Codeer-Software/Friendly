Friendly
========
Friendly is a library for creating integration tests.<br>
It has the ability to manipulate other processes.<br>
It is currently designed for Windows Applications (**WinForms**, **WPF**, and **Win32**).

## Friendly support .NetCore.
Friendly can also operate .NetCore WinForms and WPF apps. But please write the test code in .Net Framework. Sorry.

## Features ...
#### Invoke separate process's API.
It can invoke all methods, properties, and fields.
It's like a selenium's javascript execution.
#### DLL injection.
It can inject .net assembly. And can execute inserted methods.

## Getting Started
Install Friendly.Windows from NuGet

    PM> Install-Package Codeer.Friendly.Windows
https://www.nuget.org/packages/Codeer.Friendly.Windows/

## We win 2nd place at Microsoft MVP Showcase. Thank you!
http://blogs.msdn.com/b/mvpawardprogram/archive/2014/11/04/mvp-showcase-winners.aspx

## Simple sample
Here is some sample code to show how you can get started with Friendly.
This is a perfect ordinary Windows Application that is manipulation target.
(There is no kind of trick.)
```xaml
<Window x:Class="Target.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="MainWindow" Height="350" Width="525">
    <Grid>
        <TextBox x:Name="_textBox" Text="{Binding Path=TextData}"/>
    </Grid>
</Window>
```
```cs  
using System.ComponentModel;
using System.Windows;

namespace Target
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new VM();
        }

        string MyFunc(int value)
        {
            return value.ToString();
        }
    }

    class VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (_, __) => { };

        string _textData;
        public string TextData
        {
            get { return _textData; }
            set
            {
                _textData = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(TextData)));
            }
        }
    }
}
```
This is a test application (using VSTest):
```cs  
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Sample
{
    [TestClass]
    public class Test
    {
        WindowsAppFriend _app;

        [TestInitialize]
        public void TestInitialize()
        {
            //attach to target process!
            var path = Path.GetFullPath("../../../Target/bin/Debug/Target.exe");
            _app = new WindowsAppFriend(Process.Start(path));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Process process = Process.GetProcessById(_app.ProcessId);
            _app.Dispose();
            process.CloseMainWindow();
        }

        [TestMethod]
        public void Manipulate()
        {           
            //static method
            dynamic window = _app.Type<Application>().Current.MainWindow;

            //instance method
            string value = window.MyFunc(5);
            Assert.AreEqual("5", value);

            //instance property
            window.DataContext.TextData = "abc";

            //instance field.
            string text = window._textBox.Text;
            Assert.AreEqual("abc", text);
        }
    }
}
```
## Friendly packages.
It's a very powerful feature. With .Net knowledge, most operations are possible.<br>
However, since it is difficult to complete the test with just this, we have prepared more convenient libraries.<br>
![Libraries.jpg](Img/Libraries.jpg)

### [Infrastructure](#Friendly-Infrastructure)<br>
It call another process's api. And it injection .net dlls.<br>

### [Friendly.Windows.Grasp](https://github.com/Codeer-Software/Friendly.Windows.Grasp)<br>
The main function is to search the window. Then it provides the basics for Win32 level operation.<br>

### Control Drivers<br>
Provides operations for basic controls such as buttons and text boxes.<br>

basic<br>
[Friendly.Windows.NativeStandardControls(Win32)](https://github.com/Codeer-Software/Friendly.Windows.NativeStandardControls)<br>
[Friendly.FormsStandardControls(WinForms)](https://github.com/ShinichiIshizuka/Ong.Friendly.FormsStandardControls)<br>
[Friendly.WPFStandardControls(WPF)](https://github.com/Roommetro/Friendly.WPFStandardControls/)<br>

3rd party cotnrols<br>
[Friendly.XamControls(WPF)](https://github.com/Codeer-Software/Friendly.XamControls)<br>
[Friendly.FarPoint(WinForms)](https://github.com/Codeer-Software/Friendly.XamControls)<br>
[Friendly.C1.Win(WinForms)](https://github.com/Codeer-Software/Friendly.C1.Win)<br>

For example, you'll use these when manuplate with WPF apps.<br>
Friendly.Windows.Grasp<br>
Friendly.WPFStandardControls<br>
Friendly.NativeStandardContorls<br>

```cs  
using System.Diagnostics;
using System.IO;
using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RM.Friendly.WPFStandardControls;

namespace Scenario
{
    [TestClass]
    public class Test
    {
        WindowsAppFriend _app;

        [TestInitialize]
        public void TestInitialize()
        {
            var path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location), "../../../DemoApp/bin/Debug/DemoApp.exe"));
            var info = new ProcessStartInfo(path) { WorkingDirectory = Path.GetDirectoryName(path) };
            _app = new WindowsAppFriend(Process.Start(info));
        }

        [TestCleanup]
        public void TestCleanup() => Process.GetProcessById(_app.ProcessId).Kill();

        [TestMethod]
        public void Sample()
        {
            // get by type full name from target process's windows.
            var window = _app.WaitForIdentifyFromTypeFullName("DemoApp.Views.MainWindow");

            // get by field.
            AppVar userControl = window.Dynamic()._userControl;

            // get by binding.
            WPFDataGrid dataGrid = window.LogicalTree().ByBinding("SelectedItem.Value").Single().Dynamic();

            // edit.
            dataGrid.EmulateChangeCellText(1, 2, "abc");

            // * This sample code was written mixed, 
            //   But usually, the scenario and specific logic of screen element are separated like the page object pattern of Selenium.
        }
    }
}
```

## Friendly Infrastructure
Here, we will explain the basic functions of Friendly.
It's about another process's API call and DLL injection.

#### Attention! Match the Processor Architecture. (x86 or x64)
The target and test processes must use the same processor architectue.
If you are using VSTest, you can set this by using the Visual Studio menus as shown below.<br>
![Match the Processor Architecture](https://e1e82e8d-a-0cb309f2-s-sites.googlegroups.com/a/codeer.co.jp/english-home/test-automation/friendly-fundamental/CpuType.png?attachauth=ANoY7cprljI9CC8x0rTkRwfg5HBpKd0YCFHFC6qBaDgXmiO3vM_QgrB-0HANaGG8P4Oqw9io-zhGqJSCq9OOZC_4eZFD9sdVDJLBblfoNFznSoXhDYnyTVIS81ctl-rNBeWrMgciHQSCndb2YSFKaCOsVg_flygABUpVmTFrDqt7lZLhDG8vYrXAaRy2qzeJBD1nG5NMftXHcI0teetvoNZlwSWWUu6Lr6Y-fBXWvM49g-MrYsXiU2TPtG8XsCEHQQtu9ZdDPe0q&attredirects=0)

#### Using Statements
```cs  
using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
```
#### Connection to Execution Thread
Attach using WindowsAppFriend.
Operations can be executed on the main window thread:
```cs  
public WindowsAppFriend(Process process);
```
Operation can also be executed on a specified window thread:
```cs  
public WindowsAppFriend(IntPtr windowHandle);
```
#### Invoking Static Operations(Any OK)
```cs  
dynamic sampleForm1 = _app.Type<Application>().Current.MainWindow;

dynamic sampleForm2 = _app.Type(typeof(Application)).Current.MainWindow;

dynamic sampleForm4 = _app.Type("System.Windows.Forms.Application").Current.MainWindow;
```
#### Invokeing Instance Operations
```cs  
//method
string value = window.MyFunc(5);

//property
window.DataContext.TextData = "abc";

//field.
string text = window._textBox.Text;
```
Variables are referenced from the target process.
You can access public and private members.

#### Instantiating New Objects(Any OK)
```cs  
dynamic listBox1 = _app.Type<ListBox>()();

dynamic listBox2 = _app.Type(typeof(ListBox))();

dynamic listBox4 = _app.Type(" System.Windows.Controls.ListBox")();

dynamic list = _app.Type<List<int>>()(new int[]{1, 2, 3, 4, 5});
```

#### Rules for Arguments

You can use serializable objects and reference them in the target process.
If you use serializable objects, they will be serialized and a copy will be sent to the target process. 
```cs  
// serializable object
window.MyFunc(5);
window.DataContext.TextData = "abc";

// new instance in target process.
dynamic textBox = _app.Type<TextBox>()();

// reference to target process
window.Content.Children.Add(textBox);
```

#### Rules for Return Values
```cs  
// referenced object exists in target process' memory. 
dynamic reference = window._textBox.Text;

// when you perform a cast, it will be marshaled from the target process.
string text = reference;
```

#### Note the Casting Behavior
```cs  
// OK
string cast = (string)reference;

// OK
string substitution = reference;

// No good. Result is false.
bool isString = reference is string;

// No good. Result is null.
string textAs = reference as string;

// No good. Throws an exception.
string.IsNullOrEmpty(reference);

// OK
string.IsNullOrEmpty((string)reference);
```

#### Special Casts
IEnumerable
```cs  
foreach (var w in _app.Type<Application>().Current.Windows)
{
}
```
AppVar
```cs  
dynamic window = _app.Type<Application>().Current.MainWindow;

AppVar appVar = window;
appVar["Title"]("abc");
```
AppVar is part of the old style interface.<br>
You will need to use AppVar if you use the old interface or if you can't use the .NET framework 4.0.<br>
It will also use in the Friendly libraries interface. Please refer [here](#AppVar-and-old-interface).

Async
Friendly operations are executed synchronously.
But you can use the Async class to execute them asynchronously.
```cs  
// Async can be specified anywhere among the arguments.
var async = new Async();
window.MyFunc(async, 5);

// You can check whether it has completed.
if (async.IsCompleted)
{
    //・・・
}

// You can wait for it to complete.
async.WaitForCompletion();
```

Return Values
```cs  
// Invoke getter.
var async = new Async();

// Text will obtain its value when the operation completes.
var text = window.MyFunc(async, 5);

// When the operation finishes, the value will be available.
async.WaitForCompletion();
string textValue = (string)text;
```
#### Copy() and Null()
```cs  
Dictionary<int, string> dic = new Dictionary<int, string>();
dic.Add(1, "1");

// Object is serialized and a copy will be sent to the target process 
dynamic dicInTarget = _app.Copy(dic);
            
// Null is useful for out arguments
dynamic value = _app.Null();
dicInTarget.TryGetValue(1, value);
Assert.AreEqual("1", (string)value);
```
#### Dll injection.
```cs  
[TestMethod]
public void DllInjection()
{
    dynamic window = _app.Type<Application>().Current.MainWindow;
    dynamic textBox = window._textBox;

    //The code let tasrget process load current assembly.
    WindowsAppExpander.LoadAssembly(_app, GetType().Assembly);

    //You can use class defined in current assembly.
    dynamic observer = _app.Type<Observer>()(textBox);

    //Check change text.
    textBox.Text = "abc";
    Assert.IsTrue((bool)observer.TextChanged);
}

class Observer
{
    internal bool TextChanged { get; set; }
    internal Observer(TextBox textBox)
    {
        textBox.TextChanged += delegate { TextChanged = true; };
    }
}
```

Native dll methods.
```cs  
[TestMethod]
public void DllInjectionPInvoke()
{
    WindowsAppExpander.LoadAssembly(_app, GetType().Assembly);

    Process process = Process.GetProcessById(_app.ProcessId);
    _app.Type(GetType()).MoveWindow(process.MainWindowHandle, 0, 0, 200, 200, true);

    dynamic rectInTarget = _app.Type<RECT>()();
    _app.Type(GetType()).GetWindowRect(process.MainWindowHandle, rectInTarget);
    RECT rect = (RECT)rectInTarget;

    Assert.AreEqual(0, rect.left);
    Assert.AreEqual(0, rect.top);
    Assert.AreEqual(200, rect.right);
    Assert.AreEqual(200, rect.bottom);
}

[DllImport("User32.dll")]
static extern bool MoveWindow(IntPtr handle, int x, int y, int width, int height, bool redraw);

[DllImport("user32.dll")]
[return: MarshalAs(UnmanagedType.Bool)]
static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

[Serializable]
[StructLayout(LayoutKind.Sequential)]
internal struct RECT
{
    public int left;
    public int top;
    public int right;
    public int bottom;
}
```

## AppVar and old interface
TODO