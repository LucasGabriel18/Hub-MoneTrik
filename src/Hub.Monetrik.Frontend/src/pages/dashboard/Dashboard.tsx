import { useEffect, useState } from "react";
import {
  ChevronDown,
  ChevronUp,
  CircleGauge,
  DollarSign,
  Calendar,
  Package,
  AlertCircle,
  Loader2,
} from "lucide-react";
import Navbar from "../../components/Navbar";
import "./dash.css";
import { despesasService } from "../../services/DespesasService";
import { DespesasMapper, type Despesa } from "../../utils/DespesasUtils";

function Dashboard() {
  const [despesas, setDespesas] = useState<Despesa[]>([]);
  const [loading, setLoading] = useState(true);
  const [erro, setErro] = useState<string | null>(null);
  const [despesasExpandidas, setDespesasExpandidas] = useState<Set<number>>(
    new Set()
  );

  const buscarDespesas = async () => {
    try {
      setLoading(true);
      setErro(null);

      const response = await despesasService.buscarDespesas();
      const despesasMapeadas = DespesasMapper.mapDespesasFromApi(
        response.despesas
      );

      setDespesas(despesasMapeadas);
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
    return new Intl.NumberFormat("pt-BR", {
      style: "currency",
      currency: "BRL",
    }).format(valor);
  };

  const formatarData = (data: string) => {
    if (data.includes("/")) {
      return data;
    }
    return new Date(data).toLocaleDateString("pt-BR");
  };

  const getStatusColor = (status: string) => {
    switch (status.toLowerCase()) {
      case "pago":
        return "#28a745";
      case "pendente":
        return "#ffc107";
      case "vencido":
        return "#dc3545";
      default:
        return "#6c757d";
    }
  };

  const calcularTotalDespesas = () => {
    return despesas.reduce((total, despesa) => total + despesa.valorTotal, 0);
  };

  const calcularParcelasPagas = () => {
    return despesas.reduce((total, despesa) => {
      return total + despesa.parcelas.filter((p) => p.status === "pago").length;
    }, 0);
  };

  const calcularParcelasPendentes = () => {
    return despesas.reduce((total, despesa) => {
      return (
        total + despesa.parcelas.filter((p) => p.status === "pendente").length
      );
    }, 0);
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

  return (
    <>
      <Navbar />
      <div className="dashboard-container">
        <div className="dashboard-header">
          <h1 className="titulo-dashboard">
            <CircleGauge className="titulo-icon" size={28} strokeWidth={2.25} />
            Dashboard de Despesas
          </h1>

          <div className="dashboard-resumo">
            <div className="resumo-card">
              <DollarSign className="resumo-icon" size={20} />
              <div>
                <span className="resumo-label">Total de Despesas</span>
                <span className="resumo-valor">
                  {formatarValor(calcularTotalDespesas())}
                </span>
              </div>
            </div>

            <div className="resumo-card">
              <Package className="resumo-icon" size={20} />
              <div>
                <span className="resumo-label">Quantidade de Despesas</span>
                <span className="resumo-valor">{despesas.length}</span>
              </div>
            </div>

            <div className="resumo-card">
              <Calendar className="resumo-icon" size={20} />
              <div>
                <span className="resumo-label">Parcelas Pagas</span>
                <span className="resumo-valor">{calcularParcelasPagas()}</span>
              </div>
            </div>

            <div className="resumo-card">
              <Calendar className="resumo-icon" size={20} />
              <div>
                <span className="resumo-label">Parcelas Pendentes</span>
                <span className="resumo-valor">
                  {calcularParcelasPendentes()}
                </span>
              </div>
            </div>
          </div>
        </div>

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
                <div className="header-cell">Forma de Pagamento</div>
                <div className="header-cell">Parcelas</div>
                <div className="header-cell">Valor Total</div>
                <div className="header-cell">Data Criação</div>
                <div className="header-cell"></div>
              </div>

              {despesas.map((despesa) => (
                <div key={despesa.id} className="despesa-item">
                  <div className="despesa-row">
                    <div className="despesa-cell">{despesa.id}</div>
                    <div className="despesa-cell despesa-titulo">
                      {despesa.titulo}
                    </div>
                    <div className="despesa-cell">{despesa.descricao}</div>
                    <div className="despesa-cell">
                      <span className="categoria-tag">{despesa.categoria}</span>
                    </div>
                    <div className="despesa-cell">{despesa.tipo}</div>
                    <div className="despesa-cell">
                      <span className="forma-pagamento-tag">
                        {despesa.formaPagamento}
                      </span>
                    </div>
                    <div className="despesa-cell">
                      <span className="parcelas-badge">
                        {despesa.quantidadeParcelas}x
                      </span>
                    </div>
                    <div className="despesa-cell despesa-valor">
                      {formatarValor(despesa.valorTotal)}
                    </div>
                    <div className="despesa-cell">
                      {formatarData(despesa.dataCriacao)}
                    </div>
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
                            <div className="parcela-cell">
                              {formatarData(parcela.dataVencimento)}
                            </div>
                            <div className="parcela-cell">
                              {parcela.dataPagamento
                                ? formatarData(parcela.dataPagamento)
                                : "-"}
                            </div>
                            <div
                              className="parcela-cell parcela-status"
                              style={{ color: getStatusColor(parcela.status) }}
                            >
                              {parcela.status.toUpperCase()}
                            </div>
                            <div className="parcela-cell">
                              <span className="forma-pagamento-parcela">
                                {parcela.formaPagamento}
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