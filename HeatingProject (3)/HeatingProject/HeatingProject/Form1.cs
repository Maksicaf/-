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
            var groupedByStartDate = heatingSystemsTable.AsEnumerable()
                .GroupBy(row => row.Field<DateTime>("StartDate"))
                .OrderBy(g => g.Key);

            foreach (var group in groupedByStartDate)
            {
                string fileName = $"{group.Key:yyyy-MM-dd}.xml";
                var serializer = new XmlSerializer(typeof(List<HeatingSystem>));
                using (var writer = new StreamWriter(fileName))
                {
                    var list = group.Select(row => new HeatingSystem
                    {
                        City = row.Field<string>("City"),
                        BoilerNumber = row.Field<int>("BoilerNumber"),
                        NumberOfHeatingObjects = row.Field<int>("NumberOfHeatingObjects"),
                        StartDate = row.Field<DateTime>("StartDate"),
                        StartTemperature = row.Field<double>("StartTemperature"),
                        EndDate = row.Field<DateTime>("EndDate")
                    }).ToList();
                    serializer.Serialize(writer, list);
                }
            }
            MessageBox.Show("Дані відсортовані та збережені у файли.");
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
    }
}
