using CSharpEgitimKampi501.Dtos;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Csharp_Bootcamp_501
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Data Source=ACER-ASPIRE;Initial Catalog=C#_Bootcamp_501;Integrated Security=True");
        private async void btnList_Click(object sender, EventArgs e)
        {
            string query = "Select * From Product";
            var values = await connection.QueryAsync<ResultProductDto>(query);
            dataGridView1.DataSource = values;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            string query = "insert into Product (productName,productCategory,productStock,productPrice) values (@productName,@productCategory,@productStock,@productPrice)";
            var parameter = new DynamicParameters();
            parameter.Add("productName", txtProductName.Text);
            parameter.Add("productCategory", txtProductCategory.Text);
            parameter.Add("productStock", txtProductStock.Text);
            parameter.Add("productPrice", txtProductPrice.Text);
            await connection.ExecuteAsync(query, parameter);
            MessageBox.Show("Ekleme başarılı...");
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            string query = "delete from Product where productID=@productID";
            var parameter = new DynamicParameters();
            parameter.Add("productID", txtProductId.Text);
            await connection.ExecuteAsync(query, parameter);
            MessageBox.Show("Silme başarılı...");
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            string query = "update Product set productName=@productName,productCategory=@productCategory,productStock=@productStock,productPrice=@productPrice where productID=@productID";
            var parameter = new DynamicParameters();
            parameter.Add("productID", txtProductId.Text);
            parameter.Add("productName", txtProductName.Text);
            parameter.Add("productCategory", txtProductCategory.Text);
            parameter.Add("productStock", txtProductStock.Text);
            parameter.Add("productPrice", txtProductPrice.Text);
            await connection.ExecuteAsync(query, parameter);
            MessageBox.Show("Güncelleme başarılı...");
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string query = "Select Count(*) From Product";
            var values = await connection.QueryFirstOrDefaultAsync<int>(query);
            lblTotalProductCount.Text=values.ToString();

            string query2 = "select productName from Product where productPrice=(select max(productPrice) from Product)";
            var maxPriceProductName = await connection.QueryFirstOrDefaultAsync<string>(query2);
            lblMaxPriceProductName.Text = maxPriceProductName.ToString();

            string query3 = "select count(distinct(productCategory)) from Product";
            var categoryCount = await connection.QueryFirstOrDefaultAsync<int>(query3);
            lblDistinctCategoryCount.Text = categoryCount.ToString();
        }
    }
}
