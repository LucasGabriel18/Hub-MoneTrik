import { useState } from "react";
import {
  ChevronDown,
  ChevronUp,
  CircleGauge,
  DollarSign,
  Calendar,
  Package,
} from "lucide-react";
import Navbar from "../../components/Navbar";
import "./dash.css";

interface Parcela {
  id: number;
  nrParcela: number;
  valor: number;
  dataVencimento: string;
  dataPagamento?: string;
  status: "pago" | "pendente" | "vencido";
  despesaId: number;
}

interface Despesa {
  id: number;
  titulo: string;
  descricao: string;
  categoria: string;
  tipo: string;
  formaPagamento: string;
  quantidadeParcelas: number;
  valorTotal: number;
  dataCriacao: string;
  parcelas: Parcela[];
}

// Dados mockados para estruturar o front
const despesasMock: Despesa[] = [
  {
    id: 1,
    titulo: "Aluguel",
    descricao: "Pagamento mensal do aluguel",
    categoria: "Moradia",
    tipo: "Despesa",
    formaPagamento: "Cartão de Crédito",
    quantidadeParcelas: 12,
    valorTotal: 1200.0,
    dataCriacao: "2024-01-01",
    parcelas: [
      {
        id: 1,
        nrParcela: 1,
        valor: 100.0,
        dataVencimento: "2024-01-05",
        dataPagamento: "2024-01-05",
        status: "pago",
        despesaId: 1,
      },
      {
        id: 2,
        nrParcela: 2,
        valor: 100.0,
        dataVencimento: "2024-02-05",
        dataPagamento: "2024-02-05",
        status: "pago",
        despesaId: 1,
      },
      {
        id: 3,
        nrParcela: 3,
        valor: 100.0,
        dataVencimento: "2024-03-05",
        status: "pendente",
        despesaId: 1,
      },
      {
        id: 4,
        nrParcela: 4,
        valor: 100.0,
        dataVencimento: "2024-04-05",
        status: "pendente",
        despesaId: 1,
      },
    ],
  },
  {
    id: 2,
    titulo: "Compras Supermercado",
    descricao: "Compras mensais de alimentação",
    categoria: "Alimentação",
    tipo: "Despesa",
    formaPagamento: "Débito",
    quantidadeParcelas: 1,
    valorTotal: 350.0,
    dataCriacao: "2024-07-15",
    parcelas: [
      {
        id: 5,
        nrParcela: 1,
        valor: 350.0,
        dataVencimento: "2024-07-15",
        dataPagamento: "2024-07-15",
        status: "pago",
        despesaId: 2,
      },
    ],
  },
  {
    id: 3,
    titulo: "Financiamento Carro",
    descricao: "Parcelas do financiamento do veículo",
    categoria: "Transporte",
    tipo: "Despesa",
    formaPagamento: "Débito Automático",
    quantidadeParcelas: 60,
    valorTotal: 30000.0,
    dataCriacao: "2023-01-01",
    parcelas: [
      {
        id: 6,
        nrParcela: 18,
        valor: 500.0,
        dataVencimento: "2024-07-10",
        dataPagamento: "2024-07-10",
        status: "pago",
        despesaId: 3,
      },
      {
        id: 7,
        nrParcela: 19,
        valor: 500.0,
        dataVencimento: "2024-08-10",
        status: "pendente",
        despesaId: 3,
      },
      {
        id: 8,
        nrParcela: 20,
        valor: 500.0,
        dataVencimento: "2024-09-10",
        status: "pendente",
        despesaId: 3,
      },
    ],
  },
];

function Dashboard() {
  const [despesas] = useState<Despesa[]>(despesasMock);
  const [despesasExpandidas, setDespesasExpandidas] = useState<Set<number>>(
    new Set()
  );

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
                <span className="resumo-label">Total de Despesas</span>
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
          <div className="table-header">
            <div className="header-cell">ID</div>
            <div className="header-cell">Título</div>
            <div className="header-cell">Descrição</div>
            <div className="header-cell">Categoria</div>
            <div className="header-cell">Forma Pagamento</div>
            <div className="header-cell">Parcelas</div>
            <div className="header-cell">Valor Total</div>
            <div className="header-cell">Data Criação</div>
            <div className="header-cell"></div>
          </div>

          {despesas.map((despesa) => (
            <div key={despesa.id} className="despesa-item">
              <div
                className="despesa-row"
                onClick={() => toggleDespesa(despesa.id)}
              >
                <div className="despesa-cell">{despesa.id}</div>
                <div className="despesa-cell despesa-titulo">
                  {despesa.titulo}
                </div>
                <div className="despesa-cell">{despesa.descricao}</div>
                <div className="despesa-cell">
                  <span className="categoria-tag">{despesa.categoria}</span>
                </div>
                <div className="despesa-cell">{despesa.formaPagamento}</div>
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
                <div className="despesa-cell despesa-toggle">
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
                      <div>Ações</div>
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
                      </div>
                    ))}
                  </div>
                </div>
              )}
            </div>
          ))}
        </div>
      </div>
    </>
  );
}

export default Dashboard;
