import { useEffect, useState } from "react";
import {
  ChevronDown,
  ChevronUp,
  CircleGauge,
  Calendar,
  Package,
  AlertCircle,
  Loader2,
  TrendingUp,
  Clock,
  CheckCircle,
  XCircle,
  Target,
  BarChart3,
  Wallet
} from "lucide-react";
import Navbar from "../../components/Navbar";
import "./dash.css";
import { despesasService } from "../../services/DespesasService";
import { DespesasMapper, type Despesa } from "../../utils/DespesasUtils";
import { CalculosFinanceiros, type ResumoFinanceiro } from "../../utils/CalculosFinanceiros";

function Dashboard() {
  const [despesas, setDespesas] = useState<Despesa[]>([]);
  const [loading, setLoading] = useState(true);
  const [erro, setErro] = useState<string | null>(null);
  const [despesasExpandidas, setDespesasExpandidas] = useState<Set<number>>(new Set());
  const [resumoFinanceiro, setResumoFinanceiro] = useState<ResumoFinanceiro | null>(null);

  const buscarDespesas = async () => {
    try {
      setLoading(true);
      setErro(null);

      const response = await despesasService.buscarDespesas();
      const despesasMapeadas = DespesasMapper.mapDespesasFromApi(response.despesas);
      
      setDespesas(despesasMapeadas);
      
      // Calcular resumo financeiro
      const resumo = CalculosFinanceiros.calcularResumoFinanceiro(despesasMapeadas);
      setResumoFinanceiro(resumo);
      
    } catch (error) {
      console.error("Erro ao buscar despesas:", error);
      setErro(error instanceof Error ? error.message : "Erro desconhecido");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    buscarDespesas();
  }, []);

  const toggleDespesa = (despesaId: number) => {
    const novasExpandidas = new Set(despesasExpandidas);
    if (novasExpandidas.has(despesaId)) {
      novasExpandidas.delete(despesaId);
    } else {
      novasExpandidas.add(despesaId);
    }
    setDespesasExpandidas(novasExpandidas);
  };

  const formatarValor = (valor: number) => {
    return CalculosFinanceiros.formatarValor(valor);
  };

  const formatarData = (data: string) => {
    if (data.includes("/")) {
      return data;
    }
    return new Date(data).toLocaleDateString("pt-BR");
  };

  const getStatusColor = (status: string) => {
    switch (status.toLowerCase()) {
      case "pago": return "#28a745";
      case "pendente": return "#ffc107";
      case "vencido": return "#dc3545";
      default: return "#6c757d";
    }
  };

  if (loading) {
    return (
      <>
        <Navbar />
        <div className="dashboard-container">
          <div className="loading-container">
            <Loader2 className="loading-icon" size={32} />
            <span>Carregando despesas...</span>
          </div>
        </div>
      </>
    );
  }

  if (erro) {
    return (
      <>
        <Navbar />
        <div className="dashboard-container">
          <div className="erro-container">
            <AlertCircle className="erro-icon" size={24} />
            <span>Erro ao carregar despesas: {erro}</span>
            <button className="btn-tentar-novamente" onClick={buscarDespesas}>
              Tentar Novamente
            </button>
          </div>
        </div>
      </>
    );
  }

  if (!resumoFinanceiro) return null;

  return (
    <>
      <Navbar />
      <div className="dashboard-container">
        <div className="dashboard-header">
          <h1 className="titulo-dashboard">
            <CircleGauge className="titulo-icon" size={28} strokeWidth={2.25} />
            Dashboard Financeiro
          </h1>

          {/* Cards de Resumo Principal */}
          <div className="dashboard-resumo">
            <div className="resumo-card">
              <Wallet className="resumo-icon" size={20} />
              <div>
                <span className="resumo-label">Total de Despesas</span>
                <span className="resumo-valor">{formatarValor(resumoFinanceiro.totalDespesas)}</span>
              </div>
            </div>

            <div className="resumo-card resumo-success">
              <CheckCircle className="resumo-icon" size={20} />
              <div>
                <span className="resumo-label">Valor Pago</span>
                <span className="resumo-valor">{formatarValor(resumoFinanceiro.totalPagas)}</span>
                <span className="resumo-extra">{CalculosFinanceiros.formatarPercentual(resumoFinanceiro.percentualPago)}</span>
              </div>
            </div>

            <div className="resumo-card resumo-warning">
              <XCircle className="resumo-icon" size={20} />
              <div>
                <span className="resumo-label">Valor em Aberto</span>
                <span className="resumo-valor">{formatarValor(resumoFinanceiro.totalAberto)}</span>
                <span className="resumo-extra">{resumoFinanceiro.parcelasPendentes + resumoFinanceiro.parcelasVencidas} parcelas</span>
              </div>
            </div>

            <div className="resumo-card resumo-info">
              <Calendar className="resumo-icon" size={20} />
              <div>
                <span className="resumo-label">Gasto em {CalculosFinanceiros.obterNomeMes()}</span>
                <span className="resumo-valor">{formatarValor(resumoFinanceiro.gastoMesAtual)}</span>
                <span className="resumo-extra">Mês atual</span>
              </div>
            </div>
          </div>

          {/* Cards de Análise Avançada */}
          <div className="dashboard-analise">
            <div className="analise-card">
              <TrendingUp className="analise-icon" size={24} />
              <div className="analise-content">
                <h3>Projeção Anual</h3>
                <span className="analise-valor">{formatarValor(resumoFinanceiro.projecaoAnual)}</span>
                <span className="analise-descricao">Baseado no padrão atual</span>
              </div>
            </div>

            <div className="analise-card">
              <BarChart3 className="analise-icon" size={24} />
              <div className="analise-content">
                <h3>Média Mensal</h3>
                <span className="analise-valor">{formatarValor(resumoFinanceiro.mediaMensal)}</span>
                <span className="analise-descricao">Histórico de gastos</span>
              </div>
            </div>

            <div className="analise-card">
              <Target className="analise-icon" size={24} />
              <div className="analise-content">
                <h3>Status das Parcelas</h3>
                <div className="parcelas-status">
                  <div className="status-item">
                    <CheckCircle size={16} color="#28a745" />
                    <span>{resumoFinanceiro.parcelasPagas} Pagas</span>
                  </div>
                  <div className="status-item">
                    <Clock size={16} color="#ffc107" />
                    <span>{resumoFinanceiro.parcelasPendentes} Pendentes</span>
                  </div>
                  <div className="status-item">
                    <XCircle size={16} color="#dc3545" />
                    <span>{resumoFinanceiro.parcelasVencidas} Vencidas</span>
                  </div>
                </div>
              </div>
            </div>

            <div className="analise-card">
              <Package className="analise-icon" size={24} />
              <div className="analise-content">
                <h3>Resumo Geral</h3>
                <div className="resumo-geral">
                  <div className="resumo-item">
                    <span className="resumo-numero">{despesas.length}</span>
                    <span className="resumo-texto">Despesas</span>
                  </div>
                  <div className="resumo-item">
                    <span className="resumo-numero">{resumoFinanceiro.parcelasPagas + resumoFinanceiro.parcelasPendentes + resumoFinanceiro.parcelasVencidas}</span>
                    <span className="resumo-texto">Parcelas</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

        {/* Tabela de Despesas (mantém o código existente) */}
        <div className="despesas-container">
          {despesas.length === 0 ? (
            <div className="vazio-container">
              <CircleGauge className="vazio-icon" size={48} />
              <h3>Nenhuma despesa encontrada</h3>
              <p>Comece adicionando suas primeiras despesas.</p>
            </div>
          ) : (
            <>
              <div className="table-header">
                <div className="header-cell">ID</div>
                <div className="header-cell">Título</div>
                <div className="header-cell">Descrição</div>
                <div className="header-cell">Categoria</div>
                <div className="header-cell">Tipo</div>
                <div className="header-cell">Forma Pagamento</div>
                <div className="header-cell">Parcelas</div>
                <div className="header-cell">Valor Total</div>
                <div className="header-cell">Data Criação</div>
                <div className="header-cell"></div>
              </div>

              {despesas.map((despesa) => (
                <div key={despesa.id} className="despesa-item">
                  <div className="despesa-row">
                    <div className="despesa-cell">{despesa.id}</div>
                    <div className="despesa-cell despesa-titulo">{despesa.titulo}</div>
                    <div className="despesa-cell">{despesa.descricao}</div>
                    <div className="despesa-cell">
                      <span className="categoria-tag">{despesa.categoria}</span>
                    </div>
                    <div className="despesa-cell">{despesa.tipo}</div>
                    <div className="despesa-cell">
                      <span className="forma-pagamento-tag">{despesa.formaPagamento}</span>
                    </div>
                    <div className="despesa-cell">
                      <span className="parcelas-badge">{despesa.quantidadeParcelas}x</span>
                    </div>
                    <div className="despesa-cell despesa-valor">{formatarValor(despesa.valorTotal)}</div>
                    <div className="despesa-cell">{formatarData(despesa.dataCriacao)}</div>
                    <div
                      className="despesa-cell despesa-toggle"
                      onClick={() => toggleDespesa(despesa.id)}
                    >
                      {despesasExpandidas.has(despesa.id) ? (
                        <ChevronUp size={20} />
                      ) : (
                        <ChevronDown size={20} />
                      )}
                    </div>
                  </div>

                  {despesasExpandidas.has(despesa.id) && (
                    <div className="parcelas-dropdown">
                      <div className="parcelas-header">
                        <h4>Parcelas ({despesa.parcelas.length})</h4>
                      </div>

                      <div className="parcelas-table">
                        <div className="parcelas-table-header">
                          <div>Nº Parcela</div>
                          <div>Valor</div>
                          <div>Vencimento</div>
                          <div>Pagamento</div>
                          <div>Status</div>
                          <div>Forma de Pagamento</div>
                        </div>

                        {despesa.parcelas.map((parcela) => (
                          <div key={parcela.id} className="parcela-row">
                            <div className="parcela-cell">
                              {parcela.nrParcela}/{despesa.quantidadeParcelas}
                            </div>
                            <div className="parcela-cell parcela-valor">
                              {formatarValor(parcela.valor)}
                            </div>
                            <div className="parcela-cell" title={parcela.dataVencimento}>
                              {formatarData(parcela.dataVencimento)}
                            </div>
                            <div className="parcela-cell">
                              {parcela.dataPagamento ? formatarData(parcela.dataPagamento) : "-"}
                            </div>
                            <div
                              className="parcela-cell parcela-status"
                              style={{ color: getStatusColor(parcela.status) }}
                            >
                              {parcela.status.toUpperCase()}
                            </div>
                            <div className="parcela-cell">
                              <span className="forma-pagamento-parcela">
                                {parcela.formaPagamento || despesa.formaPagamento}
                              </span>
                            </div>
                          </div>
                        ))}
                      </div>
                    </div>
                  )}
                </div>
              ))}
            </>
          )}
        </div>
      </div>
    </>
  );
}

export default Dashboard;