﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogo_da_velha
{
    class Program
    {
        struct Player
        {
            public string Nome;
            public int Pontuacao;
            public int Jogadas;
            public int Perdas;
            public int Ganhos;
        }
        static void Main(string[] args)
        {
            /*
                * @ Descrição: Jogo da velha. 
                * @ Maicom F
            */
            IN_Game();
        }


        static void IN_Game(Player backup_p1 = new Player(), Player backup_p2 = new Player(), bool restart = false)
        {

            string[,] velha = new string[3, 3];
            bool[,] velha_check = new bool[3, 3];
            InitVector(velha);


            Player p1 = new Player();
            Player p2 = new Player();
            if (!restart)
            {
                while (true)
                {
                    Console.Write("Digite o nome do 1º jogdor: ");
                    try
                    {
                        p1.Nome = Console.ReadLine();
                        if (p1.Nome.Length < 3)
                        {
                            Console.WriteLine("Nome inválido! Por favor utilize pelo menos 3 caracteres!");
                            continue;
                        }
                        if (p1.Nome.Length > 12)
                        {
                            Console.WriteLine("Nome inválido! Por favor utilize no máximo 12 caracteres!");
                            continue;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Mensagem de erro: {0}", e.Message);
                        Console.Write("Formato inválido, por favor digite nome do jogador: ");
                        continue;
                    }
                    break;
                }
                Console.Clear();
                while (true)
                {
                    Console.Write("Digite o nome do 2º jogdor: ");
                    try
                    {
                        p2.Nome = Console.ReadLine();
                        if (p2.Nome.Length < 3)
                        {
                            Console.WriteLine("Nome inválido! Por favor utilize pelo menos 3 caracteres!");
                            continue;
                        }
                        if (p2.Nome.Length > 12)
                        {
                            Console.WriteLine("Nome inválido! Por favor utilize no máximo 12 caracteres!");
                            continue;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Mensagem de erro: {0}", e.Message);
                        Console.Write("Formato inválido, por favor digite nome do jogador: ");
                        continue;
                    }
                    break;
                }
                Console.Clear();
            }
            else
            {
                p1 = backup_p1;
                p2 = backup_p2;
            }

            bool player = false;
            bool in_game = true;
            byte game_over = 0;

            while (in_game)
            {
                Console.Clear();
                Console.WriteLine(">>> {0}[{1}][X] vs {2}[{3}][O]\n", p1.Nome, p1.Jogadas, p2.Nome, p2.Jogadas);
                DrawGame(velha);

                while (true)
                {
                    string player_name = (player) ? p1.Nome : p2.Nome;
                    Console.Write("\n\nVez do {0}\nDigite local: ", player_name);
                    string[] cord = Console.ReadLine().Split(' ');

                    byte x, y;
                    try
                    {
                        y = byte.Parse(cord[0]);
                        x = byte.Parse(cord[1]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Mensagem de erro: {0}", e.Message);
                        Console.WriteLine("Formato incorreto!\n Use x y(x espaço y)");
                        continue;
                    }
                    if (x > 2 || x < 0 || y > 2 || y < 0)
                    {
                        Console.WriteLine("Opção invalida!(Utilize valores entre 0 e 2 no x e no y)");
                        continue;
                    }
                    if (velha_check[x, y])
                    {
                        Console.WriteLine("Opção já selecionada!");
                        continue;
                    }

                    velha_check[x, y] = true;
                    velha[x, y] = (player) ? ("X") : ("O");

                    if (player)
                        p1.Jogadas++;
                    else
                        p2.Jogadas++;

                    game_over++;
                    if (CheckIfIsTrue(velha))
                    {
                        if (player)
                        {
                            p1.Ganhos++;
                            p2.Perdas++;
                            p1.Pontuacao += 12;
                            p2.Pontuacao -= 8;
                        }
                        else
                        {
                            p2.Ganhos++;
                            p1.Perdas++;
                            p2.Pontuacao += 12;
                            p1.Pontuacao -= 8;
                        }
                        Console.Clear();
                        DrawGame(velha);

                        Console.WriteLine("O jogador {0} ganhou!", player_name);

                        in_game = false;
                    }
                    else
                    {
                        if(game_over == 9)
                        {
                            Console.WriteLine("Velha");
                            in_game = false;

                        }
                    }
                    break;
                }
                if (in_game) player = !player;
            }

            Console.WriteLine("______________________________________________");
            Console.WriteLine("\n\n\n>>> Jogo finalizado!\n");
            Console.WriteLine("Dados:");
            Console.WriteLine("-------------- || {0} || --------------", p1.Nome);
            Console.WriteLine("Jogadas: {0}", p1.Jogadas);
            Console.WriteLine("Ganhos: {0}", p1.Ganhos);
            Console.WriteLine("Perdas: {0}", p1.Perdas);
            Console.WriteLine("Score: {0}", p1.Pontuacao);
            Console.WriteLine("-------------- || {0} || --------------", p1.Nome);

            Console.WriteLine("\n\n====================================");

            Console.WriteLine("-------------- || {0} || --------------", p2.Nome);
            Console.WriteLine("Jogadas: {0}", p2.Jogadas);
            Console.WriteLine("Ganhos: {0}", p2.Ganhos);
            Console.WriteLine("Perdas: {0}", p2.Perdas);
            Console.WriteLine("Score: {0}", p2.Pontuacao);
            Console.WriteLine("-------------- || {0} || --------------", p2.Nome);


            Console.Write("\n\nDeseja reiniciar o jogo?(Será com os mesmos jogadores atuais)[S/N]: ");
            string c = Console.ReadLine().ToLower();
            if (c == "s")
            {
                Console.Clear();
                IN_Game(p1, p2, true);
                return;
            }
            Console.WriteLine("Aplicação encerrada!\nBy Maicom F :|");
        }

        static void DrawGame(string[,] v)
        {
            Console.WriteLine("    0      1      2   ");
            Console.WriteLine("______________________");
            Console.WriteLine("|------|------|------|");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("|   {0}  |  {1}   |   {2}  |   {3}", v[i, 0], v[i, 1], v[i, 2], i);
                Console.WriteLine("|------|------|------|");
            }
        }
        static void InitVector(string[,] v)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    v[i, j] = " ";
        }
        static bool CheckIfIsTrue(string[,] v)
        {
            for (int i = 0; i < 3; i++)
            {
                if (v[i, 0] != " ")
                {
                    if (v[i, 0] == v[i, 1] && v[i, 1] == v[i, 2]) return true;
                }
                if(v[0, i] != " ")
                {
                    if (v[0, i] == v[1, i] && v[1, i] == v[2, i])  return true;
                }
            }

            if (v[1, 1] != " ")
            {
                if (v[0, 0] == v[1, 1] && v[1, 1] == v[2, 2]) return true;
                if (v[1, 2] == v[1, 1] && v[1, 1] == v[2, 0]) return true;
            }
            
            return false;
        }
    }
}
