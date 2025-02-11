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

    const [usuario, setUsuario] = useState<UsuarioProps | null>(null)

    const cpf = location.state.dados.cpf;

    async function buscarDadosUsuario() {
        const response = await api.get(`/api/Usuario/${cpf}`)
        setUsuario(response.data)
    }

    useEffect(() => {
        buscarDadosUsuario();
    }, []);

    return (
        <div>
            <h1>Bem-vindo, {usuario?.nome}</h1>
            <p>O que deseja fazer hoje?</p>

            <div>
                <button onClick={() => navigate('/alterar_dados', { state: { cpf } })}>Alterar dados cadastrais</button>
            </div>
        </div>
    );
}
