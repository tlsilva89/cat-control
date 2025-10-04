export interface Vaccine {
  id: number;
  catId: number;
  catNome: string;
  tipoVacina: string;
  dataAplicacao: Date;
  proximaAplicacao?: Date;
  localAplicacao?: string;
  veterinario?: string;
  valor?: number;
  observacoes?: string;
  diasParaProxima?: number;
}

export interface CreateVaccineRequest {
  catId: number;
  tipoVacina: string;
  dataAplicacao: Date;
  proximaAplicacao?: Date;
  localAplicacao?: string;
  veterinario?: string;
  valor?: number;
  observacoes?: string;
}

export interface UpdateVaccineRequest {
  tipoVacina?: string;
  dataAplicacao?: Date;
  proximaAplicacao?: Date;
  localAplicacao?: string;
  veterinario?: string;
  valor?: number;
  observacoes?: string;
}
