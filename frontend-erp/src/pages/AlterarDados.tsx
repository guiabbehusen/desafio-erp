import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { api } from "../lib/axios";
import { UsuarioProps } from "../model/UsuarioDto";

export function AlterarDados() {
    const location = useLocation();
    const navigate = useNavigate();

    const cpf = location.state?.cpf; 


    const [usuario, setUsuario] = useState<UsuarioProps | null>(null);
    const [novoNome, setNovoNome] = useState<string>("");
    const [novoEmail, setNovoEmail] = useState<string>("");
    const [novoTelefone, setNovoTelefone] = useState<string>("");

    if (!cpf) {
        return <div>Erro ao carregar os dados, você será redirecionado de volta à tela de login.</div>;
    }

    async function buscarDadosUsuario() {
        try {
            const response = await api.get<UsuarioProps>(`/api/Usuario/${cpf}`);
            const usuarioData = response.data;

            setUsuario(usuarioData); 
            setNovoNome(usuarioData.nome);
            setNovoEmail(usuarioData.email);
            setNovoTelefone(usuarioData.telefone);
        } catch (error) {
            console.error("Erro ao buscar dados do usuário:", error);
            alert("Erro ao carregar os dados do usuário.");
            navigate("/login"); 
        }
    }

    useEffect(() => {
        buscarDadosUsuario();
    }, [cpf]);

    async function handleAlteracao(event: React.FormEvent) {
        event.preventDefault();

        try {
            const resposta = await api.put(`/api/Usuario/${cpf}`, {
                Nome: novoNome,
                Email: novoEmail,
                Telefone: novoTelefone,
            });

            if (resposta.status === 200) {
                alert("Dados alterados com sucesso!");
                navigate("/home", { state: { usuario: resposta.data } });
            }
        } catch (error) {
            alert("Erro ao alterar dados.");
            console.error("Erro ao editar dados: ", error);
        }
    }

    if (!usuario) {
        return <div>Carregando dados do usuário...</div>;
    }

    return (
        <div className="container mt-4">
            <h2 className="text-center mb-4">Alterar dados cadastrais</h2>

            <form onSubmit={handleAlteracao}>
                <div className="mb-3">
                    <label htmlFor="nome" className="form-label">Nome</label>
                    <input
                        id="nome"
                        type="text"
                        className="form-control"
                        value={novoNome}
                        onChange={(e) => setNovoNome(e.target.value)}
                    />
                </div>

                <div className="mb-3">
                    <label htmlFor="email" className="form-label">Email</label>
                    <input
                        id="email"
                        type="email"
                        className="form-control"
                        value={novoEmail}
                        onChange={(e) => setNovoEmail(e.target.value)}
                    />
                </div>

                <div className="mb-3">
                    <label htmlFor="telefone" className="form-label">Telefone</label>
                    <input
                        id="telefone"
                        type="text"
                        className="form-control"
                        value={novoTelefone}
                        onChange={(e) => setNovoTelefone(e.target.value)}
                    />
                </div>

                <button type="submit" className="btn btn-success w-100 mt-4">Salvar alterações</button>
            </form>
        </div>
    );
}
