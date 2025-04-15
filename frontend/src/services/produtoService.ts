import api, { getProductApiUrl } from './apiService';

export const fetchProducts = async (page: number, sort: string) => {
  const response = await api.get(`${getProductApiUrl()}`, {
    params: { page, sort },
  });
  return response.data;
};

export const deleteProduct = async (id: number) => {
  await api.delete(`${getProductApiUrl()}/${id}`);
};

export interface Produto {
  idProduto?: number;
  nome: string;
  valor: number;
}

export const createProduct = async (dto: Produto) => {
  await api.post(`${getProductApiUrl()}`, dto);
};

export const updateProduct = async (idProduto: number, dto: Produto) => {
  await api.put(`${getProductApiUrl()}/${idProduto}`, dto);
};

export const exportProductsToExcel = async (): Promise<Blob> => {
  const response = await api.get(`${getProductApiUrl()}/exportar`, {
    responseType: 'blob',
  });
  return response.data;
};