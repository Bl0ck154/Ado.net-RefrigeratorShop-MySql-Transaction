using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Ado.net___HW_4___MySql_Winforms
{
	public static class MyTransaction
	{
		public static void EnlistTransaction(this MySqlDataAdapter dataAdapter, MySqlTransaction transaction)
		{
			MySqlCommandBuilder mySqlCommandBuilder = new MySqlCommandBuilder(dataAdapter);
			dataAdapter.UpdateCommand.Transaction = transaction;
			dataAdapter.InsertCommand.Transaction = transaction;
			dataAdapter.DeleteCommand.Transaction = transaction;
		}
	}
	public partial class Form1 : Form
	{
		MySqlConnection sqlConnection;
		DataSet dataSet;
		MySqlDataAdapter dataAdapter;
		public Form1()
		{
			InitializeComponent();

			MySqlConnectionStringBuilder sqlConnectionStringBuilder = new MySqlConnectionStringBuilder();
			sqlConnectionStringBuilder.Server = "91.217.9.155";
			sqlConnectionStringBuilder.Database = "krivbass_dz";
			sqlConnectionStringBuilder.UserID = "krivbass_dz";
			sqlConnectionStringBuilder.Password = "itstep";
			sqlConnectionStringBuilder.SslMode = MySqlSslMode.None;


			sqlConnection = new MySqlConnection(sqlConnectionStringBuilder.ConnectionString);

			Load += Form1_Load;
		}
		/*
Разработать приложение для магазина холодильников.
Хранить товар, продавцов и покупателей.
Когда покупатель приобретает товар, оформляется чек. 
Чек содержит дату, ФИО покупателя, ФИО продавца и купленные позиции. 
Процесс покупки хлодильника осущиствить через механизм транзакцию 
(Вставка строки в таблицу для хранения чеков и Снятие с остатков у покупателя и изменение количества купленных товаров у покупателя)*/
		private void Form1_Load(object sender, EventArgs e)
		{
			string[] createCommands = { @"CREATE TABLE IF NOT EXISTS Buyers (
								id INT not null AUTO_INCREMENT,
								name varchar(100) not null,
								boughtCount int null default 0,
								PRIMARY KEY(id))",
				@"CREATE TABLE IF NOT EXISTS Sellers (
								id INT not null  AUTO_INCREMENT,
								name varchar(100) not null,
								soldCount int null default 0,
								PRIMARY KEY(id))",
				@"CREATE TABLE IF NOT EXISTS Goods (
								id INT not null  AUTO_INCREMENT,
								name varchar(100) not null,
								price INT not null,
								count INT not null,
								PRIMARY KEY(id))",
				@"CREATE TABLE IF NOT EXISTS Orders (
								id INT not null  AUTO_INCREMENT,
								date DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
								buyerName varchar(100) not null,
								sellerName varchar(100) not null,
								good_id INT not null,
								count INT not null,
								amount INT not null,
								PRIMARY KEY(id))" };
			try
			{
				sqlConnection.Open();

				foreach (var item in createCommands)
				{
					try
					{
						new MySqlCommand(item, sqlConnection).ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "1");
					}
				}
				try
				{
					new MySqlCommand(@"insert into Buyers VALUES (1, 'Kostya', 0), (2, 'Olena', 0), (3, 'Frederich', 0)",
						sqlConnection).ExecuteNonQuery();
					new MySqlCommand(@"insert into Sellers VALUES (1, 'Vasiliy', 0), (2, 'Konstantin', 0), (3, 'Marina', 0)",
						sqlConnection).ExecuteNonQuery();
					new MySqlCommand(@"insert into Goods VALUES (1, 'Nord', 5000, 400), (2, 'LG', 9000, 250), (3, 'Apsheron', 800, 10), (4, 'Vesna', 600, 50)",
						sqlConnection).ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "2");
				}


				dataSet = new DataSet();
				dataAdapter = new MySqlDataAdapter("select * from Buyers", sqlConnection);
				MySqlCommandBuilder mySqlCommandBuilder = new MySqlCommandBuilder(dataAdapter);
				dataAdapter.Fill(dataSet, "Buyers");
				dataGridView1.DataSource = dataSet.Tables[0];
				dataAdapter.SelectCommand = new MySqlCommand("select * from Sellers", sqlConnection);
				dataAdapter.Fill(dataSet, "Sellers");
				dataAdapter.SelectCommand = new MySqlCommand("select * from Goods", sqlConnection);
				dataAdapter.Fill(dataSet, "Goods");
				dataAdapter.SelectCommand = new MySqlCommand("select * from Orders", sqlConnection);
				dataAdapter.Fill(dataSet, "Orders");

				foreach (DataTable table in dataSet.Tables)
				{
					comboBoxTables.Items.Add(table.TableName);
				}
				comboBoxTables.SelectedIndex = 0;

				foreach (DataRow item in dataSet.Tables["Buyers"].Rows)
				{
					comboBoxBuyer.Items.Add(item.ItemArray[1].ToString());
				}
				comboBoxBuyer.SelectedIndex = 0;

				foreach (DataRow item in dataSet.Tables["Sellers"].Rows)
				{
					comboBoxSeller.Items.Add(item.ItemArray[1].ToString());
				}
				comboBoxSeller.SelectedIndex = 0;

				foreach (DataRow item in dataSet.Tables["Goods"].Rows)
				{
					comboBoxProducts.Items.Add(item.ItemArray[1].ToString() + " - ₴" +
						item.ItemArray[2].ToString());
				}
				comboBoxProducts.SelectedIndex = 0;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "3");
			}
			finally
			{
				sqlConnection?.Clone();
			}
		}

		private void comboBoxTables_SelectedIndexChanged(object sender, EventArgs e)
		{
			dataGridView1.DataSource = dataSet.Tables[comboBoxTables.SelectedIndex];
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				using (MySqlTransaction mySqlTransaction = sqlConnection.BeginTransaction())
				{
					dataAdapter.EnlistTransaction(mySqlTransaction);

					int count = 1;
					DataRow dr = dataSet.Tables["Orders"].NewRow();
					dr[0] = dataSet.Tables["Orders"].Rows.Count;
					dr[1] = DateTime.Now;
					dr[2] = dataSet.Tables["Buyers"].Rows[comboBoxBuyer.SelectedIndex][1].ToString();
					dr[3] = dataSet.Tables["Sellers"].Rows[comboBoxSeller.SelectedIndex][1].ToString();
					dr[4] = comboBoxProducts.SelectedIndex;
					dr[5] = count;
					dr[6] = dataSet.Tables["Goods"].Rows[comboBoxProducts.SelectedIndex][2];

					dr = dataSet.Tables["Buyers"].Rows[comboBoxBuyer.SelectedIndex];
					dr[2] = (int)dr[2] + 1;
					dr = dataSet.Tables["Sellers"].Rows[comboBoxSeller.SelectedIndex];
					dr[2] = (int)dr[2] + 1;
					dr = dataSet.Tables["Goods"].Rows[comboBoxProducts.SelectedIndex];
					dr[2] = (int)dr[3] - 1;

					dataAdapter.Update(dataSet);

					mySqlTransaction.Commit();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
