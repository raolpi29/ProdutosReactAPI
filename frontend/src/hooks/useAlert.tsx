import React, { useState, useCallback } from 'react';
import Alert from '../components/Alert'; // Adjust the path as needed

const useAlert = () => {
  const [message, setMessage] = useState<string | null>(null);

  const showAlert = useCallback((msg: string) => {
    setMessage(msg);
  }, []);

  const closeAlert = useCallback(() => {
    setMessage(null);
  }, []);

  const AlertComponent = () => {
    return message ? (
      <Alert message={message} onClose={closeAlert} />
    ) : null;
  };

  return {
    showAlert,
    AlertComponent,
  };
};

export default useAlert;
