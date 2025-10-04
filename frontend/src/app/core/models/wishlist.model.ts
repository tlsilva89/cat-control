export interface Wishlist {
  id: number;
  catId?: number;
  catNome?: string;
  nomeProduto: string;
  categoria?: string;
  precoEstimado?: number;
  prioridade: string;
  linkProduto?: string;
  loja?: string;
  comprado: boolean;
  dataCompra?: Date;
  observacoes?: string;
}

export interface CreateWishlistRequest {
  catId?: number;
  nomeProduto: string;
  categoria?: string;
  precoEstimado?: number;
  prioridade: string;
  linkProduto?: string;
  loja?: string;
  observacoes?: string;
}
