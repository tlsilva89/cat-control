export interface Stock {
  id: number;
  nomeProduto: string;
  categoria: string;
  quantidadeAtual: number;
  quantidadeMinima: number;
  unidade?: string;
  dataValidade?: Date;
  precoUnitario?: number;
  consumoMedioDiario?: number;
  marca?: string;
  observacoes?: string;
  alertaReposicao: boolean;
  diasParaVencer?: number;
  diasEstimadosDuracao?: number;
}

export interface CreateStockRequest {
  nomeProduto: string;
  categoria: string;
  quantidadeAtual: number;
  quantidadeMinima: number;
  unidade?: string;
  dataValidade?: Date;
  precoUnitario?: number;
  consumoMedioDiario?: number;
  marca?: string;
  observacoes?: string;
}

export interface UpdateStockRequest {
  nomeProduto?: string;
  categoria?: string;
  quantidadeAtual?: number;
  quantidadeMinima?: number;
  unidade?: string;
  dataValidade?: Date;
  precoUnitario?: number;
  consumoMedioDiario?: number;
  marca?: string;
  observacoes?: string;
}
