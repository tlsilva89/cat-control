export interface User {
  id: number;
  nome: string;
  email: string;
  createdAt: Date;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  nome: string;
  email: string;
  password: string;
}

export interface AuthResponse {
  token: string;
  userId: number;
  nome: string;
  email: string;
}
