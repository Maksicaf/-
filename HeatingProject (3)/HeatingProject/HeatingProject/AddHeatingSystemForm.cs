using System;
using System.Windows.Forms;

namespace HeatingProject
{
    public partial class AddHeatingSystemForm : Form
    {
        public HeatingSystem NewHeatingSystem { get; private set; }

        public AddHeatingSystemForm()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            NewHeatingSystem = new HeatingSystem
            {
                City = cityTextBox.Text,
                BoilerNumber = int.Parse(boilerNumberTextBox.Text),
                NumberOfHeatingObjects = int.Parse(heatingObjectsTextBox.Text),
                StartDate = startDatePicker.Value.Date,
                StartTemperature = double.Parse(startTemperatureTextBox.Text),
                EndDate = endDatePicker.Value.Date
            };

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
