import React, { useState } from "react";
import { api } from "../lib/axios";
import { useNavigate } from "react-router-dom";

export const Login: React.FC = () => {
    const [usuario, setUsuario] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [backendError, setBackendError] = useState<string>('');
    const navigate = useNavigate();

    async function doLoginevent(event: React.FormEvent) {
        event.preventDefault();

        try {
            const resposta = await api.post('/api/Login', {
                "Login": usuario,
                "Senha": password
            }, {
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if (resposta.status === 200 && resposta.data) {
                console.log("Resposta da API:", resposta.data); 
                navigate('/home', { state: { dados: resposta.data } });
            } else {
                setBackendError("Usuário ou senha incorretos.");
            }

        } catch (error) {
            console.log("error: ", error);
            setBackendError("Usuário ou senha incorretos.");
        }
    }

    return (
        <div className="container mt-5">
            <h1 className="text-center">Bem-vindo</h1>
            <p style={{ color: "red" }}>{backendError}</p>

            <form className="mt-4" onSubmit={doLoginevent}>
                <div className="mb-3">
                    <label htmlFor="usuario" className="form-label">Usuário</label>
                    <input type="text" className="form-control" id="usuario" placeholder="Digite seu nome de usuário" required value={usuario} onChange={(event) => setUsuario(event.target.value)} />
                </div>
                <div className="mb-3">
                    <label htmlFor="password" className="form-label">Senha</label>
                    <input type="password" className="form-control" id="password" placeholder="Digite sua senha" required value={password} onChange={(event) => setPassword(event.target.value)} />
                </div>
                <button type="submit" className="btn btn-primary w-100">Entrar</button>
            </form>
            <p className="mt-3 text-center">
                Não possui uma conta? <a href="/cadastro">Cadastrar</a>
            </p>
            <p className="text-center">
                <a href="/contato">Entrar em contato</a>
            </p>
        </div>
    );
};
