import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { api } from "../lib/axios";
import { UsuarioProps } from "../model/UsuarioDto";

export function Home() {
    const location = useLocation();
    const navigate = useNavigate();

    if (!location.state?.dados) {
        setTimeout(() => {
            navigate('/login');
        }, 3000);
        return <div>Erro ao carregar os dados, você será redirecionado de volta à tela de login.</div>;
    }

    const [usuario, setUsuario] = useState<UsuarioProps | null>(null);
    const cpf = location.state.dados.cpf;

    async function buscarDadosUsuario() {
        const response = await api.get(`/api/Usuario/${cpf}`);
        setUsuario(response.data);
    }

    useEffect(() => {
        buscarDadosUsuario();
    }, []);

    return (
        <div className="container mt-5">
            <div className="text-center">
                <h1 className="display-4 font-weight-bold">Bem-vindo à Cad+</h1>
                <p className="lead text-muted">Olá, {usuario?.nome}! O que deseja fazer hoje?</p>
            </div>

            <div className="d-flex justify-content-center gap-4 mt-5">
                <button
                    onClick={() => navigate('/alterar_dados', { state: { cpf } })}
                    className="btn btn-success btn-lg rounded-pill px-4 py-2 shadow"
                >
                    Alterar dados cadastrais
                </button>
                <button
                    onClick={() => navigate('/alterar_endereco', { state: { cpf } })}
                    className="btn btn-info btn-lg rounded-pill px-4 py-2 shadow"
                >
                    Alterar Endereço
                </button>
            </div>
            <div className="mt-5 text-center">
                <p className="text-muted">
                    Precisa de ajuda?
                    <a href="/contato" className="font-weight-bold text-info ms-2" style={{ cursor: 'pointer' }}>
                        Entre em contato
                    </a>
                </p>
            </div>


            <div className="text-center mt-5">
                <footer className="text-muted">
                    <p>Cad+ - ERP para Hospitais - Transformando o gerenciamento hospitalar com soluções inovadoras e personalizadas.</p>
                    <p>&copy; 2025 Cad+. Todos os direitos reservados.</p>
                </footer>
            </div>
        </div>
    );
}
