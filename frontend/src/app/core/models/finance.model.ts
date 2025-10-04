export interface Finance {
  id: number;
  catId?: number;
  catNome?: string;
  descricao: string;
  categoria: string;
  valor: number;
  dataGasto: Date;
  formaPagamento?: string;
  recorrente: boolean;
  frequenciaRecorrencia?: string;
  observacoes?: string;
}

export interface CreateFinanceRequest {
  catId?: number;
  descricao: string;
  categoria: string;
  valor: number;
  dataGasto: Date;
  formaPagamento?: string;
  recorrente: boolean;
  frequenciaRecorrencia?: string;
  observacoes?: string;
}

export interface FinanceSummary {
  totalGasto: number;
  gastoMensal: number;
  gastoPorCategoria: { [key: string]: number };
  ultimosGastos: Finance[];
}
