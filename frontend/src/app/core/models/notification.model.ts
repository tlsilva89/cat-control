export interface Notification {
  id: number;
  catId?: number;
  catNome?: string;
  tipo: string;
  titulo: string;
  mensagem: string;
  dataNotificacao: Date;
  lida: boolean;
  prioridade: string;
  referenciaId?: number;
  createdAt: Date;
}

export interface CreateNotificationRequest {
  catId?: number;
  tipo: string;
  titulo: string;
  mensagem: string;
  dataNotificacao: Date;
  prioridade: string;
  referenciaId?: number;
}
