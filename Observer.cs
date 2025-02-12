/*
Exercício - Gerenciamento de Notificações (Observer Pattern)
Você foi contratado para desenvolver um sistema de notificações para uma plataforma que pode enviar mensagens via e-mail, SMS e push notifications.

Requisitos:
O sistema deve permitir que diferentes métodos de notificação sejam cadastrados.
Sempre que um novo evento ocorrer (exemplo: "Novo Pedido", "Pagamento Aprovado", "Pedido Enviado"), todos os métodos de notificação devem ser acionados automaticamente.
Deve ser possível adicionar ou remover métodos de notificação sem modificar o código principal.

Tarefa:
Explique por que escolheu esse padrão.
Por que usar o Padrão Observer?
✅ Desacoplamento: O código principal não precisa saber quantos ou quais notificadores existem, apenas dispara eventos.
✅ Extensibilidade: Podemos adicionar novos métodos de notificação (como WhatsApp) sem alterar a lógica principal.
✅ Facilidade de manutenção: Podemos ativar ou desativar notificadores sem mudar o código base.

Esse padrão é muito usado em eventos, notificações e sistemas de mensagens assíncronas. 🚀
*/

namespace ObserverPattern
{
    // Interface para os observadores (notificadores)
    interface IObservador
    {
        void Notificar(string mensagem);
    }

    // Notificação por e-mail
    class EmailNotificacao : IObservador
    {
        public void Notificar(string mensagem)
        {
            Console.WriteLine("Enviando e-mail: " + mensagem);
        }
    }

    // Notificação por SMS
    class SMSNotificacao : IObservador
    {
        public void Notificar(string mensagem)
        {
            Console.WriteLine("Enviando SMS: " + mensagem);
        }
    }

    // Notificação por Push Notification
    class PushNotificacao : IObservador
    {
        public void Notificar(string mensagem)
        {
            Console.WriteLine("Enviando Push Notification: " + mensagem);
        }
    }

    // Classe Sujeito que gerencia os observadores
    class Sujeito
    {
        private List<IObservador> observadores = new List<IObservador>();

        public void AdicionarObservador(IObservador observador)
        {
            observadores.Add(observador);
        }

        public void RemoverObservador(IObservador observador)
        {
            observadores.Remove(observador);
        }

        public void NotificarObservadores(string mensagem)
        {
            foreach (var observador in observadores)
            {
                observador.Notificar(mensagem);
            }
        }
    }

    // Sistema que dispara notificações
    class SistemaNotificacao : Sujeito
    {
        public void NovoEvento(string evento)
        {
            Console.WriteLine($"\nNovo evento ocorrido: {evento}");
            NotificarObservadores(evento);
        }
    }

    // Classe principal para teste
    class Program
    {
        static void Main(string[] args)
        {
            SistemaNotificacao sistema = new SistemaNotificacao();

            IObservador email = new EmailNotificacao();
            IObservador sms = new SMSNotificacao();
            IObservador push = new PushNotificacao();

            // Adicionando os notificadores ao sistema
            sistema.AdicionarObservador(email);
            sistema.AdicionarObservador(sms);
            sistema.AdicionarObservador(push);

            // Disparando eventos
            sistema.NovoEvento("Pedido confirmado");
            sistema.NovoEvento("Pagamento aprovado");

            // Removendo SMS das notificações
            sistema.RemoverObservador(sms);

            // Disparando novo evento sem SMS
            sistema.NovoEvento("Pedido enviado");

            Console.ReadLine();
        }
    }
}
