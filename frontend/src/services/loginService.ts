import api, { getLoginApiUrl } from './apiService';

// Define an interface for login credentials
export interface LoginCredentials {
  login: string;
  senha: string;
}

// Update the login function to use the interface
export const login = async ({ login, senha }: LoginCredentials) => {
  var url = getLoginApiUrl() + '/login';
  try {
    const response = await api.post(url, { login, senha });
    return response.data;
  } catch (error) {
    throw error;
  }
};