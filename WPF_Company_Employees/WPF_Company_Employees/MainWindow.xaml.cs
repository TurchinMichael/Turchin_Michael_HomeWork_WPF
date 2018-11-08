﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SqlClient;

namespace WPF_Company_Employees
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        Presenter p;

        public MainWindow()
        {
            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
            connectionString.DataSource = @"(LocalDB)\MSSQLLocalDB";
            connectionString.InitialCatalog = "HWWPF";
            connectionString.IntegratedSecurity = true;
            connectionString.Pooling = false;

            // Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="C:\Users\Графическая Станция\HWWPF.mdf";Integrated Security=True;Connect Timeout=30
            //string connectionString =  @"";
            // инициализация
            InitializeComponent();
            p = new Presenter(this);

            #region ToDo
            mainGrid.DataContext = p;           
            #endregion

            // заполнение заранее известной информацией (для открывающихся Combo_Box)
            gender_Combo.ItemsSource = Enum.GetValues(typeof(Gender));  /* в теории можно избежать привязкой*/
            position_Combo.ItemsSource = Enum.GetValues(typeof(PositionName));  /* в теории можно избежать привязкой*/
            status_Combo.ItemsSource = Enum.GetValues(typeof(Status));  /* в теории можно избежать привязкой*/
            Change_Employee_Department_Combo.ItemsSource = departments_Combo.Items;

            // events
            addDepButton.Click += delegate { p.AddDepartmentFormCall(); };
            editDepNameButton.Click += delegate { p.editDepartmentName(); };
            addButton.Click += delegate { p.AddEmployeeFormCall(); };
            deleteButton.Click += delegate { p.DeleteEmployee(); };
            applyButton.Click += delegate { p.ChangeInformation();  };
            departments_Combo.SelectionChanged += delegate { p.fillEmployeesList(); };
            employeesView.SelectionChanged += delegate { p.fillEmployeeInfo(); };
            Change_Employee_Department_Combo.SelectionChanged += delegate { p.Change_Employee_Department(); };

            // TestZone AddEmployeeInDepartment

            // (1, 50, '2018-08-11', '1993-8-21',  1, 1, '8-963-777-39-97', 1)

            try
            {
                testButton.Click += delegate
                {

                    // Full Name
                    var sqlFullName = $@"insert into FullName(First_Name, Last_Name, Patronymic) values (N'{name_Box.Text}', N'{lastName_Box.Text}', N'{patronymic_Box.Text}'";

                // надо salary закрыть изменения, и автоматически подставлять его от выбранной должности

                var sqlReq = $@"INSERT INTO Employee(Gender,[Full Name], [Employment Date], [Date Of Birth], Position, Address, [Phone Number], Status) 
VALUES ({ gender_Combo.SelectedIndex + 1}, (SELECT max(Id) FROM FullName), 
'{employmentDate_Picker.SelectedDate.Value.Year}-{employmentDate_Picker.SelectedDate.Value.Month}-{employmentDate_Picker.SelectedDate.Value.Day}', 
'{dateOfBirth_Picker.SelectedDate.Value.Year}-{dateOfBirth_Picker.SelectedDate.Value.Month}-{dateOfBirth_Picker.SelectedDate.Value.Day}', 
{position_Combo.SelectedIndex + 1}, '{phoneNumber_Box.Text}', {/* STATUS ПО ПОЛЮ*/ status_Combo.SelectedIndex})";


                    using (SqlConnection connection = new SqlConnection(connectionString.ToString()))
                    {
                        connection.Open();

                    }
                };
            }
            catch (Exception e)
            { MessageBox.Show(e.ToString()); }
        }
        
        #region IView

        public bool departments_ComboIsEditable
        {
            get => departments_Combo.IsEditable;
            set => departments_Combo.IsEditable = value;
        }
        public int Selected_Change_Employee_Department_Combo
        {
            get => Change_Employee_Department_Combo.SelectedIndex;
            set => Change_Employee_Department_Combo.SelectedIndex = value;
        }
        public int SelectedDepartment
        {
            get => departments_Combo.SelectedIndex;
            set => departments_Combo.SelectedIndex = value;
        }
        public int SelectedEmployee
        {
            get => employeesView.SelectedIndex;
            set => employeesView.SelectedIndex = value;
        }
        public int GenderEmployee
        {
            get => gender_Combo.SelectedIndex;
            set => gender_Combo.SelectedIndex = value;
        }
        public string FirstName
        {
            get => name_Box.Text;
            set => name_Box.Text = value;
        }
        public string LastName
        {
            get => lastName_Box.Text;
            set => lastName_Box.Text = value;
        }
        public string Patronymic
        {
            get => patronymic_Box.Text;
            set => patronymic_Box.Text = value;
        }
        public DateTime EmploymentDate
        {
            get => employmentDate_Picker.SelectedDate.Value;
            set => employmentDate_Picker.SelectedDate = value;
        }
        public DateTime DateOfBirth
        {
            get => dateOfBirth_Picker.SelectedDate.Value;
            set => dateOfBirth_Picker.SelectedDate = value;
        }
        public int PositionNameEnum
        {
            get => position_Combo.SelectedIndex;
            set => position_Combo.SelectedIndex = value;
        }
        public decimal Salary
        {
            get => decimal.Parse(salary_Box.Text);
            set => salary_Box.Text = value.ToString();
        }
        public string County
        {
            get => county_Box.Text;
            set => county_Box.Text = value;
        }
        public string Region
        {
            get => region_Box.Text;
            set => region_Box.Text = value;
        }
        public string City
        {
            get => city_Box.Text;
            set => city_Box.Text = value;
        }
        public string Street
        {
            get => street_Box.Text;
            set => street_Box.Text = value;
        }
        public int StreetNumber
        {
            get => int.Parse(streetNumber_Box.Text);
            set => streetNumber_Box.Text = value.ToString();
        }
        public int ApartmentNumber
        {
            get => int.Parse(apartmentNumber_Box.Text);
            set => apartmentNumber_Box.Text = value.ToString();
        }
        public string PhoneNumber
        {
            get => phoneNumber_Box.Text;
            set => phoneNumber_Box.Text = value;
        }
        public int StatusNow
        {
            get => status_Combo.SelectedIndex;
            set => status_Combo.SelectedIndex = value;
        }
        public IEnumerable<string> employeeList
        {
            get => employeesView.ItemsSource as IEnumerable<string>;
            set => employeesView.ItemsSource = value;
        }
        public IEnumerable<string> departmentList
        {
            get => departments_Combo.ItemsSource as IEnumerable<string>;
            set => departments_Combo.ItemsSource = value;
        }
        public string DepartmentComboText
        {
            get => departments_Combo.Text;
            set => departments_Combo.Text = value;
        }

        #endregion
    }
}