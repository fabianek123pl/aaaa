
namespace librus
{
    public partial class MainPage : ContentPage
    {
        private Dictionary<string, Dictionary<string, List<string>>> gradesData = new Dictionary<string, Dictionary<string, List<string>>>();

        public MainPage()
        {
            InitializeComponent();
        }

        private void DisplayGrades_Clicked(object sender, EventArgs e)
        {
            string selectedClass = ClassPicker.SelectedItem?.ToString();
            string selectedStudent = StudentPicker.SelectedItem?.ToString();

            if (gradesData.ContainsKey(selectedClass) && gradesData[selectedClass].ContainsKey(selectedStudent))
            {
                string grades = string.Join("\n", gradesData[selectedClass][selectedStudent]);
                GradesLabel.Text = $"Oceny dla {selectedStudent} z klasy {selectedClass}:\n{grades}";
            }
            else
            {
                GradesLabel.Text = "Brak ocen dla wybranego ucznia w danej klasie.";
            }
        }

        private void AddGrade_Clicked(object sender, EventArgs e)
        {
            string selectedClass = ClassPicker.SelectedItem?.ToString();
            string selectedStudent = StudentPicker.SelectedItem?.ToString();
            string newGrade = GradeEntry.Text;

            if (!gradesData.ContainsKey(selectedClass))
            {
                gradesData[selectedClass] = new Dictionary<string, List<string>>();
            }

            if (!gradesData[selectedClass].ContainsKey(selectedStudent))
            {
                gradesData[selectedClass][selectedStudent] = new List<string>();
            }

            gradesData[selectedClass][selectedStudent].Add(newGrade);

            GradeEntry.Text = "";
        }

        private void RemoveGrade_Clicked(object sender, EventArgs e)
        {
            string selectedClass = ClassPicker.SelectedItem?.ToString();
            string selectedStudent = StudentPicker.SelectedItem?.ToString();
            string gradeToRemove = RemoveGradeEntry.Text;

            if (gradesData.ContainsKey(selectedClass) && gradesData[selectedClass].ContainsKey(selectedStudent))
            {
                gradesData[selectedClass][selectedStudent].Remove(gradeToRemove);
            }

            RemoveGradeEntry.Text = "";
        }
    }
}