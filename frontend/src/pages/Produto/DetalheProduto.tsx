import React, { useState } from 'react';
import { createProduct, updateProduct, Produto } from '../../services/produtoService';

interface DetalheProdutoProps {
  produto?: Produto;
  onClose: () => void;
  onSave: () => void;
}

const DetalheProduto: React.FC<DetalheProdutoProps> = ({ produto, onClose, onSave }) => {
  const [name, setName] = useState<string>(produto?.nome || '');
  const [value, setValue] = useState<string>(
    produto?.valor ? new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(produto.valor) : ''
  );

  const handleValueChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const rawValue = e.target.value.replace(/\D/g, '');
    const formattedValue = new Intl.NumberFormat('pt-BR', {
      style: 'currency',
      currency: 'BRL',
    }).format(Number(rawValue) / 100);
    setValue(formattedValue);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const numericValue = Number(value.replace(/[^0-9,-]+/g, '').replace(',', '.'));
      const dto = { nome: name, valor: numericValue };
      if (produto) {
        if (produto.idProduto !== undefined) {
          await updateProduct(produto.idProduto, dto);
        } else {
          throw new Error('Id de produto inválido');
        }
      } else {
        await createProduct(dto);
      }
      onSave();
    } catch (error) {
      console.error('Falha ao salvar o produto', error);
    }
  };

  return (
    <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
      <div className="bg-white p-6 rounded shadow-md w-96">
        <h2 className="text-2xl font-bold mb-4">{produto ? 'Editar Produto' : 'Cadastrar Produto'}</h2>
        <form onSubmit={handleSubmit}>
          <div className="mb-4">
            <label className="block text-sm font-medium mb-1">Nome</label>
            <input
              type="text"
              value={name}
              onChange={(e) => setName(e.target.value)}
              className="w-full px-3 py-2 border rounded"
              required
            />
          </div>
          <div className="mb-4">
            <label className="block text-sm font-medium mb-1">Valor</label>
            <input
              type="text"
              value={value}
              onChange={handleValueChange}
              className="w-full px-3 py-2 border rounded"
              required
            />
          </div>
          <div className="flex justify-end space-x-2">
            <button
              type="button"
              onClick={onClose}
              className="bg-gray-300 px-4 py-2 rounded hover:bg-gray-400"
            >
              Fechar
            </button>
            <button
              type="submit"
              className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
            >
              Salvar
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default DetalheProduto;