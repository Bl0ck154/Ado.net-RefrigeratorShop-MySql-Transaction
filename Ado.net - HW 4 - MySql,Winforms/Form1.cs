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
	public partial class Form1 : Form
	{
		MySqlConnection sqlConnection;
		DataSet dataSet;
		MySqlDataAdapter dataAdapterBuyers, dataAdapterSellers, dataAdapterGoods, dataAdapterOrders;
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
			//			MessageBox.Show(ex.Message, "1");
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
			//		MessageBox.Show(ex.Message, "2");
				}
				
				dataSet = new DataSet();
				dataAdapterBuyers = new MySqlDataAdapter("select * from Buyers", sqlConnection);
				setDataAdapterCommands(dataAdapterBuyers);
				dataAdapterBuyers.Fill(dataSet, "Buyers");
				dataGridView1.DataSource = dataSet.Tables[0];
				dataAdapterSellers = new MySqlDataAdapter("select * from Sellers", sqlConnection);
				setDataAdapterCommands(dataAdapterSellers);
				dataAdapterSellers.Fill(dataSet, "Sellers");
				dataAdapterGoods = new MySqlDataAdapter("select * from Goods", sqlConnection);
				setDataAdapterCommands(dataAdapterGoods);
				dataAdapterGoods.Fill(dataSet, "Goods");
				dataAdapterOrders = new MySqlDataAdapter("select * from Orders", sqlConnection);
				setDataAdapterCommands(dataAdapterOrders);
				dataAdapterOrders.Fill(dataSet, "Orders");

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
				sqlConnection?.Close();
			}
		}
		void EnlistTransaction(MySqlDataAdapter dataAdapter, MySqlTransaction transaction)
		{
			dataAdapter.UpdateCommand.Transaction = transaction;
			dataAdapter.InsertCommand.Transaction = transaction;
			dataAdapter.DeleteCommand.Transaction = transaction;
		}
		void setDataAdapterCommands(MySqlDataAdapter dataAdapter)
		{
			MySqlCommandBuilder mySqlCommandBuilder = new MySqlCommandBuilder(dataAdapter);
			dataAdapter.UpdateCommand = mySqlCommandBuilder.GetUpdateCommand();
			dataAdapter.InsertCommand = mySqlCommandBuilder.GetInsertCommand();
			dataAdapter.DeleteCommand = mySqlCommandBuilder.GetDeleteCommand();
		}
		private void comboBoxTables_SelectedIndexChanged(object sender, EventArgs e)
		{
			dataGridView1.DataSource = dataSet.Tables[comboBoxTables.SelectedIndex];
		}
		void setTransaction(MySqlTransaction mySqlTransaction)
		{
			EnlistTransaction(dataAdapterBuyers, mySqlTransaction);
			EnlistTransaction(dataAdapterSellers,mySqlTransaction);
			EnlistTransaction(dataAdapterGoods,mySqlTransaction);
			EnlistTransaction(dataAdapterOrders,mySqlTransaction);
		}
		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				sqlConnection.Open();
				using (MySqlTransaction mySqlTransaction = sqlConnection.BeginTransaction())
				{
					setTransaction(mySqlTransaction);

					int count = 1;
					DataRow dr = dataSet.Tables["Orders"].NewRow();
					dr["id"] = dataSet.Tables["Orders"].Rows.Count;
					dr["date"] = DateTime.Now;
					dr["buyerName"] = dataSet.Tables["Buyers"].Rows[comboBoxBuyer.SelectedIndex]["name"].ToString();
					dr["sellerName"] = dataSet.Tables["Sellers"].Rows[comboBoxSeller.SelectedIndex]["name"].ToString();
					dr["good_id"] = comboBoxProducts.SelectedIndex;
					dr["count"] = count;
					dr["amount"] = (int)dataSet.Tables["Goods"].Rows[comboBoxProducts.SelectedIndex]["price"] * count;
					dataSet.Tables["Orders"].Rows.Add(dr);
					dataAdapterOrders.Update(dataSet, "Orders");

					DataRow dr2 = dataSet.Tables["Buyers"].Rows[comboBoxBuyer.SelectedIndex];
					dr2[2] = (int)dr2[2]+1;
					dataAdapterBuyers.Update(dataSet, "Buyers");

					DataRow dr3 = dataSet.Tables["Sellers"].Rows[comboBoxSeller.SelectedIndex];
					dr3[2] = (int)dr3[2] + 1;
					dataAdapterSellers.Update(dataSet, "Sellers");

					DataRow dr4 = dataSet.Tables["Goods"].Rows[comboBoxProducts.SelectedIndex];
					dr4[3] = (int)dr4[3] - 1;
					dataAdapterGoods.Update(dataSet, "Goods");
					
					mySqlTransaction.Commit();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				sqlConnection?.Close();
			}
		}
	}
}
