using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace HeatingProject
{
    public partial class MainForm : Form
    {
        private List<HeatingSystem> heatingSystems;
        private DataTable heatingSystemsTable;

        public MainForm()
        {
            InitializeComponent();
            LoadDataFromXML();
            InitializeHeatingSystemsTable();
            PopulateHeatingSystemsTable();
            dataGridView.DataSource = heatingSystemsTable;
        }

        private void LoadDataFromXML()
        {
            var serializer = new XmlSerializer(typeof(List<HeatingSystem>), new XmlRootAttribute("HeatingSystems"));
            using (var reader = new StreamReader("heatingsystems.xml"))
            {
                heatingSystems = (List<HeatingSystem>)serializer.Deserialize(reader);
            }
        }

        private void InitializeHeatingSystemsTable()
        {
            heatingSystemsTable = new DataTable();
            heatingSystemsTable.Columns.Add("City", typeof(string));
            heatingSystemsTable.Columns.Add("BoilerNumber", typeof(int));
            heatingSystemsTable.Columns.Add("NumberOfHeatingObjects", typeof(int));
            heatingSystemsTable.Columns.Add("StartDate", typeof(DateTime));
            heatingSystemsTable.Columns.Add("StartTemperature", typeof(double));
            heatingSystemsTable.Columns.Add("EndDate", typeof(DateTime));
            heatingSystemsTable.Columns.Add("HeatingSeasonDuration", typeof(int));
        }

        private void PopulateHeatingSystemsTable()
        {
            foreach (var hs in heatingSystems)
            {
                var row = heatingSystemsTable.NewRow();
                row["City"] = hs.City;
                row["BoilerNumber"] = hs.BoilerNumber;
                row["NumberOfHeatingObjects"] = hs.NumberOfHeatingObjects;
                row["StartDate"] = hs.StartDate;
                row["StartTemperature"] = hs.StartTemperature;
                row["EndDate"] = hs.EndDate;
                row["HeatingSeasonDuration"] = hs.GetHeatingSeasonDuration();
                heatingSystemsTable.Rows.Add(row);
            }
        }

        private void showAllButton_Click(object sender, EventArgs e)
        {
            dataGridView.DataSource = heatingSystemsTable;
        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            var filtered = heatingSystemsTable.AsEnumerable()
                .Where(row => row.Field<DateTime>("StartDate") > new DateTime(2023, 10, 15))
                .CopyToDataTable();
            dataGridView.DataSource = filtered;
        }

        private void longestSeasonButton_Click(object sender, EventArgs e)
        {
            var filtered = heatingSystemsTable.AsEnumerable()
                .Where(row => row.Field<int>("HeatingSeasonDuration") > 180)
                .CopyToDataTable();
            dataGridView.DataSource = filtered;
            MessageBox.Show($"Кількість котелень з тривалістю опалювального сезону більше 6 місяців: {filtered.Rows.Count}");
        }

        private void shortestSeasonButton_Click(object sender, EventArgs e)
        {
            var shortestSeason = heatingSystemsTable.AsEnumerable()
                .OrderBy(row => row.Field<int>("HeatingSeasonDuration"))
                .First();
            MessageBox.Show($"Найкоротший сезон у котельні №{shortestSeason["BoilerNumber"]} міста {shortestSeason["City"]} тривалістю {shortestSeason["HeatingSeasonDuration"]} днів.");
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            // Конвертуємо DataTable в List<HeatingSystem>
            var heatingSystemsList = heatingSystemsTable.AsEnumerable()
                .Select(row => new HeatingSystem
                {
                    City = row.Field<string>("City"),
                    BoilerNumber = row.Field<int>("BoilerNumber"),
                    NumberOfHeatingObjects = row.Field<int>("NumberOfHeatingObjects"),
                    StartDate = row.Field<DateTime>("StartDate"),
                    StartTemperature = row.Field<double>("StartTemperature"),
                    EndDate = row.Field<DateTime>("EndDate")
                }).ToList();

            // Сортуємо список за StartDate
            QuickSort(heatingSystemsList, 0, heatingSystemsList.Count - 1);

            // Групуємо за StartDate та зберігаємо у файли
            var groupedByStartDate = heatingSystemsList.GroupBy(hs => hs.StartDate);

            foreach (var group in groupedByStartDate)
            {
                string fileName = $"{group.Key:yyyy-MM-dd}.xml";
                var serializer = new XmlSerializer(typeof(List<HeatingSystem>));
                using (var writer = new StreamWriter(fileName))
                {
                    serializer.Serialize(writer, group.ToList());
                }
            }
            MessageBox.Show("Дані відсортовані та збережені у файли.");
        }

        private void QuickSort(List<HeatingSystem> list, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Partition(list, left, right);
                QuickSort(list, left, pivotIndex - 1);
                QuickSort(list, pivotIndex + 1, right);
            }
        }

        private int Partition(List<HeatingSystem> list, int left, int right)
        {
            DateTime pivot = list[right].StartDate;
            int low = left - 1;

            for (int j = left; j < right; j++)
            {
                if (list[j].StartDate <= pivot)
                {
                    low++;
                    Swap(list, low, j);
                }
            }
            Swap(list, low + 1, right);
            return low + 1;
        }

        private void Swap(List<HeatingSystem> list, int i, int j)
        {
            (list[j], list[i]) = (list[i], list[j]);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            using (var addForm = new AddHeatingSystemForm())
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    var newSystem = addForm.NewHeatingSystem;
                    heatingSystems.Add(newSystem);

                    var row = heatingSystemsTable.NewRow();
                    row["City"] = newSystem.City;
                    row["BoilerNumber"] = newSystem.BoilerNumber;
                    row["NumberOfHeatingObjects"] = newSystem.NumberOfHeatingObjects;
                    row["StartDate"] = newSystem.StartDate;
                    row["StartTemperature"] = newSystem.StartTemperature;
                    row["EndDate"] = newSystem.EndDate;
                    row["HeatingSeasonDuration"] = newSystem.GetHeatingSeasonDuration();
                    heatingSystemsTable.Rows.Add(row);

                    SaveDataToXML();
                }
            }
        }

        private void SaveDataToXML()
        {
            var serializer = new XmlSerializer(typeof(List<HeatingSystem>), new XmlRootAttribute("HeatingSystems"));
            using (var writer = new StreamWriter("heatingsystems.xml"))
            {
                serializer.Serialize(writer, heatingSystems);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow selectedRow in dataGridView.SelectedRows)
                {
                    int boilerNumber = (int)selectedRow.Cells["BoilerNumber"].Value;
                    var systemToRemove = heatingSystems.FirstOrDefault(hs => hs.BoilerNumber == boilerNumber);
                    if (systemToRemove != null)
                    {
                        heatingSystems.Remove(systemToRemove);
                    }

                    heatingSystemsTable.Rows.RemoveAt(selectedRow.Index);
                }
                SaveDataToXML();
            }
            else
            {
                MessageBox.Show("Виберіть рядок для видалення.");
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = tbSearch.Text.ToLower();

            // Сортуємо список за City
            heatingSystems.Sort((a, b) => string.Compare(a.City.ToLower(), b.City.ToLower(), StringComparison.Ordinal));

            // Виконуємо бінарний пошук
            int left = 0;
            int right = heatingSystems.Count - 1;
            bool found = false;

            while (left <= right)
            {
                int mid = (left + right) / 2;
                string midValue = heatingSystems[mid].City.ToLower();

                int comparison = string.Compare(midValue, searchText, StringComparison.Ordinal);
                if (comparison == 0)
                {
                    // Знайдено, виділяємо відповідні рядки у DataGridView
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        if (string.Equals(row.Cells["City"].Value.ToString(), heatingSystems[mid].City, StringComparison.OrdinalIgnoreCase))
                        {
                            row.Selected = true;
                            dataGridView.FirstDisplayedScrollingRowIndex = row.Index;
                            break;
                        }
                    }
                    found = true;
                    break;
                }
                else if (comparison < 0)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            if (!found)
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    row.Selected = false;
                }
            }
        }
    }
}
