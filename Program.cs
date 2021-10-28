using System;

namespace GerenciamentoDeContasBancarias
{
    public class ContaPoupanca : Conta
    {
        #region Metodos
        public void CalcularTributo()
        {
            Console.WriteLine($"O tributo da conta poupança é {(saldoDaConta * 0.01M).ToString("C")}");
        }
        #endregion
    }

    public class ContaInvestimento : Conta
    {
        #region Metodos
        public void CalcularTributo()
        {
            Console.WriteLine($"O tributo da conta investimento é {(saldoDaConta * 0.02M).ToString("C")}");
        }
        #endregion
    }
    public class Cliente
    {
        #region Campos
        public string nome { get; set; }
        public string rg { get; set; }
        public string cpf { get; set; }
        public string endereco { get; set; }
        #endregion

        #region Construtor
        public Cliente()
        {
            nome = "Gessy Sousa";
            rg = "0102030405";
            cpf = "0123456789";
            endereco = "rua dos bobos, número 0";


        }
        #endregion
    }
    public class Conta
    {
        #region Campos
        public int numeroDaAgencia { get; set; }
        public int numeroDaConta { get; set; }
        public Cliente nomeDoTitular { get; set; }
        public decimal saldoDaConta { get; set; }
        #endregion

        #region Construtor
        public Conta()
        {
            numeroDaAgencia = 0001;
            numeroDaConta = 012345;
            saldoDaConta = 200;

        }
        #endregion

        #region Métodos
        public bool Sacar(decimal valorSaque)
        {
            
            if (valorSaque > saldoDaConta)
            {
                return false;
            }
            else
            {
                saldoDaConta = saldoDaConta - valorSaque;

                return true;
            }
        }

       public bool Depositar(decimal valorDeposito)
        {
            if (valorDeposito >= 0)
            {
                saldoDaConta = saldoDaConta + valorDeposito;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Transferir(decimal valorTransferencia, Conta ContaDestino)
        {
            if (Sacar(valorTransferencia))
            {
                ContaDestino.Depositar(valorTransferencia);
                Console.WriteLine($"Saldo da conta destino {ContaDestino.saldoDaConta.ToString("C")}");
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SimulaInvestimento(decimal valorInvestido)
        {
            for(int i = 0; i < 12; i++)
            {
                valorInvestido = valorInvestido + (valorInvestido * 0.01M);
            }
            Console.WriteLine($"Ao final de 12 meses, o valor investido será {valorInvestido.ToString("C")}");

        }
        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            Conta MinhaConta = new Conta();
            Cliente cliente = new Cliente();
            MinhaConta.nomeDoTitular = cliente;
            Console.WriteLine("Gerenciamento de contas bancárias:");
            Console.WriteLine($"O número da agência é {MinhaConta.numeroDaAgencia}");
            Console.WriteLine($"O número da conta é {MinhaConta.numeroDaConta}");
            Console.WriteLine($"O nome do titular é {MinhaConta.nomeDoTitular.nome}");
            Console.WriteLine($"O RG do titular é {MinhaConta.nomeDoTitular.rg}");
            Console.WriteLine($"O CPF do titular é {MinhaConta.nomeDoTitular.cpf}");
            Console.WriteLine($"O endereço do titular é {MinhaConta.nomeDoTitular.endereco}");
            Console.WriteLine($"O saldo da conta é {MinhaConta.saldoDaConta.ToString("C")}");

            Conta ContaDestino = new Conta();
            Cliente clienteDestino = new Cliente();
            ContaDestino.numeroDaAgencia = 0002;
            ContaDestino.numeroDaConta = 987654;
            ContaDestino.saldoDaConta = 100;
            clienteDestino.nome = "Rafael Schmidt";
            clienteDestino.rg = "0908070605";
            clienteDestino.cpf = "9876543210";
            clienteDestino.endereco = "Pasárgada";

            string opcaoUsuario = ObterOpcaoUsuario();
            while(opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        Console.WriteLine("Informe o valor do saque:");
                        decimal valorSaque = decimal.Parse(Console.ReadLine());

                        if(MinhaConta.Sacar(valorSaque))
                        {
                            Console.WriteLine($"Saque realizado com sucesso. Seu saldo atual é {MinhaConta.saldoDaConta.ToString("C")}");
                        }
                        else
                        {
                            Console.WriteLine($"O valor do saque é maior do que o saldo da conta. O saque deve ser menor do que {MinhaConta.saldoDaConta.ToString("C")}");
                        }
                        break;
                    case "2":
                        Console.WriteLine("Informe o valor a ser depositado:");
                        decimal valorDeposito = decimal.Parse(Console.ReadLine());
                        if (MinhaConta.Depositar(valorDeposito))
                        {
                            Console.WriteLine($"Deposito realizado com sucesso. Seu saldo atual é {MinhaConta.saldoDaConta.ToString("C")}");
                        }
                        else
                        {
                            Console.WriteLine("Deposite um valor acima de 0");
                        }
                        break;
                    case "3":
                        


                        Console.WriteLine("Informe o valor a ser transferido:");
                        decimal valorTransferencia = decimal.Parse(Console.ReadLine());
                        if (MinhaConta.Transferir(valorTransferencia, ContaDestino))
                        {
                            Console.WriteLine($"Sua transferência foi realizada com sucesso. Seu saldo atual é de {MinhaConta.saldoDaConta.ToString("C")}");
                        }
                        else
                        {
                            Console.WriteLine($"Saldo insuficiente. Sua transferência deve ser menor do que {MinhaConta.saldoDaConta.ToString("C")}");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Informe o valor a ser investido:");
                        decimal valorInvestido = decimal.Parse(Console.ReadLine());
                        MinhaConta.SimulaInvestimento(valorInvestido);
                        break;
                    case "5":
                        ContaPoupanca contaPoupanca = new ContaPoupanca();
                        contaPoupanca.saldoDaConta = MinhaConta.saldoDaConta;
                        contaPoupanca.CalcularTributo();
                        break;
                    case "6":
                        ContaInvestimento contaInvestimento = new ContaInvestimento();
                        contaInvestimento.saldoDaConta = MinhaConta.saldoDaConta;
                        contaInvestimento.CalcularTributo();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                opcaoUsuario = ObterOpcaoUsuario();
            }
            Console.WriteLine("Obrigado por utilizar nossos serviços!");
            Console.ReadLine();
            
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("Escolha a opção desejada:");
            Console.WriteLine("1 - Sacar");
            Console.WriteLine("2 - Depositar");
            Console.WriteLine("3 - Transferir");
            Console.WriteLine("4 - Simular Investimento");
            Console.WriteLine("5 - Verificar Tributo Poupança");
            Console.WriteLine("6 - Verificar Tributo Conta Investimento");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;

        }
    }
}
