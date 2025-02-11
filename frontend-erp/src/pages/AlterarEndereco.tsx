import { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";
import { api } from "../lib/axios";
import { EnderecoProps } from "../model/UsuarioDto";


export function AlterarEndereco() {
    const location = useLocation();
    const cpf = location.state?.cpf;

    const [enderecos, setEnderecos] = useState<EnderecoProps[]>([]);
    const [novoEndereco, setNovoEndereco] = useState<EnderecoProps>({
        cep: "",
        rua: "",
        numero: "",
        bairro: "",
        cidade: "",
        estado: "",
    });

    useEffect(() => {
        async function buscarDados() {
            try {
                const response = await api.get(`/api/Usuario/${cpf}`);
                console.log(response.data.enderecos)
                setEnderecos(response.data.enderecos);
            } catch (error) {
                console.error("Erro ao buscar dados: ", error);
            }
        }
        buscarDados();
        console.log(enderecos)
    }, [cpf]);

    async function handleSalvarEndereco() {
        try {
            const response = await api.post(`/api/Usuario/adicionar-endereco/${cpf}`, {
                cep: novoEndereco.cep,
                rua: novoEndereco.rua,
                numero: novoEndereco.numero,
                bairro: novoEndereco.bairro,
                cidade: novoEndereco.cidade,
                estado: novoEndereco.estado,
                usuarioCPF: cpf 
            });

            setEnderecos([...enderecos, response.data]);

            setNovoEndereco({ cep: "", rua: "", numero: "", bairro: "", cidade: "", estado: "" });

            alert("Endereço salvo com sucesso!");
        } catch (error) {
            console.error("Erro ao salvar endereço: ", error);
        }
    }



    async function handleDeletarEndereco(cep: string) {
        try {
            await api.delete(`/api/Usuario/deletar-endereco/${cpf}/${cep}`);
            setEnderecos(enderecos.filter(endereco => endereco.cep !== cep));
            alert("Endereço deletado com sucesso!");
        } catch (error) {
            console.error("Erro ao deletar endereço: ", error);
        }
    }

    return (
        <div className="container">
            <h2>Gerenciar Endereços</h2>
            {enderecos.length > 0 ? (
                enderecos.map((endereco) => (
                    <div key={endereco.cep} className="alert alert-secondary d-flex justify-content-between align-items-center">
                        <p>{endereco.rua}, {endereco.numero} - {endereco.bairro}, {endereco.cidade} - {endereco.estado} ({endereco.cep})</p>
                        <button className="btn btn-danger" onClick={() => handleDeletarEndereco(endereco.cep)}>
                            Deletar
                        </button>
                    </div>
                ))
            ) : (
                <p>Nenhum endereço cadastrado.</p>
            )}

            <h3 className="mt-4">Adicionar Novo Endereço</h3>
            <div className="row">
                {Object.keys(novoEndereco).map((campo) => (
                    <div className="col-md-4" key={campo}>
                        <input
                            type="text"
                            className="form-control mb-2"
                            placeholder={campo.toUpperCase()}
                            value={(novoEndereco as any)[campo]}
                            onChange={(e) => setNovoEndereco({ ...novoEndereco, [campo]: e.target.value })}
                        />
                    </div>
                ))}
            </div>
            <button className="btn btn-success mt-3" onClick={handleSalvarEndereco}>
                Salvar Endereço
            </button>
        </div>
    );
}
