export interface Credentials {
  email: string;
  password: string;
}

export interface AuthenticatedUser {
  id: string;
  email: string;
}

export interface AuthResponse {
  accessToken: string;
  expiresAt: string;
  user: AuthenticatedUser;
}
