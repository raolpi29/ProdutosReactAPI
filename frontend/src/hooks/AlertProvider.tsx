import React, { createContext, useContext, useState, useCallback, useEffect } from 'react';
import Alert from '../components/Alert';

type AlertType = 'success' | 'error' | 'info';

interface AlertContextType {
  showAlert: (message: string, type?: AlertType, duration?: number) => void;
}

const AlertContext = createContext<AlertContextType | undefined>(undefined);

export const AlertProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [message, setMessage] = useState<string | null>(null);
  const [type, setType] = useState<AlertType>('info');

  const showAlert = useCallback((msg: string, t: AlertType = 'info', duration = 10000) => {
    setMessage(msg);
    setType(t);

    setTimeout(() => {
      setMessage(null);
    }, duration);
  }, []);

  const closeAlert = useCallback(() => {
    setMessage(null);
  }, []);

  return (
    <AlertContext.Provider value={{ showAlert }}>
      {children}
      {message && <Alert message={message} type={type} onClose={closeAlert} />}
    </AlertContext.Provider>
  );
};

export const useAlert = (): AlertContextType => {
  const context = useContext(AlertContext);
  if (!context) {
    throw new Error('useAlert');
  }
  return context;
};