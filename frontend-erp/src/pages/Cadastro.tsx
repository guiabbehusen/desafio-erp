import React, { useState } from "react";
import { api } from "../lib/axios";
import { useNavigate } from "react-router-dom";

export const Cadastro: React.FC = () => {
    const [nome, setNome] = useState<string>('');
    const [cpf, setCPF] = useState<string>('');
    const [email, setEmail] = useState<string>('');
    const [telefone, setTelefone] = useState<string>('');
    const [cep, setCEP] = useState<string>('');
    const [rua, setRua] = useState<string>('');
    const [numero, setNumero] = useState<string>('');
    const [bairro, setBairro] = useState<string>('');
    const [cidade, setCidade] = useState<string>('');
    const [estado, setEstado] = useState<string>('');
    const [usuario, setUsuario] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [backendError, setBackendError] = useState<string>('');
    const navigate = useNavigate();

    async function handleCadastro(event: React.FormEvent) {
        event.preventDefault();

        try {
            const resposta = await api.post('/api/Usuario', {
                "Nome": nome,
                "CPF": cpf,
                "Email": email,
                "Telefone": telefone,
                "Login": usuario,
                "Senha": password,
                "Enderecos": [{
                    "CEP": cep,
                    "Rua": rua,
                    "Numero": numero,
                    "Bairro": bairro,
                    "Cidade": cidade,
                    "Estado": estado,
                    "UsuarioCPF": cpf
                }]
            });

            if (resposta.status === 200) {
                navigate('/');
            } else {
                setBackendError("Erro ao cadastrar usuário.");
            }
        } catch (error) {
            console.error("Error: ", error);
            setBackendError("Erro ao processar cadastro.");
        }
    }

    return (
        <div className="container mt-5">
            <h1 className="text-center">Cadastro</h1>
            <p style={{ color: "red" }}>{backendError}</p>

            <form className="mt-4" onSubmit={handleCadastro}>
                <div className="mb-3">
                    <label className="form-label">Nome completo</label>
                    <input type="text" className="form-control" required value={nome} onChange={(e) => setNome(e.target.value)} placeholder="Ex: João Silva" />
                </div>
                <div className="mb-3">
                    <label className="form-label">CPF</label>
                    <input type="text" className="form-control" required value={cpf} onChange={(e) => setCPF(e.target.value)} placeholder="Ex: 12345678900" />
                </div>
                <div className="mb-3">
                    <label className="form-label">E-mail</label>
                    <input type="email" className="form-control" required value={email} onChange={(e) => setEmail(e.target.value)} placeholder="Ex: joao@gmail.com" />
                </div>
                <div className="mb-3">
                    <label className="form-label">Telefone</label>
                    <input type="text" className="form-control" required value={telefone} onChange={(e) => setTelefone(e.target.value)} placeholder="Ex: (61) 99999-9999" />
                </div>
                <div className="mb-3">
                    <label className="form-label">Usuário</label>
                    <input type="text" className="form-control" required value={usuario} onChange={(e) => setUsuario(e.target.value)} placeholder="Ex: joao.silva" />
                </div>
                <div className="mb-3">
                    <label className="form-label">Senha segura</label>
                    <input type="password" className="form-control" required value={password} onChange={(e) => setPassword(e.target.value)} placeholder="Ex: SenhaSegura123@" />
                </div>
                <h4>Endereço</h4>
                <div className="mb-3">
                    <label className="form-label">CEP</label>
                    <input type="text" className="form-control" required value={cep} onChange={(e) => setCEP(e.target.value)} placeholder="Ex: 70000-000" />
                </div>
                <div className="mb-3">
                    <label className="form-label">Rua</label>
                    <input type="text" className="form-control" required value={rua} onChange={(e) => setRua(e.target.value)} placeholder="Ex: Rua das Flores" />
                </div>
                <div className="mb-3">
                    <label className="form-label">Número</label>
                    <input type="text" className="form-control" required value={numero} onChange={(e) => setNumero(e.target.value)} placeholder="Ex: 123" />
                </div>
                <div className="mb-3">
                    <label className="form-label">Bairro</label>
                    <input type="text" className="form-control" required value={bairro} onChange={(e) => setBairro(e.target.value)} placeholder="Ex: Centro" />
                </div>
                <div className="mb-3">
                    <label className="form-label">Cidade</label>
                    <input type="text" className="form-control" required value={cidade} onChange={(e) => setCidade(e.target.value)} placeholder="Ex: Brasília" />
                </div>
                <div className="mb-3">
                    <label className="form-label">Estado</label>
                    <input type="text" className="form-control" required value={estado} onChange={(e) => setEstado(e.target.value)} placeholder="Ex: DF" />
                </div>
                <button type="submit" className="btn btn-primary w-100">Cadastrar</button>
            </form>
            <p className="mt-3 text-center">
                Já tem uma conta? <a href="/">Fazer login</a>
            </p>
        </div>
    );
};
