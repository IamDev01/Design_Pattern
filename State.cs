/* 
Exercício - Pedido de Compra (Order Processing)
Imagine que você está desenvolvendo um sistema para processar pedidos de compra em uma loja online. O sistema deve permitir criar um pedido, processá-lo e enviá-lo para o cliente.

Requisitos:

Um pedido pode ter diferentes estados: Criado, Processando, Enviado, A caminho, Entregue.
O sistema deve garantir que a transição entre os estados ocorra de maneira controlada.
Deve ser possível adicionar novas etapas no futuro sem modificar o código existente.

Tarefa:
Implemente esse sistema utilizando o Design Pattern mais adequado.

Explique por que escolheu esse padrão. 
✅ Organização e Manutenção: Cada estado é encapsulado em sua própria classe, tornando o código mais modular e fácil de modificar ou expandir.

✅ Facilidade para Adicionar Novos Estados: Se precisarmos incluir estados como "Pedido Cancelado" ou "Pedido Devolvido", basta criar uma nova classe sem alterar a lógica existente.

✅ Evita Estruturas Condicionais Complexas: Sem o padrão State, o código poderia conter múltiplos if-else ou switch-case para controlar os estados, o que dificultaria a manutenção.

✅ Controle Rigoroso das Transições: Garante que o pedido siga um fluxo lógico, impedindo transições indevidas, como pular diretamente para "Entregue" sem passar pelos estados intermediários.

Esse padrão torna o código mais flexível, reutilizável e preparado para futuras alterações. 🚀
*/

namespace PedidoEstado
{
    // Interface para os estados do pedido
    interface IEstadoPedido
    {
        void MudarEstado(Pedido pedido);
    }

    // Estado: Criado
    class Criado : IEstadoPedido
    {
        public void MudarEstado(Pedido pedido)
        {
            Console.WriteLine("Pedido foi criado.");
            pedido.DefinirEstado(new Processando());
        }
    }

    // Estado: Processando
    class Processando : IEstadoPedido
    {
        public void MudarEstado(Pedido pedido)
        {
            Console.WriteLine("Pedido está sendo processado.");
            pedido.DefinirEstado(new Enviado());
        }
    }

    // Estado: Enviado
    class Enviado : IEstadoPedido
    {
        public void MudarEstado(Pedido pedido)
        {
            Console.WriteLine("Pedido foi enviado ao cliente.");
            pedido.DefinirEstado(new Acaminho());
        }
    }

    // Estado: A Caminho
    class Acaminho : IEstadoPedido
    {
        public void MudarEstado(Pedido pedido)
        {
            Console.WriteLine("Pedido está a caminho.");
            pedido.DefinirEstado(new Entregue());
        }
    }

    // Estado: Entregue (Estado Final)
    class Entregue : IEstadoPedido
    {
        public void MudarEstado(Pedido pedido)
        {
            Console.WriteLine("Pedido foi entregue ao cliente.");
            Console.WriteLine("Fim do processo. Nenhuma transição adicional é possível.");
        }
    }

    // Classe Pedido que gerencia os estados
    class Pedido
    {
        private IEstadoPedido estadoAtual;

        public Pedido()
        {
            estadoAtual = new Criado(); // Estado inicial
        }

        public void DefinirEstado(IEstadoPedido novoEstado)
        {
            estadoAtual = novoEstado;
        }

        public void Processar()
        {
            estadoAtual.MudarEstado(this);
        }
    }

    // Classe principal para teste
    class Program
    {
        static void Main(string[] args)
        {
            Pedido pedido = new Pedido();

            pedido.Processar(); // Criado -> Processando
            pedido.Processar(); // Processando -> Enviado
            pedido.Processar(); // Enviado -> A Caminho
            pedido.Processar(); // A Caminho -> Entregue
            pedido.Processar(); // Tentativa inválida (Já entregue)

            Console.ReadLine();
        }
    }
}
