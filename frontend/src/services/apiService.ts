import axios from 'axios';

export const getApiUrl = (): string => {
  return process.env.REACT_APP_API_URL || 'http://localhost:5000';
};

export const getLoginApiUrl = (): string => {
  return `${getApiUrl()}/v1/api/Usuario`;
};

export const getProductApiUrl = (): string => {
  return `${getApiUrl()}/v1/api/Produto`;
};

const api = axios.create({
  baseURL: process.env.REACT_APP_API_BASE_URL || 'http://localhost:3000',
});

export default api;
