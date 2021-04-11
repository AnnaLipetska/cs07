using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace task3
{
    public partial class MainWindow : Window
    {
        // Расширьте возможности программы-рефлектора из предыдущего урока следующим образом:
        // 1. Добавьте возможность выбирать, какие именно члены типа должны быть показаны
        // пользователю.При этом должна быть возможность выбирать сразу несколько членов
        // типа, например, методы и свойства.
        // 2. Добавьте возможность вывода информации об атрибутах для типов и всех членов типа,
        // которые могут быть декорированы атрибутами.

        Assembly assembly = null;
        public List<SelectableText> Members { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            SetMembers();
            DataContext = this;
        }

        private void SetMembers()
        {
            Members = new List<SelectableText>();

            var memberInfos = Enum.GetValues(typeof(MemberTypes));

            for (int i = 0; i < memberInfos.Length - 1; i++)
            {
                Members.Add(new SelectableText { IsSelected = false, TheText = memberInfos.GetValue(i).ToString() });
            }

            Members.Add(new SelectableText { IsSelected = false, TheText = "Type attributes" });
            Members.Add(new SelectableText { IsSelected = false, TheText = "Member attributes" });
        }

        public string InfoText
        {
            get { return (string)GetValue(InfoTextProperty); }
            set { SetValue(InfoTextProperty, value); }
        }
        public static readonly DependencyProperty InfoTextProperty =
            DependencyProperty.Register("InfoText", typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty));

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == true)
            {

                var path = dlg.FileName;

                try
                {
                    assembly = Assembly.LoadFile(path);
                    InfoText += $"Сборка {path} успешно загружена" + Environment.NewLine + Environment.NewLine;
                }
                catch (FileNotFoundException ex)
                {
                    InfoText += ex.Message;
                }
            }
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            if (assembly == null)
            {
                MessageBox.Show("No assembly was chosen");
                return;
            }

            InfoText += GetAssemblyInfo(assembly);
            return;
        }

        private string GetAssemblyInfo(Assembly assembly)
        {
            var infoBuilder = new StringBuilder();

            infoBuilder.Append("Список всех типов в сборке: ").Append(assembly.FullName).Append(Environment.NewLine).Append(Environment.NewLine);

            var types = assembly.GetTypes();
            int typesCounter = 0;

            var selectedMembers = Members.Where(r => r.IsSelected);

            foreach (Type type in types)
            {
                typesCounter += 1;
                infoBuilder.Append(Environment.NewLine).Append("Тип ").Append(typesCounter).Append(": ").Append(type).Append(Environment.NewLine);

                if (selectedMembers.Where(r => r.TheText == "Type attributes").Any())
                {
                    var typeAttributes = type.GetCustomAttributes(false);

                    if (typeAttributes.Any())
                    {
                        infoBuilder.Append("Aтрибуты типа: ").Append(Environment.NewLine);
                        foreach (var attribute in typeAttributes)
                        {
                            infoBuilder.Append(attribute).Append(Environment.NewLine);
                        }
                    }
                }

                var members = type.GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic);

                foreach (var member in members.Where(r => selectedMembers.Select(p => p.TheText).Contains(r.Name)))
                {
                    infoBuilder.Append(member.MemberType).Append(" ").Append(member.Name).Append(Environment.NewLine);

                    if (selectedMembers.Where(r => r.TheText == "Member attributes").Any())
                    {
                        var memberAttributes = member.GetCustomAttributes(false);
                        if (memberAttributes.Any())
                        {
                            infoBuilder.Append("Атрибуты члена: ");
                            foreach (var attribute in memberAttributes)
                            {
                                infoBuilder.Append(attribute).Append(Environment.NewLine);
                            }
                        }
                    }
                }

                infoBuilder.Append(Environment.NewLine);
            }
            infoBuilder.Append(new string('-', 60)).Append(Environment.NewLine).Append(Environment.NewLine);
            return infoBuilder.ToString();
        }
    }

    public class SelectableText
    {
        public string TheText { get; set; }
        public bool IsSelected { get; set; }
    }
}
