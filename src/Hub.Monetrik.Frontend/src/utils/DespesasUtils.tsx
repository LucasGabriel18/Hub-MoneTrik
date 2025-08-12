import type { DespesaApi, ParcelaApi } from "../interfaces/IHubMonetrikApi";

export interface Parcela {
  id: number;
  nrParcela: number;
  valor: number;
  dataVencimento: string;
  dataPagamento?: string;
  status: "pago" | "pendente" | "vencido";
  formaPagamento?: string;
  despesaId: number;
}

export interface Despesa {
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

export const mapSituacaoToStatus = (
  situacao: string
): "pago" | "pendente" | "vencido" => {
  switch (situacao.toLowerCase()) {
    case "pago":
      return "pago";
    case "pendente":
      return "pendente";
    case "vencido":
      return "vencido";
    default:
      return "pendente";
  }
};

export const mapParcelaFromApi = (parcela: ParcelaApi): Parcela => {
  return {
    id: parcela.id,
    nrParcela: parcela.numeroParcela,
    valor: parcela.valorParcela,
    dataVencimento: parcela.dataVencimento,
    dataPagamento: parcela.situacao === "Pago" ? parcela.dataVencimento : undefined,
    status: mapSituacaoToStatus(parcela.situacao),
    despesaId: parcela.despesaId,
  };
};

export const mapDespesaFromApi = (despesa: DespesaApi): Despesa => {
  // Inferir forma de pagamento das parcelas se não vier na despesa
  const formasPagamento = despesa.parcelas
    .map(p => p.formaPagamento)
    .filter(Boolean);
  
  const formaPagamentoPrincipal = formasPagamento.length > 0 
    ? formasPagamento[0] 
    : "Não informado";

  return {
    id: despesa.id,
    titulo: despesa.titulo,
    descricao: despesa.descricao,
    categoria: despesa.categoria,
    tipo: despesa.tipo,
    formaPagamento: formaPagamentoPrincipal || "Não informado",
    quantidadeParcelas: despesa.totalParcelas,
    valorTotal: despesa.valorTotal,
    dataCriacao: despesa.dataRegistro,
    parcelas: despesa.parcelas.map(mapParcelaFromApi),
  };
};

export const mapDespesasFromApi = (despesas: DespesaApi[]): Despesa[] => {
  return despesas.map(mapDespesaFromApi);
};

// Para compatibilidade, mantenha a classe também
export class DespesasMapper {
  static mapSituacaoToStatus = mapSituacaoToStatus;
  static mapParcelaFromApi = mapParcelaFromApi;
  static mapDespesaFromApi = mapDespesaFromApi;
  static mapDespesasFromApi = mapDespesasFromApi;
}
