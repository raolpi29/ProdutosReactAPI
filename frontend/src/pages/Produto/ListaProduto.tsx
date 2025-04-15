import React, { useState, useEffect, useCallback } from 'react';
import axios from 'axios';
import saveAs from 'file-saver';
import { getProductApiUrl } from '../../services/apiService';
import { exportProductsToExcel } from '../../services/produtoService';
import DetalheProduto from './DetalheProduto';
import ConfirmationModal from '../../components/ConfirmationModal';

interface Produto {
  idProduto: number;
  nome: string;
  valor: number;
}

const ListaProduto: React.FC = () => {
  const [products, setProducts] = useState([] as Produto[]);
  const [currentPage, setCurrentPage] = useState(1);
  const [sortOrder, setSortOrder] = useState('asc');
  const [showModal, setShowModal] = useState(false);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [productToDelete, setProductToDelete] = useState<number | null>(null);
  const [showEditModal, setShowEditModal] = useState(false);
  const [productToEdit, setProductToEdit] = useState<Produto | null>(null);
  const [totalPages, setTotalPages] = useState(1);
  const itemsPerPage: number = 10;

  const fetchProdutos = useCallback(async (): Promise<void> => {
    try {
      const response = await axios.get(`${getProductApiUrl()}`, {
        params: {
          page: currentPage,
          sort: sortOrder,
        },
      });
      const { dados } = response.data;
      setProducts(dados.itens);
      setTotalPages(dados.totalPaginas);
    } catch (error) {
      console.error('Falha ao buscar produtos:', error);
    }
  }, [currentPage, sortOrder]);

  useEffect(() => {
    fetchProdutos();
  }, [fetchProdutos]);

  const handleDelete = async (): Promise<void> => {
    if (productToDelete !== null) {
      try {
        await axios.delete(`${getProductApiUrl()}/${productToDelete}`);
        setProductToDelete(null);
        setShowDeleteModal(false);
        fetchProdutos();
      } catch (error) {
        console.error('Falha ao excluir produto:', error);
      }
    }
  };

  const openDeleteModal = (id: number): void => {
    setProductToDelete(id);
    setShowDeleteModal(true);
  };

  const closeDeleteModal = (): void => {
    setProductToDelete(null);
    setShowDeleteModal(false);
  };

  const openEditModal = (produto: Produto): void => {
    setProductToEdit(produto);
    setShowEditModal(true);
  };

  const closeEditModal = (): void => {
    setProductToEdit(null);
    setShowEditModal(false);
  };

  const downloadExcel = async (): Promise<void> => {
    try {
      const blob = await exportProductsToExcel();
      saveAs(blob, `produtos_${new Date().toISOString().replace(/[-:.]/g, '').slice(0, 15)}.xlsx`);
    } catch (error) {
      console.error('Falha ao baixar o arquivo Excel:', error);
    }
  };

  const handleOpenModal = () => {
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
  };

  const handleSaveProduto = () => {
    setShowEditModal(false);
    setShowModal(false);
    fetchProdutos();
  };

  return (
    <div className="p-4">
      <h1 className="text-2xl font-bold mb-4">Lista de Produtos</h1>
      <div className="mb-4 flex space-x-2">
        <button
          onClick={downloadExcel}
          className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600"
        >
          Baixar Excel
        </button>
        <button
          onClick={handleOpenModal}
          className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
        >
          Incluir Produto
        </button>
      </div>
      {showModal && (
        <DetalheProduto onClose={handleCloseModal} onSave={handleSaveProduto} />
      )}
      {showEditModal && productToEdit && (
        <DetalheProduto
          produto={productToEdit}
          onClose={closeEditModal}
          onSave={handleSaveProduto}
        />
      )}
      {showDeleteModal && (
        <ConfirmationModal
          title="Confirmar Exclusão"
          message="Realmente deseja excluir este produto?"
          onConfirm={handleDelete}
          onCancel={closeDeleteModal}
        />
      )}
      <table className="w-full border-collapse border border-gray-300">
        <thead>
          <tr>
            <th className="border border-gray-300 px-4 py-2">Nome do Produto</th>
            <th className="border border-gray-300 px-4 py-2">Valor (R$)</th>
            <th className="border border-gray-300 px-4 py-2">Ações</th>
          </tr>
        </thead>
        <tbody>
          {products.map((produto) => (
            <tr key={produto.idProduto}>
              <td className="border border-gray-300 px-4 py-2">{produto.nome}</td>
              <td className="border border-gray-300 px-4 py-2">
                {new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(produto.valor)}
              </td>
              <td className="border border-gray-300 px-4 py-2">
                <button
                  onClick={() => openEditModal(produto)}
                  className="bg-blue-500 text-white px-2 py-1 rounded hover:bg-blue-600 mr-2"
                >
                  Editar
                </button>
                <button
                  onClick={() => openDeleteModal(produto.idProduto)}
                  className="bg-red-500 text-white px-2 py-1 rounded hover:bg-red-600"
                >
                  Excluir
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <div className="mt-4 flex justify-between">
        <button
          onClick={() => setCurrentPage((prev) => Math.max(prev - 1, 1))}
          className="bg-gray-300 px-4 py-2 rounded hover:bg-gray-400"
        >
          Anterior
        </button>
        <button
          onClick={() => setCurrentPage((prev) => (prev < totalPages ? prev + 1 : prev))}
          className="bg-gray-300 px-4 py-2 rounded hover:bg-gray-400"
          disabled={currentPage >= totalPages}
        >
          Próximo
        </button>
      </div>
    </div>
  );
};

export default ListaProduto;