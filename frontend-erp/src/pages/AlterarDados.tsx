import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { api } from "../lib/axios";
import { EnderecoProps, UsuarioProps } from "../model/UsuarioDto";

export function AlterarDados() {
    const location = useLocation();

    const cpf = location.state.cpf;

    const navigate = useNavigate();

    const [usuario, setUsuario] = useState<UsuarioProps | null>(null)
    
    if (!cpf) {
        return <div>Erro ao carregar os dados, você será redirecionado de volta à tela de login.</div>;
    }

    const [novoNome, setNovoNome] = useState<string>("");
    const [novoEmail, setNovoEmail] = useState<string>("");
    const [novoTelefone, setNovoTelefone] = useState<string>("");
    const [novosEnderecos, setNovosEnderecos] = useState<EnderecoProps[]>([]);
    const [novoEndereco, setNovoEndereco] = useState<string>("");

    async function buscarDadosUsuario() {
        const response = await api.get<UsuarioProps>(`/api/Usuario/${cpf}`)
        const usuario = response.data
        setNovoNome(usuario.nome)
        setNovoEmail(usuario.email)
        setNovoTelefone(usuario.telefone)
        setNovosEnderecos(usuario.enderecos)
    }

    useEffect(() => {
        buscarDadosUsuario();
    }, []);

    async function handleAlteracao(event: React.FormEvent) {
        event.preventDefault();

        try {
            const resposta = await api.put(`/api/Usuario/${usuario?.cpf}`, {
                Nome: novoNome,
                Email: novoEmail,
                Telefone: novoTelefone,
                //Enderecos: novosEnderecos
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

    async function handleAdicionarEndereco(event: React.FormEvent) {
        event.preventDefault();

        try {
            const resposta = await api.put(`/api/Usuario/adicionar-endereco/${usuario?.cpf}`, {
                Rua: novoEndereco,
                UsuarioCPF: usuario?.cpf
            });

            if (resposta.status === 200) {
                alert("Endereço adicionado com sucesso!");
                setNovosEnderecos([...novosEnderecos, resposta.data]);
                setNovoEndereco("");
            }
        } catch (error) {
            alert("Erro ao adicionar endereço.");
            console.error("Erro ao adicionar endereço: ", error);
        }
    }

    async function handleDeletarEndereco(cep: string) {
        try {
            const resposta = await api.delete(`/api/Usuario/deletar-endereco/${usuario?.cpf}/${cep}`);

            if (resposta.status === 200) {
                alert("Endereço deletado com sucesso!");
                //setNovosEnderecos(novosEnderecos.filter(endereco => endereco.CEP !== cep));
            }
        } catch (error) {
            alert("Erro ao deletar endereço.");
            console.error("Erro ao deletar endereço: ", error);
        }
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

                {/* <h3 className="mt-4">Endereços</h3>
                {novosEnderecos.length > 0 ? (
                    novosEnderecos.map((endereco, index) => (
                        <div key={index} className="alert alert-secondary d-flex justify-content-between align-items-center">
                            <p>{endereco.rua}, {endereco.cep}</p>
                            <button
                                type="button"
                                className="btn btn-danger"
                                onClick={() => handleDeletarEndereco(endereco.cep)}
                            >
                                Remover
                            </button>
                        </div>
                    ))
                ) : (
                    <p className="text-muted">Não há endereços cadastrados.</p>
                )} */}

                {/* <div className="mb-3">
                    <label htmlFor="novoEndereco" className="form-label">Novo Endereço</label>
                    <input
                        id="novoEndereco"
                        type="text"
                        className="form-control"
                        value={novoEndereco}
                        onChange={(e) => setNovoEndereco(e.target.value)}
                    />
                    <button
                        type="button"
                        className="btn btn-primary mt-2"
                        onClick={handleAdicionarEndereco}
                    >
                        Adicionar Endereço
                    </button>
                </div> */}

                <button type="submit" className="btn btn-success w-100 mt-4">Salvar alterações</button>
            </form>
        </div>
    );
}
