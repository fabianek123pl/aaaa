
namespace librus
{
    public partial class MainPage : ContentPage
    {
        private Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> gradesData =
            new Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>();

        public MainPage()
        {
            InitializeComponent();
        }

        private void DisplayGrades_Clicked(object sender, EventArgs e)
        {
            string selectedClass = ClassPicker.SelectedItem?.ToString();
            string selectedStudent = StudentPicker.SelectedItem?.ToString();

            if (gradesData.ContainsKey(selectedClass) &&
                gradesData[selectedClass].ContainsKey(selectedStudent) &&
                gradesData[selectedClass][selectedStudent].ContainsKey(SubjectPicker.SelectedItem?.ToString()))
            {
                string subject = SubjectPicker.SelectedItem?.ToString();
                string grades = string.Join("\n", gradesData[selectedClass][selectedStudent][subject]);
                GradesLabel.Text = $"Oceny dla {selectedStudent} z klasy {selectedClass} z przedmiotu {subject}:\n{grades}";
            }
            else
            {
                GradesLabel.Text = "Brak ocen dla wybranego ucznia w danej klasie i przedmiocie.";
            }
        }

        private void AddGrade_Clicked(object sender, EventArgs e)
        {
            string selectedClass = ClassPicker.SelectedItem?.ToString();
            string selectedStudent = StudentPicker.SelectedItem?.ToString();
            string selectedSubject = SubjectPicker.SelectedItem?.ToString();
            string newGrade = GradeEntry.Text;

            if (!gradesData.ContainsKey(selectedClass))
            {
                gradesData[selectedClass] = new Dictionary<string, Dictionary<string, List<string>>>();
            }

            if (!gradesData[selectedClass].ContainsKey(selectedStudent))
            {
                gradesData[selectedClass][selectedStudent] = new Dictionary<string, List<string>>();
            }

            if (!gradesData[selectedClass][selectedStudent].ContainsKey(selectedSubject))
            {
                gradesData[selectedClass][selectedStudent][selectedSubject] = new List<string>();
            }

            gradesData[selectedClass][selectedStudent][selectedSubject].Add(newGrade);

            GradeEntry.Text = "";
        }

        private void RemoveGrade_Clicked(object sender, EventArgs e)
        {
            string selectedClass = ClassPicker.SelectedItem?.ToString();
            string selectedStudent = StudentPicker.SelectedItem?.ToString();
            string selectedSubject = SubjectPicker.SelectedItem?.ToString();
            string gradeToRemove = RemoveGradeEntry.Text;

            if (gradesData.ContainsKey(selectedClass) &&
                gradesData[selectedClass].ContainsKey(selectedStudent) &&
                gradesData[selectedClass][selectedStudent].ContainsKey(selectedSubject))
            {
                gradesData[selectedClass][selectedStudent][selectedSubject].Remove(gradeToRemove);
            }

            RemoveGradeEntry.Text = "";
        }
    }
}