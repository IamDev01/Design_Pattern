/*
 Exercício - Cálculo de Impostos para Diferentes Países

Você precisa desenvolver um sistema que calcule impostos para uma loja de comércio eletrônico. 
O cálculo do imposto varia de acordo com o país de destino da compra. 
A loja vende para diferentes países e o imposto de cada país tem uma fórmula específica.

Requisitos:
O sistema deve permitir calcular o imposto de forma flexível, ou seja, a estratégia de cálculo pode ser
alterada conforme o país de destino da compra.
Não deve ser necessário alterar o código de cálculo de impostos sempre que adicionar um novo país.
O sistema deve ser facilmente extensível para adicionar novos países e suas respectivas estratégias de cálculo de impostos.

Tarefa:
Explique por que escolheu esse padrão.
Vantagens do Padrão Strategy para este caso:

Flexibilidade: Você pode adicionar novos países e suas respectivas fórmulas de imposto sem alterar o código existente. Basta criar uma nova classe que implementa a interface ImpostoStrategy.
Desacoplamento: A lógica do cálculo de imposto é isolada, e a classe Pedido não precisa saber como os impostos são calculados, ela apenas delega a tarefa para a estratégia correta.
Extensibilidade: É fácil adicionar novos algoritmos (estratégias de imposto) no futuro.
 */
using System;

namespace StrategyPattern
{
    // Interface para as estratégias de imposto
    interface IImpostoStrategy
    {
        decimal CalcularImposto(decimal valorCompra);
    }

    // Estratégia de imposto para o Brasil
    class ImpostoBrasil : IImpostoStrategy
    {
        public decimal CalcularImposto(decimal valorCompra)
        {
            return valorCompra * 0.2m; // Imposto de 20% no Brasil
        }
    }

    // Estratégia de imposto para os EUA
    class ImpostoEUA : IImpostoStrategy
    {
        public decimal CalcularImposto(decimal valorCompra)
        {
            return valorCompra * 0.1m; // Imposto de 10% nos EUA
        }
    }

    // Estratégia de imposto para a Europa
    class ImpostoEuropa : IImpostoStrategy
    {
        public decimal CalcularImposto(decimal valorCompra)
        {
            return valorCompra * 0.15m; // Imposto de 15% na Europa
        }
    }

    // Classe Pedido que utiliza uma estratégia de imposto
    class Pedido
    {
        private IImpostoStrategy impostoStrategy;

        public void DefinirImpostoStrategy(IImpostoStrategy novoImpostoStrategy)
        {
            impostoStrategy = novoImpostoStrategy;
        }

        public decimal CalcularTotalComImposto(decimal valorCompra)
        {
            return valorCompra + impostoStrategy.CalcularImposto(valorCompra);
        }
    }

    // Classe principal para testar o sistema
    class Program
    {
        static void Main(string[] args)
        {
            Pedido pedido = new Pedido();

            // Definindo a estratégia de imposto para o Brasil
            pedido.DefinirImpostoStrategy(new ImpostoBrasil());
            decimal totalBrasil = pedido.CalcularTotalComImposto(1000m);  // Valor da compra = 1000
            Console.WriteLine("Total no Brasil: " + totalBrasil);

            // Mudando para a estratégia de imposto dos EUA
            pedido.DefinirImpostoStrategy(new ImpostoEUA());
            decimal totalEUA = pedido.CalcularTotalComImposto(1000m);
            Console.WriteLine("Total nos EUA: " + totalEUA);

            // Mudando para a estratégia de imposto da Europa
            pedido.DefinirImpostoStrategy(new ImpostoEuropa());
            decimal totalEuropa = pedido.CalcularTotalComImposto(1000m);
            Console.WriteLine("Total na Europa: " + totalEuropa);

            Console.ReadLine();
        }
    }
}
