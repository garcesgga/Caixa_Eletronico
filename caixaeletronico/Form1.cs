using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


/*Crie um programa em C# com Windows Forms que simule um caixa eletrônico simples. O programa deve ter:
Um formulário principal com um campo para exibir o saldo da conta.
Botões para as operações de depósito, saque e extrato.
Ao clicar em "Depósito", o programa deve solicitar o valor do depósito e atualizar o saldo.
Ao clicar em "Saque", o programa deve solicitar o valor do saque, verificar se há saldo suficiente e atualizar o saldo.
Ao clicar em "Extrato", o programa deve exibir um histórico das operações realizadas (depósitos e saques).
Utilize uma List<T> para armazenar o histórico das operações.
Utilize exceções para tratar erros como saldo insuficiente ou valor inválido.*/


namespace caixaeletronico
{
    public partial class Form1 : Form
    {
        private double saldo = 1000.00;
        private List<string> extrato = new List<string>();
        public Form1()
        {
            InitializeComponent();
            AtualizarSaldo();
        }

        private void btnDeposito_Click(object sender, EventArgs e)
        {
            try
            {
                double valor = double.Parse(txtValor.Text);
                if (valor <= 0)
                {
                    throw new ArgumentException("O valor deve ser maior do que zero!");
                }
                saldo += valor;
                extrato.Add($"Depósito de R$ {valor:C}");
                AtualizarSaldo();
                LimparCampoValor();
            }
            catch (FormatException)
            {
                MessageBox.Show("Digite um valor válido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaque_Click(object sender, EventArgs e)
        {
            try
            {
                double valor = double.Parse(txtValor.Text);
                if (valor <= 0)
                {
                    throw new ArgumentException("O valor deve ser maior do que zero!");
                }
                if (valor > saldo)
                {
                    throw new InvalidOperationException("Saldo insuficiente!");
                }
                saldo -= valor;
                extrato.Add($"Saque: -{valor:C}");
                AtualizarSaldo();
                LimparCampoValor();
            }
            catch (FormatException)
            {
                MessageBox.Show("Digite um valor válido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnExtrato_Click(object sender, EventArgs e)
        {
            string historico = "Extrato:\n";
            foreach(string operacao in extrato)
            {
                historico += operacao + "\n";
            }
            MessageBox.Show(historico, "Extrato", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AtualizarSaldo()
        {
            lblSaldo.Text =$"Saldo: {saldo:C}";
        }

        private void lblSaldo_Click(object sender, EventArgs e)
        {

        }

        private void LimparCampoValor()
        {
            txtValor.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}