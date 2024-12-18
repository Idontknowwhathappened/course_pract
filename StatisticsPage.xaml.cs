using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Win32;
using VeterinaryClinic.VeterinaryClinicDataSetTableAdapters;

namespace VeterinaryClinic
{
    public partial class StatisticsPage : Page
    {
        private readonly ServicesTableAdapter servicesTableAdapter = new ServicesTableAdapter();
        private readonly QueriesTableAdapter queriesTableAdapter = new QueriesTableAdapter();
        public SeriesCollection ChartSeries { get; set; }
        readonly List<Color> colors = new List<Color>
        {
            Colors.Red,
            Colors.Blue,
            Colors.Green,
            Colors.Orange,
            Colors.Purple,
            Colors.Cyan,
            Colors.Yellow
        };
        public StatisticsPage()
        {
            InitializeComponent();
            DataContext = this;
            LoadChartsData();
            LoadListData();
        }

        private void LoadChartsData()
        {
            var serviceData = servicesTableAdapter.GetServicePopularity();
            if (serviceData == null || serviceData.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для отображения диаграммы.");
                return;
            }
            var chartSeries = new SeriesCollection();

            int colorIndex = 0;

            foreach (DataRow row in serviceData.Rows)
            {
                string serviceName = row["name"].ToString();
                double usageCount = row["usage_count"] != DBNull.Value ? Convert.ToDouble(row["usage_count"]) : 0.0;

                // Пропускаем услуги с нулевым значением
                if (usageCount == 0)
                    continue;

                Color seriesColor = colors[colorIndex % colors.Count];
                colorIndex++;

                chartSeries.Add(new PieSeries
                {
                    Title = serviceName,
                    Values = new ChartValues<double> { usageCount },
                    DataLabels = true,
                    LabelPoint = point => serviceName,
                });
            }

            ServicesChart.AxisX.Clear();

            ChartSeries = chartSeries;
        }

        private void ExportServicesToCsv_Click(object sender, RoutedEventArgs e)
        {
            var serviceData = servicesTableAdapter.GetServicePopularity();

            if (serviceData == null || serviceData.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV file (*.csv)|*.csv",
                FileName = "ServicesPopularityReport.csv"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Открываем StreamWriter с кодировкой UTF-8
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.UTF8))
                    {
                        // Записываем заголовки
                        var columnNames = serviceData.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
                        writer.WriteLine(string.Join(";", columnNames));

                        // Записываем строки
                        foreach (DataRow row in serviceData.Rows)
                        {
                            var fields = row.ItemArray.Select(field => field.ToString());
                            writer.WriteLine(string.Join(";", fields));
                        }
                    }

                    MessageBox.Show("Данные успешно экспортированы в CSV.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при экспорте данных: {ex.Message}");
                }
            }
        }

        private void BackupDatabase_Click(object sender, RoutedEventArgs e)
        {
            var folderDialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
            {
                Description = "Выберите папку для сохранения резервной копии базы данных",
                UseDescriptionForTitle = true
            };

            if (folderDialog.ShowDialog() == true)
            {
                string backupFolder = folderDialog.SelectedPath;

                try
                {
                    // Вызов хранимой процедуры через TableAdapter
                    queriesTableAdapter.BackupDatabase(backupFolder);

                    MessageBox.Show($"Резервная копия успешно создана в папке: {backupFolder}",
                                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при создании резервной копии: {ex.Message}",
                                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void LoadListData()
        {
            var dataTable = servicesTableAdapter.GetServicePopularity();
            ServicesDataGrid.ItemsSource = dataTable;
        }

        private void ExportServicesToSql_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем SQL-скрипт из метода
                string sqlScript = queriesTableAdapter.GetServicesToSQL()?.ToString();

                if (string.IsNullOrWhiteSpace(sqlScript))
                {
                    MessageBox.Show("SQL-скрипт пуст или не был сгенерирован.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Открываем диалог для выбора пути сохранения
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "SQL Script (*.sql)|*.sql",
                    FileName = "ExportedServicesScript.sql"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    // Сохраняем скрипт в файл
                    File.WriteAllText(saveFileDialog.FileName, sqlScript, System.Text.Encoding.UTF8);
                    MessageBox.Show("SQL-скрипт успешно экспортирован.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте SQL-скрипта: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
