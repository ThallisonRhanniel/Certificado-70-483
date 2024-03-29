﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Parte_3._4___Propriedades_Indexadas
{
    class PropriedadesIndexadas : IAulaItem
    {
        public void Executar()
        {
            var sala = new Sala();
            sala.SetReserva("D01", new ClienteCinema("Maria de Souza"));
            sala.SetReserva("D02", new ClienteCinema("José da Silva"));

            sala.ImprimirReservas();
        }
    }

    class ClienteCinema
    {
        public ClienteCinema(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; set; }

        public override string ToString()
        {
            return Nome;
        }
    }

    class Sala
    {
        private readonly IDictionary<string, ClienteCinema> reservas
            = new Dictionary<string, ClienteCinema>();

        public ClienteCinema GetReserva(string codigoAssento)
        {
            return reservas[codigoAssento];
        }

        public void SetReserva(string codigoAssento, ClienteCinema value)
        {
            reservas[codigoAssento] = value;
        }

        public void ImprimirReservas()
        {
            Console.WriteLine("Assentos Reservados");
            Console.WriteLine("===================");
            foreach (var reserva in reservas)
            {
                Console.WriteLine($"{reserva.Key} - {reserva.Value}");
            }
        }
        //var sala = new Sala();
        //sala["D01"] = new ClienteCinema("Maria de Souza");
        //sala["D02"] = new ClienteCinema("José da Silva");
        public ClienteCinema this[string codigoAssento]
        {
            get { return reservas[codigoAssento]; }

            set { reservas[codigoAssento] = value; }

        }
    }
}
