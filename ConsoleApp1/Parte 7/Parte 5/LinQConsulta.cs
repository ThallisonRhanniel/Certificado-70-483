﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Parte_7.Parte_5
{
    class Program
    {
        static void Main(string[] args)
        {
            var filmes = GetFilmes();
            var diretores = GetDiretores();

            var novoFilme = new Filme
            {
                Titulo = "A Fantástica Fábrica de Chocolate",
                Ano = 2005,
                Diretor = new Diretor { Id = 3, Nome = "Tim Burton" },
                DiretorId = 3,
                Minutos = 115
            };

            filmes.Add(novoFilme);

            Imprimir(filmes);

            //LINQ = CONSULTA INTEGRADA À LINGUAGEM

            //SELECT f.*
            //FROM filmes AS f  //ALIAS
            //WHERE f.Diretor = "Tim Burton"

            var consulta =
                from f in filmes
                where f.Diretor.Nome == "Tim Burton"
                select f;

            Imprimir(consulta);

            //SELECT f.Nome AS Titulo, f.Diretor
            //FROM filmes AS f  //ALIAS
            //WHERE f.Diretor = "Tim Burton"

            var consulta2 =
                from f in filmes
                where f.Diretor.Nome == "Tim Burton"
                select new FilmeResumido
                {
                    Titulo = f.Titulo,
                    Diretor = f.Diretor.Nome
                };
            Imprimir(consulta2);


            var consulta3 =
                from f in filmes
                where f.Diretor.Nome == "Tim Burton"
                select new //OBJETO ANÔNIMO
                {
                    f.Titulo,
                    Diretor = f.Diretor.Nome
                };

            Console.WriteLine($"{"Título",-40} {"Diretor",-20}");
            Console.WriteLine(new string('=', 64));
            foreach (var filme in consulta3)
            {
                Console.WriteLine($"{filme.Titulo,-40} {filme.Diretor,-20}");
            }
            Console.WriteLine();

            //SELECT f.Nome AS Titulo, d.Nome AS Diretor
            //FROM filmes AS f  //ALIAS
            //INNER JOIN diretores AS d
            //  ON d.Id = f.DiretorId
            //WHERE d.Nome = "Tim Burton"

            var consulta4 =
                from f in filmes
                join d in diretores
                    on f.DiretorId equals d.Id
                where f.Diretor.Nome == "Tim Burton"
                select new //OBJETO ANÔNIMO
                {
                    f.Titulo,
                    Diretor = d.Nome
                };

            Console.WriteLine($"{"Título",-40} {"Diretor",-20}");
            Console.WriteLine(new string('=', 64));
            foreach (var filme in consulta4)
            {
                Console.WriteLine($"{filme.Titulo,-40} {filme.Diretor,-20}");
            }
            Console.WriteLine();

            //SELECT d.Nome AS Diretor, COUNT(*) AS Quantidade
            //FROM filmes AS f  //ALIAS
            //INNER JOIN diretores AS d
            //  ON d.Id = f.DiretorId
            //GROUP BY d.Id

            var consulta5 =
                from f in filmes
                join d in diretores
                    on f.DiretorId equals d.Id
                group f by d
                    into agrupado
                select new //OBJETO ANÔNIMO
                {
                    Diretor = agrupado.Key,
                    Quantidade = agrupado.Count(),
                    Total = agrupado.Sum(f => f.Minutos),
                    Min = agrupado.Min(f => f.Minutos),
                    Max = agrupado.Max(f => f.Minutos),
                    Media = (int)agrupado.Average(f => f.Minutos)
                };

            Console.WriteLine(
                $"{"Nome",-30}" +
                $"\t{"Qtd"}" +
                $"\t{"Total"}" +
                $"\t{"Min"}" +
                $"\t{"Max"}" +
                $"\t{"Media"}");
            foreach (var item in consulta5)
            {
                Console.WriteLine(
                    $"{item.Diretor.Nome,-30}" +
                    $"\t{item.Quantidade}" +
                    $"\t{item.Total}" +
                    $"\t{item.Min}" +
                    $"\t{item.Max}" +
                    $"\t{item.Media}");
            }

            int tamanhoPagina = 4;
            int pagina = 0;

            while (pagina * tamanhoPagina < filmes.Count())
            {
                Console.WriteLine();
                Console.WriteLine("Página: " + (pagina + 1));
                Console.WriteLine();

                var relatorio =
                    from f in filmes
                        .Skip(pagina * tamanhoPagina)
                        .Take(tamanhoPagina)
                    select f;

                Imprimir(relatorio);

                pagina++;
            }



            Console.ReadKey();
        }

        private static void Imprimir(IEnumerable<Filme> filmes)
        {
            Console.WriteLine($"{"Título",-40} {"Diretor",-20} {"Ano",4}");
            Console.WriteLine(new string('=', 64));
            foreach (var filme in filmes)
            {
                Console.WriteLine($"{filme.Titulo,-40} {filme.Diretor.Nome,-20} {filme.Ano,4}");
            }
            Console.WriteLine();
        }

        private static void Imprimir(IEnumerable<FilmeResumido> filmes)
        {
            Console.WriteLine($"{"Título",-40} {"Diretor",-20}");
            Console.WriteLine(new string('=', 64));
            foreach (var filme in filmes)
            {
                Console.WriteLine($"{filme.Titulo,-40} {filme.Diretor,-20}");
            }
            Console.WriteLine();
        }

        private static List<Diretor> GetDiretores()
        {
            return new List<Diretor>
            {
                new Diretor { Id = 1, Nome = "Quentin Tarantino" },
                new Diretor { Id = 2, Nome = "James Cameron" },
                new Diretor { Id = 3, Nome = "Tim Burton" }
            };
        }

        private static List<Filme> GetFilmes()
        {
            return new List<Filme> {
                new Filme {
                    DiretorId = 1,
                    Diretor = new Diretor { Nome = "Quentin Tarantino" },
                    Titulo = "Pulp Fiction",
                    Ano = 1994,
                    Minutos = 2 * 60 + 34
                },
                new Filme {
                    DiretorId = 1,
                    Diretor = new Diretor { Nome = "Quentin Tarantino" },
                    Titulo = "Django Livre",
                    Ano = 2012,
                    Minutos = 2 * 60 + 45
                },
                new Filme {
                    DiretorId = 1,
                    Diretor = new Diretor { Nome = "Quentin Tarantino" },
                    Titulo = "Kill Bill Volume 1",
                    Ano = 2003,
                    Minutos = 1 * 60 + 51
                },

                new Filme {
                    DiretorId = 2,
                    Diretor = new Diretor { Nome = "James Cameron" },
                    Titulo = "Avatar",
                    Ano = 2009,
                    Minutos = 2 * 60 + 42
                },
                new Filme {
                    DiretorId = 2,
                    Diretor = new Diretor { Nome = "James Cameron" },
                    Titulo = "Titanic",
                    Ano = 1997,
                    Minutos = 3 * 60 + 14
                },
                new Filme {
                    DiretorId = 2,
                    Diretor = new Diretor { Nome = "James Cameron" },
                    Titulo = "O Exterminador do Futuro",
                    Ano = 1984,
                    Minutos = 1 * 60 + 47
                },

                new Filme {
                    DiretorId = 3,
                    Diretor = new Diretor { Nome = "Tim Burton" },
                    Titulo = "O Estranho Mundo de Jack",
                    Ano = 1993,
                    Minutos = 1 * 60 + 16
                },
                new Filme {
                    DiretorId = 3,
                    Diretor = new Diretor { Nome = "Tim Burton" },
                    Titulo = "Alice no País das Maravilhas",
                    Ano = 2010,
                    Minutos = 1 * 60 + 48
                },
                new Filme {
                    DiretorId = 3,
                    Diretor = new Diretor { Nome = "Tim Burton" },
                    Titulo = "A Noiva Cadáver",
                    Ano = 2005,
                    Minutos = 1 * 60 + 17
                }
            };
        }
    }

    class Diretor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    class Filme
    {
        public int DiretorId { get; set; }
        public Diretor Diretor { get; set; }
        public string Titulo { get; set; }
        public int Ano { get; set; }
        public int Minutos { get; set; }
    }

    class FilmeResumido
    {
        public string Titulo { get; set; }
        public string Diretor { get; set; }
    }
}
