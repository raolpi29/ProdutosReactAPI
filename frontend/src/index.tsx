import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import { AlertProvider } from './hooks/AlertProvider';
import AxiosInterceptorProvider from './interceptors/AxiosInterceptorProvider';
import './index.css';

const root = ReactDOM.createRoot(document.getElementById('root')!);
root.render(
  <React.StrictMode>
    <AlertProvider>
      <AxiosInterceptorProvider>
        <App />
      </AxiosInterceptorProvider>
    </AlertProvider>
  </React.StrictMode>
);