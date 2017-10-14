﻿using System;
using System.Collections.Generic;
using System.Linq;
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

namespace EmployeeDatabaseTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int nextEmployeeIdNum; 

        public MainWindow()
        {
            InitializeComponent();
            // TODO: auto update the next employee id number
            nextEmployeeIdNum = 3;
        }

        private void createEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            String firstName = inputFirstNameBox.Text;
            String lastName = inputLastNameBox.Text;
            SqlManage.CreateEmployee(nextEmployeeIdNum, firstName, lastName);
            nextEmployeeIdNum++;

            ClearAll();
            outputTextBlock.Text = firstName + " " + lastName + " added.";
        }

        private void disableEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            String firstName = inputFirstNameBox.Text;
            String lastName = inputLastNameBox.Text;
            SqlManage.DisableEmployee(firstName, lastName);

            ClearAll();
            outputTextBlock.Text = String.Format("Disabled {0} {1}",
                firstName, lastName);
        }

        private void updateEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            String firstName = inputFirstNameBox.Text;
            String lastName = inputLastNameBox.Text;
            try
            {
                int ID = Convert.ToInt32(inputEmployeeIdBox.Text);
                SqlManage.UpdateEmployee(ID, firstName, lastName);
                ClearAll();
                outputTextBlock.Text = String.Format("Updated employee {0}, {1} {2}",
                    ID, firstName, lastName);
            }
            catch (FormatException)
            {
                ClearAll();
                outputTextBlock.Text = "Employee id must be a number";
            }
            catch (OverflowException)
            {
                ClearAll();
                outputTextBlock.Text = "Employee id cannot be that large";
            }
        }

        private void listAllEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
            String output = SqlManage.ListAllEmployees();
            outputTextBlock.Text = output;
        }

        private void clearAllButton_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        void ClearAll()
        {
            
                List<TextBox> textBoxes = new List<TextBox>
            { inputFirstNameBox, inputLastNameBox, inputEmployeeIdBox };

                foreach (var textbox in textBoxes)
                {
                    textbox.Text = "";
                }
                outputTextBlock.Text = "";
            
        }
    }
}
