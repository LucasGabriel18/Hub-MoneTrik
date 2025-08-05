export interface ApiResponse<T> {
  success: boolean;
  data: T;
  errors?: Array<{
    message: string;
    type: string;
  }>;
}

export interface ParcelaApi {
  id: number;
  despesaId: number;
  numeroParcela: number;
  valorParcela: number;
  dataVencimento: string;
  formaPagamento?: string;
  situacao: string;
}

export interface DespesaApi {
  id: number;
  titulo: string;
  descricao: string;
  categoria: string;
  tipo: string;
  totalParcelas: number;
  valorTotal: number;
  dataRegistro: string;
  parcelas: ParcelaApi[];
}

export interface BuscarDespesasResponse {
  despesas: DespesaApi[];
}
