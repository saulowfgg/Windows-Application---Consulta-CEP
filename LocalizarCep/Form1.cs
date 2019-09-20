using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace LocalizarCep
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void picLocalizar_Click(object sender, EventArgs e)
        {
            
             try
            {
           
                DataSet ds = new DataSet();
                String xml = "https://viacep.com.br/ws/@cep/xml/".Replace("@cep", txtCEP.Text);
                ds.ReadXml(xml);
                txtEndereco.Text = ds.Tables[0].Rows[0]["logradouro"].ToString();
                txtBairro.Text = ds.Tables[0].Rows[0]["bairro"].ToString();
                txtCidade.Text = ds.Tables[0].Rows[0]["localidade"].ToString();
                txtUF.Text = ds.Tables[0].Rows[0]["uf"].ToString();
                
                
                if (File.Exists("Historico_Ceps.csv"))
                {
                    using (StreamWriter escritor = new StreamWriter("Historico_Ceps.csv", true)) 
                        escritor.WriteLine( txtCEP.Text + ";" + @"""" + txtEndereco.Text + @"""" + ";" + txtBairro.Text + ";" + txtCidade.Text + ";" + txtUF.Text);
                    
                }
                else
                {
                    StreamWriter escritor = new StreamWriter("Historico_Ceps.csv");
                    escritor.WriteLine("CEP;Endereço;Bairro;Cidade;UF");
                    escritor.WriteLine(txtCEP.Text + ";" + @"""" + txtEndereco.Text + @"""" + ";" + txtBairro.Text + ";" + txtCidade.Text + ";" + txtUF.Text);
                    escritor.Close();

                }
                
                
            }
            catch
            {
                txtCEP.Focus();
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtCEP.Text = "";
            txtEndereco.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            txtUF.Text = "";
            txtCEP.Focus();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtCEP_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                picLocalizar_Click(sender, e);
        }
    }
}
