import React, { useEffect } from 'react';
import api from '../services/apiService';
import { useAlert } from '../hooks/AlertProvider';

const AxiosInterceptorProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const { showAlert } = useAlert();

  useEffect(() => {
    const interceptor = api.interceptors.response.use(
      (response) => response,
      (error) => {
        let message = 'Erro inesperado';
      
        const data = error?.response?.data;
      
        console.log(data)
        if (Array.isArray(data)) {
          message = data.map((e: any) => e.mensagem).join(' | ');
        } else if (typeof data === 'object' && data?.mensagem) {
          message = data.mensagem;
        } else if (error?.message) {
          message = error.message;
        }
      
        showAlert(message, 'error');
        return Promise.reject(error);
      }
    );

    return () => {
      api.interceptors.response.eject(interceptor);
    };
  }, [showAlert]);

  return <>{children}</>;
};

export default AxiosInterceptorProvider;