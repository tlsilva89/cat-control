export interface Cat {
  id: number;
  nome: string;
  dataNascimento?: Date;
  raca?: string;
  cor?: string;
  sexo?: string;
  castrado: boolean;
  peso?: number;
  numeroMicrochip?: string;
  fotoUrl?: string;
  observacoes?: string;
  idadeAnos?: number;
  idadeMeses?: number;
}

export interface CreateCatRequest {
  nome: string;
  dataNascimento?: Date;
  raca?: string;
  cor?: string;
  sexo?: string;
  castrado: boolean;
  peso?: number;
  numeroMicrochip?: string;
  fotoUrl?: string;
  observacoes?: string;
}

export interface UpdateCatRequest {
  nome?: string;
  dataNascimento?: Date;
  raca?: string;
  cor?: string;
  sexo?: string;
  castrado?: boolean;
  peso?: number;
  numeroMicrochip?: string;
  fotoUrl?: string;
  observacoes?: string;
}
