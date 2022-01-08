using System.Windows;
using System.Windows.Controls;
using Desktop_App.Repository;
using Desktop_App.Models;
using System.Collections.Generic;

namespace Desktop_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ScheduleRepository scheduleRepo;
        private Database database = new Database();

        public MainWindow()
        {
            this.scheduleRepo = new ScheduleRepository(this.database);
            InitializeComponent();
            this.populateList();
        }

        public void populateList ()
        {
            this.listView.Items.Clear();
            foreach (Schedule schedule in this.scheduleRepo.FindAll() )
            {
                this.listView.Items.Add(schedule);
            }
        }

        private void listView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (this.listView.SelectedItems.Count == 1)
            {
                var row = (Schedule) this.listView.SelectedItems[0];
                if (this.datePicker.SelectedDate != null)
                {
                    if (this.scheduleRepo.updateDate(this.datePicker.SelectedDate.Value, row.Id))
                    {
                        this.status.Text = "Succes!";
                        this.populateList();
                    }
                    else
                    {
                        this.status.Text = "Error!";
                    }
                }
                else
                {
                    this.status.Text = "Selecteaza o data pentru a edita!";
                }
            }
            else
            {
                this.status.Text = "Selecteaza doar un rand din tabel pentru a il edita!";
            }
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.listView.SelectedItems.Count == 1)
            {
                var row = (Schedule)this.listView.SelectedItems[0];
                if (this.scheduleRepo.deleteDate(row.Id))
                {
                    this.status.Text = "Succes!";
                    this.populateList();
                }
                else
                {
                    this.status.Text = "Error!";
                }
            }
            else
            {
                this.status.Text = "Selecteaza doar un rand din tabel pentru a il edita!";
            }
        }
    }
}
