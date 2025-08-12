import { type Despesa } from './DespesasUtils';

export interface ResumoFinanceiro {
  totalDespesas: number;
  totalPagas: number;
  totalAberto: number;
  gastoMesAtual: number;
  projecaoAnual: number;
  parcelasPagas: number;
  parcelasPendentes: number;
  parcelasVencidas: number;
  percentualPago: number;
  mediaMensal: number;
}

export class CalculosFinanceiros {
  static calcularResumoFinanceiro(despesas: Despesa[]): ResumoFinanceiro {
    const agora = new Date();
    const mesAtual = agora.getMonth();
    const anoAtual = agora.getFullYear();

    let totalDespesas = 0;
    let totalPagas = 0;
    let totalAberto = 0;
    let gastoMesAtual = 0;
    let parcelasPagas = 0;
    let parcelasPendentes = 0;
    let parcelasVencidas = 0;
    
    // Map para armazenar gastos por mês para calcular projeção
    const gastosPorMes = new Map<string, number>();

    despesas.forEach(despesa => {
      totalDespesas += despesa.valorTotal;

      despesa.parcelas.forEach(parcela => {
        const valorParcela = parcela.valor;
        const dataVencimento = this.parseData(parcela.dataVencimento);
        const mesVencimento = dataVencimento.getMonth();
        const anoVencimento = dataVencimento.getFullYear();
        
        // Chave para agrupar por mês/ano
        const chaveData = `${anoVencimento}-${mesVencimento}`;

        switch (parcela.status.toLowerCase()) {
          case 'pago':
            totalPagas += valorParcela;
            parcelasPagas++;
            
            // Se foi pago no mês atual
            if (mesVencimento === mesAtual && anoVencimento === anoAtual) {
              gastoMesAtual += valorParcela;
            }
            
            // Adicionar ao histórico de gastos mensais
            gastosPorMes.set(chaveData, (gastosPorMes.get(chaveData) || 0) + valorParcela);
            break;
            
          case 'pendente':
            totalAberto += valorParcela;
            parcelasPendentes++;
            
            // Se vence no mês atual, conta como gasto do mês
            if (mesVencimento === mesAtual && anoVencimento === anoAtual) {
              gastoMesAtual += valorParcela;
            }
            break;
            
          case 'vencido':
            totalAberto += valorParcela;
            parcelasVencidas++;
            break;
        }
      });
    });

    // Calcular média mensal baseada no histórico
    const valoresGastos = Array.from(gastosPorMes.values());
    const mediaMensal = valoresGastos.length > 0 
      ? valoresGastos.reduce((sum, valor) => sum + valor, 0) / valoresGastos.length 
      : gastoMesAtual;

    // Projeção anual: (média mensal * 12) ou baseada nos próximos meses
    const mesesRestantes = 12 - mesAtual;
    const projecaoAnual = (gastoMesAtual * 12) + (mediaMensal * mesesRestantes);

    const percentualPago = totalDespesas > 0 ? (totalPagas / totalDespesas) * 100 : 0;

    return {
      totalDespesas,
      totalPagas,
      totalAberto,
      gastoMesAtual,
      projecaoAnual,
      parcelasPagas,
      parcelasPendentes,
      parcelasVencidas,
      percentualPago: Math.round(percentualPago * 100) / 100,
      mediaMensal
    };
  }

  private static parseData(dataStr: string): Date {
    // Se já é uma data em formato brasileiro dd/mm/yyyy
    if (dataStr.includes('/')) {
      const [dia, mes, ano] = dataStr.split('/');
      return new Date(parseInt(ano), parseInt(mes) - 1, parseInt(dia));
    }
    
    // Se é uma string ISO
    return new Date(dataStr);
  }

  static formatarValor(valor: number): string {
    return new Intl.NumberFormat('pt-BR', {
      style: 'currency',
      currency: 'BRL'
    }).format(valor);
  }

  static formatarPercentual(percentual: number): string {
    return `${percentual.toFixed(1)}%`;
  }

  static obterNomeMes(numeroMes?: number): string {
    const meses = [
      'Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho',
      'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'
    ];
    
    const mes = numeroMes ?? new Date().getMonth();
    return meses[mes];
  }
}