using System.Collections;

namespace GestaoDeEquipamentos.ConsoleApp
{
    internal class Program
    {
        static int idEquipamento = 1;

        static int idChamado = 1;

        static ArrayList listaIdsEquipamento = new ArrayList();
        static ArrayList listaNomesEquipamento = new ArrayList();
        static ArrayList listaPrecosEquipamento = new ArrayList();
        static ArrayList listaNumerosSerieEquipamento = new ArrayList();
        static ArrayList listaDatasFabricaoEquipamento = new ArrayList();
        static ArrayList listaFabricanteEquipamento = new ArrayList();

        static ArrayList listaIdsChamado = new ArrayList();
        static ArrayList listaTituloChamado = new ArrayList();
        static ArrayList listaDescricoesChamado = new ArrayList();
        static ArrayList listaIdsEquipamentoChamado = new ArrayList();
        static ArrayList listaDatasAberturaChamado = new ArrayList();

        static void Main(string[] args)
        {
            while (true)
            {
                //apresentar o menu principal

                string opcao = ApresentarMenuPrincipal();

                if (opcao == "s")
                    break;

                if (opcao == "1")
                {
                    string opcaoCadastroEquipamentos = ApresentarMenuCadastroEquipamento();

                    if (opcaoCadastroEquipamentos == "s")
                        continue;

                    CadastroEquipamentos(opcaoCadastroEquipamentos);
                }
                else if (opcao == "2")
                {
                    string opcaoCadastroChamados = ApresentarMenuCadastroChamado();

                    if (opcaoCadastroChamados == "s")
                        continue;

                    ControleChamados(opcaoCadastroChamados);
                }
            }
        }

        

        #region Equipamentos

        static string ApresentarMenuCadastroEquipamento()
        {
            Console.Clear();

            Console.WriteLine("Cadastro de Equipamentos");

            Console.WriteLine("\tDigite 1 para Inserir um Novo Equipamento");

            Console.WriteLine("\tDigite 2 para Visulizar Equipamentos");

            Console.WriteLine("\tDigite 3 para Editar Equipamentos");

            Console.WriteLine("\tDigite 4 para Excluir Equipamentos");

            Console.WriteLine("\tDigite s para voltar para o menu principal");

            string opcao = Console.ReadLine();

            return opcao;
        }
        static void CadastroEquipamentos(string opcaoCadastroEquipamentos)
        {
            if (opcaoCadastroEquipamentos == "1")
            {
                InserirNovoEquipamento();
            }
            else if (opcaoCadastroEquipamentos == "2")
            {
                VisualizarEquipamentos();

                if (listaIdsEquipamento.Count > 0)
                    Console.ReadLine();
            }
            else if (opcaoCadastroEquipamentos == "3")
            {
                EditarEquipamento();
            }
            else if (opcaoCadastroEquipamentos == "4")
            {
                //Excluir um equipamento existente
                ExcluirEquipamento();
            }
        }
        static void InserirNovoEquipamento()
        {
            MostrarCabecalho("Cadastro de Equipamentos", "Inserindo Novo Equipamento: ");
            GravarEquipamento(idEquipamento, "INSERIR");
            ApresentarMensagem("Equipamento inserido com sucesso!", ConsoleColor.Green);
        }
        static void VisualizarEquipamentos()
        {
            if (!existeEquipamentoNaLista())
                return;

            MostrarCabecalho("Cadastro de Equipamentos", "Visualizando Equipamentos: ");

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("{0,-10} | {1,-40} | {2,-30}", "Id", "Nome", "Fabricante");

            Console.WriteLine("---------------------------------------------------------------------------------------");

            for (int i = 0; i < listaIdsEquipamento.Count; i++)
            {
                Console.WriteLine("{0,-10} | {1,-40} | {2,-30}",
                    listaIdsEquipamento[i], listaNomesEquipamento[i], listaFabricanteEquipamento[i]);
            }

            Console.ResetColor();
        }

        static void EditarEquipamento()
        {
            MostrarCabecalho("Cadastro de Equipamentos", "Editando Equipamento: ");

            if (!existeEquipamentoNaLista())
                return;

            VisualizarEquipamentos();

            int idSelecionado = EncontrarEquipamento();

            GravarEquipamento(idSelecionado, "EDITAR");

            ApresentarMensagem("Equipamento editado com sucesso!", ConsoleColor.Green);
        }
        static void ExcluirEquipamento()
        {
            MostrarCabecalho("Cadastro de Equipamentos", "Excluindo Equipamento: ");

            if (!existeEquipamentoNaLista())
                return;

            VisualizarEquipamentos();

            int idSelecionado = EncontrarEquipamento();

            int posicao = listaIdsEquipamento.IndexOf(idSelecionado);

            listaIdsEquipamento.RemoveAt(posicao);
            listaNomesEquipamento.RemoveAt(posicao);
            listaPrecosEquipamento.RemoveAt(posicao);
            listaNumerosSerieEquipamento.RemoveAt(posicao);
            listaDatasFabricaoEquipamento.RemoveAt(posicao);
            listaFabricanteEquipamento.RemoveAt(posicao);

            ApresentarMensagem("Equipamento excluído com sucesso!", ConsoleColor.Green);
        }

        static int EncontrarEquipamento()
        {
            int idSelecionado = 0;
            Console.WriteLine();

            while (true)
            {
                Console.Write("Digite o Id do Equipamento: ");

                idSelecionado = Convert.ToInt32(Console.ReadLine());

                if (listaIdsEquipamento.Contains(idSelecionado))
                    break;
                else
                    ApresentarMensagem("Id inválido, tente novamente", ConsoleColor.Red);
            }

            return idSelecionado;
        }

        static void GravarEquipamento(int id, string tipoOperacao)
        {
            string nome;

            while (true)
            {
                Console.Write("Digite o nome do Equipamento: ");
                nome = Console.ReadLine();

                if (nome.Length < 6)
                {
                    ApresentarMensagem("Nome inválido. Informe no mínimo 6 letras", ConsoleColor.Red);
                }
                else
                    break;
            }

            Console.Write("Digite o preco do Equipamento: ");
            decimal preco = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Digite o número de série: ");
            string numeroSerie = Console.ReadLine();

            Console.Write("Digite a data de fabricação: ");
            string dataFabricacao = Console.ReadLine();

            Console.Write("Digite o Fabricante: ");
            string fabricante = Console.ReadLine();

            if (tipoOperacao == "INSERIR")
            {
                //utilizado para inserção
                listaIdsEquipamento.Add(id);
                listaNomesEquipamento.Add(nome);
                listaPrecosEquipamento.Add(preco);
                listaNumerosSerieEquipamento.Add(numeroSerie);
                listaDatasFabricaoEquipamento.Add(dataFabricacao);
                listaFabricanteEquipamento.Add(fabricante);
                idEquipamento++;
            }
            else if (tipoOperacao == "EDITAR")
            {
                //utilizado para edição
                int posicao = listaIdsEquipamento.IndexOf(id);

                listaIdsEquipamento[posicao] = id;
                listaNomesEquipamento[posicao] = nome;
                listaPrecosEquipamento[posicao] = preco;
                listaNumerosSerieEquipamento[posicao] = numeroSerie;
                listaDatasFabricaoEquipamento[posicao] = dataFabricacao;
                listaFabricanteEquipamento[posicao] = fabricante;
            }
        }

        static bool existeEquipamentoNaLista()
        {
            if (listaIdsEquipamento.Count == 0)
            {
                ApresentarMensagem("Nenhum equipamento cadastrado!", ConsoleColor.DarkYellow);
                Console.ReadLine();
                return false;
            }
            return true;
        }

        #endregion

        #region Chamados

        static string ApresentarMenuCadastroChamado()
        {
            Console.Clear();

            Console.WriteLine("Cadastro de Chamados");

            Console.WriteLine("\tDigite 1 para Inserir um Novo Chamado");

            Console.WriteLine("\tDigite 2 para Visulizar Chamados");

            Console.WriteLine("\tDigite 3 para Editar Chamados");

            Console.WriteLine("\tDigite 4 para Excluir Chamados");

            Console.WriteLine("\tDigite s para voltar para o menu principal");

            string opcao = Console.ReadLine();

            return opcao;
        }
        static void ControleChamados(string opcaoCadastroChamados)
        {
            if (opcaoCadastroChamados == "1")
            {
                InserirNovoChamado();
            }
            else if (opcaoCadastroChamados == "2")
            {
                VisualizarChamados();

                if (listaIdsChamado.Count > 0)
                    Console.ReadLine();
            }
            else if (opcaoCadastroChamados == "3")
            {
                EditarChamado();
            }
            else if (opcaoCadastroChamados == "4")
            {
                //Excluir um equipamento existente
                ExcluirChamado();
            }
        }
        static void InserirNovoChamado()
        {
            MostrarCabecalho("Cadastro de Chamados", "Inserindo Novo Chamado: ");
            
            if (!existeEquipamentoNaLista())
                return;

            GravarChamado(idChamado, "INSERIR");
            ApresentarMensagem("Chamado inserido com sucesso!", ConsoleColor.Green);
        }
        
        static void VisualizarChamados()
        {
            if (!existeChamadoNaLista())
                return;

            MostrarCabecalho("Cadastro de Chamados", "Visualizando Chamados: ");

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("{0,-5} | {1,-15} | {2,-15} | {3,-10} | {4,-10} | {5,-25} | {6,-10}",
                            "Id", "Titulo", "Nome Eq", "Numero Eq", "Fabricante", "Data abertura chamado",
                            "Dias aberto");

            Console.WriteLine("---------------------------------------------------------------------------------------");

            DateTime hoje = DateTime.Now;

            for (int i = 0; i < listaIdsChamado.Count; i++)
            {
                var diasAbertos = hoje.Subtract(Convert.ToDateTime(listaDatasAberturaChamado[i]));
                int posicao = listaIdsEquipamento.IndexOf(listaIdsEquipamentoChamado[i]);
                Console.WriteLine("{0,-5} | {1,-15} | {2,-15} | {3,-10} | {4,-10} | {5,-25} | {6,-10}",
                    listaIdsChamado[i], listaTituloChamado[i], listaNomesEquipamento[posicao],
                    listaNumerosSerieEquipamento[posicao],listaFabricanteEquipamento[posicao],
                    listaDatasAberturaChamado[i], diasAbertos.Days);
            }

            Console.ResetColor();
        }
        static void EditarChamado()
        {
            MostrarCabecalho("Cadastro de Chamado", "Editando Chamado: ");

            if (!existeChamadoNaLista())
                return;

            VisualizarChamados();

            int idSelecionado = EncontrarChamado();

            GravarChamado(idSelecionado, "EDITAR");

            ApresentarMensagem("Chamado editado com sucesso!", ConsoleColor.Green);
        }

        static void ExcluirChamado()
        {
            MostrarCabecalho("Cadastro de Chamado", "Excluindo Chamado: ");

            if (!existeChamadoNaLista())
                return;

            VisualizarChamados();

            int idSelecionado = EncontrarChamado();

            int posicao = listaIdsChamado.IndexOf(idSelecionado);

            listaIdsChamado.RemoveAt(posicao);
            listaTituloChamado.RemoveAt(posicao);
            listaDescricoesChamado.RemoveAt(posicao);
            listaIdsEquipamentoChamado.RemoveAt(posicao);
            listaDatasAberturaChamado.RemoveAt(posicao);

            ApresentarMensagem("Chamado excluído com sucesso!", ConsoleColor.Green);
        }

        static int EncontrarChamado()
        {
            int idSelecionado = 0;
            Console.WriteLine();

            while (true)
            {
                Console.Write("Digite o Id do Chamado: ");

                idSelecionado = Convert.ToInt32(Console.ReadLine());

                if (listaIdsChamado.Contains(idSelecionado))
                    break;
                else
                    ApresentarMensagem("Id inválido, tente novamente", ConsoleColor.Red);
            }

            return idSelecionado;
        }

        static void GravarChamado(int id, string tipoOperacao)
        {
            VisualizarEquipamentos();

            Console.WriteLine();

            int idEquipamentoChamado = EncontrarEquipamento();

            Console.Write("Digite o título do chamado: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite a descricao do chamado: ");
            string descricao = Console.ReadLine();

            Console.Write("Digite a data de abertura do chamado: ");
            string[] dataAbertura = Console.ReadLine().Split("/");
            DateTime data = new DateTime(Convert.ToInt32(dataAbertura[2]),
                                         Convert.ToInt32(dataAbertura[1]),
                                         Convert.ToInt32(dataAbertura[0]));

            if (tipoOperacao == "INSERIR")
            {
                //utilizado para inserção

                listaIdsChamado.Add(id);
                listaTituloChamado.Add(titulo);
                listaDescricoesChamado.Add(descricao);
                listaIdsEquipamentoChamado.Add(idEquipamentoChamado);
                listaDatasAberturaChamado.Add(data);
                idChamado++;
            }
            else if (tipoOperacao == "EDITAR")
            {
                //utilizado para edição

                int posicao = listaIdsChamado.IndexOf(id);
                listaIdsChamado[posicao] = id;
                listaTituloChamado[posicao] = titulo;
                listaDescricoesChamado[posicao] = descricao;
                listaDatasAberturaChamado[posicao] = data;
            }
        }

        static bool existeChamadoNaLista()
        {
            if (listaIdsChamado.Count == 0)
            {
                ApresentarMensagem("Nenhum chamado cadastrado!", ConsoleColor.DarkYellow);
                Console.ReadLine();
                return false;
            }
            return true;
        }

        #endregion

        static void ApresentarMensagem(string mensagem, ConsoleColor cor)
        {
            Console.WriteLine();
            Console.ForegroundColor = cor;
            Console.WriteLine(mensagem);
            Console.ResetColor();
            Console.ReadLine();
        }

        static void MostrarCabecalho(string titulo, string subtitulo)
        {
            Console.Clear();

            Console.WriteLine(titulo);

            Console.WriteLine(subtitulo);

            Console.WriteLine();
        }

        static string ApresentarMenuPrincipal()
        {
            Console.Clear();

            Console.WriteLine("Gestão de Equipamentos 1.0");

            Console.WriteLine("Digite 1 para Cadastrar Equipamentos");

            Console.WriteLine("Digite 2 para Controlar Chamados");

            Console.WriteLine("Digite s para Sair");

            string opcao = Console.ReadLine();

            return opcao;
        }
    }
}