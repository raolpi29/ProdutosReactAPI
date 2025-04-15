import React from 'react';

interface AlertProps {
  message: string;
  type?: 'success' | 'error' | 'info';
  onClose: () => void;
}

const colorMap = {
  success: 'bg-green-500',
  error: 'bg-red-500',
  info: 'bg-blue-500',
};

const Alert: React.FC<AlertProps> = ({ message, type = 'info', onClose }) => {
  return (
    <div className={`fixed bottom-4 right-4 z-50 text-white px-6 py-4 rounded shadow-lg ${colorMap[type]}`}>
      <div className="flex justify-between items-center gap-4">
        <span>{message}</span>
        <button onClick={onClose} className="text-sm underline hover:text-gray-200">
          Fechar
        </button>
      </div>
    </div>
  );
};

export default Alert;