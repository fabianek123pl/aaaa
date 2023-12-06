
namespace librus
{
    public partial class MainPage : ContentPage
    {
        private Dictionary<string, Dictionary<string, Dictionary<string, List<int>>>> gradesData =
            new Dictionary<string, Dictionary<string, Dictionary<string, List<int>>>>();

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
                List<int> grades = gradesData[selectedClass][selectedStudent][subject];
                string gradesString = string.Join("\n", grades.Select(g => g.ToString()));
                GradesLabel.Text = $"Oceny dla {selectedStudent} z klasy {selectedClass} z przedmiotu {subject}:\n{gradesString}";
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
            string newGradeText = GradeEntry.Text;

            if (int.TryParse(newGradeText, out int newGrade) && newGrade >= 1 && newGrade <= 6)
            {
                if (!gradesData.ContainsKey(selectedClass))
                {
                    gradesData[selectedClass] = new Dictionary<string, Dictionary<string, List<int>>>();
                }

                if (!gradesData[selectedClass].ContainsKey(selectedStudent))
                {
                    gradesData[selectedClass][selectedStudent] = new Dictionary<string, List<int>>();
                }

                if (!gradesData[selectedClass][selectedStudent].ContainsKey(selectedSubject))
                {
                    gradesData[selectedClass][selectedStudent][selectedSubject] = new List<int>();
                }

                gradesData[selectedClass][selectedStudent][selectedSubject].Add(newGrade);

                GradeEntry.Text = "";
            }
            else
            {
                DisplayAlert("Błąd", "Ocena musi być liczbą całkowitą od 1 do 6.", "OK");
            }
        }

        private void RemoveGrade_Clicked(object sender, EventArgs e)
        {
            string selectedClass = ClassPicker.SelectedItem?.ToString();
            string selectedStudent = StudentPicker.SelectedItem?.ToString();
            string selectedSubject = SubjectPicker.SelectedItem?.ToString();
            string gradeToRemoveText = RemoveGradeEntry.Text;

            if (int.TryParse(gradeToRemoveText, out int gradeToRemove) && gradeToRemove >= 1 && gradeToRemove <= 6)
            {
                if (gradesData.ContainsKey(selectedClass) &&
                    gradesData[selectedClass].ContainsKey(selectedStudent) &&
                    gradesData[selectedClass][selectedStudent].ContainsKey(selectedSubject))
                {
                    gradesData[selectedClass][selectedStudent][selectedSubject].Remove(gradeToRemove);
                }

                RemoveGradeEntry.Text = "";
            }
            else
            {
                DisplayAlert("Błąd", "Ocena do usunięcia musi być liczbą całkowitą od 1 do 6.", "OK");
            }
        }
    }
}